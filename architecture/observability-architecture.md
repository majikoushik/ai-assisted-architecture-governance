# Observability Architecture

## Foundation

The API includes health check readiness and correlation ID middleware.

## Future Telemetry

Use structured logs, request logging, Application Insights, Log Analytics, and AI interaction metadata.

## Required Metadata

Capture correlation ID, project ID, requirement ID, artifact ID, provider name, prompt template version, status, and error summary where relevant.

## Safety

Telemetry must avoid secrets and sensitive prompt content.
