# High-Level Design

## Business Context

Architecture teams often receive incomplete business requirements and must convert them into reviewable architecture artifacts. This platform demonstrates a governed, AI-assisted workflow that helps architects move from intake to structured drafts while preserving human review and traceability.

## System Context

Primary users are solution architects, architecture reviewers, engineering leads, and platform teams. The system includes:

- Angular governance portal for project workspaces, requirement intake, artifact preview, prompt catalog, and review workflow.
- ASP.NET Core API for REST endpoints, validation, artifact generation, export, and review management.
- SQL Server locally and Azure SQL as the cloud target.
- Source-controlled prompt catalog under `prompts/`.
- Mock deterministic AI provider for local demos and tests.
- Azure OpenAI provider adapter for production-readiness experiments.
- Observability building blocks for correlation IDs, health checks, request logging, and safe AI telemetry metadata.

## Major Components

| Component | Responsibility |
| --- | --- |
| Angular Portal | User-facing architecture governance workspace. |
| API Host | HTTP routing, Swagger, health checks, middleware, and dependency registration. |
| Application Layer | Use-case orchestration, commands, queries, validators, artifact generation flow. |
| Domain Layer | Architecture projects, requirements, artifacts, prompt templates, reviews, statuses. |
| Infrastructure Layer | EF Core persistence, prompt loading, AI provider wiring. |
| AI Abstractions | Provider contract shared by mock and Azure OpenAI implementations. |
| Building Blocks | Observability, security policy, and export helpers. |

## Architecture Governance Workflow

1. Architect creates a project workspace.
2. Architect submits a business or technical requirement.
3. The application validates the request and selected artifact type.
4. The prompt repository loads the matching versioned prompt template.
5. The configured AI provider returns a structured Markdown draft.
6. The artifact is persisted with version, provider, prompt metadata, correlation ID, and status.
7. A reviewer records review comments and status.
8. The artifact can be exported as Markdown.

## Key Quality Attributes

- **Security:** No committed secrets, secure configuration path, future Entra ID direction, Key Vault readiness.
- **Responsible AI:** Human review notice, prompt constraints, synthetic demo data, safe telemetry policy.
- **Traceability:** Project, requirement, prompt template, artifact version, provider, and review records.
- **Observability:** Correlation IDs, health checks, structured logs, Application Insights readiness.
- **Testability:** Mock provider, deterministic sample outputs, layered tests.
- **Azure readiness:** Container Apps, Static Web Apps, Azure SQL, Azure OpenAI, Key Vault, Log Analytics, Application Insights.

## Known Limits

The current implementation is an MVP portfolio platform. Authentication, authorization, prompt evaluation scoring, production networking, and formal architecture board approvals are documented future scope.
