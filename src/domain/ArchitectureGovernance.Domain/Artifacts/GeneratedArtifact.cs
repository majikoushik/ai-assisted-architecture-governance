using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Requirements;

namespace ArchitectureGovernance.Domain.Artifacts;

public class GeneratedArtifact
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid RequirementSubmissionId { get; private set; }
    public ArtifactType ArtifactType { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string MarkdownContent { get; private set; } = string.Empty;
    public string Version { get; private set; } = "1.0.0";
    public ReviewStatus Status { get; private set; } = ReviewStatus.Draft;
    public string ProviderName { get; private set; } = string.Empty;
    public string PromptTemplateName { get; private set; } = string.Empty;
    public string PromptTemplateVersion { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string CorrelationId { get; private set; } = string.Empty;

    // EF Core constructor
    private GeneratedArtifact() { }

    public GeneratedArtifact(
        Guid projectId,
        Guid requirementSubmissionId,
        ArtifactType artifactType,
        string title,
        string markdownContent,
        string version,
        string providerName,
        string promptTemplateName,
        string promptTemplateVersion,
        string correlationId)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        RequirementSubmissionId = requirementSubmissionId;
        ArtifactType = artifactType;
        Title = title;
        MarkdownContent = markdownContent;
        Version = version;
        ProviderName = providerName;
        PromptTemplateName = promptTemplateName;
        PromptTemplateVersion = promptTemplateVersion;
        CorrelationId = correlationId;
        Status = ReviewStatus.Draft;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateStatus(ReviewStatus newStatus)
    {
        // Valid transitions:
        // Draft -> NeedsReview, Rejected
        // NeedsReview -> Reviewed, Rejected
        // Reviewed -> Approved, Rejected
        // Rejected -> NeedsReview

        bool isValid = Status switch
        {
            ReviewStatus.Draft => newStatus is ReviewStatus.NeedsReview or ReviewStatus.Rejected,
            ReviewStatus.NeedsReview => newStatus is ReviewStatus.Reviewed or ReviewStatus.Rejected,
            ReviewStatus.Reviewed => newStatus is ReviewStatus.Approved or ReviewStatus.Rejected,
            ReviewStatus.Rejected => newStatus is ReviewStatus.NeedsReview,
            ReviewStatus.Approved => false, // No transitions from Approved without a new version
            _ => false
        };

        if (!isValid && Status != newStatus)
        {
            throw new InvalidOperationException($"Cannot transition status from {Status} to {newStatus}");
        }

        Status = newStatus;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateContent(string markdownContent, string version)
    {
        MarkdownContent = markdownContent;
        Version = version;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
