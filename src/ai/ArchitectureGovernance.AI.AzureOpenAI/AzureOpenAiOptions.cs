namespace ArchitectureGovernance.AI.AzureOpenAI;

public class AzureOpenAiOptions
{
    public const string SectionName = "AzureOpenAI";

    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
}
