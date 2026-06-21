namespace ArchitectureGovernance.AI.Abstractions;

public interface IArchitectureAiProvider
{
    Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default);
}
