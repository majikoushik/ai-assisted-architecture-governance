# AI-Assisted Architecture Governance

![CI](https://img.shields.io/badge/CI-GitHub%20Actions-2088FF)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![Angular](https://img.shields.io/badge/Angular-18-DD0031)
![Azure Ready](https://img.shields.io/badge/Azure-blueprint--ready-0078D4)
![Responsible AI](https://img.shields.io/badge/Responsible%20AI-human--review--required-16697A)

## Executive Summary

AI-Assisted Architecture Governance is a portfolio-grade enterprise architecture accelerator. It demonstrates how a Solution Architect can use AI-assisted engineering to convert business requirements into structured, reviewable, versioned, and exportable architecture artifacts.

This is not a generic chatbot. The product is organized around architecture governance: requirement intake, prompt-governed artifact generation, risk and assumption review, human approval workflow, safe AI telemetry, and Azure-ready deployment design.

## What This Project Demonstrates

- AI-assisted SDLC and Solution Architecture workflow design.
- HLD, LLD, ADR, NFR, security, API contract, and risk review artifact generation.
- Prompt engineering as versioned architecture assets.
- Mock-provider-first local development with Azure OpenAI readiness.
- Human-in-the-loop review, artifact versioning, and traceability.
- ASP.NET Core Web API layered architecture with validation, Problem Details, health checks, and correlation IDs.
- Angular enterprise dashboard patterns with typed API services and interceptors.
- Azure deployment blueprint using Container Apps, Static Web Apps, Azure SQL, Key Vault, Azure OpenAI, Log Analytics, and Application Insights.
- CI, Docker, Bicep, testing, operational runbook, and responsible AI documentation.

## Architecture Governance Workflow

1. Create an architecture project.
2. Capture a synthetic or approved business requirement.
3. Select expected artifact types.
4. Load a versioned prompt template from the repository.
5. Generate a deterministic mock draft locally or use Azure OpenAI when explicitly configured.
6. Store the artifact with provider, prompt template, version, status, timestamp, and correlation metadata.
7. Review the artifact through a human architect workflow.
8. Export Markdown for review packs or documentation repositories.

## Responsible AI Positioning

All generated architecture content is draft content.

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

The repository deliberately preserves these boundaries:

- `AI_PROVIDER=Mock` is the local and CI default.
- Azure OpenAI is optional and configuration-driven.
- Prompt templates instruct the model to separate known facts from assumptions.
- Full prompts, full requirement text, secrets, and full AI responses should not be logged by default.
- Security and API review outputs are governance aids, not formal approvals.
- Public demos should use only synthetic requirements and sample data.

## Core Capabilities

| Capability | Current implementation |
| --- | --- |
| Project workspace | Create, list, view, and update architecture projects. |
| Requirement intake | Capture requirement title, text, domain context, submitter, and expected artifacts. |
| Prompt catalog | Versioned Markdown prompt templates loaded from `prompts/`. |
| Artifact generation | Requirement analysis, HLD, LLD, ADR, NFR, security, API contract, and risk review drafts. |
| Artifact versioning | New generated artifacts increment version per requirement and artifact type. |
| Human review | Review records capture reviewer, status, comments, and timestamps. |
| Export | Generated artifacts can be exported as Markdown. |
| Observability | Correlation ID middleware, request logging, health checks, and safe AI telemetry metadata. |
| Azure readiness | Azure OpenAI adapter, Bicep blueprint, Key Vault pattern, Application Insights readiness. |

## Technology Stack

| Layer | Technology |
| --- | --- |
| Backend API | ASP.NET Core 8 Web API, C#, MediatR, FluentValidation |
| Domain/Application | Layered architecture with domain entities, commands, queries, DTOs, validators |
| Persistence | Entity Framework Core, SQL Server locally, Azure SQL target |
| AI providers | Mock deterministic provider by default, Azure OpenAI adapter through abstraction |
| Frontend | Angular 18, strict TypeScript, standalone components, reactive forms |
| Observability | Correlation IDs, structured request logging, health endpoints, Application Insights readiness |
| DevOps | GitHub Actions, Dockerfiles, Docker Compose |
| Cloud blueprint | Azure Container Apps, Static Web Apps, Azure SQL, Azure OpenAI, Key Vault, Log Analytics |

## Repository Structure

```text
src/
  api/                         ASP.NET Core API host and controllers
  application/                 Use cases, validation, DTOs, queries, commands
  domain/                      Governance domain entities and rules
  infrastructure/              EF Core, prompt repository, provider wiring
  ai/                          AI abstractions, mock provider, Azure OpenAI provider
  building-blocks/             Observability, security, exporting helpers
  web/architecture-governance-portal/
tests/                         Backend and frontend tests
prompts/                       Versioned prompt templates
samples/                       Synthetic demo inputs and mock outputs
architecture/                  HLD, LLD, NFRs, ADRs, diagrams, architecture guidance
docs/                          Setup, testing, operations, deployment, responsible AI docs
infra/bicep/                   Azure deployment blueprint
.github/workflows/             CI and deployment templates
```

## System Architecture

Key diagrams:

- [System context](architecture/diagrams/system-context.md)
- [Container diagram](architecture/diagrams/container-diagram.md)
- [Requirement-to-artifact flow](architecture/diagrams/requirement-to-artifact-flow.md)
- [AI provider sequence](architecture/diagrams/ai-provider-sequence.md)
- [Prompt template flow](architecture/diagrams/prompt-template-flow.md)
- [Review and versioning flow](architecture/diagrams/review-versioning-flow.md)
- [Safe AI telemetry flow](architecture/diagrams/safe-ai-telemetry-flow.md)
- [Azure deployment](architecture/diagrams/azure-deployment.md)
- [CI/CD pipeline](architecture/diagrams/cicd-pipeline.md)

## Prompt Catalog

Prompt templates are first-class architecture assets under `prompts/`. Each template includes version, purpose, expected input, output format, constraints, assumptions, risks, open questions, anti-hallucination guidance, and the mandatory human review notice.

Current templates:

- Requirement analysis
- High-level design generation
- Low-level design generation
- Architecture decision record generation
- Non-functional requirement review
- Security review
- API contract review
- Risk and assumption review

## Generated Architecture Artifacts

The platform generates structured Markdown drafts for:

- Requirement analysis and capability mapping.
- High-level and low-level design.
- ADR candidates.
- NFR checklist and quality attribute review.
- Security and responsible AI considerations.
- API contract governance review.
- Risks, assumptions, dependencies, and open questions.

Sample mock outputs are available under [samples/outputs](samples/outputs/).

## Human Review and Versioning

Generated artifacts are stored with:

- Artifact type and title.
- Version number.
- Draft/review status.
- Provider name.
- Prompt template name and version.
- Requirement and project traceability.
- Correlation ID.

Reviewers can record comments and statuses such as `NeedsReview`, `Reviewed`, `Approved`, and `Rejected`. Approval means a human reviewer has accepted the draft for the demo workflow; it is not a substitute for enterprise architecture board approval.

## Application Screens

Screenshots are intentionally documented as placeholders until final images are captured from a running environment.

| Screen | Purpose | Screenshot |
| --- | --- | --- |
| Dashboard | Architecture governance command center | `docs/screenshots/dashboard.png` |
| Project workspace | Project details, requirements, artifacts, reviews | `docs/screenshots/project-workspace.png` |
| Requirement detail | Requirement analysis and artifact generation actions | `docs/screenshots/requirement-detail.png` |
| Artifact viewer | Markdown preview, version selector, export, review form | `docs/screenshots/artifact-viewer.png` |
| Prompt catalog | Prompt governance and version inspection | `docs/screenshots/prompt-catalog.png` |

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

## Local Development

Prerequisites:

- .NET 8 SDK
- Node.js 20, 21, or 22
- SQL Server local instance or Docker SQL Server
- Docker Desktop for containerized development

Backend:

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

Frontend:

```powershell
cd src/web/architecture-governance-portal
npm install
npm start
```

Default local URLs:

- Angular portal: `http://localhost:4200`
- API: `http://localhost:5080`
- Swagger: `http://localhost:5080/swagger`

## Docker-Based Development

```powershell
copy .env.example .env
docker compose build
docker compose up
```

Docker Compose starts:

- API on `http://localhost:5080`
- Web portal on `http://localhost:4200`
- SQL Server on `localhost:1433`

Keep `AI_PROVIDER=Mock` for local demos unless you intentionally configure Azure OpenAI through environment variables or secure local secrets.

## Testing

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
docker compose build
docker compose config
```

## Observability and Safe AI Telemetry

The backend records operational metadata such as provider name, artifact type, prompt template version, duration, status, and correlation ID. It must not log API keys, secrets, full confidential requirements, full prompts, or full AI responses by default.

Health endpoints:

- `/health/live` for process liveness.
- `/health/ready` for dependency readiness.
- `/api/v1/platform/readiness` for demo platform readiness.

## Security and Secrets Management

- No real secrets are required for local development.
- `.env.example` contains only safe placeholders.
- Azure OpenAI endpoint, key, and deployment name are configuration values, not frontend values.
- Production direction uses Azure Key Vault and Managed Identity.
- Future auth direction is Microsoft Entra ID with role-based authorization for Architect, Reviewer, Admin, and Viewer roles.

## Azure Deployment Blueprint

The Bicep blueprint under `infra/bicep/` models:

- Azure Container Registry
- Azure Container Apps Environment
- Backend API Container App
- Azure Static Web Apps
- Azure SQL Database
- Azure OpenAI
- Azure Key Vault
- Log Analytics
- Application Insights

The blueprint is deployment-ready as an architecture reference, but production use still requires security review, networking decisions, environment-specific parameters, cost review, and organizational compliance approval.

## Architecture Decision Records

ADRs are stored under [architecture/adr](architecture/adr/), covering architecture style, AI provider abstraction, mock-provider-first development, Azure OpenAI target provider, prompt versioning, observability, artifact workflow, human review, safe AI telemetry, Azure blueprint, Angular architecture, and Docker/DevOps strategy.

## Roadmap

Completed portfolio roadmap:

- Epic 0 to Epic 12: foundation through Azure deployment blueprint.
- Epic 13: repository portfolio polish, diagrams, sample outputs, docs, ADRs, and first-impression UI improvements.

Candidate next epics:

- Authentication and role-based authorization with Microsoft Entra ID.
- Artifact comparison and diff view between versions.
- Architecture review board workflow.
- Prompt evaluation harness and golden-output tests.
- Application Insights dashboards and alert rules.
- PDF export and document package generation.

## Portfolio Value

This repository is designed to show senior Solution Architect capability, not just framework usage. It demonstrates enterprise architecture thinking, responsible AI governance, Azure OpenAI readiness, prompt engineering discipline, API-first backend design, Angular frontend organization, DevOps maturity, observability, secure configuration, and documentation quality suitable for recruiter and interviewer review.
