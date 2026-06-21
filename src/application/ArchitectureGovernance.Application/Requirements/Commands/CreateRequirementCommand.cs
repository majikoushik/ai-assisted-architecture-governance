using ArchitectureGovernance.Application.Requirements.DTOs;
using ArchitectureGovernance.Application.Requirements.Mappers;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Domain.Requirements;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchitectureGovernance.Application.Requirements.Commands;

public record CreateRequirementCommand(
    Guid ProjectId,
    string Title,
    string RequirementText,
    string BusinessDomain,
    string? DomainContext,
    string SubmittedBy,
    List<string> ExpectedArtifactTypes
) : IRequest<RequirementDto>;

public class CreateRequirementCommandValidator : AbstractValidator<CreateRequirementCommand>
{
    public CreateRequirementCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.RequirementText).NotEmpty().MinimumLength(10).MaximumLength(10000); // 10k limit for safety
        RuleFor(x => x.BusinessDomain).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SubmittedBy).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ExpectedArtifactTypes).NotEmpty();
        RuleForEach(x => x.ExpectedArtifactTypes).Must(BeAValidArtifactType)
            .WithMessage("One or more expected artifact types are unsupported.");
    }

    private bool BeAValidArtifactType(string type)
    {
        return Enum.TryParse<ArtifactType>(type, true, out _);
    }
}

public class CreateRequirementCommandHandler : IRequestHandler<CreateRequirementCommand, RequirementDto>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateRequirementCommandHandler> _logger;

    public CreateRequirementCommandHandler(IAppDbContext context, ILogger<CreateRequirementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RequirementDto> Handle(CreateRequirementCommand request, CancellationToken cancellationToken)
    {
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == request.ProjectId, cancellationToken);
        if (!projectExists)
        {
            throw new ArgumentException($"Project with ID {request.ProjectId} not found.");
        }

        var artifactTypes = request.ExpectedArtifactTypes
            .Select(t => Enum.Parse<ArtifactType>(t, true))
            .ToList();

        var requirement = new RequirementSubmission(
            request.ProjectId,
            request.Title,
            request.RequirementText,
            request.BusinessDomain,
            request.SubmittedBy,
            artifactTypes,
            request.DomainContext
        );

        _context.Requirements.Add(requirement);
        await _context.SaveChangesAsync(cancellationToken);

        // RESPONSIBLE AI: Do not log the full requirement text.
        _logger.LogInformation("Requirement created with ID {RequirementId} for Project ID {ProjectId}. Title: {Title}. ArtifactTypes: {Types}", 
            requirement.Id, requirement.ProjectId, requirement.Title, string.Join(",", request.ExpectedArtifactTypes));

        return requirement.ToDto();
    }
}
