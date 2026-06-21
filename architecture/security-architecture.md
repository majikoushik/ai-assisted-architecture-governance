# Security Architecture

## Current Controls

- Environment-based configuration.
- `.env.example` with safe placeholders.
- Azure OpenAI configuration is not required for local execution.
- Angular frontend does not receive Azure OpenAI secrets.
- Correlation IDs support incident triage without exposing sensitive content.
- Prompt and AI telemetry guidance excludes full confidential content by default.

## Target Controls

- Microsoft Entra ID authentication.
- Role-based authorization for Architect, Reviewer, Admin, and Viewer.
- Azure Key Vault for API secrets and connection strings.
- Managed Identity for Azure resource access.
- Private networking and firewall rules for Azure SQL where appropriate.
- Secure CORS policy for production frontend origin.

## AI-Specific Security

- Treat submitted requirements as potentially sensitive.
- Avoid logging full prompts and responses.
- Add prompt injection detection and content filtering before production use.
- Use synthetic sample data in public demos.

## Security Review Scope

Generated security review artifacts are draft checklists for architecture discussion. They are not a formal threat model, penetration test, compliance assessment, or production approval.
