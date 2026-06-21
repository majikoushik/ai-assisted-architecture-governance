# ADR-0002: AI Provider Abstraction

## Status

Accepted

## Context

The platform must support local development without real AI credentials while remaining ready for Azure OpenAI.

## Decision

Define an AI provider abstraction and keep provider-specific details in adapter projects.

## Consequences

The application can use mock and Azure providers interchangeably through configuration. Provider differences must be normalized through clear request and response contracts.

## Alternatives Considered

Direct Azure OpenAI calls from application services were rejected because they would couple governance workflows to one provider.
