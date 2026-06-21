# Risk and Assumption Review Prompt

Version: `v1.0.0`

## Purpose

Extract and organize architecture assumptions, risks, dependencies, and open questions.

## Expected Input

- Requirement or architecture artifact content
- Known project context

## Expected Output Format

- Human review notice
- Confirmed facts
- Assumptions
- Risks
- Dependencies
- Mitigations
- Open questions

## Constraints

- Separate facts from assumptions.
- Avoid creating unsupported certainty.
- Prioritize risks that affect architecture approval.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List all inferred assumptions.

## Risks

List risks with impact and suggested mitigation.

## Open Questions

List stakeholder questions needed for governance review.
