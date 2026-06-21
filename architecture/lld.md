# Low-Level Design

## Service Internals

The backend follows layered architecture with thin controllers or endpoints, application orchestration, domain rules, and infrastructure adapters.

## API Flow

Requests enter through `/api/v1/*`, receive a correlation ID, are validated, and are handled by application services. Errors should be returned as Problem Details.

## Prompt Flow

Prompt templates are stored under `prompts/`, versioned in source control, and referenced by generated artifacts.

## Artifact Flow

Future epics will persist requirements, generated artifacts, prompt metadata, AI interaction metadata, and review records.

## Error Handling

The API uses global exception handling readiness through ASP.NET Core Problem Details. Detailed exceptions remain server-side.
