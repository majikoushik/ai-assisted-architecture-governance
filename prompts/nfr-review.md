# NFR Review Prompt

Version: `v1.0.0`

## Purpose

Generate a non-functional requirement checklist for architecture governance review.

## Expected Input

- Requirement text
- Business criticality
- Known availability, latency, compliance, or scale needs

## Expected Output Format

- Human review notice
- Performance
- Scalability
- Availability
- Reliability
- Security
- Observability
- Maintainability
- Cost
- Compliance readiness
- Assumptions
- Risks
- Open questions

## Constraints

- Mark missing measurable targets as gaps.
- Avoid inventing service-level objectives.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List inferred NFR assumptions.

## Risks

List NFR risks and missing evidence.

## Open Questions

Ask for measurable targets and operational constraints.
