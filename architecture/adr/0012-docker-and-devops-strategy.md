# ADR-0012: Docker and DevOps Strategy

## Status

Accepted

## Context

The repository should be easy to build locally and validate in CI, while preserving mock-provider-first behavior and avoiding required cloud credentials.

## Decision

Provide Dockerfiles for API and web, Docker Compose for API/web/SQL Server local execution, and GitHub Actions for backend, frontend, and Docker build validation.

## Consequences

- Recruiters and reviewers can see practical DevOps maturity.
- Local demos do not require Azure OpenAI credentials.
- CI can validate core build and test behavior.
- Future deployment workflows can build on the same container strategy.

## Alternatives Considered

- Local-only setup: rejected because it weakens deployment readiness.
- Cloud-only setup: rejected because it would make local demos harder and require secrets.
