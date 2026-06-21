using FluentValidation;

namespace ArchitectureGovernance.Application.Reviews.Commands;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(v => v.ArtifactId)
            .NotEmpty().WithMessage("ArtifactId is required.");

        RuleFor(v => v.ReviewerName)
            .NotEmpty().WithMessage("ReviewerName is required.")
            .MaximumLength(200).WithMessage("ReviewerName must not exceed 200 characters.");

        RuleFor(v => v.ReviewStatus)
            .NotEmpty().WithMessage("ReviewStatus is required.");

        RuleFor(v => v.Comments)
            .MaximumLength(4000).WithMessage("Comments must not exceed 4000 characters.");
    }
}
