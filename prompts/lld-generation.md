# Prompt: LLD Generation

Version: `v1.0.0`

## Purpose

Generate a Low-Level Design draft that expands the requirement into component responsibilities, API boundaries, data model notes, validation, error handling, logging, and test considerations.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- HLD draft or requirement analysis, if available.

## Instructions

You are assisting a Solution Architect and senior engineer. Create an implementation-oriented LLD draft while preserving architecture governance boundaries. Use only supplied facts and mark assumptions.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. Requirement Traceability.
4. Component Responsibilities.
5. API Boundaries and DTO Guidance.
6. Data Model Recommendations.
7. Main Workflows.
8. Validation Rules.
9. Error Handling.
10. Logging and Telemetry.
11. Security Implementation Notes.
12. Testing Considerations.
13. Assumptions.
14. Risks.
15. Open Questions.

## Constraints

- Avoid over-specifying technology not present in the requirement unless listed as a recommendation.
- Do not expose secrets or confidential content.
- Do not claim implementation readiness without human review.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
