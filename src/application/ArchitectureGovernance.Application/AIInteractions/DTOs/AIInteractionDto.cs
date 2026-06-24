namespace ArchitectureGovernance.Application.AIInteractions.DTOs;

public record AIInteractionDto(
    Guid Id,
    Guid ProjectId,
    Guid RequirementSubmissionId,
    string ProviderName,
    string ModelName,
    string PromptTemplateVersion,
    DateTimeOffset RequestTimestamp,
    DateTimeOffset? ResponseTimestamp,
    string Status,
    int? TokenCountEstimate,
    string? ErrorSummary,
    string CorrelationId
);
