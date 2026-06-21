using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.AI.AzureOpenAI;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace ArchitectureGovernance.AI.Tests;

public class AzureOpenAiProviderTests
{
    [Fact]
    public async Task GenerateArtifactDraftAsync_ReturnsFallback_WhenConfigIsMissing()
    {
        // Arrange
        var options = Options.Create(new AzureOpenAiOptions
        {
            Endpoint = "",
            ApiKey = "",
            DeploymentName = ""
        });
        
        var logger = NullLogger<AzureOpenAiProvider>.Instance;
        var provider = new AzureOpenAiProvider(options, logger);

        var request = new ArchitectureAiRequest(
            ProjectId: Guid.NewGuid(),
            RequirementId: Guid.NewGuid(),
            ArtifactType: "HighLevelDesign",
            RequirementTitle: "Test Req",
            RequirementText: "Test Text",
            BusinessDomain: "Test Domain",
            DomainContext: "Test Context",
            PromptTemplateName: "hld",
            PromptTemplateVersion: "1.0",
            PromptTemplateContent: "System prompt",
            CorrelationId: Guid.NewGuid().ToString()
        );

        // Act
        var result = await provider.GenerateArtifactDraftAsync(request);

        // Assert
        Assert.Equal("Failed", result.Status);
        Assert.Equal("AzureOpenAI (Misconfigured)", result.ProviderName);
        Assert.Contains("Error: Azure OpenAI Configuration Missing", result.Markdown);
        Assert.Contains("This artifact is AI-assisted draft content", result.Markdown);
    }
}
