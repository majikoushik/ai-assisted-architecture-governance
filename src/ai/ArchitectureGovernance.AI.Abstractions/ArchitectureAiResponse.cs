namespace ArchitectureGovernance.AI.Abstractions;

public sealed record ArchitectureAiResponse(
    string ArtifactType,
    string Markdown,
    string ProviderName,
    string PromptTemplateName,
    string PromptTemplateVersion,
    DateTimeOffset GenerationTimestamp,
    string Status,
    string[] Warnings,
    string HumanReviewNotice);
