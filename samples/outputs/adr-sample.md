# Mock Output: ADR Candidate

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## ADR-001: Use Azure SQL for Transactional Loan Application Data

## Status

Proposed

## Context

Loan processing requires structured transactional data, status transitions, audit references, and reporting queries. The supplied requirement does not indicate a need for globally distributed writes.

## Decision

Use Azure SQL Database as the primary transactional store for application, applicant, workflow, and audit metadata.

## Consequences

- Supports relational integrity and EF Core.
- Aligns with enterprise reporting and backup patterns.
- Requires schema migration discipline.

## Alternatives Considered

- Azure Cosmos DB: useful for global scale, but not justified from the supplied requirement.
- Blob-only metadata: insufficient for workflow and reporting queries.

## Open Questions

- Are active-active multi-region writes required?
