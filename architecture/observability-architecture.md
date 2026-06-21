# Observability Architecture

## Foundation

The API includes health check readiness and correlation ID middleware.

## Telemetry Strategy (Azure Target)

The platform is designed to natively push telemetry into **Azure Application Insights** and **Azure Log Analytics**.

- **Application Insights:** Integrated via `Microsoft.ApplicationInsights.AspNetCore`. Captures incoming HTTP requests, dependency calls (SQL Server, Azure OpenAI), exceptions, and trace logs.
- **Log Analytics:** Acts as the central sink for Azure Container Apps system logs, stdout/stderr container logs, and the backing store for Application Insights workspaces.
- **Correlation:** Every request traversing the API boundary generates or passes an `X-Correlation-ID`. This ID is injected into every structured log event.

## Required Custom Metadata (Safe AI Telemetry)

We track AI interaction health without compromising data privacy. The custom AI Provider telemetry captures:
- Correlation ID
- Active Provider Name (`Mock` or `AzureOpenAI`)
- Prompt Template Version
- Artifact Type generated
- Generation Duration (ms)
- Status (Success/Failed)

## Safety

Telemetry must avoid secrets and sensitive prompt content.
