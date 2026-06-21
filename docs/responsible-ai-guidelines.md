# Responsible AI Guidelines

## Required Principles

- AI output is draft content only.
- Human architect review is mandatory.
- Assumptions, risks, and open questions must be visible.
- Sensitive prompts and secrets must not be logged.
- Prompt templates must reduce hallucination and unsupported certainty.

## Local Development

Use the deterministic mock provider by default.

## Production Direction

Use Azure OpenAI through provider abstraction, secure configuration, Key Vault readiness, and safe telemetry.
