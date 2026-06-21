# High-Level Design

## Business Context

The platform helps solution architects convert business requirements into structured, versioned, and reviewable architecture artifacts.

## System Context

Primary users are architects, reviewers, and engineering leads. The system includes an Angular portal, ASP.NET Core API, SQL Server persistence, prompt catalog, mock AI provider, and future Azure OpenAI provider.

## Major Components

- Angular governance portal for intake, artifact review, and prompt catalog views.
- ASP.NET Core API for enterprise API workflows.
- Application layer for use case orchestration.
- Domain layer for architecture governance concepts.
- Infrastructure layer for persistence, provider adapters, and exporting.
- AI provider abstraction with mock-first local development.

## Quality Attributes

The target architecture emphasizes security, observability, testability, traceability, prompt versioning, and Azure deployment readiness.
