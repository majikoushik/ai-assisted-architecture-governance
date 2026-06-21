# Deployment Architecture

## Local Deployment

Local development can run with:

- .NET API directly through `dotnet run`.
- Angular portal through `npm start`.
- SQL Server local instance or Docker.
- Mock AI provider by default.

Docker Compose starts API, web portal, and SQL Server for an integrated local environment.

## Azure Blueprint

The target Azure deployment uses:

- Azure Static Web Apps for Angular.
- Azure Container Apps for ASP.NET Core API.
- Azure Container Registry for API images.
- Azure SQL Database for persistence.
- Azure OpenAI for configured AI generation.
- Azure Key Vault for secrets.
- Application Insights and Log Analytics for telemetry.

## Configuration Strategy

Local and CI:

```text
AI_PROVIDER=Mock
```

Azure OpenAI optional configuration:

```text
AI_PROVIDER=AzureOpenAI
AZURE_OPENAI_ENDPOINT=<stored securely>
AZURE_OPENAI_DEPLOYMENT_NAME=<stored securely>
AZURE_OPENAI_KEY=<stored securely or replaced by managed identity pattern>
```

Secrets belong in Key Vault or secure CI/CD secrets, not in source control or frontend configuration.

## Production Caveats

The Bicep files are a deployment blueprint. Production readiness requires networking, identity, threat modeling, backup/restore, data retention, cost budgets, and compliance review.
