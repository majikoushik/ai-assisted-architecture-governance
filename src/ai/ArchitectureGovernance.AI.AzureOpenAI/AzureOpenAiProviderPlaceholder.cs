using ArchitectureGovernance.AI.Abstractions;

namespace ArchitectureGovernance.AI.AzureOpenAI;

public sealed class AzureOpenAiProviderPlaceholder : IArchitectureAiProvider
{
    public Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException(
            "Azure OpenAI integration is reserved for a future epic. Use the mock provider for local development.");
    }
}
