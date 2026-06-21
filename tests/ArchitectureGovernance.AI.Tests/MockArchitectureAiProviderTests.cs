using ArchitectureGovernance.AI.Abstractions;
using ArchitectureGovernance.AI.Mock;
using Xunit;

namespace ArchitectureGovernance.AI.Tests;

public sealed class MockArchitectureAiProviderTests
{
    [Fact]
    public async Task GenerateArtifactDraftAsync_ReturnsHumanReviewNotice()
    {
        var provider = new MockArchitectureAiProvider();
        var request = new ArchitectureAiRequest(
            ProjectId: Guid.NewGuid(),
            RequirementId: Guid.NewGuid(),
            ArtifactType: "HighLevelDesign",
            RequirementTitle: "Build a new system",
            RequirementText: "It needs to do everything.",
            BusinessDomain: "Retail",
            DomainContext: "Sales",
            PromptTemplateName: "hld",
            PromptTemplateVersion: "1.0",
            PromptTemplateContent: "System prompt",
            CorrelationId: Guid.NewGuid().ToString());

        var response = await provider.GenerateArtifactDraftAsync(request);

        Assert.Equal("MockDeterministicProvider", response.ProviderName);
        Assert.Contains("must be reviewed by a qualified architect", response.Markdown);
    }
}
