# Non-Functional Requirements

## Scalability

Design for horizontal API scaling. The Azure Blueprint leverages **Azure Container Apps** for event-driven, serverless container scaling. Durable state is stored securely in **Azure SQL Database**.

## Availability

The Azure deployment relies on highly available managed services. Azure Container Apps handles container health-probe restarts, and Azure SQL provides automated backups. Future deployments should utilize revision-based rollout for zero-downtime updates.

## Performance

Prompt execution latency is tracked independently from internal API request latency via Application Insights. A configurable timeout (`TimeoutSeconds`) ensures the backend does not hang on slow AI model completions.

## Security

No secrets are committed. Future identity direction is Azure Entra ID with role-based access.

## Observability

Use correlation IDs, structured logs, health checks, Application Insights readiness, and safe AI interaction metadata.

## Maintainability

Keep boundaries clear, prompts versioned, and tests aligned to governance workflows.

## Cost Awareness

Track model usage metadata where available and use deterministic mock providers for local development and tests.
