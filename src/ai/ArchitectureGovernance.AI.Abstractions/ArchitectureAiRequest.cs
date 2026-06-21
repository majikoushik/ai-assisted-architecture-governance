namespace ArchitectureGovernance.AI.Abstractions;

public sealed record ArchitectureAiRequest(
    Guid ProjectId,
    Guid RequirementId,
    string ArtifactType,
    string RequirementTitle,
    string RequirementText,
    string BusinessDomain,
    string DomainContext,
    string PromptTemplateName,
    string PromptTemplateVersion,
    string PromptTemplateContent,
    string CorrelationId);
