namespace ArchitectureGovernance.AI.AzureOpenAI;

public class AzureOpenAiOptions
{
    public const string SectionName = "AzureOpenAI";

    public string Endpoint { get; set; } = string.Empty;
    public string? ApiKey { get; set; }
    public string DeploymentName { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
    public bool UseDefaultAzureCredential { get; set; } = false;
}
