# ADR-0005: Prompt Template Versioning

## Status

Accepted

## Context

Prompts are architecture assets that affect generated artifacts and governance traceability.

## Decision

Store prompt templates in source control under `prompts/` and include explicit version placeholders.

## Consequences

Generated artifacts can record which prompt version influenced draft output. Prompt changes can be reviewed like code.

## Alternatives Considered

Embedding prompts in code was rejected because it makes prompt review, traceability, and documentation weaker.
