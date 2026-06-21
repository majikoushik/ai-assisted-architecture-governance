# Prompt: Security Review

Version: `v1.0.0`

## Purpose

Generate a security architecture review draft for a submitted requirement.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- Known identity, data, compliance, and integration constraints, if provided.

## Instructions

You are assisting a Solution Architect. Produce a security review draft that identifies concerns, assumptions, risks, and follow-up questions. This is not a formal security assessment.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. Known Security Facts.
4. Authentication Considerations.
5. Authorization Considerations.
6. Sensitive Data Handling.
7. Secret Management.
8. API Security.
9. Logging and Telemetry Safety.
10. OWASP Considerations.
11. Threat Modeling Readiness.
12. Cloud Security Considerations.
13. Responsible AI Security Considerations.
14. Assumptions.
15. Risks.
16. Open Questions.

## Constraints

- Do not claim the system is secure.
- Do not invent regulatory obligations.
- Recommend formal threat modeling for production use.
- Avoid exposing sensitive content.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
