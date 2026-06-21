# LLD Generation Prompt

Version: `v1.0.0`

## Purpose

Generate a Low-Level Design (LLD) draft that translates approved HLD direction into implementation-ready structures based on the provided requirement.

## Expected Input

- Requirement text and context
- Business domain

## Expected Output Format

The output must be structured Markdown and include the following sections:

- **Executive Summary**: A brief overview of the LLD.
- **Requirement Traceability**: Link the design to the provided requirement.
- **Component-Level Design & Module Responsibilities**: Detail the specific modules, their responsibilities, and how they interact.
- **API Boundaries & Request/Response DTOs**: Suggest endpoint names, and payload structures.
- **Data Model Recommendations**: Propose database tables, relationships, and ORM usage.
- **Validation & Error Handling**: Outline input validation and global error response formats.
- **Logging & Telemetry**: Specify what should be logged and how correlation is handled.
- **Security Implementation**: Cover authentication, authorization, and sensitive data protection.
- **Integration Details & Sequence Flow**: Describe a typical workflow sequence.
- **Testing Considerations**: Propose unit, integration, and mocking strategies.
- **Assumptions**: Clearly list any assumed technical context.
- **Risks**: Highlight possible failure modes or integration risks.
- **Open Questions**: Identify areas where more stakeholder input is needed.

## Constraints

- Do not invent table schemas, endpoints, or infrastructure details without marking them as recommendations.
- Keep implementation guidance testable and reviewable.
- Do NOT log full sensitive requirement text.
- Separate confirmed inputs from assumptions.

## Human Review Notice

Always include this exact text at the beginning of the output:
> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.
