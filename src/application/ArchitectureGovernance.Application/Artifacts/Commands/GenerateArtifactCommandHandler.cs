using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Application.Prompts.Services;
using ArchitectureGovernance.Domain;
using ArchitectureGovernance.Domain.AIInteractions;
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
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public GenerateArtifactCommandHandler(
        IAppDbContext context,
        IArchitectureAiProvider aiProvider,
        IPromptRepository promptRepository,
        ILogger<GenerateArtifactCommandHandler> logger,
        ICorrelationIdProvider correlationIdProvider)
    {
        _context = context;
        _aiProvider = aiProvider;
        _promptRepository = promptRepository;
        _logger = logger;
        _correlationIdProvider = correlationIdProvider;
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

        // Retrieve the appropriate prompt template based on ArtifactType
        var domainArtifactType = request.ArtifactType;
        var promptTemplateId = GetPromptTemplateIdForType(domainArtifactType);

        var promptTemplate = await _promptRepository.GetByIdAsync(promptTemplateId, cancellationToken);
        if (promptTemplate == null)
            throw new KeyNotFoundException($"PromptTemplate with id {promptTemplateId} not found.");

        var correlationId = _correlationIdProvider.CorrelationId;
        
        var aiInteractionLog = new AIInteractionLog(
            request.ProjectId,
            request.RequirementSubmissionId,
            "Provider", 
            "Model",
            promptTemplate.Version,
            DateTimeOffset.UtcNow,
            correlationId
        );
        _context.AIInteractionLogs.Add(aiInteractionLog);

        ArchitectureAiResponse generatedResponse;
        try
        {
            generatedResponse = await _aiProvider.GenerateArtifactDraftAsync(
                new ArchitectureAiRequest(
                    request.ProjectId,
                    request.RequirementSubmissionId,
                    domainArtifactType.ToString(),
                    promptTemplate.Name,
                    promptTemplate.Version,
                    promptTemplate.Content,
                    requirement.RequirementText,
                    requirement.Title,
                    project.BusinessDomain,
                    requirement.DomainContext,
                    correlationId),
                cancellationToken);

            aiInteractionLog.Complete(DateTimeOffset.UtcNow, "Success");
        }
        catch (Exception ex)
        {
            aiInteractionLog.Fail(DateTimeOffset.UtcNow, ex.Message);
            await _context.SaveChangesAsync(cancellationToken);
            throw; 
        }

        var existingArtifactsCount = await _context.Artifacts
            .CountAsync(a => a.RequirementSubmissionId == requirement.Id && a.ArtifactType == domainArtifactType, cancellationToken);
        
        var newVersion = $"v1.0.{existingArtifactsCount + 1}";

        var artifact = new GeneratedArtifact(
            request.ProjectId,
            request.RequirementSubmissionId,
            domainArtifactType,
            $"{project.Name} - {promptTemplate.Name}",
            generatedResponse.Markdown,
            newVersion,
            generatedResponse.ProviderName,
            promptTemplate.Name,
            promptTemplate.Version,
            correlationId);

        _context.Artifacts.Add(artifact);
        await _context.SaveChangesAsync(cancellationToken);

        return ArtifactDto.FromEntity(artifact);
    }

    private static string GetPromptTemplateIdForType(ArtifactType type)
    {
        return type switch
        {
            ArtifactType.RequirementAnalysis => "requirement-analysis",
            ArtifactType.HighLevelDesign => "hld-generation",
            ArtifactType.LowLevelDesign => "lld-generation",
            ArtifactType.ArchitectureDecisionRecord => "adr-generation",
            ArtifactType.NonFunctionalRequirementReview => "nfr-review",
            ArtifactType.ApiContractReview => "api-contract-review",
            ArtifactType.SecurityReview => "security-review",
            ArtifactType.RiskAndAssumptionReview => "risk-assumption-review",
            _ => throw new InvalidOperationException($"No prompt template configured for {type}")
        };
    }
}
