# ADR-0003: Mock Provider First

## Status

Accepted

## Context

The repository must run locally and in CI without Azure OpenAI credentials.

## Decision

Use a deterministic mock AI provider as the default provider.

## Consequences

Developers and reviewers can run demos and tests safely. Mock output must remain realistic enough to support portfolio demonstrations.

## Alternatives Considered

Requiring Azure OpenAI credentials for local development was rejected because it creates setup friction and secret-handling risk.
