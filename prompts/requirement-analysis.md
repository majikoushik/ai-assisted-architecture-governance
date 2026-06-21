# Requirement Analysis Prompt

Version: `v1.0.0`

## Purpose

Analyze a business or technical requirement and produce architecture governance-ready findings.

## Expected Input

- Requirement title
- Requirement text
- Business domain context
- Expected artifact types

## Expected Output Format

- Human review notice
- Executive summary
- Business capabilities
- Actors
- Key workflows
- Candidate services
- Assumptions
- Risks
- Open questions

## Constraints

- Do not invent facts not present in the requirement.
- Mark assumptions clearly.
- Prefer concise, reviewable Markdown.
- Do not include production approval language.

## Human Review Notice

This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Assumptions

List inferred assumptions separately from facts.

## Risks

List delivery, architecture, security, integration, and compliance risks.

## Open Questions

List questions that should be answered by stakeholders before design approval.
