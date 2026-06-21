# Security Review Prompt

Version: `v1.0.0`

## Purpose

Suggest security considerations and an assessment of architecture security readiness based on business requirements.

## Expected Input

- Requirement text and context
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections:

- **Executive Summary**: A brief overview of the security review.
- **Authentication Considerations**: How users and services authenticate.
- **Authorization Considerations**: Role-based access control and scopes.
- **Sensitive Data Handling**: Encryption at rest and in transit.
- **Secret Management**: Storage and rotation of secrets.
- **Input Validation Risks**: Preventing injection attacks.
- **API Security Concerns**: Rate limiting, CORS, and exposed endpoints.
- **Logging and Telemetry Safety**: Preventing PII and secrets in logs.
- **OWASP Considerations**: Highlighting relevant OWASP Top 10 risks.
- **Threat-modeling Readiness**: Key threat vectors to model.
- **Cloud Security Considerations**: Relevant Azure security configurations.
- **Responsible AI Security Considerations**: Prompt injection and data privacy.
- **Assumptions**: Inferred security constraints.
- **Risks**: Current security vulnerabilities or gaps.
- **Open Questions**: What must be clarified with security stakeholders.

## Constraints

- This is not a substitute for formal security assessment or penetration testing.
- Do not invent unavailable facts or claim production readiness.
- Separate confirmed inputs from assumptions.
- Do NOT log full sensitive requirement text.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
