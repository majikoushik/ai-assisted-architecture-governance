using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Domain;
using ArchitectureGovernance.Domain.Artifacts;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public class GenerateArtifactCommandHandler : IRequestHandler<GenerateArtifactCommand, ArtifactDto>
{
    private readonly IAppDbContext _context;
    private readonly IArchitectureAiProvider _aiProvider;
    private readonly IPromptRepository _promptRepository;
    private readonly ILogger<GenerateArtifactCommandHandler> _logger;

    public GenerateArtifactCommandHandler(
        IAppDbContext context,
        IArchitectureAiProvider aiProvider,
        IPromptRepository promptRepository,
        ILogger<GenerateArtifactCommandHandler> logger)
    {
        _context = context;
        _aiProvider = aiProvider;
        _promptRepository = promptRepository;
        _logger = logger;
    }

    public async Task<ArtifactDto> Handle(GenerateArtifactCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);
        
        if (project == null)
            throw new KeyNotFoundException($"Project with id {request.ProjectId} not found.");

        var requirement = await _context.Requirements
            .FirstOrDefaultAsync(r => r.Id == request.RequirementSubmissionId, cancellationToken);

        if (requirement == null)
            throw new KeyNotFoundException($"RequirementSubmission with id {request.RequirementSubmissionId} not found.");

        if (requirement.ProjectId != request.ProjectId)
            throw new ValidationException(new[] { new FluentValidation.Results.ValidationFailure("RequirementSubmissionId", "Requirement does not belong to the specified project.") });

        // Retrieve the appropriate prompt template
        var promptTemplateId = "requirement-analysis";
        var promptTemplate = await _promptRepository.GetByIdAsync(promptTemplateId, cancellationToken);
        if (promptTemplate == null)
            throw new KeyNotFoundException($"PromptTemplate with id {promptTemplateId} not found.");

        var correlationId = Guid.NewGuid().ToString();

        // Ensure sensitive info is not fully logged
        _logger.LogInformation("Generating artifact type {ArtifactType} for Project {ProjectId}, Requirement {RequirementId}. CorrelationId: {CorrelationId}", 
            request.ArtifactType, request.ProjectId, request.RequirementSubmissionId, correlationId);

        var aiRequest = new ArchitectureAiRequest(
            ProjectId: project.Id,
            RequirementId: requirement.Id,
            ArtifactType: ArchitectureGovernance.Domain.ArtifactType.RequirementAnalysis.ToString(),
            RequirementTitle: requirement.Title,
            RequirementText: requirement.RequirementText,
            BusinessDomain: project.BusinessDomain,
            DomainContext: requirement.DomainContext,
            PromptTemplateName: promptTemplate.Name,
            PromptTemplateVersion: promptTemplate.Version,
            CorrelationId: correlationId
        );

        var aiResponse = await _aiProvider.GenerateArtifactDraftAsync(aiRequest, cancellationToken);

        var generatedArtifact = new GeneratedArtifact(
            projectId: project.Id,
            requirementSubmissionId: requirement.Id,
            artifactType: ArchitectureGovernance.Domain.ArtifactType.RequirementAnalysis,
            title: $"{project.Name} - Requirement Analysis",
            markdownContent: aiResponse.Markdown,
            version: "1.0.0",
            providerName: aiResponse.ProviderName,
            promptTemplateName: promptTemplate.Name,
            promptTemplateVersion: promptTemplate.Version,
            correlationId: correlationId
        );

        _context.Artifacts.Add(generatedArtifact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Artifact generated and saved. Id: {ArtifactId}, Provider: {Provider}, CorrelationId: {CorrelationId}", 
            generatedArtifact.Id, aiResponse.ProviderName, correlationId);

        return ArtifactDto.FromEntity(generatedArtifact);
    }
}
