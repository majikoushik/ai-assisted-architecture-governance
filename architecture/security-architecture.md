# Security Architecture

## Current Foundation

Epic 0 includes secure configuration placeholders, no committed secrets, and a mock AI provider for local development.

## Current Foundation & Azure Target

The platform relies on the following Azure-native security patterns:

- **Azure Key Vault:** Centralized storage for Azure SQL Connection Strings and Azure OpenAI API Keys. Secrets are NEVER injected into Docker images at build time.
- **Managed Identity & RBAC:** Azure Container Apps use System-Assigned Managed Identities to authenticate to Azure Key Vault using Role-Based Access Control (RBAC) (e.g., `Key Vault Secrets User`), eliminating the need for client secrets in connection strings.
- **Secure Defaults:** The local environment securely defaults to `Mock` AI to prevent accidental credential leakage in development.
- **Future Enhancements:** Azure Entra ID authentication and Role-Based Authorization for Architect, Reviewer, and Admin personas.

## Logging Safety

Logs must not include API keys, tokens, connection strings, full confidential prompts, or sensitive AI responses.
