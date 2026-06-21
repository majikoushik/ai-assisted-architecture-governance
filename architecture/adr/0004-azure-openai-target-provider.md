# ADR-0004: Azure OpenAI as Target Provider

## Status

Accepted

## Context

The platform should demonstrate Azure-ready AI architecture for enterprise architecture governance.

## Decision

Azure OpenAI is the target production AI provider, implemented through the provider abstraction in a future epic.

## Consequences

Configuration must avoid hardcoded endpoints, keys, tenant IDs, subscription IDs, and deployment names. The design should support Key Vault and Managed Identity.

## Alternatives Considered

Generic public model API integration was deferred because the portfolio goal emphasizes Azure architecture readiness.
