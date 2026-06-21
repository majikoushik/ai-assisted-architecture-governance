# Azure Bicep Infrastructure

This folder contains the Infrastructure as Code (IaC) definitions required to deploy the AI-Assisted Architecture Governance platform to Azure.

## Modules Included
- Azure Container Apps (Environment & App)
- Azure Container Registry (ACR)
- Azure SQL Database
- Azure Static Web Apps
- Azure Key Vault
- Azure Application Insights & Log Analytics
- Azure OpenAI (Optional)

## Local Validation

You can validate the Bicep templates without an Azure subscription:

```bash
az bicep build --file main.bicep
```

## Deployment Example

To deploy the infrastructure, you will use the Azure CLI:

```bash
# Create Resource Group
az group create --name rg-architecture-governance-dev --location eastus2

# Deploy infrastructure (What-If mode to preview changes)
az deployment group what-if \
  --resource-group rg-architecture-governance-dev \
  --template-file main.bicep \
  --parameters parameters/dev.parameters.json

# Execute deployment
az deployment group create \
  --resource-group rg-architecture-governance-dev \
  --template-file main.bicep \
  --parameters parameters/dev.parameters.json
```
