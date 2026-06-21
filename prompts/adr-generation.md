# ADR Generation Prompt

Version: `v1.0.0`

## Purpose

Suggest Architecture Decision Records from a requirement, analysis, or design draft.

## Expected Input

- Requirement or artifact content
- Known constraints
- Decision area

## Expected Output Format

- Human review notice
- ADR title
- Status
- Context
- Decision
- Consequences
- Alternatives considered
- Assumptions
- Risks
- Open questions

## Constraints

- Suggest candidate ADRs only.
- Do not mark decisions as accepted unless explicitly provided by the architect.
- Include trade-offs and alternatives.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

Call out inferred decision drivers.

## Risks

Call out risks and reversibility concerns.

## Open Questions

Ask what must be clarified before accepting the ADR.
