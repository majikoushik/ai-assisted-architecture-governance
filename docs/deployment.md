# Deployment

## Local

Docker Compose is prepared for API, web, and SQL Server services.

## Azure Target

- Angular portal: Azure Static Web Apps.
- API: Azure Container Apps.
- Container registry: Azure Container Registry.
- Database: Azure SQL Database.
- AI provider: Azure OpenAI.
- Secrets: Azure Key Vault with Managed Identity.
- Monitoring: Application Insights and Log Analytics.

Infrastructure as Code should use Bicep under `infra/bicep/`.
