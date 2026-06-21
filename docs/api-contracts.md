# API Contracts

The API is version-ready under `/api/v1`.

Current foundation endpoint:

```http
GET /api/v1/platform/readiness
```

Future endpoints will cover projects, requirements, artifacts, prompt templates, reviews, and AI interaction metadata.

Errors should use Problem Details and include a correlation ID where possible.
