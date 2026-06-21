using FluentValidation;

namespace ArchitectureGovernance.Application.Projects.Commands;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(200).WithMessage("Project name must not exceed 200 characters.");

        RuleFor(v => v.BusinessDomain)
            .NotEmpty().WithMessage("Business domain is required.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(v => v.Owner)
            .NotEmpty().WithMessage("Owner is required.");
    }
}
