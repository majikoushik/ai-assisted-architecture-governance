using ArchitectureGovernance.AI.Abstractions;

namespace ArchitectureGovernance.AI.Mock;

public sealed class MockArchitectureAiProvider : IArchitectureAiProvider
{
    public Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        var humanReviewNotice = "This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.";
        
        var markdown = request.ArtifactType == "HighLevelDesign"
            ? $"""
            > {humanReviewNotice}

            # High-Level Design Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock High-Level Design for local development.

            ## Business Context
            {request.BusinessDomain} - {request.DomainContext}

            ## Architecture Goals
            Deliver a robust, scalable, and Azure-ready architecture supporting {request.RequirementTitle}.

            ## Scope and Assumptions
            - Requirement details are synthetic or approved for demo use.
            - Target deployment is Azure.
            
            ## System Context
            This system integrates with existing identity providers and downstream reporting.

            ## Major Components & Suggested Service Boundaries
            - **API Gateway**: Routes traffic and enforces rate limits.
            - **Core Service**: Handles the primary business logic.
            - **Data Store**: Manages persistent state.

            ## Integration Points & Data Flow Overview
            - External API -> Gateway -> Core Service -> Database.

            ## Security Considerations
            - Enforce OAuth2/OIDC at the gateway.
            - Encrypt data at rest and in transit.

            ## Observability Considerations
            - Centralized structured logging.
            - Distributed tracing with correlation IDs.

            ## Deployment, Scalability, Availability & Resilience
            - Deploy via container apps.
            - Auto-scale based on HTTP metrics.
            - Multi-zone availability setup.

            ## Key Risks
            - Integration, security, and compliance details require validation.

            ## Open Questions
            - What are the target availability and recovery objectives?
            """
            : $"""
            > {humanReviewNotice}

            # {request.ArtifactType} Draft: {request.RequirementTitle}

            ## Summary
            Deterministic mock output for local development and automated tests. This is a generated {request.ArtifactType} based on the requirement "{request.RequirementTitle}".

            ## Domain Context
            {request.BusinessDomain} - {request.DomainContext}

            ## Assumptions
            - Requirement details are synthetic or approved for demo use.
            - Human architecture review is required before decisions are accepted.

            ## Risks
            - Missing stakeholder context may affect recommendations.

            ## Open Questions
            - Which systems, teams, and compliance obligations are in scope?
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
