using FluentValidation;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public class GenerateArtifactCommandValidator : AbstractValidator<GenerateArtifactCommand>
{
    public GenerateArtifactCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.RequirementSubmissionId).NotEmpty();
        RuleFor(v => v.ArtifactType)
            .IsInEnum().WithMessage("Invalid artifact type.");
    }
}
