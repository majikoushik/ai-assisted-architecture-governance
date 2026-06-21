# Deployment Architecture

## Local Foundation

The application is containerized natively for ease of developer setup and operational parity.

- **`docker-compose.yml`** orchestrates the platform for local validation.
- **Backend (`api`)**: Built as a multi-stage Docker image, running as a non-root user for security, and accessible at port `8080`.
- **Frontend (`web`)**: Built as a multi-stage Docker image, with the Angular production build packaged within an Nginx Alpine container for performant static asset serving and routing.
- **Database (`sqlserver`)**: Microsoft SQL Server image with a named volume for persistent storage across container restarts.

## Azure Target Direction

The local Docker setup maps precisely to the deployed Azure environment.

1. **Angular Frontend**: Deployed to **Azure Static Web Apps**.
2. **Backend API**: Deployed to **Azure Container Apps** for managed, scalable serverless container execution.
3. **Container Registry**: **Azure Container Registry** stores the `.NET` backend image.
4. **Database**: **Azure SQL Database**.
5. **AI Provider**: **Azure OpenAI**, providing enterprise-grade, compliant AI access.
6. **Secrets**: **Azure Key Vault** stores connection strings and OpenAI API Keys. Container Apps retrieve these securely via **Managed Identity**.
7. **Monitoring**: **Azure Application Insights** and **Log Analytics** capture standard telemetry and our Safe AI Telemetry metadata.

### Environment Separation
The architecture supports discrete environments (e.g., `dev`, `test`, `prod`) using Bicep parameters.

## Infrastructure as Code (IaC)

Bicep is used for all Azure Infrastructure. The templates reside under `infra/bicep/`:
- `main.bicep`: Orchestrates the deployment.
- `modules/`: Contains modular templates for SQL, ACA, ACR, SWA, Key Vault, and Log Analytics.
- `parameters/`: Contains environment-specific configurations (e.g., `dev.parameters.json`).

## CI/CD Pipeline

The GitHub Actions workflows under `.github/workflows/` establish the deployment flow:
1. **CI Validation (`ci.yml`)**: Unit test validation and Docker image build verification.
2. **Container Build (`container-build-template.yml`)**: Builds the backend API image and securely pushes it to Azure Container Registry using Managed Identity.
3. **Azure Deployment (`azure-deploy-template.yml`)**: Deploys the Bicep IaC, deploys the backend image to Azure Container Apps, and deploys the Angular app to Azure Static Web Apps.
