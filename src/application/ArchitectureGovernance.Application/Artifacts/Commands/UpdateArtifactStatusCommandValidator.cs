using FluentValidation;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public class UpdateArtifactStatusCommandValidator : AbstractValidator<UpdateArtifactStatusCommand>
{
    public UpdateArtifactStatusCommandValidator()
    {
        RuleFor(v => v.ArtifactId)
            .NotEmpty().WithMessage("ArtifactId is required.");

        RuleFor(v => v.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(v => v.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy is required.")
            .MaximumLength(200).WithMessage("UpdatedBy must not exceed 200 characters.");

        RuleFor(v => v.Reason)
            .MaximumLength(4000).WithMessage("Reason must not exceed 4000 characters.");
    }
}
