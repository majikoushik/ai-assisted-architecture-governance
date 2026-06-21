# API Contract Review Prompt

Version: `v1.0.0`

## Purpose

Review or suggest API contract considerations for architecture governance.

## Expected Input

- Requirement text
- Existing or proposed API contract
- Consumer and producer context

## Expected Output Format

- Human review notice
- Endpoint naming
- Request and response DTO recommendations
- Error response standards
- Versioning readiness
- Idempotency concerns
- Pagination needs
- OpenAPI readiness
- Assumptions
- Risks
- Open questions

## Constraints

- Prefer RESTful conventions unless another style is explicitly required.
- Do not invent final payload schemas without marking them as suggestions.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List contract assumptions.

## Risks

List compatibility, security, and integration risks.

## Open Questions

Ask contract questions for API consumers and platform owners.
