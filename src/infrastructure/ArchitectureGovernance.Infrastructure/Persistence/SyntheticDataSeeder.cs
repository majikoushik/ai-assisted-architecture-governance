using ArchitectureGovernance.Domain;
using ArchitectureGovernance.Domain.AIInteractions;
using ArchitectureGovernance.Domain.Artifacts;
using ArchitectureGovernance.Domain.Projects;
using ArchitectureGovernance.Domain.Requirements;
using ArchitectureGovernance.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Infrastructure.Persistence;

public static class SyntheticDataSeeder
{
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Projects.AnyAsync(cancellationToken))
        {
            return;
        }

        var loanPlatform = new ArchitectureProject(
            "Loan Origination Modernization",
            "Retail Banking",
            "Synthetic architecture governance workspace for customer onboarding, document upload, approvals, notifications, audit logging, and reporting.",
            "Asha Raman");
        loanPlatform.UpdateStatus(ProjectStatus.Active);

        var careCoordination = new ArchitectureProject(
            "Care Coordination Platform",
            "Healthcare",
            "Synthetic workspace for patient care plans, provider collaboration, consent-aware data sharing, and operational reporting.",
            "Daniel Morgan");
        careCoordination.UpdateStatus(ProjectStatus.UnderReview);

        var orderManagement = new ArchitectureProject(
            "Order Management Resilience",
            "E-Commerce",
            "Synthetic workspace for order orchestration, inventory reservation, payment integration, fulfillment events, and customer notifications.",
            "Priya Shah");

        await context.Projects.AddRangeAsync(new[] { loanPlatform, careCoordination, orderManagement }, cancellationToken);

        var loanRequirement = new RequirementSubmission(
            loanPlatform.Id,
            "Automated loan processing and approval workflow",
            "Build a loan processing system with customer onboarding, document upload, KYC checks, approval workflow, notifications, audit logging, and reporting.",
            loanPlatform.BusinessDomain,
            "business.architect@example.com",
            new[]
            {
                ArchitectureGovernance.Domain.Requirements.ArtifactType.RequirementAnalysis,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.HighLevelDesign,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.LowLevelDesign,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.ArchitectureDecisionRecord,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.NonFunctionalRequirementReview,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.ApiContractReview,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.SecurityReview,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.RiskAndAssumptionReview
            },
            "Use synthetic banking data only. Target cloud direction is Azure Container Apps, Azure SQL, Azure Blob Storage, Azure OpenAI, Key Vault, and Application Insights.");
        loanRequirement.UpdateStatus(RequirementStatus.AnalysisCompleted);

        var healthcareRequirement = new RequirementSubmission(
            careCoordination.Id,
            "Consent-aware care coordination workspace",
            "Create a care coordination platform that allows clinicians, case managers, and patients to collaborate on care plans with consent-aware access and audit trails.",
            careCoordination.BusinessDomain,
            "healthcare.architect@example.com",
            new[]
            {
                ArchitectureGovernance.Domain.Requirements.ArtifactType.RequirementAnalysis,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.HighLevelDesign,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.SecurityReview,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.NonFunctionalRequirementReview
            },
            "Synthetic scenario for portfolio demonstration. Do not use real patient data.");
        healthcareRequirement.UpdateStatus(RequirementStatus.AnalysisPending);

        var ecommerceRequirement = new RequirementSubmission(
            orderManagement.Id,
            "Event-driven order orchestration",
            "Modernize order management with inventory reservation, payment authorization, fulfillment events, customer notifications, dashboards, and retry handling.",
            orderManagement.BusinessDomain,
            "commerce.architect@example.com",
            new[]
            {
                ArchitectureGovernance.Domain.Requirements.ArtifactType.RequirementAnalysis,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.ApiContractReview,
                ArchitectureGovernance.Domain.Requirements.ArtifactType.RiskAndAssumptionReview
            },
            "Synthetic order-management scenario for local demos.");

        await context.Requirements.AddRangeAsync(new[] { loanRequirement, healthcareRequirement, ecommerceRequirement }, cancellationToken);

        var artifacts = BuildLoanArtifacts(loanPlatform.Id, loanRequirement.Id).ToList();
        artifacts.Add(new GeneratedArtifact(
            careCoordination.Id,
            healthcareRequirement.Id,
            ArchitectureGovernance.Domain.ArtifactType.SecurityReview,
            "Care Coordination Platform - Security Review",
            BuildSecurityReviewMarkdown("care coordination", "patient consent, role-based access, and auditability"),
            "1.0.0",
            "SyntheticFallback",
            "security-review",
            "v1.0.0",
            "synthetic-care-security"));

        artifacts.Add(new GeneratedArtifact(
            orderManagement.Id,
            ecommerceRequirement.Id,
            ArchitectureGovernance.Domain.ArtifactType.ApiContractReview,
            "Order Management Resilience - API Contract Review",
            BuildApiReviewMarkdown(),
            "1.0.0",
            "SyntheticFallback",
            "api-contract-review",
            "v1.0.0",
            "synthetic-commerce-api"));

        await context.Artifacts.AddRangeAsync(artifacts, cancellationToken);

        var primaryArtifact = artifacts.First();
        await context.Reviews.AddRangeAsync(new[]
        {
            new ReviewRecord(primaryArtifact.Id, loanPlatform.Id, loanRequirement.Id, "Maya Chen", ReviewStatus.NeedsReview, "Architecture draft is usable for stakeholder review. Confirm regulatory reporting and document-retention requirements.", "synthetic-review-001"),
            new ReviewRecord(artifacts[1].Id, loanPlatform.Id, loanRequirement.Id, "Ravi Kumar", ReviewStatus.Reviewed, "HLD covers main components. Add explicit data residency and DR assumptions before approval.", "synthetic-review-002")
        }, cancellationToken);

        var completedAt = DateTimeOffset.UtcNow.AddMinutes(-3);
        var interactions = artifacts.Take(6).Select((artifact, index) =>
        {
            var log = new AIInteractionLog(
                artifact.ProjectId,
                artifact.RequirementSubmissionId,
                artifact.ProviderName,
                "mock-governance-model",
                artifact.PromptTemplateVersion,
                DateTimeOffset.UtcNow.AddMinutes(-20 + index),
                artifact.CorrelationId);
            log.Complete(completedAt.AddSeconds(index * 11), "Completed", 1200 + index * 125);
            return log;
        });

        await context.AIInteractionLogs.AddRangeAsync(interactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<GeneratedArtifact> BuildLoanArtifacts(Guid projectId, Guid requirementId)
    {
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.RequirementAnalysis, "Loan Origination Modernization - Requirement Analysis", BuildRequirementAnalysisMarkdown(), "1.0.0", "SyntheticFallback", "requirement-analysis", "v1.0.0", "synthetic-loan-analysis");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.HighLevelDesign, "Loan Origination Modernization - HLD", BuildHldMarkdown(), "1.0.0", "SyntheticFallback", "hld-generation", "v1.0.0", "synthetic-loan-hld");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.LowLevelDesign, "Loan Origination Modernization - LLD", BuildLldMarkdown(), "1.0.0", "SyntheticFallback", "lld-generation", "v1.0.0", "synthetic-loan-lld");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.ArchitectureDecisionRecord, "ADR Candidate - Mock-first AI Provider", BuildAdrMarkdown(), "1.0.0", "SyntheticFallback", "adr-generation", "v1.0.0", "synthetic-loan-adr");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.NonFunctionalRequirementReview, "Loan Origination Modernization - NFR Review", BuildNfrMarkdown(), "1.0.0", "SyntheticFallback", "nfr-review", "v1.0.0", "synthetic-loan-nfr");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.ApiContractReview, "Loan Origination Modernization - API Contract Review", BuildApiReviewMarkdown(), "1.0.0", "SyntheticFallback", "api-contract-review", "v1.0.0", "synthetic-loan-api");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.SecurityReview, "Loan Origination Modernization - Security Review", BuildSecurityReviewMarkdown("loan processing", "KYC data, document metadata, approval decisions, and audit trails"), "1.0.0", "SyntheticFallback", "security-review", "v1.0.0", "synthetic-loan-security");
        yield return new GeneratedArtifact(projectId, requirementId, ArchitectureGovernance.Domain.ArtifactType.RiskAndAssumptionReview, "Loan Origination Modernization - Risk and Assumption Review", BuildRiskMarkdown(), "1.0.0", "SyntheticFallback", "risk-assumption-review", "v1.0.0", "synthetic-loan-risk");
    }

    private static string BuildRequirementAnalysisMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # Requirement Analysis

        ## Summary

        The requirement describes a synthetic loan origination platform covering onboarding, document upload, approval workflows, notifications, audit logging, and reporting.

        ## Business Capabilities

        | Capability | Notes |
        | --- | --- |
        | Customer onboarding | Capture applicant profile, consent, and eligibility context. |
        | Document management | Upload, classify, scan, and retain application documents. |
        | Approval workflow | Route applications through underwriting and exception handling. |
        | Notifications | Send customer and internal workflow updates. |
        | Audit and reporting | Maintain decision history, review status, and operational reports. |

        ## Actors

        - Applicant
        - Loan officer
        - Underwriter
        - Compliance reviewer
        - Operations manager

        ## Open Questions

        - What document retention period is required?
        - Which KYC and credit bureau integrations are in scope?
        - What approval SLA applies to standard and exception flows?
        """;

    private static string BuildHldMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # High-Level Design Draft

        ## System Context

        The platform exposes an API-backed web portal for loan officers and operations users. External integrations include identity, document storage, notification providers, reporting tools, and optional credit/KYC services.

        ## Key Components

        | Component | Responsibility |
        | --- | --- |
        | Web Portal | Requirement intake, application tracking, review queue, artifact viewing. |
        | API Gateway/API | REST endpoints, validation, correlation IDs, Problem Details responses. |
        | Workflow Service | Application state machine and approval routing. |
        | Document Service | Metadata, storage references, malware scanning integration readiness. |
        | Audit Service | Immutable business events and review history. |

        ## Azure Direction

        Deploy backend to Azure Container Apps, frontend to Azure Static Web Apps, data to Azure SQL, documents to Blob Storage, secrets to Key Vault, and telemetry to Application Insights.
        """;

    private static string BuildLldMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # Low-Level Design Draft

        ## API Boundaries

        - `POST /api/v1/applications`
        - `POST /api/v1/applications/{id}/documents`
        - `POST /api/v1/applications/{id}/workflow-actions`
        - `GET /api/v1/applications/{id}/audit-events`

        ## Validation Rules

        - Applicant identity fields are required.
        - Uploaded document metadata must include type, source, checksum, and classification.
        - Workflow transitions must be validated against current status and user role.

        ## Telemetry

        Track correlation ID, application ID, workflow action, actor role, outcome, provider latency, and safe error summaries.
        """;

    private static string BuildAdrMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # ADR Candidate: Use Mock-First AI Provider for Local Demos

        ## Context

        The platform must run locally and in CI without requiring Azure OpenAI credentials.

        ## Decision

        Use a provider abstraction with a deterministic mock provider as the default local provider and an Azure OpenAI adapter for production readiness.

        ## Consequences

        - Local demos are repeatable and safe.
        - Prompt contracts can be tested without cloud dependency.
        - Production rollout still requires model configuration, safety evaluation, and operational monitoring.
        """;

    private static string BuildNfrMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # NFR Review

        | Area | Draft Consideration |
        | --- | --- |
        | Availability | Define RTO/RPO and failover expectations for workflow and reporting. |
        | Performance | Measure document upload latency and approval queue response times. |
        | Security | Enforce role-based access and protect document metadata. |
        | Observability | Emit correlation IDs, workflow metrics, and safe AI metadata. |
        | Cost | Separate hot workflow data from document archive storage. |
        """;

    private static string BuildApiReviewMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # API Contract Review

        ## Recommendations

        - Use resource-oriented routes and plural nouns.
        - Return Problem Details for validation and business-rule failures.
        - Include correlation ID in every response.
        - Use idempotency keys for command-style workflow operations.
        - Document pagination for list endpoints.

        ## Candidate Endpoints

        | Method | Route | Purpose |
        | --- | --- | --- |
        | POST | `/api/v1/applications` | Create application. |
        | GET | `/api/v1/applications/{id}` | Retrieve application detail. |
        | POST | `/api/v1/applications/{id}/workflow-actions` | Perform approval action. |
        """;

    private static string BuildSecurityReviewMarkdown(string scenario, string sensitiveData) => $"""
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # Security Review

        ## Scope

        Synthetic review for {scenario}.

        ## Sensitive Data

        Protect {sensitiveData}. Do not log secrets, full confidential requirements, full prompts, or full AI responses.

        ## Controls

        - Use Microsoft Entra ID for future authentication.
        - Enforce role-based authorization for architect, reviewer, admin, and viewer personas.
        - Store provider secrets in Key Vault or environment configuration.
        - Apply malware scanning and content validation to uploaded files.
        - Keep audit records tamper-evident and queryable.
        """;

    private static string BuildRiskMarkdown() => """
        > This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

        # Risk and Assumption Review

        ## Assumptions

        - Synthetic data is used for local demonstration.
        - Azure OpenAI is optional and disabled unless configured.
        - Human architect review is required before any production decision.

        ## Risks

        | Risk | Mitigation |
        | --- | --- |
        | Incomplete regulatory requirements | Confirm compliance scope with stakeholders. |
        | Unclear integration SLAs | Capture external dependency contracts early. |
        | Sensitive prompt logging | Use safe metadata-only telemetry policy. |
        """;
}
