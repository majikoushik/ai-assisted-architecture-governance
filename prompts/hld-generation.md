# Prompt: HLD Generation

Version: `v1.0.0`

## Purpose

Generate a High-Level Design draft from a submitted requirement and domain context.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- Requirement analysis summary, if available.

## Instructions

You are assisting a Solution Architect. Produce a structured HLD draft suitable for human review. Use only provided context. Distinguish facts from assumptions and call out unknowns.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. Business Context.
4. System Context.
5. Major Components.
6. Candidate Service Boundaries.
7. Integration Points.
8. Data Flow Overview.
9. Security Considerations.
10. Observability Considerations.
11. Deployment Considerations.
12. Assumptions.
13. Risks.
14. Open Questions.

## Constraints

- Do not invent named systems, SLAs, compliance obligations, or vendors unless provided.
- Use Azure-ready recommendations where appropriate.
- Keep the artifact reviewable and concise.
- Do not claim the design is production-approved.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
