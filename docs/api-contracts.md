# API Contracts

## API Style

The API uses RESTful `/api/v1/*` routes, request DTOs, validation, Swagger/OpenAPI in development, and Problem Details style errors.

## Main Resources

| Resource | Routes |
| --- | --- |
| Projects | `POST /api/v1/projects`, `GET /api/v1/projects`, `GET /api/v1/projects/{projectId}` |
| Requirements | `POST /api/v1/requirements`, `GET /api/v1/requirements/{requirementId}`, `GET /api/v1/projects/{projectId}/requirements` |
| Artifacts | `POST /api/v1/artifacts/generate`, `GET /api/v1/artifacts/{artifactId}`, `GET /api/v1/artifacts/{artifactId}/export/markdown` |
| Prompts | `GET /api/v1/prompts`, `GET /api/v1/prompts/{promptTemplateId}` |
| Reviews | `POST /api/v1/reviews`, `GET /api/v1/artifacts/{artifactId}/reviews` |
| Platform | `GET /api/v1/platform/readiness`, `/health/live`, `/health/ready` |

## Governance Expectations

- Controllers should not construct prompts or call databases directly.
- Validation should catch missing fields and unsupported statuses or artifact types.
- Error responses should be safe for frontend display.
- Correlation IDs should be propagated for supportability.
