# DevOps and Local Development Guide

This guide describes how to run, test, and deploy the AI-Assisted Architecture Governance platform. The repository is designed to be fully containerized, adhering to "12-factor app" principles for Azure-ready deployments.

---

## 1. Local Development without Docker

For rapid iteration, you can run the services directly on your host machine.

### Backend (API)
Ensure you have the **.NET 8 SDK** installed.

```bash
# Restore packages
dotnet restore ArchitectureGovernance.sln

# Run tests
dotnet test ArchitectureGovernance.sln

# Start the API
cd src/api/ArchitectureGovernance.Api
dotnet run
```
*The API will start at `http://localhost:5080`. Entity Framework Core migrations will apply automatically on startup.*

### Frontend (Angular Portal)
Ensure you have **Node.js (v22)** installed.

```bash
cd src/web/architecture-governance-portal
npm install
npm start
```
*The portal will start at `http://localhost:4200`.*

---

## 2. Local Development with Docker Compose

To test the application exactly as it would run in production (with Nginx and a containerized SQL Server), use Docker Compose.

### Setup Environment Variables
1. Copy the example environment file:
   ```bash
   cp .env.example .env
   ```
2. (Optional) Edit `.env` to configure Azure OpenAI if you are testing the production AI provider. By default, `AI_PROVIDER=Mock` is used for deterministic local testing.

### Start the Platform
```bash
docker compose up --build
```
This single command will:
1. Start the SQL Server container (`sqlserver`).
2. Build and start the .NET API container (`api`), which automatically applies database migrations.
3. Build the Angular SPA, pack it into an Nginx container (`web`), and reverse-proxy `/api` to the backend.

**Access the application at:** `http://localhost:4200`

### Stop the Platform
```bash
docker compose down
```
*Note: SQL Server data is persisted in a named volume (`sqlserver-data`). Add `-v` to remove volumes.*

---

## 3. Environment Variables Configuration

The application is configured exclusively via environment variables to prevent accidental commit of secrets.

| Variable | Description | Default / Example |
|---|---|---|
| `SQLSERVER_SA_PASSWORD` | Password for local SQL Server container | `Change_this_local_password_only_123!` |
| `AI_PROVIDER` | AI backend to use. Options: `Mock`, `AzureOpenAI` | `Mock` |
| `AZURE_OPENAI_ENDPOINT` | Azure OpenAI resource URL | `https://your-resource.openai.azure.com/` |
| `AZURE_OPENAI_KEY` | Key for Azure OpenAI | (Empty) |
| `AZURE_OPENAI_DEPLOYMENT_NAME` | Deployment name for completion | (Empty) |
| `API_BASE_URL` | Used by frontend Nginx proxy | `http://api:8080` (Docker default) |

**Important:** Never commit `.env` containing real Azure OpenAI secrets.

---

## 4. Continuous Integration (CI)

We use GitHub Actions (`.github/workflows/ci.yml`) as our CI pipeline. The pipeline triggers on pushes and PRs to `main`.

It includes three parallelized/sequential jobs:
1. **Backend**: Restores, builds, and runs `dotnet test`.
2. **Frontend**: Installs Node dependencies, builds production assets, and runs `npm test`.
3. **Docker Validation**: Runs `docker compose build` to ensure all `Dockerfile` configurations are syntax-error free and images package correctly.

---

## 5. Path to Azure Deployment (Readiness)

This Docker setup maps directly to our Azure target state:
1. **Azure Container Registry (ACR)**: Built images (`api`, `web`) will be published here.
2. **Azure Container Apps (ACA)**: The `api` and `web` containers will be deployed as Container Apps, inheriting environment variables securely from **Azure Key Vault**.
3. **Azure SQL Database**: Will replace the local `sqlserver` container.
4. **Azure Application Insights**: Already integrated via the Observability Building Block to capture container telemetry.

---

## 6. Troubleshooting

- **Database Connection Failures**: Ensure SQL Server is fully healthy. The `docker-compose.yml` uses a healthcheck, so the `api` container waits automatically.
- **Frontend Nginx 502 Bad Gateway**: This means Nginx cannot reach the backend API. Ensure the `api` service is running and `API_BASE_URL` matches the internal Docker DNS (`http://api:8080`).
- **Tests Failing in CI**: The Angular tests use Headless Chrome. Ensure you are not relying on local browser plugins.
