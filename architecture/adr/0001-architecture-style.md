# ADR-0001: Layered Architecture Style

## Status

Accepted

## Context

The platform needs clear boundaries between API concerns, use case orchestration, domain concepts, infrastructure adapters, and AI provider implementations.

## Decision

Use a layered architecture with API, Application, Domain, Infrastructure, AI provider projects, and reusable building blocks.

## Consequences

This keeps the foundation understandable and testable. It adds some project overhead, but the boundaries support future enterprise features.

## Alternatives Considered

Minimal single-project API was rejected because it would hide important architecture responsibilities.
