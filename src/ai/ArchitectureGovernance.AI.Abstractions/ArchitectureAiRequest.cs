namespace ArchitectureGovernance.AI.Abstractions;

public sealed record ArchitectureAiRequest(
    string RequirementText,
    string ArtifactType,
    string PromptTemplateName,
    string PromptTemplateVersion,
    string CorrelationId);
