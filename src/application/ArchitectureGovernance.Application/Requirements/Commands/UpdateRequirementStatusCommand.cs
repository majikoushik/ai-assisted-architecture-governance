using ArchitectureGovernance.Application.Requirements.DTOs;
using ArchitectureGovernance.Application.Requirements.Mappers;
using ArchitectureGovernance.Application.Common.Interfaces;
using ArchitectureGovernance.Domain.Requirements;
using FluentValidation;
using MediatR;

namespace ArchitectureGovernance.Application.Requirements.Commands;

public record UpdateRequirementStatusCommand(Guid Id, string Status) : IRequest<RequirementDto?>;

public class UpdateRequirementStatusCommandValidator : AbstractValidator<UpdateRequirementStatusCommand>
{
    public UpdateRequirementStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Status).Must(BeAValidStatus).WithMessage("Invalid requirement status.");
    }

    private bool BeAValidStatus(string status)
    {
        return Enum.TryParse<RequirementStatus>(status, true, out _);
    }
}

public class UpdateRequirementStatusCommandHandler : IRequestHandler<UpdateRequirementStatusCommand, RequirementDto?>
{
    private readonly IAppDbContext _context;

    public UpdateRequirementStatusCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<RequirementDto?> Handle(UpdateRequirementStatusCommand request, CancellationToken cancellationToken)
    {
        var requirement = await _context.Requirements.FindAsync(new object[] { request.Id }, cancellationToken);
        if (requirement == null) return null;

        requirement.UpdateStatus(Enum.Parse<RequirementStatus>(request.Status, true));
        await _context.SaveChangesAsync(cancellationToken);

        return requirement.ToDto();
    }
}
