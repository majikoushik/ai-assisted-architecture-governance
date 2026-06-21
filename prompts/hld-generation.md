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

- Executive Summary
- Business Context
- Architecture Goals
- Scope and Assumptions
- System Context
- Major Components & Suggested Service Boundaries
- Integration Points & Data Flow Overview
- Security Considerations
- Observability Considerations
- Deployment, Scalability, Availability & Resilience
- Key Risks
- Open Questions

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
