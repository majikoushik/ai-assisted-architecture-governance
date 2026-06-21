# Operational Runbook: AI-Assisted Architecture Governance

This runbook outlines operational procedures, health check mechanics, telemetry setup, and troubleshooting guides for the AI-Assisted Architecture Governance platform.

## 1. System Architecture & Observability
The system is built on an enterprise-grade ASP.NET Core API and an Angular frontend, primarily targeting Azure hosting (Container Apps, SQL Database, App Insights, Key Vault).

### Observability Features
- **Correlation IDs:** Every request is assigned a `X-Correlation-ID`. This ID is logged by the API in all structural logs and returned to the client in success and error responses.
- **Problem Details:** All HTTP errors use RFC 7807 Problem Details.
- **AI Telemetry:** AI Provider calls (Mock and Azure OpenAI) are tracked with exact execution duration and prompt versioning metadata. Sensitive data is scrubbed.
- **Application Insights:** Integrated natively using `Microsoft.ApplicationInsights.AspNetCore`.

## 2. Health Checks

The backend provides built-in ASP.NET Core Health Checks under `/health/*` routes.

### Liveness Probe
- **URL:** `GET /health/live`
- **Purpose:** Confirms the application process is running and responding to HTTP requests.
- **Expected Status:** `200 OK` (Healthy)

### Readiness Probe
- **URL:** `GET /health/ready`
- **Purpose:** Verifies that the application can accept traffic, ensuring critical dependencies like the Azure SQL database are reachable.
- **Expected Status:** `200 OK` (Healthy)

### Platform Metadata
- **URL:** `GET /api/v1/platform/readiness`
- **Purpose:** Exposes environment context (e.g., Development/Production) and the active AI Provider (`MockDeterministicProvider` vs `AzureOpenAI`).

## 3. Telemetry & Logs
Logs are emitted via `Microsoft.Extensions.Logging` and are forwarded to Application Insights when hosted in Azure.

### Safe AI Telemetry
When an artifact is generated, a specific log event is emitted containing:
- `ProviderName`
- `ArtifactType`
- `PromptTemplateName` & `PromptTemplateVersion`
- `DurationMs`
- `CorrelationId`
- `Status` (Success/Failed)

*Rule: Raw user inputs and generated contents are deliberately excluded from these logs to prevent PII/Confidentiality leakage.*

## 4. Troubleshooting Guide

### Issue: Backend returns 500 Internal Server Error
1. Extract the `X-Correlation-ID` from the response headers or the Problem Details payload.
2. Query Application Insights or local console logs filtering by `CorrelationId`.
3. Locate the stack trace mapped by the `GlobalExceptionHandler`.

### Issue: AI Artifact Generation fails constantly
1. Check `/api/v1/platform/readiness` to determine the active provider.
2. If `AzureOpenAI`:
   - Verify `AzureOpenAi:Endpoint` and `AzureOpenAi:ApiKey` are correct in the environment or Key Vault.
   - Verify network access from the Container App to the Azure OpenAI VNet.
   - Check if Azure OpenAI rate limits have been exceeded in Application Insights metrics.
3. If `Mock`:
   - Verify the local application isn't suffering from resource exhaustion.

### Issue: Readiness check fails (Unhealthy)
1. The primary cause is typically a database connection failure.
2. Ensure the connection string `DefaultConnection` is valid.
3. Verify firewall rules allow access to the database from the API host.

## 5. Deployment Procedures
*See `azure-deployment-guide.md` for specific infrastructure deployment commands using Bicep or Azure CLI.*
