# Local Development

The local default AI provider is `Mock`, so no Azure OpenAI credentials are required.

## API

Run the ASP.NET Core API and check:

- `GET /health`
- `GET /api/v1/platform/readiness`

## Portal

Run the Angular app from `src/web/architecture-governance-portal`.

## Secrets

Use `dotnet user-secrets` to configure the Azure OpenAI provider locally without committing credentials to source control.

To switch to the Azure OpenAI provider locally:

```bash
cd src/api/ArchitectureGovernance.Api
dotnet user-secrets init
dotnet user-secrets set "AiProvider:Provider" "AzureOpenAI"
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://your-resource.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_API_KEY"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4o"
```

## 4. Azure & Production Deployment

For instructions on deploying the application to Azure using Bicep Infrastructure as Code, setting up Key Vault, and configuring GitHub Actions, see the **[Azure Deployment Guide](azure-deployment-guide.md)**.

For full Docker containerization and local cluster setup, refer to the **[DevOps and Local Development Guide](devops-guide.md)**.

For more details on the provider configuration and responsible AI considerations, see the [Azure OpenAI Provider Documentation](azure-openai-provider.md).
