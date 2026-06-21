# Security Architecture

## Current Foundation

Epic 0 includes secure configuration placeholders, no committed secrets, and a mock AI provider for local development.

## Future Direction

- Azure Entra ID authentication.
- Role-based authorization for Architect, Reviewer, Admin, and Viewer.
- Azure Key Vault for secrets.
- Managed Identity for Azure resource access.
- Secure headers and intentional CORS configuration.

## Logging Safety

Logs must not include API keys, tokens, connection strings, full confidential prompts, or sensitive AI responses.
