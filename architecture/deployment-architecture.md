# Deployment Architecture

## Local Foundation

Docker Compose is prepared for API, web, and SQL Server containers.

## Azure Direction

- Azure Static Web Apps for the Angular portal.
- Azure Container Apps for the API.
- Azure Container Registry for container images.
- Azure SQL Database for persistence.
- Azure OpenAI for production AI capabilities.
- Azure Key Vault and Managed Identity for secrets and access.
- Application Insights and Log Analytics for monitoring.

## Infrastructure as Code

Bicep is the preferred IaC approach and will live under `infra/bicep/`.
