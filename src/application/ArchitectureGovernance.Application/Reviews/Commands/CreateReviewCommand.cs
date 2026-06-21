using System.ComponentModel.DataAnnotations;
using ArchitectureGovernance.Domain.Artifacts;

namespace ArchitectureGovernance.Application.Reviews.Commands;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid ArtifactId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

public class CreateReviewCommand : MediatR.IRequest<ReviewDto>
{
    public Guid ArtifactId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string ReviewStatus { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
}
