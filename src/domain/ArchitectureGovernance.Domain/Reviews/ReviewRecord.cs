using ArchitectureGovernance.Domain.Artifacts;
using SharedKernel;

namespace ArchitectureGovernance.Domain.Reviews;

public class ReviewRecord : Entity
{
    public Guid ArtifactId { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid RequirementSubmissionId { get; private set; }
    public string ReviewerName { get; private set; } = string.Empty;
    public ReviewStatus Status { get; private set; }
    public string Comments { get; private set; } = string.Empty;
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
        ArtifactId = artifactId;
        ProjectId = projectId;
        RequirementSubmissionId = requirementSubmissionId;
        ReviewerName = reviewerName;
        Status = status;
        Comments = comments;
        CorrelationId = correlationId;
    }
}
