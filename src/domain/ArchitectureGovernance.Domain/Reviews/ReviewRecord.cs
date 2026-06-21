using ArchitectureGovernance.Domain.Artifacts;

namespace ArchitectureGovernance.Domain.Reviews;

public class ReviewRecord
{
    public Guid Id { get; private set; }
    public Guid ArtifactId { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid RequirementSubmissionId { get; private set; }
    public string ReviewerName { get; private set; } = string.Empty;
    public ReviewStatus Status { get; private set; }
    public string Comments { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string CorrelationId { get; private set; } = string.Empty;

    private ReviewRecord() { } // EF Core

    public ReviewRecord(
        Guid artifactId,
        Guid projectId,
        Guid requirementSubmissionId,
        string reviewerName,
        ReviewStatus status,
        string comments,
        string correlationId)
    {
        Id = Guid.NewGuid();
        ArtifactId = artifactId;
        ProjectId = projectId;
        RequirementSubmissionId = requirementSubmissionId;
        ReviewerName = reviewerName;
        Status = status;
        Comments = comments;
        CorrelationId = correlationId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
