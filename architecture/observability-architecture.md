# Observability Architecture

## Goals

Observability should make the platform supportable without exposing sensitive AI inputs. The design prioritizes request correlation, health checks, operational metadata, and Azure monitoring readiness.

## Implemented Patterns

- Correlation ID middleware.
- Request logging middleware.
- Global exception handling.
- Liveness and readiness health endpoints.
- AI telemetry metadata logs from providers.
- Application Insights registration readiness.

## Safe AI Telemetry

AI provider telemetry can include:

- Provider name.
- Artifact type.
- Prompt template name and version.
- Duration.
- Status.
- Correlation ID.

Telemetry must not include API keys, secrets, full prompts, full requirement text, or full AI responses by default.

## Azure Target

- Application Insights for application telemetry.
- Log Analytics Workspace for centralized logs.
- Azure Monitor alerts for availability, failures, and cost signals.

## Operational Use

The operational runbook in `docs/operational-runbook.md` documents health checks, log review, AI provider failure triage, and recovery actions.
