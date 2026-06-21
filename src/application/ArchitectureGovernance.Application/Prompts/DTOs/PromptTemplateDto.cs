namespace ArchitectureGovernance.Application.Prompts.DTOs;

public record PromptTemplateDto(
    string Id,
    string Name,
    string ArtifactType,
    string Version,
    string Purpose,
    string Content,
    string Status);
