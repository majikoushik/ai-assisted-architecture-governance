# Prompt: Risk and Assumption Review

Version: `v1.0.0`

## Purpose

Extract and organize architecture assumptions, risks, dependencies, and open questions from a submitted requirement.

## Expected Input

- Requirement title.
- Requirement text.
- Business domain.
- Domain context.
- Known constraints, if provided.

## Instructions

You are assisting a Solution Architect. Identify risk categories and assumptions without inventing facts. Make uncertainty explicit and prioritize questions for stakeholders.

## Output Format

Return Markdown with these sections:

1. Human Review Notice.
2. Executive Summary.
3. Known Facts.
4. Key Assumptions.
5. Business Risks.
6. Technical Risks.
7. Security Risks.
8. Operational Risks.
9. Data Risks.
10. AI-Related Risks.
11. Cloud Deployment Risks.
12. Dependency Risks.
13. Risk Severity Table.
14. Mitigation Recommendations.
15. Open Questions.

## Constraints

- Do not invent confirmed dependencies.
- Use severity labels such as Low, Medium, High only as draft recommendations.
- Do not claim risks are fully mitigated.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
