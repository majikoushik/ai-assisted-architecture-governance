# ADR-0007: Artifact Generation Workflow

## Status

Accepted

## Context

The platform must generate multiple architecture artifact types from submitted requirements while preserving traceability, prompt versioning, provider metadata, and human review readiness.

## Decision

Artifact generation will be handled by an application-layer command that validates project and requirement relationships, resolves a prompt template by artifact type, calls `IArchitectureAiProvider`, stores the generated Markdown, and records provider and prompt metadata.

## Consequences

- Artifact generation remains outside controllers.
- Prompt templates are source-controlled and traceable.
- Mock and Azure OpenAI providers can share the same workflow.
- The handler must keep logging safe and avoid full prompt or requirement logging.

## Alternatives Considered

- Controller-driven generation: rejected because it would mix HTTP and orchestration concerns.
- Provider-specific generation endpoints: rejected because it would leak provider choices into the API surface.
