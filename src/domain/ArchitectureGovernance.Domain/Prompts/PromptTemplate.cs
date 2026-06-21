namespace ArchitectureGovernance.Domain.Prompts;

public sealed record PromptTemplate(
    string Id,
    string Name,
    string ArtifactType,
    string Version,
    string Purpose,
    string Content,
    string Status);
