using FluentValidation;

namespace ArchitectureGovernance.Application.Artifacts.Commands;

public class GenerateArtifactCommandValidator : AbstractValidator<GenerateArtifactCommand>
{
    public GenerateArtifactCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.RequirementSubmissionId).NotEmpty();
        RuleFor(x => x.ArtifactType)
            .NotEmpty()
            .Must(type => type == "RequirementAnalysis" || type == "HighLevelDesign")
            .WithMessage("Only 'RequirementAnalysis' and 'HighLevelDesign' are supported.");
    }
}
