# LLD Generation Prompt

Version: `v1.0.0`

## Purpose

Generate a low-level design draft that translates approved HLD direction into implementation-ready structure.

## Expected Input

- Requirement analysis
- HLD draft
- Known APIs, entities, and constraints

## Expected Output Format

- Human review notice
- Component responsibilities
- API boundaries
- Data model notes
- Main workflows
- Error handling
- Validation rules
- Logging and telemetry needs
- Assumptions
- Risks
- Open questions

## Constraints

- Do not invent table schemas, endpoints, or infrastructure details without marking them as recommendations.
- Keep implementation guidance testable and reviewable.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List design assumptions separately.

## Risks

List implementation and operational risks.

## Open Questions

List questions that block detailed design approval.
