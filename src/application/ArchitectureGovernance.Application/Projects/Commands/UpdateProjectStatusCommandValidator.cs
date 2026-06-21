using FluentValidation;

namespace ArchitectureGovernance.Application.Projects.Commands;

public class UpdateProjectStatusCommandValidator : AbstractValidator<UpdateProjectStatusCommand>
{
    public UpdateProjectStatusCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Project ID is required.");

        RuleFor(v => v.Status)
            .IsInEnum().WithMessage("Status must be a valid project status.");
    }
}
