# ADR-0009: Safe AI Telemetry

## Status

Accepted

## Context

AI provider calls need operational telemetry for supportability, but architecture requirements and prompts may contain sensitive information.

## Decision

The platform will log AI operational metadata such as provider, artifact type, prompt template version, duration, status, and correlation ID. It will avoid logging full prompts, full requirement text, full responses, secrets, tokens, and connection strings by default.

## Consequences

- Operations can troubleshoot provider calls without exposing sensitive content.
- Production use still requires data classification, privacy review, and retention policy.
- Developers must preserve the safe logging boundary when adding new providers or telemetry.

## Alternatives Considered

- Logging full prompts and responses: rejected due to privacy and confidentiality risk.
- Logging no AI metadata: rejected because it reduces supportability and cost analysis.
