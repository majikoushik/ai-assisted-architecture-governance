# Non-Functional Requirements

## Security

- Secrets must not be committed.
- Azure OpenAI keys, deployment names, and endpoints are configuration values.
- Azure OpenAI credentials must never be exposed to the Angular frontend.
- Future authentication should use Microsoft Entra ID with role-based authorization.
- Logs must exclude secrets, tokens, connection strings, full prompts, full requirements, and full AI responses by default.

## Availability and Reliability

- API exposes `/health/live` and `/health/ready`.
- Docker Compose provides local dependency startup ordering for SQL Server.
- Azure target uses Container Apps and Azure SQL, with future zone redundancy and backup strategy decisions documented as production scope.
- AI provider failures should produce safe failure responses and not crash the workflow.

## Performance and Scalability

- MVP load is modest and demo-oriented.
- Azure Container Apps provides scale-out direction.
- Artifact generation latency depends on provider selection and prompt size.
- Prompt templates should remain focused to control token usage and cost.

## Observability

- Every request should have a correlation ID.
- AI telemetry should capture provider, artifact type, prompt version, duration, status, and correlation ID.
- Application Insights and Log Analytics are the target Azure monitoring stack.
- Operational runbooks document health, logs, and incident triage.

## Maintainability

- Layered backend structure separates API, application, domain, infrastructure, AI providers, and building blocks.
- Prompt templates are source controlled.
- Tests cover domain status rules, application commands, API behavior, prompt loading, mock provider, Azure provider configuration readiness, and exception handling.

## Cost Awareness

- Mock provider is default for local and CI.
- Azure OpenAI use should be explicitly enabled and monitored.
- Azure SQL, Container Apps, Log Analytics, Application Insights, and OpenAI resources require environment-specific cost review.

## Responsible AI

- All generated artifacts are drafts.
- Prompts require assumptions, risks, open questions, and review notice.
- Public demos use synthetic requirements only.
- Production usage requires privacy, legal, security, compliance, and data governance review.
