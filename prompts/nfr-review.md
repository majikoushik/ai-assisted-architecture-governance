# Prompt: NFR Review

Version: `v1.0.0`

## Purpose

Generate a non-functional requirement checklist and review notes for a submitted requirement.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- Known NFR targets, if provided.

## Instructions

You are assisting a Solution Architect. Identify NFR considerations and gaps. Use only supplied context for known facts and mark missing targets as open questions.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. Known NFR Facts.
4. Performance.
5. Scalability.
6. Availability.
7. Reliability.
8. Security.
9. Observability.
10. Maintainability.
11. Cost.
12. Compliance Readiness.
13. AI Provider Dependency Considerations.
14. NFR Checklist.
15. Assumptions.
16. Risks.
17. Open Questions.

## Constraints

- Do not invent target SLAs, RTO, RPO, throughput, or compliance obligations.
- If NFRs are missing, identify them as gaps.
- Do not claim production readiness.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
