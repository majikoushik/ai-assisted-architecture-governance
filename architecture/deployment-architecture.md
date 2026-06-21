# Deployment Architecture

## Local Foundation

The application is containerized natively for ease of developer setup and operational parity.

- **`docker-compose.yml`** orchestrates the platform for local validation.
- **Backend (`api`)**: Built as a multi-stage Docker image, running as a non-root user for security, and accessible at port `8080`.
- **Frontend (`web`)**: Built as a multi-stage Docker image, with the Angular production build packaged within an Nginx Alpine container for performant static asset serving and routing.
- **Database (`sqlserver`)**: Microsoft SQL Server image with a named volume for persistent storage across container restarts.

## Azure Target Direction

The local Docker setup maps precisely to future Azure deployments:

- **Azure Container Registry (ACR)**: Target registry for pushing the `api` and `web` container images.
- **Azure Container Apps (ACA)**: Target compute environment for hosting the backend `api` and optionally the `web` container, offering managed scalability and direct VNet integration.
- **Azure Static Web Apps**: Alternative hosting option for the Angular portal if decoupled from Nginx.
- **Azure SQL Database**: Managed, highly-available replacement for the local SQL Server container.
- **Azure OpenAI**: Target AI provider, configured dynamically via environment variables safely injected from **Azure Key Vault**.
- **Azure Application Insights**: Integrated monitoring platform collecting standard ASP.NET Core instrumentation and custom AI telemetry metadata.

## Infrastructure as Code

Bicep is the preferred IaC approach and will live under `infra/bicep/`. Future epics will automate the provisioning of the Azure resources using standard Bicep templates.

## CI/CD Pipeline

The current GitHub Actions workflow (`.github/workflows/ci.yml`) establishes the foundational quality gates:
- Unit test validation.
- Docker image build validation (`docker compose build`) to prevent broken containers from merging to `main`.
