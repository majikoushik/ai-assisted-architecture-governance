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

## Environment Variables

When deploying to Azure Container Apps, ensure the following environment variables are securely mapped (preferably from Key Vault references):

- `AiProvider__Provider` = `AzureOpenAI`
- `AzureOpenAI__Endpoint` = `<your-endpoint-url>`
- `AzureOpenAI__ApiKey` = `<your-api-key>`
- `AzureOpenAI__DeploymentName` = `<your-deployment-name>`
- `AzureOpenAI__TimeoutSeconds` = `60`
