# Non-Functional Requirement (NFR) Review Prompt

Version: `v1.0.0`

## Purpose

Suggest non-functional requirement considerations and an assessment of architecture readiness based on business requirements.

## Expected Input

- Requirement text and context
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections:

- **Executive Summary**: A brief overview of the NFR review.
- **Performance Considerations**: Target response times and throughput.
- **Scalability Considerations**: How the system should scale under load.
- **Availability Considerations**: Target uptime and multi-region needs.
- **Reliability Considerations**: Fault tolerance and disaster recovery.
- **Security Considerations**: High-level security and compliance.
- **Observability Considerations**: Logging, metrics, and tracing.
- **Maintainability Considerations**: Code structure and deployment cadence.
- **Cost Considerations**: Potential cost drivers.
- **Compliance-readiness Considerations**: Data privacy and regulatory needs.
- **AI Provider Dependency Considerations**: Any relevant AI latency/limits (if applicable).
- **NFR Checklist**: A boolean checklist of actionable NFRs.
- **Priority Ranking**: Prioritization of NFRs (e.g., Security > Availability > Cost).
- **Assumptions**: Inferred NFR constraints.
- **Risks**: Trade-offs or missing NFRs that pose risks.
- **Open Questions**: What must be clarified with stakeholders.

## Constraints

- Do not invent unavailable facts or claim production readiness.
- Separate confirmed inputs from assumptions.
- Ensure the output looks like a professional, enterprise-grade architecture draft.
- Do NOT log full sensitive requirement text.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
