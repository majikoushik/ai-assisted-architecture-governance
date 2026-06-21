# ADR-0006: Observability Strategy

## Status

Accepted

## Context

Architecture governance workflows need traceability across API requests, AI provider calls, generated artifacts, and human reviews.

## Decision

Start with correlation ID middleware, health checks, and documentation for structured logging and Application Insights readiness.

## Consequences

Future epics can attach logs, metrics, and AI interaction metadata to the same correlation context. Sensitive prompts and secrets must be excluded from telemetry.

## Alternatives Considered

Adding full observability tooling in Epic 0 was deferred to keep the foundation focused.
