# Low-Level Design

## Backend Flow

Requests enter through `/api/v1/*` controllers. Middleware assigns or propagates a correlation ID, request logging records operational metadata, and global exception handling returns Problem Details style responses.

Controllers stay thin and delegate to MediatR commands and queries. Application handlers validate relationships, call the domain and infrastructure boundaries, and return DTOs. EF Core persists projects, requirements, generated artifacts, and review records.

## Artifact Generation Internals

1. `GenerateArtifactCommand` receives project ID, requirement submission ID, and artifact type.
2. `GenerateArtifactCommandValidator` checks required fields and supported artifact types.
3. The handler loads the project and requirement from `IAppDbContext`.
4. The handler maps artifact type to a prompt file such as `hld-generation.md`.
5. `FilePromptRepository` parses prompt title, version, purpose, and full content.
6. The handler constructs `ArchitectureAiRequest` with requirement and prompt metadata.
7. `IArchitectureAiProvider` generates a draft using the configured provider.
8. The handler increments artifact version for the requirement and artifact type.
9. `GeneratedArtifact` is saved with provider name, prompt name, prompt version, status, and correlation ID.

## Prompt Loading

Prompt templates are Markdown files under `prompts/`. The file repository parses:

- Title from the first Markdown heading.
- Version from the `Version: ` line.
- Purpose from the `## Purpose` section.
- Artifact type from the prompt file name.

This keeps prompt templates versioned in source control and inspectable through the prompt catalog UI.

## Review Workflow

`ReviewRecord` captures artifact ID, reviewer name, review status, comments, and timestamps. Review status is part of the human-in-the-loop workflow and does not replace production architecture governance.

## Error Handling

Expected failures are surfaced through validation and not-found exceptions handled by global middleware. The API should return safe error messages with correlation IDs and avoid exposing stack traces to the frontend.

## Frontend Structure

The Angular portal uses standalone components, feature folders, typed API services, and HTTP interceptors. Feature areas include dashboard, projects, requirements, artifact generation, artifact viewer, prompt catalog, reviews, and system health.

## Data Access

EF Core maps domain entities to SQL Server tables. Local Docker uses SQL Server 2022. Azure target is Azure SQL Database.
