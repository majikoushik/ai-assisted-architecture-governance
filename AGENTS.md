# AGENTS.md

# AI-Assisted Architecture Governance — Agent Operating Guide

This file defines how AI coding agents must work in this repository.

Repository name:

```text
ai-assisted-architecture-governance
```

This project is a portfolio-grade architecture tool intended to demonstrate how a Solution Architect can use AI-assisted engineering to convert business requirements into structured architecture artifacts.

The project should showcase:

- AI-assisted Solution Architecture
- Azure OpenAI integration readiness
- Requirements analysis
- HLD and LLD generation
- ADR generation
- NFR assessment
- API contract review
- Security and risk review
- Architecture governance workflows
- .NET backend engineering
- Angular frontend engineering
- Azure-ready deployment architecture
- Prompt engineering and responsible AI design
- DevOps, observability, testing, and documentation maturity

This is not a simple chatbot project. It should look like an enterprise architecture governance accelerator built by a senior Solution Architect.

---

# 1. Project Vision

Build an AI-assisted architecture governance platform that helps architects and engineering teams transform business requirements into structured, reviewable, and version-controlled architecture artifacts.

The tool should support a simplified but realistic architecture workflow:

1. Capture business requirement or feature description.
2. Analyze requirement for domain, actors, capabilities, risks, and assumptions.
3. Generate architecture recommendations.
4. Generate HLD draft.
5. Generate LLD draft.
6. Generate ADR suggestions.
7. Generate NFR checklist.
8. Review API contracts.
9. Review security and compliance considerations.
10. Export generated artifacts as Markdown.
11. Maintain traceability between input requirement and generated outputs.
12. Provide Azure-ready deployment and observability design.

The repository should become a strong GitHub portfolio project for a Solution Architect specializing in .NET, Azure, microservices, enterprise modernization, and AI-enabled SDLC.

---

# 2. Target Audience

This repository should be understandable and impressive to:

- Solution Architect recruiters
- Engineering managers
- Azure architects
- Enterprise architects
- Technical architects
- Senior .NET interviewers
- AI engineering reviewers
- Platform engineering teams
- Architecture governance teams

The repository must clearly demonstrate architecture thinking, AI governance, enterprise engineering practices, and practical implementation skills.

---

# 3. Core Product Concept

The application should allow a user to enter a business requirement such as:

```text
Build a loan processing system with customer onboarding, document upload, approval workflow, notifications, audit logging, and reporting.
```

The platform should generate structured outputs such as:

- Requirement analysis
- Capability map
- Suggested service boundaries
- System context
- Container-level architecture
- HLD draft
- LLD draft
- ADR candidates
- NFR checklist
- Security concerns
- Observability concerns
- API contract suggestions
- Deployment considerations
- Risks and assumptions
- Open questions for stakeholders

The generated content must be clearly marked as AI-assisted draft output requiring human architect review.

---

# 4. Architecture Principles

Every implementation must follow these principles.

## 4.1 Architecture Governance First

The project should not behave like a generic text generator.

It must be structured around architecture governance workflows:

- Requirement intake
- Architecture analysis
- Artifact generation
- Risk identification
- Decision capture
- Review readiness
- Export and traceability

## 4.2 Human-in-the-Loop AI

The application must never imply that AI output is automatically final or authoritative.

All generated architecture content must be treated as:

```text
AI-assisted draft requiring human architect review.
```

The UI and documentation should reinforce this.

## 4.3 Enterprise-Grade Simplicity

Keep the MVP simple enough to run locally, but design it with clear enterprise architecture thinking.

Good architecture means:

- Clear boundaries
- Safe AI usage
- Reviewable outputs
- Maintainable code
- Testable services
- Secure configuration
- Observability readiness
- Azure deployment readiness

## 4.4 Prompt Engineering as First-Class Architecture

Prompt templates are part of the product.

They must be:

- Versioned
- Stored in the repository
- Documented
- Reviewed
- Testable where practical
- Designed to reduce hallucination
- Designed to produce structured outputs
- Designed to ask for assumptions and risks

## 4.5 Azure-Ready AI Design

Local development may use mock AI providers.

Production direction should align with Azure:

- Azure OpenAI
- Azure AI Foundry readiness
- Azure App Configuration optional
- Azure Key Vault
- Azure Container Apps
- Azure SQL Database
- Azure Blob Storage
- Azure Application Insights
- Azure Log Analytics
- Azure API Management optional

## 4.6 API-First Design

Backend APIs must be designed as enterprise APIs.

Each API should include:

- Clear route naming
- Request and response DTOs
- Validation rules
- Proper HTTP status codes
- Problem Details error responses
- Swagger/OpenAPI documentation
- Correlation ID propagation
- Versioning readiness
- Consistent response patterns

## 4.7 Secure and Responsible AI by Default

Never commit secrets.

Never hardcode API keys.

Never log prompts containing sensitive information.

Never log AI provider secrets or full confidential inputs.

Use environment-based configuration.

Use safe sample data only.

Document responsible AI boundaries.

---

# 5. Recommended Technology Stack

## 5.1 Backend

Use:

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server for local development and Azure SQL alignment
- FluentValidation or equivalent validation approach
- Swagger/OpenAPI
- Serilog or structured logging equivalent
- Health checks
- Problem Details
- Docker support
- Clean Architecture or vertical-slice architecture where appropriate

Backend should support provider abstraction for AI:

- Mock AI provider for local development
- Azure OpenAI provider for production readiness
- Optional future provider abstraction for additional models

## 5.2 Frontend

Use Angular.

Frontend expectations:

- Angular with strict TypeScript
- Feature-based folder structure
- Reactive forms
- Typed API models
- Centralized API service layer
- HTTP interceptors for correlation ID and error handling
- Professional enterprise dashboard UI
- Clear artifact preview screens
- Markdown rendering for generated architecture artifacts
- Export/download support where practical
- Clear loading and error states

## 5.3 Database

Use SQL Server locally unless a future epic decides otherwise.

Database should store:

- Architecture projects
- Requirement submissions
- Generated artifacts
- Artifact versions
- Prompt template metadata
- AI interaction metadata
- Review status
- Audit events

Do not store real confidential business requirements in seed data.

## 5.4 AI Provider

The MVP must support a mock AI provider first.

Azure OpenAI should be added through a clean abstraction.

Required AI provider capabilities:

- Requirement analysis
- HLD generation
- LLD generation
- ADR generation
- NFR checklist generation
- Security review generation
- API contract review generation
- Risk and assumption extraction

Do not require real Azure OpenAI credentials for local development.

## 5.5 DevOps

Use GitHub Actions for CI.

Minimum pipeline expectations:

- Restore backend dependencies
- Build backend
- Run backend tests
- Install frontend dependencies
- Build Angular frontend
- Run frontend tests where practical
- Validate Docker builds where practical

Future deployment direction:

- Build Docker images
- Push to Azure Container Registry
- Deploy backend to Azure Container Apps
- Deploy frontend to Azure Static Web Apps
- Use Azure Key Vault for AI provider secrets
- Use Application Insights for telemetry

---

# 6. Recommended Repository Structure

Use this structure unless there is a strong reason to change it.

```text
ai-assisted-architecture-governance/
│
├── README.md
├── AGENTS.md
├── docker-compose.yml
├── .gitignore
├── .editorconfig
│
├── src/
│   ├── api/
│   │   └── ArchitectureGovernance.Api/
│   │
│   ├── application/
│   │   └── ArchitectureGovernance.Application/
│   │
│   ├── domain/
│   │   └── ArchitectureGovernance.Domain/
│   │
│   ├── infrastructure/
│   │   └── ArchitectureGovernance.Infrastructure/
│   │
│   ├── ai/
│   │   ├── ArchitectureGovernance.AI.Abstractions/
│   │   ├── ArchitectureGovernance.AI.Mock/
│   │   └── ArchitectureGovernance.AI.AzureOpenAI/
│   │
│   ├── building-blocks/
│   │   ├── SharedKernel/
│   │   ├── Observability/
│   │   ├── Security/
│   │   └── Exporting/
│   │
│   └── web/
│       └── architecture-governance-portal/
│
├── tests/
│   ├── ArchitectureGovernance.Api.Tests/
│   ├── ArchitectureGovernance.Application.Tests/
│   ├── ArchitectureGovernance.Domain.Tests/
│   ├── ArchitectureGovernance.Infrastructure.Tests/
│   └── ArchitectureGovernance.AI.Tests/
│
├── prompts/
│   ├── README.md
│   ├── requirement-analysis.md
│   ├── hld-generation.md
│   ├── lld-generation.md
│   ├── adr-generation.md
│   ├── nfr-review.md
│   ├── api-contract-review.md
│   ├── security-review.md
│   └── risk-assumption-review.md
│
├── samples/
│   ├── banking-loan-platform-requirement.md
│   ├── healthcare-care-coordination-requirement.md
│   └── ecommerce-order-management-requirement.md
│
├── architecture/
│   ├── README.md
│   ├── hld.md
│   ├── lld.md
│   ├── nfrs.md
│   ├── responsible-ai-architecture.md
│   ├── prompt-engineering-strategy.md
│   ├── api-governance.md
│   ├── security-architecture.md
│   ├── observability-architecture.md
│   ├── deployment-architecture.md
│   ├── data-model.md
│   ├── diagrams/
│   │   ├── system-context.md
│   │   ├── container-diagram.md
│   │   ├── requirement-to-artifact-flow.md
│   │   ├── ai-provider-sequence.md
│   │   └── azure-deployment.md
│   └── adr/
│       ├── 0001-architecture-style.md
│       ├── 0002-ai-provider-abstraction.md
│       ├── 0003-mock-provider-first.md
│       ├── 0004-azure-openai-target-provider.md
│       ├── 0005-prompt-template-versioning.md
│       └── 0006-observability-strategy.md
│
├── docs/
│   ├── setup.md
│   ├── local-development.md
│   ├── api-contracts.md
│   ├── testing-strategy.md
│   ├── prompt-catalog.md
│   ├── responsible-ai-guidelines.md
│   ├── deployment.md
│   ├── azure-deployment-guide.md
│   ├── devops-guide.md
│   ├── operational-runbook.md
│   ├── roadmap.md
│   └── screenshots/
│
├── infra/
│   ├── bicep/
│   └── scripts/
│
└── .github/
    └── workflows/
        ├── ci.yml
        └── azure-deploy-template.yml
```

The agent may simplify during the earliest foundation, but it must preserve this long-term direction.

---

# 7. Core Domain Model

Use clear domain language.

Suggested domain entities:

## ArchitectureProject

Represents a governance workspace for one solution or initiative.

Fields may include:

- Project ID
- Project name
- Business domain
- Description
- Owner
- Status
- Created timestamp
- Updated timestamp

## RequirementSubmission

Represents the business or technical requirement submitted for AI-assisted analysis.

Fields may include:

- Requirement submission ID
- Project ID
- Title
- Requirement text
- Domain context
- Submitted by
- Created timestamp
- Status

## GeneratedArtifact

Represents an AI-assisted architecture artifact.

Fields may include:

- Artifact ID
- Project ID
- Requirement submission ID
- Artifact type
- Title
- Markdown content
- Version
- Status
- Generated by provider
- Prompt template version
- Created timestamp
- Reviewed timestamp

Artifact types:

- RequirementAnalysis
- HighLevelDesign
- LowLevelDesign
- ArchitectureDecisionRecord
- NonFunctionalRequirementReview
- ApiContractReview
- SecurityReview
- RiskAndAssumptionReview

## PromptTemplate

Represents a versioned prompt template.

Fields may include:

- Prompt template ID
- Name
- Artifact type
- Version
- Template content
- Status
- Created timestamp

## AIInteractionLog

Represents safe metadata about AI interactions.

Do not store secrets.

Do not store sensitive full prompts unless explicitly safe for demo use.

Fields may include:

- Interaction ID
- Project ID
- Requirement submission ID
- Provider name
- Model name or deployment name
- Prompt template version
- Request timestamp
- Response timestamp
- Status
- Token count estimate if available
- Error summary if failed
- Correlation ID

## ReviewRecord

Represents human review of generated artifacts.

Fields may include:

- Review ID
- Artifact ID
- Reviewer
- Review status
- Comments
- Reviewed timestamp

Review statuses:

- Draft
- NeedsReview
- Reviewed
- Approved
- Rejected

---

# 8. MVP Scope

The MVP should include these capabilities.

## 8.1 Architecture Project Creation

Users should be able to create a new architecture project.

Fields:

- Project name
- Business domain
- Description
- Owner

## 8.2 Requirement Submission

Users should submit a requirement for analysis.

Fields:

- Project ID
- Requirement title
- Requirement text
- Domain context
- Expected artifact types

## 8.3 Requirement Analysis

System should analyze requirement and produce:

- Summary
- Business capabilities
- Actors
- Key workflows
- Candidate services
- Assumptions
- Risks
- Open questions

## 8.4 HLD Generation

System should generate a High-Level Design draft with:

- Business context
- System context
- Key components
- Integration points
- Data flow
- Security considerations
- Observability considerations
- Deployment considerations

## 8.5 LLD Generation

System should generate a Low-Level Design draft with:

- Component responsibilities
- API boundaries
- Data model
- Main workflows
- Error handling
- Validation rules
- Logging and telemetry needs

## 8.6 ADR Generation

System should suggest ADRs with:

- Title
- Context
- Decision
- Consequences
- Alternatives considered

## 8.7 NFR Review

System should generate an NFR checklist covering:

- Performance
- Scalability
- Availability
- Reliability
- Security
- Observability
- Maintainability
- Cost
- Compliance readiness

## 8.8 Security Review

System should generate security considerations covering:

- Authentication
- Authorization
- Secret management
- Sensitive data handling
- Logging safety
- API security
- Threat modeling readiness

## 8.9 API Contract Review

System should review or suggest API design considerations.

Output should include:

- Endpoint naming
- Request/response DTO recommendations
- Error response standards
- Versioning readiness
- Idempotency concerns
- Pagination where relevant
- OpenAPI documentation readiness

## 8.10 Artifact Export

Users should be able to view and export generated artifacts as Markdown.

PDF export may be future scope unless explicitly requested.

---

# 9. AI and Prompt Engineering Standards

## 9.1 Prompt Templates

Prompt templates must be stored under:

```text
prompts/
```

Each prompt template should define:

- Purpose
- Expected input
- Expected output structure
- Constraints
- Safety reminders
- Architecture quality expectations
- Markdown output format

## 9.2 Prompt Template Versioning

Every generated artifact must store the prompt template name and version used.

Prompt template versions can be simple strings such as:

```text
v1.0.0
```

## 9.3 Structured Outputs

AI outputs should prefer structured Markdown.

Where practical, instruct the model to produce:

- Headings
- Tables
- Bullet points
- Risks
- Assumptions
- Open questions
- Review notes

## 9.4 Hallucination Reduction

Prompts should instruct the AI to:

- Avoid inventing unavailable facts
- Mark assumptions clearly
- Ask open questions
- Distinguish facts from recommendations
- Avoid claiming production certainty without evidence

## 9.5 Responsible AI Notice

Each generated artifact should include or be accompanied by a notice:

```text
This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
```

## 9.6 Mock Provider Requirement

The system must work locally without Azure OpenAI credentials.

Use a mock provider that returns deterministic but realistic sample outputs for demos and tests.

## 9.7 Azure OpenAI Provider

Azure OpenAI integration should be implemented through an abstraction.

Do not hardcode:

- API keys
- Endpoint URLs
- Deployment names
- Model names
- Tenant IDs
- Subscription IDs

Use environment variables and Key Vault readiness.

---

# 10. Backend Coding Standards

## 10.1 Controllers

Controllers must be thin.

Controllers should:

- Accept requests
- Validate model state if applicable
- Call application services or handlers
- Return appropriate HTTP responses

Controllers should not:

- Contain AI prompt construction logic
- Contain architecture generation logic
- Directly perform database logic
- Hide exception handling locally

## 10.2 Application Layer

Application services or handlers should:

- Coordinate use cases
- Apply validation
- Call domain services
- Call AI provider abstraction
- Store generated artifacts
- Record AI interaction metadata
- Return clear result objects

## 10.3 Domain Layer

Domain should contain meaningful behavior around:

- Artifact type rules
- Review status transitions
- Project lifecycle status
- Prompt template version metadata
- Generated artifact lifecycle

## 10.4 Infrastructure Layer

Infrastructure should contain:

- EF Core DbContext
- Database configurations
- Azure OpenAI adapter
- Mock AI adapter
- Markdown export adapter
- Logging integrations
- Future Azure storage integrations

## 10.5 Error Handling

Use consistent error handling.

Expected patterns:

- Global exception middleware
- Problem Details responses
- Validation error response format
- Correlation ID in error response
- No stack traces exposed to frontend
- Detailed server logs only

## 10.6 Validation

Validate all incoming requests.

Validation must cover:

- Required fields
- Length limits
- Supported artifact types
- Supported project status
- Supported review status
- Invalid project references
- Empty or unsafe requirement submissions

## 10.7 Configuration

Use:

- `appsettings.json`
- `appsettings.Development.json`
- environment variables
- Key Vault readiness for production

Never commit real secrets.

---

# 11. Angular Coding Standards

## 11.1 Structure

Use feature-based Angular structure.

Preferred structure:

```text
architecture-governance-portal/
│
├── src/
│   ├── app/
│   │   ├── core/
│   │   │   ├── interceptors/
│   │   │   ├── services/
│   │   │   └── models/
│   │   │
│   │   ├── shared/
│   │   │   ├── components/
│   │   │   ├── pipes/
│   │   │   └── validators/
│   │   │
│   │   ├── features/
│   │   │   ├── dashboard/
│   │   │   ├── projects/
│   │   │   ├── requirements/
│   │   │   ├── artifact-generation/
│   │   │   ├── artifact-viewer/
│   │   │   ├── prompt-catalog/
│   │   │   └── reviews/
│   │   │
│   │   └── layout/
│   │
│   └── environments/
```

## 11.2 UI Expectations

The UI should look like a professional architecture governance dashboard.

Expected qualities:

- Clean navigation
- Dashboard cards
- Project workspace
- Requirement submission form
- Artifact generation workflow
- Markdown artifact preview
- Review status badges
- Prompt catalog screen
- Responsive layout
- Loading states
- Empty states
- Error states

## 11.3 Forms

Use reactive forms.

Forms should include:

- Required validation
- Length validation
- Helpful placeholder text
- User-friendly error messages
- Disabled submit button when invalid
- Loading state during API call

## 11.4 API Integration

Use typed services for API calls.

Do not call HTTP APIs directly from components.

Use environment configuration for API base URL.

Use interceptors for:

- Correlation ID
- Error handling
- Future auth token handling

## 11.5 Markdown Rendering

Generated artifacts are likely Markdown.

Markdown rendering must be safe.

Do not render untrusted HTML unless sanitized.

Prefer safe Markdown rendering configuration.

---

# 12. API Design Standards

Use RESTful conventions.

Suggested endpoints:

```text
POST   /api/v1/projects
GET    /api/v1/projects
GET    /api/v1/projects/{projectId}

POST   /api/v1/requirements
GET    /api/v1/requirements/{requirementId}
GET    /api/v1/projects/{projectId}/requirements

POST   /api/v1/artifacts/generate
GET    /api/v1/artifacts/{artifactId}
GET    /api/v1/projects/{projectId}/artifacts
GET    /api/v1/requirements/{requirementId}/artifacts
GET    /api/v1/artifacts/{artifactId}/export/markdown

GET    /api/v1/prompts
GET    /api/v1/prompts/{promptTemplateId}

POST   /api/v1/reviews
GET    /api/v1/artifacts/{artifactId}/reviews

GET    /api/v1/ai-interactions
GET    /api/v1/ai-interactions/{interactionId}
```

Use consistent response models.

Example success response:

```json
{
  "data": {},
  "correlationId": "string",
  "timestamp": "2026-01-01T10:00:00Z"
}
```

Example error response should follow Problem Details style:

```json
{
  "type": "https://example.com/problems/validation-error",
  "title": "Validation failed",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "correlationId": "string",
  "errors": {}
}
```

---

# 13. Observability Standards

Every backend service should include:

- Correlation ID middleware
- Structured logging
- Request logging
- Error logging
- Health check endpoint
- Application Insights readiness
- AI provider call telemetry metadata
- Safe prompt logging policy
- Safe response logging policy

Minimum log fields:

- Timestamp
- Level
- Service name
- Correlation ID
- Event name
- Project ID where relevant
- Requirement ID where relevant
- Artifact ID where relevant
- Provider name where relevant
- Message
- Exception details where applicable

Do not log:

- API keys
- Secrets
- Full confidential requirements
- Full AI prompts if sensitive
- Full AI responses if sensitive
- Connection strings
- Tokens

---

# 14. Security Standards

Security must be considered in every epic.

Minimum expectations:

- Input validation
- Output safety
- Secure configuration
- No secrets in repository
- No real confidential business requirements
- No sensitive data in logs
- CORS configured intentionally
- Future authentication and authorization hooks
- Secure headers where practical
- OWASP awareness
- Responsible AI considerations

Future authentication direction:

- Azure Entra ID or Entra External ID
- JWT-based API authorization
- Role-based access control

Possible roles:

- Architect
- Reviewer
- Admin
- Viewer

For MVP, authentication may be simulated or deferred, but architecture notes must explain future direction.

---

# 15. Responsible AI Governance Standards

The repository must include responsible AI documentation.

Document:

- AI-assisted output limitations
- Human review requirement
- Prompt design strategy
- Hallucination mitigation
- Data privacy considerations
- Secret handling
- Sensitive input handling
- Provider abstraction
- Logging policy
- Model configuration strategy
- Evaluation strategy readiness

Generated artifacts must clearly show:

- Assumptions
- Risks
- Open questions
- Human review notice

Do not claim AI output is production-approved.

---

# 16. Azure Deployment Direction

All deployment work should align with this target architecture.

## 16.1 Frontend

Preferred Azure options:

- Azure Static Web Apps
- Azure Storage Static Website with CDN readiness

## 16.2 Backend APIs

Preferred:

- Azure Container Apps
- Azure Container Registry

## 16.3 Database

Preferred:

- Azure SQL Database

## 16.4 AI Provider

Preferred:

- Azure OpenAI
- Azure AI Foundry readiness

## 16.5 Secrets

Preferred:

- Azure Key Vault
- Managed Identity

## 16.6 Monitoring

Preferred:

- Azure Application Insights
- Azure Log Analytics
- Azure Monitor

## 16.7 Storage

Optional future:

- Azure Blob Storage for exported artifacts
- Azure Blob Storage for generated document archive

## 16.8 Infrastructure as Code

Use Bicep as preferred Azure IaC unless a future epic selects Terraform.

Infrastructure files should be placed under:

```text
infra/bicep/
```

---

# 17. Documentation Requirements

Documentation is part of the product.

Every major epic must update documentation.

Minimum documentation files:

## README.md

Should include:

- Project overview
- Architecture summary
- Tech stack
- Features
- How to run locally
- How to run tests
- API documentation link
- Responsible AI note
- Azure deployment direction
- Screenshots when UI exists

## architecture/hld.md

Should include:

- Business context
- System context
- Major components
- AI provider abstraction
- Integration points
- Key quality attributes

## architecture/lld.md

Should include:

- Service internals
- Main classes/modules
- API flow
- Database interaction
- Prompt generation flow
- Artifact generation flow
- Error handling
- Event handling

## architecture/responsible-ai-architecture.md

Should include:

- Human review model
- AI output limitations
- Sensitive data guidance
- Prompt safety
- Logging safety
- Provider abstraction
- Governance workflow

## architecture/prompt-engineering-strategy.md

Should include:

- Prompt catalog
- Prompt versioning
- Prompt structure
- Output quality rules
- Hallucination reduction strategy
- Evaluation readiness

## architecture/nfrs.md

Should include:

- Scalability
- Availability
- Performance
- Security
- Observability
- Maintainability
- Reliability
- Cost awareness
- AI provider latency
- AI provider failure handling

## architecture/adr/

Every major architecture choice should have an ADR.

ADR format:

```markdown
# ADR-000X: Decision Title

## Status

Accepted

## Context

Why this decision is needed.

## Decision

What decision was made.

## Consequences

Positive and negative consequences.

## Alternatives Considered

Other options considered and why they were not selected.
```

---

# 18. Testing Standards

Testing must be included from the beginning.

## Backend Testing

Expected test types:

- Unit tests
- Application service tests
- Domain rule tests
- API integration tests where practical
- Validation tests
- AI provider abstraction tests
- Mock provider tests
- Prompt template loading tests
- Artifact generation tests

Important scenarios:

- Project creation
- Requirement submission
- Invalid requirement submission
- Artifact generation using mock provider
- Unsupported artifact type
- Prompt template not found
- AI provider failure
- Export artifact as Markdown
- Review status transition
- AI interaction metadata created
- Correlation ID propagation

## Frontend Testing

Expected test types:

- Component tests where practical
- Service tests
- Form validation tests
- Routing smoke tests where practical
- Markdown viewer smoke test where practical

## Test Data

Use synthetic data only.

No real confidential business information.

---

# 19. CI/CD Standards

GitHub Actions should be added early.

Minimum CI workflow:

```text
on:
  pull_request
  push to main
```

Pipeline should:

- Restore backend dependencies
- Build backend
- Run backend tests
- Install frontend dependencies
- Build Angular frontend
- Run frontend tests where practical

Future CD workflow should:

- Build Docker images
- Push images to Azure Container Registry
- Deploy backend to Azure Container Apps
- Deploy frontend to Azure Static Web Apps
- Use GitHub secrets
- Use Azure Key Vault
- Avoid hardcoded credentials

---

# 20. Agent Workflow Rules

When receiving an epic-based prompt, the agent must follow this workflow.

## Step 1: Understand the Epic

Read:

- Existing code
- README
- AGENTS.md
- Relevant architecture docs
- Existing tests
- Existing workflows
- Existing prompt templates

Do not assume the repository is empty.

## Step 2: Create an Implementation Plan

Before coding, identify:

- Files to create
- Files to modify
- Architecture impact
- Prompt engineering impact
- Responsible AI impact
- Testing impact
- Documentation impact

## Step 3: Implement Incrementally

Make small, coherent changes.

Avoid massive unstructured changes.

Keep code buildable.

Do not leave half-implemented features.

## Step 4: Add or Update Tests

Every meaningful feature must include tests.

If tests cannot be added, explain why in final summary.

## Step 5: Update Documentation

Update relevant docs after implementation.

At minimum update:

- README.md when user-facing behavior changes
- architecture docs when architecture changes
- docs/roadmap.md after completing an epic
- prompt documentation when prompts are added or changed

## Step 6: Validate

Before finishing, run or provide commands to run:

- Backend build
- Backend tests
- Frontend build
- Frontend tests
- Docker Compose validation where applicable

## Step 7: Summarize

Final response should include:

- What was implemented
- Key files changed
- How to run
- How to test
- Architecture notes
- Responsible AI notes
- Next recommended epic

---

# 21. Agent Response Format

For every implementation epic, the agent should respond using this format:

```markdown
## Completed

Summary of what was implemented.

## Architecture Notes

Important architecture decisions or changes.

## Responsible AI Notes

AI safety, prompt, privacy, or review considerations.

## Files Changed

List of important files added or modified.

## How to Run

Commands to run the application.

## How to Test

Commands to run tests.

## Documentation Updated

Docs that were created or updated.

## Suggested Next Epic

Recommended next step.
```

---

# 22. MVP Epic Roadmap

The user may give short epic prompts. The agent must infer technical tasks from this roadmap.

## Epic 0: Repository Foundation

Goal:

Create the initial enterprise-grade repository foundation.

Expected output:

- Solution structure
- Backend skeleton
- Angular skeleton
- Prompt catalog skeleton
- Docker Compose skeleton
- README
- AGENTS.md
- Architecture docs
- Responsible AI docs
- ADRs
- Initial CI workflow

## Epic 1: Architecture Project Workspace

Goal:

Create project workspace management.

Expected output:

- Project entity
- Project API
- Project list UI
- Project creation UI
- Project details UI
- Tests
- Documentation update

## Epic 2: Requirement Submission

Goal:

Allow users to submit business/technical requirements.

Expected output:

- Requirement entity
- Requirement API
- Requirement submission UI
- Validation
- Safe sample requirements
- Tests
- Documentation update

## Epic 3: Prompt Catalog and Mock AI Provider

Goal:

Create prompt templates and deterministic mock AI provider.

Expected output:

- Prompt template files
- Prompt template loader
- AI provider abstraction
- Mock provider implementation
- Prompt catalog UI
- Tests
- Responsible AI documentation

## Epic 4: Requirement Analysis Artifact

Goal:

Generate requirement analysis artifact.

Expected output:

- Artifact generation API
- Requirement analysis prompt
- Generated artifact entity
- Markdown artifact viewer
- Export as Markdown
- Tests
- Documentation update

## Epic 5: HLD Generation

Goal:

Generate High-Level Design artifact.

Expected output:

- HLD prompt
- HLD generation flow
- HLD artifact viewer
- HLD export
- Tests
- Architecture documentation update

## Epic 6: LLD and ADR Generation

Goal:

Generate Low-Level Design and ADR artifacts.

Expected output:

- LLD prompt
- ADR prompt
- Artifact generation options
- Versioned artifacts
- Tests
- Documentation update

## Epic 7: NFR, Security, and API Review

Goal:

Generate governance review artifacts.

Expected output:

- NFR review prompt
- Security review prompt
- API contract review prompt
- Risk and assumption review
- Tests
- Responsible AI documentation update

## Epic 8: Review Workflow and Artifact Versioning

Goal:

Add human review workflow.

Expected output:

- Review status model
- Artifact versioning
- Review comments
- Review UI
- Tests
- Documentation update

## Epic 9: Azure OpenAI Provider Readiness

Goal:

Add Azure OpenAI provider through abstraction.

Expected output:

- Azure OpenAI adapter
- Configuration via environment variables
- Key Vault readiness docs
- No hardcoded secrets
- Mock provider remains default locally
- Tests
- Documentation update

## Epic 10: Observability and Production Readiness

Goal:

Add production-readiness patterns.

Expected output:

- Correlation ID
- Structured logging
- Health checks
- Error handling middleware
- Application Insights readiness
- Safe AI telemetry policy
- Operational runbook

## Epic 11: DevOps and Docker

Goal:

Make project easy to build and run.

Expected output:

- Dockerfiles
- Docker Compose
- GitHub Actions CI
- Build validation
- Test validation
- Developer setup documentation

## Epic 12: Azure Deployment Blueprint

Goal:

Prepare Azure deployment architecture.

Expected output:

- Bicep templates
- Azure resource plan
- Deployment documentation
- Key Vault pattern
- Azure OpenAI configuration pattern
- Application Insights pattern
- Azure Container Apps deployment direction

## Epic 13: Portfolio Polish

Goal:

Make repository impressive for GitHub visitors.

Expected output:

- Professional README
- Architecture diagrams
- Screenshots
- Roadmap
- Sample prompts
- Sample outputs
- Responsible AI section
- Final documentation review

---

# 23. Default Behavior for Short User Prompts

The user may provide prompts like:

```text
Implement Epic 1.
```

or:

```text
Add HLD generation.
```

or:

```text
Make this Azure OpenAI ready.
```

When prompts are short, the agent must not ask for unnecessary technical clarification.

The agent should use this AGENTS.md file as the source of technical direction.

The agent should proceed with best-practice implementation based on:

- Current repository state
- Project roadmap
- Enterprise architecture expectations
- AI governance expectations
- Azure deployment direction
- Existing code and documentation

Ask clarification only when a decision would significantly change architecture, cost, security, or user-facing behavior.

---

# 24. Quality Bar

A feature is not complete unless:

- Code builds successfully
- Relevant tests are added or updated
- API behavior is documented
- Frontend behavior is usable where applicable
- Prompt templates are documented where applicable
- Responsible AI concerns are addressed where applicable
- Architecture docs are updated if needed
- README or setup docs are updated if commands changed
- No secrets are committed
- No real confidential data is used
- Error handling is considered
- Logging is considered
- Validation is implemented
- Azure-readiness is preserved

---

# 25. Definition of Done

For every epic, the Definition of Done is:

```text
1. Feature implemented end-to-end or clearly scoped for the epic.
2. Backend code follows clean architecture or clear service boundaries.
3. Angular code follows feature-based structure.
4. AI provider usage goes through abstraction.
5. Mock provider works locally without Azure credentials.
6. Prompt templates are versioned and documented where applicable.
7. Generated artifacts include human review notice where applicable.
8. API contracts are documented through Swagger/OpenAPI.
9. Validation and error handling are implemented.
10. Tests are added or updated.
11. Logging and correlation ID impact are considered.
12. Responsible AI concerns are addressed.
13. Documentation is updated.
14. Local run instructions are clear.
15. Azure-readiness is preserved.
16. No secrets, credentials, or real confidential data are committed.
17. Final response explains what changed and how to validate it.
```

---

# 26. Non-Negotiable Rules

The agent must not:

- Build a generic chatbot
- Build a toy-style project
- Put all backend logic in controllers
- Put all frontend logic in components
- Skip validation
- Ignore tests
- Ignore documentation
- Hardcode secrets
- Require Azure OpenAI for local development
- Log API keys
- Log confidential prompts
- Use real confidential business data
- Claim AI output is production-approved
- Create unclear folder structures
- Mix unrelated responsibilities
- Add unnecessary complexity without explanation
- Break existing working functionality
- Remove architecture documents without replacement
- Introduce Azure resources without documenting cost and purpose

---

# 27. Preferred Naming Conventions

## Backend

Use clear names:

- `ArchitectureProject`
- `RequirementSubmission`
- `GeneratedArtifact`
- `ArtifactType`
- `PromptTemplate`
- `PromptTemplateVersion`
- `AIProvider`
- `MockAIProvider`
- `AzureOpenAIProvider`
- `AIInteractionLog`
- `ReviewRecord`
- `ReviewStatus`

Avoid vague names:

- `Data`
- `Info`
- `Helper`
- `Manager`
- `Processor`
- `Common`
- `Utility`

## Frontend

Use clear feature naming:

- `project-workspace`
- `requirement-submission`
- `artifact-generation`
- `artifact-viewer`
- `prompt-catalog`
- `review-workflow`
- `ai-interaction-history`
- `dashboard`

---

# 28. Sample Prompt Template Requirements

Every prompt template should include:

- Role instruction
- Task instruction
- Input placeholders
- Output format
- Constraints
- Human review notice
- Assumptions section
- Risks section
- Open questions section

Example prompt structure:

```markdown
# Prompt: HLD Generation

Version: v1.0.0

## Purpose

Generate a High-Level Design draft from a business requirement.

## Input

- Requirement title
- Requirement text
- Business domain
- Known constraints

## Instructions

You are assisting a Solution Architect. Generate a structured HLD draft.

## Output Format

Return Markdown with the following sections:

1. Executive Summary
2. Business Context
3. System Context
4. Major Components
5. Integration Points
6. Data Flow
7. Security Considerations
8. Observability Considerations
9. Deployment Considerations
10. Assumptions
11. Risks
12. Open Questions
13. Human Review Notice

## Constraints

- Do not invent facts.
- Mark assumptions clearly.
- Do not claim production approval.
- Keep recommendations practical.
```

---

# 29. Portfolio Presentation Expectations

This repository should visually communicate seniority.

README should eventually include:

- Project banner or title
- Architecture diagram
- Feature list
- Tech stack badges
- Local setup
- Screenshots
- Sample requirement input
- Sample AI-generated artifact output
- Prompt catalog
- Responsible AI notes
- Azure deployment architecture
- CI/CD status badge
- Roadmap
- Architecture decision records
- Observability notes
- Security notes

The repository should make the visitor think:

```text
This person understands enterprise architecture, AI-assisted SDLC, Azure, and governance-ready engineering.
```

---

# 30. Final Guidance for AI Agents

When implementing, always optimize for:

- Clear architecture
- Professional repository presentation
- Maintainability
- Enterprise realism
- Responsible AI design
- Prompt quality
- Azure readiness
- Strong documentation
- Clean code
- Testability
- Recruiter/interviewer impact

This project should become a flagship GitHub portfolio project for a Solution Architect specializing in .NET, Azure, AI-enabled enterprise architecture, and architecture governance.
