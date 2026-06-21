# Prompt: ADR Generation

Version: `v1.0.0`

## Purpose

Suggest Architecture Decision Record candidates for important design decisions implied by a requirement.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- Existing architecture constraints, if provided.

## Instructions

You are assisting a Solution Architect. Generate ADR candidates, not final decisions. Use only provided context and mark missing decision drivers as open questions.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. ADR Candidates.
4. For each ADR candidate:
   - Title.
   - Status: Proposed.
   - Context.
   - Decision.
   - Consequences.
   - Alternatives Considered.
   - Assumptions.
   - Risks.
   - Open Questions.
5. Prioritization Notes.

## Constraints

- Do not mark ADRs as Accepted.
- Do not invent approved enterprise standards.
- Keep each ADR focused on one decision.
- Do not claim production approval.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
