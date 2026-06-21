# ADR Generation Prompt

Version: `v1.0.0`

## Purpose

Suggest Architecture Decision Records from a requirement, analysis, or design draft.

## Expected Input

- Requirement or artifact content
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections for one or more candidate decisions:

- **ADR Title**: Descriptive title of the decision.
- **Status**: E.g., Proposed.
- **Context**: The forces and technical drivers behind the decision.
- **Decision**: The specific architectural choice being recommended.
- **Consequences**: Both positive and negative outcomes of the decision.
- **Alternatives Considered**: Other options and why they were rejected.
- **Assumptions**: Inferred decision drivers.
- **Risks**: Reversibility concerns and implementation risks.
- **Follow-up Actions**: Steps required after acceptance.
- **Open Questions**: What must be clarified before accepting the ADR.

## Constraints

- Suggest candidate ADRs only.
- Do not mark decisions as accepted unless explicitly provided by the architect.
- Include trade-offs and alternatives.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
