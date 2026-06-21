# Azure Deployment Guide

This guide details the steps required to deploy the AI-Assisted Architecture Governance platform to Azure using the provided Bicep Infrastructure as Code (IaC) templates and GitHub Actions.

## Target Azure Architecture

1. **Angular Frontend**: Azure Static Web Apps (SWA)
2. **Backend API**: Azure Container Apps (ACA)
3. **Container Registry**: Azure Container Registry (ACR)
4. **Database**: Azure SQL Database
5. **AI Provider**: Azure OpenAI
6. **Secrets Management**: Azure Key Vault
7. **Monitoring**: Azure Application Insights & Log Analytics

## Prerequisites

- Active Azure Subscription
- Azure CLI (`az`) installed
- Bicep CLI installed (`az bicep install`)
- GitHub repository with Actions enabled
- Docker Desktop or equivalent installed locally

## 1. Environment Variable & Secret Strategy

The deployment relies on GitHub Actions Secrets and Azure Key Vault.

**Required GitHub Secrets:**
- `AZURE_CREDENTIALS`: JSON output of `az ad sp create-for-rbac`
- `ACR_LOGIN_SERVER`: The ACR server name (e.g., `acrarchgov.azurecr.io`)
- `AZURE_STATIC_WEB_APPS_API_TOKEN`: Deployment token for SWA
- `SQL_CONNECTION_STRING_PASSWORD`: Admin password for Azure SQL deployment

**Key Vault Strategy:**
The API Container App will pull the following secrets directly from Key Vault using Managed Identity:
- Azure SQL Connection String
- Azure OpenAI API Key

*Note: Azure OpenAI API keys are NEVER exposed to the Angular frontend.*

## 2. Deploy Infrastructure (Bicep)

The IaC templates are located in `infra/bicep/`.

1. **Validate Bicep Syntax:**
   ```bash
   az bicep build --file infra/bicep/main.bicep
   ```

2. **Run What-If Analysis:**
   ```bash
   az group create --name rg-architecture-governance-dev --location eastus2
   
   az deployment group what-if \
     --resource-group rg-architecture-governance-dev \
     --template-file infra/bicep/main.bicep \
     --parameters infra/bicep/parameters/dev.parameters.json \
     --parameters sqlAdminPassword="<YourSecurePassword>"
   ```

3. **Deploy:**
   Replace `what-if` with `create` in the command above.

## 3. Container Image Build and Push Flow

Review `.github/workflows/container-build-template.yml`.

1. The GitHub Action checks out the code.
2. Authenticates to Azure via `az login`.
3. Authenticates to ACR via `az acr login`.
4. Builds the `.NET` container (`src/api/ArchitectureGovernance.Api/Dockerfile`).
5. Pushes the image tagged with the `github.sha`.

## 4. Container Apps Deployment Flow

Once the infrastructure and image are ready, the API is deployed:

```bash
az containerapp update \
  --name ca-api-archgov-dev \
  --resource-group rg-architecture-governance-dev \
  --image <acr-name>.azurecr.io/architecture-governance-api:<sha>
```
*Note: The EF Core database migrations will run automatically on Container App startup because `dbContext.Database.Migrate()` is configured in `Program.cs`.*

## 5. Angular Frontend Deployment Flow

Review `.github/workflows/azure-deploy-template.yml`.

The Angular application is deployed directly to Azure Static Web Apps. The GitHub Action:
1. Builds the Angular application (`npm run build`).
2. Uploads the `dist/architecture-governance-portal/browser` folder to the SWA using the `Azure/static-web-apps-deploy` action.

## 6. Azure OpenAI Configuration

If deploying to Production, ensure `AI_PROVIDER=AzureOpenAI`.

1. Deploy an Azure OpenAI resource (manually or via the optional `azure-openai.bicep` module).
2. Create a Model Deployment (e.g., `gpt-4`).
3. Store the API Key securely in Azure Key Vault.
4. Update the Container App environment variables:
   - `AiProvider__AzureOpenAi__Endpoint`
   - `AiProvider__AzureOpenAi__DeploymentName`
   - `AiProvider__AzureOpenAi__ApiKey` (Reference Key Vault Secret)

### Safe AI Telemetry Validation
Ensure Application Insights is logging durations and template names, but verify that *no raw prompt text or generated architectures are logged* by reviewing the traces in Log Analytics.

## 7. Cost-Awareness Notes

- **Demo Environments**: Use the `Basic` pricing tier for Azure SQL and free tiers where possible. 
- **Container Apps**: Configure scale rules to scale to `0` when idle to save costs.
- **Azure OpenAI**: Token costs can accumulate quickly during testing. Consider using the `Mock` provider for CI/CD and routine testing, switching to Azure OpenAI only for explicit staging validation.
- **Application Insights**: Default retention is 30 days. Lowering it can reduce ingestion costs.

### Cleanup Commands
To destroy a demo environment to prevent ongoing costs:
```bash
az group delete --name rg-architecture-governance-dev --yes --no-wait
```

## Known Limitations

- **Azure Static Web Apps API Routing**: If the frontend needs to call the ACA API, cross-origin resource sharing (CORS) must be configured on the ACA backend, or Azure API Management should be placed in front to route traffic under a single domain.
