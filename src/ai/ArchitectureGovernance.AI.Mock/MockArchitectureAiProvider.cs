using ArchitectureGovernance.AI.Abstractions;

namespace ArchitectureGovernance.AI.Mock;

public sealed class MockArchitectureAiProvider : IArchitectureAiProvider
{
    public Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        var markdown = $"""
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # {request.ArtifactType} Draft

        ## Summary

        Deterministic mock output for local development and automated tests.

        ## Assumptions

        - Requirement details are synthetic or approved for demo use.
        - Human architecture review is required before decisions are accepted.

        ## Risks

        - Missing stakeholder context may affect recommendations.
        - Integration, security, and compliance details require validation.

        ## Open Questions

        - Which systems, teams, and compliance obligations are in scope?
        - What are the target availability and recovery objectives?
        """;

        return Task.FromResult(new ArchitectureAiResponse(
            markdown,
            "Mock",
            "deterministic-local-provider",
            request.PromptTemplateVersion));
    }
}
