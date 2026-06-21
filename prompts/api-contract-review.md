# API Contract Review Prompt

Version: `v1.0.0`

## Purpose

Suggest API design considerations and assess API governance readiness based on business requirements.

## Expected Input

- Requirement text and context
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections:

- **Executive Summary**: A brief overview of the API contract review.
- **API Boundary Recommendations**: High-level domains and boundaries.
- **Endpoint Naming Review**: RESTful naming conventions.
- **Request DTO Guidance**: Required fields and validation.
- **Response DTO Guidance**: Expected return models.
- **Error Response Standards**: Usage of Problem Details.
- **Versioning Readiness**: URL or Header versioning strategies.
- **Idempotency Considerations**: Safe retries for mutations.
- **Pagination and Filtering Considerations**: Query parameter guidelines.
- **Authentication and Authorization Readiness**: Scopes and token requirements.
- **OpenAPI/Swagger Readiness**: Documentation expectations.
- **Backward Compatibility Concerns**: Handling breaking changes.
- **Assumptions**: Inferred API constraints.
- **Risks**: Integration or design risks.
- **Open Questions**: What must be clarified with API consumers.

## Constraints

- This is not a substitute for formal API governance approval.
- Do not invent unavailable facts or claim production readiness.
- Separate confirmed inputs from assumptions.
- Do NOT log full sensitive requirement text.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
