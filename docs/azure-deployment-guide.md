# Azure Deployment Guide

## Target Resources

- Azure Container Registry.
- Azure Container Apps Environment.
- Backend API Container App.
- Azure Static Web Apps.
- Azure SQL Database.
- Azure OpenAI.
- Azure Key Vault.
- Log Analytics Workspace.
- Application Insights.

## Validate Bicep

```powershell
az bicep build --file infra/bicep/main.bicep
```

## Parameter Files

- `infra/bicep/parameters/dev.parameters.json` is a development blueprint.
- `infra/bicep/parameters/prod.parameters.example.json` is an example only.

Do not commit production secrets or tenant-specific confidential values.

## Secrets

Use GitHub Actions secrets and Azure Key Vault for:

- Container registry credentials if needed.
- SQL admin password.
- Azure OpenAI endpoint, deployment name, and key if API key auth is used.
- Application Insights connection string where required.

## Cost Awareness

Azure SQL, Azure OpenAI, Container Apps, Log Analytics, and Application Insights can incur cost. Use budget alerts, low-tier development SKUs, and resource group cleanup for demos.

## Production Review

Before production deployment, complete security, networking, identity, data retention, compliance, backup/restore, monitoring, and incident response review.
