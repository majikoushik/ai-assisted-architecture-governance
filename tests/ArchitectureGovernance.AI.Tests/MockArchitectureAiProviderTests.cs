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
            RequirementTitle: "Test Requirement",
            RequirementText: "Test text",
            BusinessDomain: "Banking",
            DomainContext: "Core Systems",
            PromptTemplateName: "hld-generation",
            PromptTemplateVersion: "v1.0.0",
            CorrelationId: "test-correlation-id");

        var response = await provider.GenerateArtifactDraftAsync(request);

        Assert.Equal("MockDeterministicProvider", response.ProviderName);
        Assert.Contains("must be reviewed by a qualified architect", response.Markdown);
    }
}
