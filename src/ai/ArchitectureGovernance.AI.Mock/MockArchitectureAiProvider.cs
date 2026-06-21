using ArchitectureGovernance.AI.Abstractions;

namespace ArchitectureGovernance.AI.Mock;

public sealed class MockArchitectureAiProvider : IArchitectureAiProvider
{
    public Task<ArchitectureAiResponse> GenerateArtifactDraftAsync(
        ArchitectureAiRequest request,
        CancellationToken cancellationToken = default)
    {
        var humanReviewNotice = "This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.";
        
        var markdown = request.ArtifactType switch
        {
            "HighLevelDesign" => $"""
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
            """,
            "LowLevelDesign" => $"""
            > {humanReviewNotice}

            # Low-Level Design Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock Low-Level Design for local development.

            ## Requirement Traceability
            Traces back to requirement "{request.RequirementTitle}" in the {request.BusinessDomain} domain.

            ## Component-Level Design & Module Responsibilities
            - **OrderProcessor**: Validates business rules and orchestration.
            - **InventoryAdapter**: Interfaces with external inventory systems.
            - **NotificationHandler**: Emits domain events to the message broker.

            ## API Boundaries & Request/Response DTOs
            - `POST /api/v1/orders`
            - `OrderRequestDto`: Contains OrderId, CustomerId, Items.
            - `OrderResponseDto`: Contains OrderId, Status, TrackingUrl.

            ## Data Model Recommendations
            - Use a relational schema with tables for `Orders`, `OrderItems`, and `Customers`.
            - Apply Entity Framework Core for data access.

            ## Validation & Error Handling
            - Implement `FluentValidation` on all incoming requests.
            - Return standard RFC 7807 `ProblemDetails` for errors.

            ## Logging & Telemetry
            - Log structured events including `CorrelationId`.
            - Do not log sensitive PII or full requirement texts.

            ## Security Implementation
            - Use bearer token authentication.
            - Validate issuer and audience claims.

            ## Integration Details & Sequence Flow
            - 1. Client sends request.
            - 2. API validates request.
            - 3. Database is updated within a transaction.
            - 4. Domain event is published.

            ## Testing Considerations
            - Achieve 80%+ unit test coverage.
            - Mock external inventory dependencies.

            ## Assumptions & Risks
            - Assuming inventory API uses REST.
            - Risk of latency when interacting with downstream legacy services.

            ## Open Questions
            - What is the expected peak load for the `POST /api/v1/orders` endpoint?
            """,
            "ArchitectureDecisionRecord" => $"""
            > {humanReviewNotice}

            # Architecture Decision Record Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock Architecture Decision Record generated for local development.

            ---

            ## ADR 001: Selection of Primary Data Store

            ### Status
            Proposed

            ### Context
            The system for "{request.RequirementTitle}" requires persistent storage of transactional data in the {request.BusinessDomain} domain. The data is highly relational with strong consistency requirements.

            ### Decision
            We will use Azure SQL Database as the primary transactional data store.

            ### Consequences
            - Provides ACID compliance.
            - Integrates seamlessly with Entity Framework Core.
            - Increases operational cost compared to NoSQL alternatives for high-throughput simple writes.

            ### Alternatives Considered
            - Azure Cosmos DB: Rejected because the domain model has highly relational constraints and joins are required.

            ### Risks
            - Schema evolution may require downtime if not managed carefully with migrations.

            ### Follow-up Actions
            - Provision a dev instance of Azure SQL.
            - Create initial EF Core migrations.

            ---
            
            ## Open Questions
            - Should we also provision a read-replica for reporting purposes?
            """,
            _ => $"""
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
            """
        };

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
