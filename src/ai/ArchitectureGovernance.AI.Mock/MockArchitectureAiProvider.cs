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
            "NonFunctionalRequirementReview" => $"""
            > {humanReviewNotice}

            # Non-Functional Requirement Review Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock NFR Review generated for local development.

            ## Performance Considerations
            - 95th percentile response time < 200ms for APIs.

            ## Scalability Considerations
            - Must support 2x traffic spikes during peak hours.

            ## Availability Considerations
            - Target 99.9% uptime.

            ## Reliability Considerations
            - Implement retry logic with exponential backoff for external dependencies.

            ## Security Considerations
            - Ensure all data is encrypted at rest and in transit.

            ## Observability Considerations
            - Implement distributed tracing and centralized logging.

            ## Maintainability Considerations
            - Codebase must achieve 80% test coverage.

            ## Cost Considerations
            - Monitor and optimize Azure consumption costs.

            ## Compliance-readiness Considerations
            - GDPR and PII data handling compliance required.

            ## AI Provider Dependency Considerations
            - Consider API rate limits of AI providers.

            ## NFR Checklist
            - [ ] Performance SLA defined
            - [ ] Disaster recovery plan documented
            - [ ] Security audit scheduled

            ## Priority Ranking
            1. Security
            2. Availability
            3. Performance

            ## Assumptions
            - User traffic is distributed primarily during business hours.

            ## Risks
            - High cost implications for multi-region active-active deployment.

            ## Open Questions
            - Is multi-region failover required for the initial MVP?
            """,
            "SecurityReview" => $"""
            > {humanReviewNotice}

            # Security Review Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock Security Review generated for local development.

            ## Authentication Considerations
            - Enforce OIDC/OAuth2 via the enterprise identity provider.

            ## Authorization Considerations
            - Implement Role-Based Access Control (RBAC).

            ## Sensitive Data Handling
            - Encrypt PII data fields at the application level before storage.

            ## Secret Management
            - Use Azure Key Vault to store database connection strings and API keys.

            ## Input Validation Risks
            - All API requests must be validated using FluentValidation.

            ## API Security Concerns
            - Enforce rate limiting at the API Gateway.
            - Disable CORS for untrusted domains.

            ## Logging and Telemetry Safety
            - Ensure passwords and bearer tokens are never logged.

            ## OWASP Considerations
            - Mitigate Broken Access Control and Injection risks.

            ## Threat-modeling Readiness
            - A STRIDE threat model session is recommended before deployment.

            ## Cloud Security Considerations
            - Restrict database access to the VNet.

            ## Responsible AI Security Considerations
            - Filter prompt inputs to prevent prompt injection.

            ## Assumptions
            - The organization has an existing WAF (Web Application Firewall) configured.

            ## Risks
            - Lack of automated dynamic application security testing (DAST) in the pipeline.

            ## Open Questions
            - What are the compliance requirements for data retention?
            """,
            "ApiContractReview" => $"""
            > {humanReviewNotice}

            # API Contract Review Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock API Contract Review generated for local development.

            ## API Boundary Recommendations
            - Aggregate operations under a single `/api/v1/{request.BusinessDomain.ToLower().Replace(" ", "-")}` bounded context.

            ## Endpoint Naming Review
            - Ensure plural nouns are used for resource endpoints (e.g., `/users`, not `/user`).

            ## Request DTO Guidance
            - Ensure required fields are clearly marked and validated.

            ## Response DTO Guidance
            - Do not leak internal database IDs; use public GUIDs.

            ## Error Response Standards
            - All errors must use the RFC 7807 Problem Details format.

            ## Versioning Readiness
            - Use URI versioning (e.g., `/api/v1/...`) to prepare for breaking changes.

            ## Idempotency Considerations
            - `POST` endpoints should support an `Idempotency-Key` header.

            ## Pagination and Filtering Considerations
            - Use cursor-based or offset-based pagination for collection endpoints.

            ## Authentication and Authorization Readiness
            - Ensure 401 and 403 status codes are correctly returned.

            ## OpenAPI/Swagger Readiness
            - Include descriptions and example values in the OpenAPI specification.

            ## Backward Compatibility Concerns
            - Additive changes only; do not remove fields without a deprecation window.

            ## Assumptions
            - APIs are primarily consumed by first-party web and mobile clients.

            ## Risks
            - Lack of defined SLA for downstream API dependencies.

            ## Open Questions
            - Will third-party external integrators be consuming these APIs?
            """,
            "RiskAndAssumptionReview" => $"""
            > {humanReviewNotice}

            # Risk and Assumption Review Draft: {request.RequirementTitle}

            ## Executive Summary
            Deterministic mock Risk and Assumption Review generated for local development.

            ## Key Assumptions
            - The project will deploy to the existing Azure tenant.
            - Stakeholders agree on the MVP scope constraints.

            ## Business Risks
            - Market conditions may shift priority before launch.

            ## Technical Risks
            - Integration with legacy mainframes may introduce latency.

            ## Security Risks
            - Improper RBAC configuration could lead to unauthorized access.

            ## Operational Risks
            - Lack of established support runbooks for the new microservices.

            ## Data Risks
            - Data migration from the old system may encounter data quality issues.

            ## AI-related Risks
            - Generative AI features may hallucinate or return unpredictable results.

            ## Cloud Deployment Risks
            - Unoptimized resource configurations could lead to budget overruns.

            ## Dependency Risks
            - Reliance on a single third-party payment gateway.

            ## Open Questions
            - Have the business stakeholders approved the data retention policy?

            ## Mitigation Recommendations
            - Conduct a PoC for the legacy mainframe integration.
            - Set up Azure Cost Management alerts.
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
