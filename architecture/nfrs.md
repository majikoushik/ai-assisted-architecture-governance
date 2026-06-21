# Non-Functional Requirements

## Scalability

Design for horizontal API scaling and stateless compute. Store durable state in SQL Server or Azure SQL.

## Availability

Future Azure deployment should use managed services, health checks, and deployment slots or revision-based rollout where practical.

## Performance

Prompt execution latency should be monitored separately from API request latency.

## Security

No secrets are committed. Future identity direction is Azure Entra ID with role-based access.

## Observability

Use correlation IDs, structured logs, health checks, Application Insights readiness, and safe AI interaction metadata.

## Maintainability

Keep boundaries clear, prompts versioned, and tests aligned to governance workflows.

## Cost Awareness

Track model usage metadata where available and use deterministic mock providers for local development and tests.
