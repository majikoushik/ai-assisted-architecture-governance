namespace ArchitectureGovernance.AI.Abstractions;

public sealed record ArchitectureAiResponse(
    string Markdown,
    string ProviderName,
    string ModelName,
    string PromptTemplateVersion);
