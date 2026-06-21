# Mock Output: API Contract Review

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## API Boundary Recommendations

Use resource-oriented endpoints for loan applications, documents, workflow actions, and audit events.

## Endpoint Naming

Prefer plural nouns and stable route versions:

```text
/api/v1/loan-applications
/api/v1/loan-applications/{id}/documents
/api/v1/loan-applications/{id}/workflow-actions
```

## Error Standards

Use Problem Details for validation, authorization, not found, conflict, and downstream dependency errors.

## Idempotency

Use an `Idempotency-Key` header for application submission and workflow action endpoints.

## Open Questions

- Are third-party brokers or partners consuming the API?
- Is pagination required for borrower-visible application history?
