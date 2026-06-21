# Architecture Documentation

This folder documents the architecture of the AI-Assisted Architecture Governance platform. The documentation is intentionally written for Solution Architect, engineering manager, and technical interviewer review.

## Architecture Intent

The platform converts synthetic or approved business requirements into AI-assisted architecture draft artifacts. It is designed around governance rather than chat: prompt-controlled generation, artifact versioning, traceability, human review, safe telemetry, and Azure deployment readiness.

## Primary Views

- [High-Level Design](hld.md)
- [Low-Level Design](lld.md)
- [Non-Functional Requirements](nfrs.md)
- [Responsible AI Architecture](responsible-ai-architecture.md)
- [Prompt Engineering Strategy](prompt-engineering-strategy.md)
- [API Governance](api-governance.md)
- [Security Architecture](security-architecture.md)
- [Observability Architecture](observability-architecture.md)
- [Deployment Architecture](deployment-architecture.md)
- [Data Model](data-model.md)

## Diagrams

- [System context](diagrams/system-context.md)
- [Container diagram](diagrams/container-diagram.md)
- [Requirement-to-artifact flow](diagrams/requirement-to-artifact-flow.md)
- [AI provider sequence](diagrams/ai-provider-sequence.md)
- [Prompt template flow](diagrams/prompt-template-flow.md)
- [Human review and versioning flow](diagrams/review-versioning-flow.md)
- [Safe AI telemetry flow](diagrams/safe-ai-telemetry-flow.md)
- [Azure deployment](diagrams/azure-deployment.md)
- [CI/CD pipeline](diagrams/cicd-pipeline.md)

## Architecture Decision Records

ADRs live under [adr](adr/) and capture the major decisions behind architecture style, provider abstraction, prompt versioning, telemetry, Azure deployment, frontend structure, Docker, and DevOps strategy.

## Responsible AI Boundary

Generated outputs are AI-assisted drafts. They require human architect review and must not be represented as production-approved architecture decisions.
