# Risk and Assumption Review Prompt

Version: `v1.0.0`

## Purpose

Identify, categorize, and suggest mitigations for risks and assumptions based on business requirements.

## Expected Input

- Requirement text and context
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections:

- **Executive Summary**: A brief overview of the risk landscape.
- **Key Assumptions**: Core assumptions driving the current design direction.
- **Business Risks**: Risks related to domain, market, or stakeholders.
- **Technical Risks**: Architecture, legacy integration, or performance risks.
- **Security Risks**: Authentication, data, and compliance risks.
- **Operational Risks**: Deployment, observability, and support risks.
- **Data Risks**: Data quality, migration, or retention risks.
- **AI-related Risks**: Hallucination, latency, and AI governance risks.
- **Cloud Deployment Risks**: Vendor lock-in, quotas, or networking risks.
- **Dependency Risks**: Third-party APIs and libraries.
- **Open Questions**: Critical questions to de-risk the project.
- **Mitigation Recommendations**: Actionable steps to address identified risks.

## Constraints

- Do not invent unavailable facts or claim production readiness.
- Separate confirmed inputs from assumptions.
- Ensure the output looks like a professional, enterprise-grade architecture draft.
- Do NOT log full sensitive requirement text.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
