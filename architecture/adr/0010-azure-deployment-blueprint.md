# ADR-0010: Azure Deployment Blueprint

## Status

Accepted

## Context

The repository should demonstrate Azure deployment readiness without requiring a real deployment or committed secrets.

## Decision

Use Bicep to model Azure Container Apps, Static Web Apps, Azure SQL, Azure OpenAI, Key Vault, Log Analytics, Application Insights, and Container Registry as a blueprint.

## Consequences

- The project communicates Azure architecture direction.
- Deployment remains optional and environment-specific.
- Secrets and production parameters are excluded from source control.
- Cost and security review are required before real deployment.

## Alternatives Considered

- No infrastructure as code: rejected because Azure readiness is a core portfolio objective.
- Terraform: deferred; Bicep aligns directly with Azure-first portfolio positioning.
