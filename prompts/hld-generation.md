# HLD Generation Prompt

Version: `v1.0.0`

## Purpose

Generate a high-level design draft for review by a solution architect.

## Expected Input

- Requirement analysis
- Business context
- Constraints and known systems
- Target platform preferences

## Expected Output Format

- Human review notice
- Business context
- System context
- Key components
- Integration points
- Data flow
- Security considerations
- Observability considerations
- Deployment considerations
- Assumptions
- Risks
- Open questions

## Constraints

- Keep recommendations Azure-ready but avoid hardcoded subscription or tenant details.
- Distinguish confirmed requirements from recommendations.
- Do not claim the design is production-approved.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

Call out missing details that affect architecture choices.

## Risks

Identify risks that should be reviewed before HLD sign-off.

## Open Questions

Ask targeted questions for business, security, data, integration, and operations stakeholders.
