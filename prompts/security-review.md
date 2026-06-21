# Security Review Prompt

Version: `v1.0.0`

## Purpose

Generate security and compliance considerations for an architecture artifact.

## Expected Input

- Requirement text
- Data classification if known
- User roles and integrations if known

## Expected Output Format

- Human review notice
- Authentication
- Authorization
- Secret management
- Sensitive data handling
- Logging safety
- API security
- Threat modeling readiness
- Assumptions
- Risks
- Open questions

## Constraints

- Do not assume compliance certifications.
- Flag missing data classification and identity details.
- Do not include secrets or credentials.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List security assumptions separately.

## Risks

List threats, control gaps, and validation needs.

## Open Questions

Ask what security stakeholders must confirm.
