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
            "Synthetic loan platform requirement",
            "RequirementAnalysis",
            "requirement-analysis",
            "v1.0.0",
            "test-correlation");

        var response = await provider.GenerateArtifactDraftAsync(request);

        Assert.Equal("Mock", response.ProviderName);
        Assert.Contains("must be reviewed by a qualified architect", response.Markdown);
    }
}
