using ArchitectureGovernance.Domain.Artifacts;

namespace ArchitectureGovernance.Application.Artifacts;

public record ArtifactDto(
    Guid Id,
    Guid ProjectId,
    Guid RequirementSubmissionId,
    string ArtifactType,
    string Title,
    string MarkdownContent,
    string Version,
    string Status,
    string ProviderName,
    string PromptTemplateName,
    string PromptTemplateVersion,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    string CorrelationId
)
{
    public static ArtifactDto FromEntity(GeneratedArtifact artifact) => new(
        artifact.Id,
        artifact.ProjectId,
        artifact.RequirementSubmissionId,
        artifact.ArtifactType.ToString(),
        artifact.Title,
        artifact.MarkdownContent,
        artifact.Version,
        artifact.Status.ToString(),
        artifact.ProviderName,
        artifact.PromptTemplateName,
        artifact.PromptTemplateVersion,
        artifact.CreatedAt,
        artifact.UpdatedAt,
        artifact.CorrelationId
    );
}
