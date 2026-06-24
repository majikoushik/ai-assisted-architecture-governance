using SharedKernel;

namespace ArchitectureGovernance.Domain.AIInteractions;

public class AIInteractionLog : Entity
{
    public Guid ProjectId { get; private set; }
    public Guid RequirementSubmissionId { get; private set; }
    public string ProviderName { get; private set; } = string.Empty;
    public string ModelName { get; private set; } = string.Empty;
    public string PromptTemplateVersion { get; private set; } = string.Empty;
    public DateTimeOffset RequestTimestamp { get; private set; }
    public DateTimeOffset? ResponseTimestamp { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public int? TokenCountEstimate { get; private set; }
    public string? ErrorSummary { get; private set; }
    public string CorrelationId { get; private set; } = string.Empty;

    private AIInteractionLog() { }

    public AIInteractionLog(
        Guid projectId,
        Guid requirementSubmissionId,
        string providerName,
        string modelName,
        string promptTemplateVersion,
        DateTimeOffset requestTimestamp,
        string correlationId)
    {
        ProjectId = projectId;
        RequirementSubmissionId = requirementSubmissionId;
        ProviderName = providerName;
        ModelName = modelName;
        PromptTemplateVersion = promptTemplateVersion;
        RequestTimestamp = requestTimestamp;
        CorrelationId = correlationId;
        Status = "Pending";
    }

    public void Complete(DateTimeOffset responseTimestamp, string status, int? tokenCountEstimate = null)
    {
        ResponseTimestamp = responseTimestamp;
        Status = status;
        TokenCountEstimate = tokenCountEstimate;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Fail(DateTimeOffset responseTimestamp, string errorSummary)
    {
        ResponseTimestamp = responseTimestamp;
        Status = "Failed";
        ErrorSummary = errorSummary;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
