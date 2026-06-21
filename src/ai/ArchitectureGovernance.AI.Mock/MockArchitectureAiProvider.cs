using ArchitectureGovernance.AI.Abstractions;

namespace ArchitectureGovernance.AI.Mock;

public sealed class MockArchitectureAiProvider : IArchitectureAiProvider
{
    public Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        var humanReviewNotice = "This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.";
        
        var markdown = $"""
        > {humanReviewNotice}

        # {request.ArtifactType} Draft: {request.RequirementTitle}

        ## Summary

        Deterministic mock output for local development and automated tests. This is a generated {request.ArtifactType} based on the requirement "{request.RequirementTitle}".

        ## Domain Context
        {request.BusinessDomain} - {request.DomainContext}

        ## Assumptions

        - Requirement details are synthetic or approved for demo use.
        - Human architecture review is required before decisions are accepted.
        - The target deployment environment is Azure (mock assumption).

        ## Risks

        - Missing stakeholder context may affect recommendations.
        - Integration, security, and compliance details require validation.

        ## Open Questions

        - Which systems, teams, and compliance obligations are in scope?
        - What are the target availability and recovery objectives?
        """;

        var response = new ArchitectureAiResponse(
            ArtifactType: request.ArtifactType,
            Markdown: markdown,
            ProviderName: "MockDeterministicProvider",
            PromptTemplateName: request.PromptTemplateName,
            PromptTemplateVersion: request.PromptTemplateVersion,
            GenerationTimestamp: DateTimeOffset.UtcNow,
            Status: "Success",
            Warnings: Array.Empty<string>(),
            HumanReviewNotice: humanReviewNotice
        );

        return Task.FromResult(response);
    }
}
