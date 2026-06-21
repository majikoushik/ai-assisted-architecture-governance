# API Governance

## API Principles

The backend API follows enterprise API conventions:

- Versioned `/api/v1/*` routes.
- Thin controllers.
- DTO-based request and response models.
- FluentValidation for command validation.
- Problem Details style error responses.
- Correlation ID propagation.
- Swagger/OpenAPI in development.

## Representative Endpoints

```text
POST   /api/v1/projects
GET    /api/v1/projects
GET    /api/v1/projects/{projectId}
POST   /api/v1/requirements
GET    /api/v1/projects/{projectId}/requirements
POST   /api/v1/artifacts/generate
GET    /api/v1/artifacts/{artifactId}
GET    /api/v1/artifacts/{artifactId}/export/markdown
GET    /api/v1/prompts
POST   /api/v1/reviews
GET    /health/live
GET    /health/ready
```

## Response Expectations

Successful responses should include a consistent response envelope where implemented. Error responses should avoid stack traces and include a correlation ID.

## API Review Artifact

The API contract review prompt helps architects inspect endpoint naming, DTO design, error standards, versioning, idempotency, pagination, OpenAPI readiness, and authorization concerns. It is advisory and not a substitute for formal enterprise API governance.
