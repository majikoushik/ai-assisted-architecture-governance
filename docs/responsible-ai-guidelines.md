# Responsible AI Guidelines

## Mandatory Notice

Generated artifacts are AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Demo Data

Use only synthetic requirements in public demos. Do not enter real confidential business requirements unless the environment has been approved for that data classification.

## Logging Rules

Do not log:

- API keys or tokens.
- Connection strings.
- Full prompts.
- Full requirement text.
- Full AI responses.
- Confidential customer or business data.

Allowed operational metadata includes provider, artifact type, prompt version, duration, status, and correlation ID.

## Azure OpenAI

Azure OpenAI is optional and configuration-driven. Local and CI workflows should remain on the mock provider unless explicitly testing Azure provider integration in a secure environment.

## Review Boundaries

AI-generated security and API review outputs support architecture discussion. They do not replace threat modeling, penetration testing, privacy review, legal review, compliance review, or enterprise architecture board approval.
