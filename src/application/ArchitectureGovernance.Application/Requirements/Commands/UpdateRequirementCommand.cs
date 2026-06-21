using ArchitectureGovernance.Application.Requirements.DTOs;
using ArchitectureGovernance.Application.Requirements.Mappers;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Domain.Requirements;
using FluentValidation;
using MediatR;

namespace ArchitectureGovernance.Application.Requirements.Commands;

public record UpdateRequirementCommand(
    Guid Id,
    string Title,
    string RequirementText,
    string BusinessDomain,
    string? DomainContext,
    List<string> ExpectedArtifactTypes
) : IRequest<RequirementDto?>;

public class UpdateRequirementCommandValidator : AbstractValidator<UpdateRequirementCommand>
{
    public UpdateRequirementCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.RequirementText).NotEmpty().MinimumLength(10).MaximumLength(10000);
        RuleFor(x => x.BusinessDomain).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ExpectedArtifactTypes).NotEmpty();
        RuleForEach(x => x.ExpectedArtifactTypes).Must(BeAValidArtifactType)
            .WithMessage("One or more expected artifact types are unsupported.");
    }

    private bool BeAValidArtifactType(string type)
    {
        return Enum.TryParse<ArtifactType>(type, true, out _);
    }
}

public class UpdateRequirementCommandHandler : IRequestHandler<UpdateRequirementCommand, RequirementDto?>
{
    private readonly IAppDbContext _context;

    public UpdateRequirementCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<RequirementDto?> Handle(UpdateRequirementCommand request, CancellationToken cancellationToken)
    {
        var requirement = await _context.Requirements.FindAsync(new object[] { request.Id }, cancellationToken);
        if (requirement == null) return null;

        var artifactTypes = request.ExpectedArtifactTypes
            .Select(t => Enum.Parse<ArtifactType>(t, true))
            .ToList();

        requirement.UpdateDetails(
            request.Title,
            request.RequirementText,
            request.BusinessDomain,
            request.DomainContext,
            artifactTypes
        );

        await _context.SaveChangesAsync(cancellationToken);
        return requirement.ToDto();
    }
}
