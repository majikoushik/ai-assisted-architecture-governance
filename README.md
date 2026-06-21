# AI-Assisted Architecture Governance

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)
![Angular 18](https://img.shields.io/badge/Angular-18-DD0031)
![Azure Ready](https://img.shields.io/badge/Azure-OpenAI%20ready-0078D4)
![Docker](https://img.shields.io/badge/Docker-compose%20ready-2496ED)
![Responsible AI](https://img.shields.io/badge/Responsible%20AI-human%20review%20required-16697A)
![CI](https://img.shields.io/badge/CI-GitHub%20Actions%20configured-2088FF)

An enterprise-style architecture governance accelerator that converts business requirements into structured, reviewable architecture artifacts using a mock-first AI provider model with Azure OpenAI readiness.

This repository is built as a Solution Architect portfolio project. It is designed to show architecture thinking, AI-assisted SDLC governance, prompt engineering maturity, .NET backend engineering, Angular frontend engineering, Azure deployment planning, observability, security awareness, and responsible AI boundaries.

## Executive Summary

AI-Assisted Architecture Governance helps architecture and engineering teams turn a business requirement into a governed set of architecture drafts: requirement analysis, HLD, LLD, ADR candidates, NFR review, security review, API contract review, and risk and assumption review.

The project is intentionally not a toy chatbot. It models a realistic governance workflow:

1. Capture an architecture project and requirement.
2. Select expected architecture artifact types.
3. Load versioned prompt templates from the repository.
4. Generate deterministic mock AI draft artifacts locally.
5. Optionally route generation through an Azure OpenAI provider when configured.
6. Store artifact metadata, prompt version, provider metadata, status, and correlation ID.
7. Route generated artifacts through human review and versioning.
8. Export generated artifacts as Markdown for review packs or architecture repositories.

Every AI-generated artifact is treated as draft content requiring qualified architect review.

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Portfolio Value

This project demonstrates how a Solution Architect can combine enterprise architecture practice with AI-assisted engineering. It shows more than framework usage: it demonstrates requirements interpretation, architecture governance, responsible AI controls, cloud deployment thinking, prompt lifecycle management, API-first design, secure configuration, observability, CI/CD readiness, and documentation discipline.

For recruiters, engineering managers, and interviewers, the repository provides concrete evidence of:

- Translating business requirements into architecture artifacts.
- Designing AI-assisted workflows with human oversight.
- Building a mock-first AI integration that runs without paid cloud credentials.
- Preparing for Azure OpenAI without hardcoded secrets or provider lock-in.
- Documenting architecture decisions through ADRs, HLD, LLD, NFRs, security, observability, and deployment guidance.
- Structuring a .NET and Angular application as an enterprise-style platform rather than a demo script.

## What This Project Demonstrates

| Area | Demonstrated capability |
| --- | --- |
| Solution Architecture | Requirement analysis, capability mapping, service boundary thinking, HLD, LLD, ADRs, NFRs, risks, assumptions, open questions. |
| AI Governance | Human-in-the-loop review, draft-only AI output, prompt constraints, hallucination reduction guidance, safe telemetry policy. |
| Prompt Engineering | Versioned Markdown prompt templates, explicit output structures, assumptions, risks, open questions, review notice, and prompt catalog docs. |
| Azure OpenAI Readiness | Provider abstraction, mock provider default, optional Azure OpenAI adapter, environment-based configuration, Key Vault direction. |
| Backend Engineering | ASP.NET Core 8 Web API, layered projects, FluentValidation, EF Core, Problem Details, Swagger, health checks, correlation IDs. |
| Frontend Engineering | Angular 18, strict TypeScript direction, enterprise dashboard UX, typed service layer, interceptors, artifact viewer, prompt catalog. |
| DevOps | GitHub Actions workflow, Dockerfiles, Docker Compose, Bicep validation direction, repeatable local setup. |
| Security | No required real secrets locally, environment configuration, future Entra ID direction, safe logging boundaries, secure deployment guidance. |
| Observability | Health endpoints, request logging, correlation ID propagation, AI interaction metadata, Application Insights readiness. |
| Documentation | Architecture docs, diagrams, ADRs, prompt catalog, setup guide, testing guide, deployment guide, operational runbook, roadmap. |

## Current Implementation Status

This is a portfolio MVP with a working local architecture and honest production boundaries.

| Capability | Status |
| --- | --- |
| Architecture project workspace | Implemented for local/demo use. |
| Requirement intake | Implemented for synthetic or approved demo requirements. |
| Prompt catalog | Implemented with versioned Markdown prompt templates under `prompts/`. |
| Mock AI provider | Implemented as the default provider for local development and CI-style validation. |
| Azure OpenAI provider | Adapter and configuration readiness are present; use is optional and environment-driven. |
| Artifact generation | Implemented for requirement analysis, HLD, LLD, ADR, NFR review, security review, API contract review, and risk/assumption review. |
| Artifact export | Markdown export is implemented. |
| Human review workflow | Implemented as a portfolio governance workflow. |
| Observability | Correlation IDs, health checks, logging direction, and safe AI metadata patterns are included. |
| Azure deployment | Bicep blueprint is included; production deployment still requires environment-specific security, networking, cost, and compliance review. |
| Authentication and RBAC | Documented as future scope; not claimed as implemented. |

## AI-Assisted Architecture Governance Capabilities

The platform generates structured Markdown drafts for:

- Requirement Analysis: summary, capabilities, actors, workflows, candidate services, assumptions, risks, open questions.
- High-Level Design: business context, system context, components, integration points, data flow, deployment, security, observability.
- Low-Level Design: component responsibilities, API boundaries, data model, validation rules, error handling, telemetry.
- ADR Suggestions: context, decision, consequences, alternatives considered.
- NFR Review: performance, scalability, availability, reliability, security, observability, maintainability, cost, compliance readiness.
- Security Review: authentication, authorization, secret management, sensitive data handling, logging safety, API security, threat modeling readiness.
- API Contract Review: endpoint naming, DTO guidance, error standards, versioning, idempotency, pagination, OpenAPI readiness.
- Risk and Assumption Review: assumptions, risks, dependencies, constraints, stakeholder questions, review notes.

Sample deterministic mock outputs are available in [samples/outputs](samples/outputs/).

## Responsible AI and Human Review

The repository deliberately keeps AI output inside a governance workflow:

- Local and Docker demos use `AI_PROVIDER=Mock` by default.
- Azure OpenAI is optional and must be explicitly configured.
- Prompt templates instruct the provider to avoid inventing facts, mark assumptions, identify risks, and ask open questions.
- Generated artifacts include or are accompanied by a human review notice.
- Security and API review artifacts are governance aids, not formal approvals.
- Full confidential prompts, full confidential requirements, full AI responses, API keys, and connection strings should not be logged.
- Public demos should use only synthetic samples from `samples/` or other non-confidential input.

See [Responsible AI Architecture](architecture/responsible-ai-architecture.md) and [Responsible AI Guidelines](docs/responsible-ai-guidelines.md).

## Azure OpenAI Readiness

The project is mock-first but Azure-oriented:

- AI provider calls go through an abstraction.
- The mock provider supports deterministic local demos and tests.
- Azure OpenAI configuration is environment-based.
- No Azure OpenAI secrets are required for local development.
- Production direction uses Azure Key Vault and Managed Identity.
- The deployment blueprint includes Azure OpenAI, Azure Container Apps, Azure SQL, Key Vault, Log Analytics, and Application Insights.

See [Azure OpenAI Provider Guide](docs/azure-openai-provider.md), [Azure Deployment Architecture](architecture/deployment-architecture.md), and [Bicep Blueprint](infra/bicep/README.md).

## Technology Stack

| Layer | Technology |
| --- | --- |
| Backend API | ASP.NET Core 8 Web API, C#, Swagger/OpenAPI |
| Application Layer | Commands, queries, DTOs, validation, use case orchestration |
| Domain Layer | Architecture projects, requirements, artifacts, prompt metadata, review records |
| Persistence | Entity Framework Core, SQL Server local/Azure SQL target |
| AI Providers | Mock deterministic provider, Azure OpenAI adapter through abstraction |
| Frontend | Angular 18, strict TypeScript direction, feature-oriented portal |
| Observability | Correlation IDs, request logging, health checks, Application Insights readiness |
| DevOps | GitHub Actions, Dockerfiles, Docker Compose |
| Infrastructure | Azure Bicep blueprint for Container Apps, Static Web Apps, SQL, Key Vault, Azure OpenAI, Log Analytics |

## Repository Map

```text
src/
  api/                         ASP.NET Core API host and controllers
  application/                 Use cases, validation, DTOs, commands, queries
  domain/                      Governance domain entities and rules
  infrastructure/              EF Core, prompt repository, provider wiring
  ai/                          AI abstractions, mock provider, Azure OpenAI provider
  building-blocks/             Observability, security, exporting helpers
  web/architecture-governance-portal/
tests/                         Backend and frontend test projects
prompts/                       Versioned prompt templates
samples/                       Synthetic demo inputs and mock output examples
architecture/                  HLD, LLD, NFRs, ADRs, diagrams, architecture guidance
docs/                          Setup, testing, operations, deployment, responsible AI docs
infra/bicep/                   Azure deployment blueprint
.github/workflows/             CI and deployment templates
```

## Architecture Diagrams

The architecture is documented with Mermaid-friendly Markdown diagrams:

- [System context](architecture/diagrams/system-context.md)
- [Container diagram](architecture/diagrams/container-diagram.md)
- [Requirement-to-artifact flow](architecture/diagrams/requirement-to-artifact-flow.md)
- [AI provider sequence](architecture/diagrams/ai-provider-sequence.md)
- [Prompt template flow](architecture/diagrams/prompt-template-flow.md)
- [Review and versioning flow](architecture/diagrams/review-versioning-flow.md)
- [Safe AI telemetry flow](architecture/diagrams/safe-ai-telemetry-flow.md)
- [Azure deployment](architecture/diagrams/azure-deployment.md)
- [CI/CD pipeline](architecture/diagrams/cicd-pipeline.md)

## Documentation Index

| Topic | Link |
| --- | --- |
| Architecture overview | [architecture/README.md](architecture/README.md) |
| High-level design | [architecture/hld.md](architecture/hld.md) |
| Low-level design | [architecture/lld.md](architecture/lld.md) |
| NFRs | [architecture/nfrs.md](architecture/nfrs.md) |
| API governance | [architecture/api-governance.md](architecture/api-governance.md) |
| Security architecture | [architecture/security-architecture.md](architecture/security-architecture.md) |
| Observability architecture | [architecture/observability-architecture.md](architecture/observability-architecture.md) |
| Deployment architecture | [architecture/deployment-architecture.md](architecture/deployment-architecture.md) |
| Data model | [architecture/data-model.md](architecture/data-model.md) |
| Prompt engineering strategy | [architecture/prompt-engineering-strategy.md](architecture/prompt-engineering-strategy.md) |
| Responsible AI architecture | [architecture/responsible-ai-architecture.md](architecture/responsible-ai-architecture.md) |
| Architecture decision records | [architecture/adr](architecture/adr/) |
| Setup guide | [docs/setup.md](docs/setup.md) |
| Local development | [docs/local-development.md](docs/local-development.md) |
| API contracts | [docs/api-contracts.md](docs/api-contracts.md) |
| Prompt catalog | [docs/prompt-catalog.md](docs/prompt-catalog.md) |
| Testing strategy | [docs/testing-strategy.md](docs/testing-strategy.md) |
| Deployment guide | [docs/deployment.md](docs/deployment.md) |
| Azure deployment guide | [docs/azure-deployment-guide.md](docs/azure-deployment-guide.md) |
| DevOps guide | [docs/devops-guide.md](docs/devops-guide.md) |
| Operational runbook | [docs/operational-runbook.md](docs/operational-runbook.md) |
| Roadmap | [docs/roadmap.md](docs/roadmap.md) |

## Prompt Catalog

Prompt templates are first-class architecture assets under [prompts](prompts/). Each template includes purpose, version, expected input, output format, constraints, architecture quality expectations, assumptions, risks, open questions, and the human review notice.

Current prompt templates:

- [Requirement analysis](prompts/requirement-analysis.md)
- [HLD generation](prompts/hld-generation.md)
- [LLD generation](prompts/lld-generation.md)
- [ADR generation](prompts/adr-generation.md)
- [NFR review](prompts/nfr-review.md)
- [Security review](prompts/security-review.md)
- [API contract review](prompts/api-contract-review.md)
- [Risk and assumption review](prompts/risk-assumption-review.md)

## Screenshots

Final screenshot images are not committed yet. Placeholder capture briefs are included so the repository remains transparent about what should be captured before public publishing.

| Screen | Purpose | Placeholder |
| --- | --- | --- |
| Dashboard | Architecture governance command center | [dashboard placeholder](docs/screenshots/dashboard-placeholder.md) |
| Project workspace | Project details, requirements, artifacts, reviews | [project workspace placeholder](docs/screenshots/project-workspace-placeholder.md) |
| Requirement detail | Requirement analysis and artifact generation actions | [requirement detail placeholder](docs/screenshots/requirement-detail-placeholder.md) |
| Artifact viewer | Markdown preview, version selector, export, review form | [artifact viewer placeholder](docs/screenshots/artifact-viewer-placeholder.md) |
| Prompt catalog | Prompt governance and version inspection | [prompt catalog placeholder](docs/screenshots/prompt-catalog-placeholder.md) |

Use synthetic data only when capturing screenshots for public GitHub publishing.

## API Overview

Swagger is available in development at:

```text
http://localhost:5080/swagger
```

Representative endpoints:

```text
POST   /api/v1/projects
GET    /api/v1/projects
GET    /api/v1/projects/{projectId}
POST   /api/v1/requirements
GET    /api/v1/projects/{projectId}/requirements
POST   /api/v1/artifacts/generate
GET    /api/v1/artifacts/{artifactId}
GET    /api/v1/artifacts/{artifactId}/export/markdown
GET    /api/v1/prompts
POST   /api/v1/reviews
GET    /health/live
GET    /health/ready
```

## Local Setup

Prerequisites:

- .NET 8 SDK
- Node.js 20, 21, or 22
- Docker Desktop for Docker Compose and local SQL Server container
- Azure CLI for optional Bicep validation

Restore and build:

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln
```

Run the API:

```powershell
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

Run the Angular portal:

```powershell
cd src/web/architecture-governance-portal
npm install
npm start
```

Default local URLs:

- Angular portal: `http://localhost:4200`
- API: `http://localhost:5080`
- Swagger: `http://localhost:5080/swagger`

## Docker Development

```powershell
copy .env.example .env
docker compose build
docker compose up
```

Docker Compose starts:

- API on `http://localhost:5080`
- Web portal on `http://localhost:4200`
- SQL Server on `localhost:1433`

Keep `AI_PROVIDER=Mock` for local demos unless Azure OpenAI has been intentionally configured through environment variables or secure local secrets.

## Testing and Validation

Backend:

```powershell
dotnet test ArchitectureGovernance.sln --configuration Release
```

Frontend:

```powershell
cd src/web/architecture-governance-portal
npm test
```

Infrastructure syntax validation:

```powershell
az bicep build --file infra/bicep/main.bicep
```

Docker validation:

```powershell
docker compose config
docker compose build
```

## Security and Observability

Security posture:

- No real secrets are required for local development.
- `.env.example` contains safe placeholders.
- Azure OpenAI endpoint, deployment name, and key are configuration values, not frontend values.
- Future production direction uses Azure Key Vault and Managed Identity.
- Future authentication direction is Microsoft Entra ID with role-based authorization for Architect, Reviewer, Admin, and Viewer roles.

Observability posture:

- Correlation ID propagation is included.
- Health endpoints are exposed for liveness and readiness.
- AI interaction metadata is designed to capture provider, artifact type, prompt version, duration, status, and correlation ID.
- Logs must not contain API keys, secrets, full confidential requirements, full prompts, full responses, connection strings, or tokens.

## Azure Deployment Blueprint

The Bicep blueprint under [infra/bicep](infra/bicep/) models:

- Azure Container Registry
- Azure Container Apps Environment
- Backend API Container App
- Azure Static Web Apps
- Azure SQL Database
- Azure OpenAI
- Azure Key Vault
- Log Analytics
- Application Insights

The blueprint is an architecture reference and deployment starting point. Production use still requires environment-specific parameter review, network design, identity and access review, cost review, security approval, and organizational compliance validation.

## Roadmap

Completed portfolio epics:

- Epic 0 to Epic 12: repository foundation through Azure deployment blueprint.
- Epic 13: portfolio polish, diagrams, sample outputs, documentation, ADRs, and first-impression improvements.

Recommended next epics:

- Microsoft Entra ID authentication and role-based authorization.
- Artifact comparison and diff view between generated versions.
- Architecture review board workflow with approvals and decision history.
- Prompt evaluation harness with golden outputs and safety checks.
- Application Insights dashboard and alert rule templates.
- PDF export and architecture review pack generation.
- Azure OpenAI managed identity hardening.

See the full [roadmap](docs/roadmap.md).

## Portfolio Positioning

This repository is best described as:

> An AI-assisted architecture governance accelerator for generating reviewable architecture artifacts from business requirements using .NET, Angular, Azure OpenAI-ready provider abstraction, prompt governance, and responsible AI controls.

It is suitable for demonstrating Solution Architect capability across enterprise architecture, cloud readiness, responsible AI design, backend engineering, frontend engineering, DevOps, observability, and documentation quality.
