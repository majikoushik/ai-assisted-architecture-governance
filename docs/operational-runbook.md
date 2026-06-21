# Operational Runbook

## Health Checks

- `/health/live`: process liveness.
- `/health/ready`: dependency readiness, including database readiness.
- `/api/v1/platform/readiness`: demo readiness and provider indicator.

## Common Issues

| Symptom | Likely cause | Action |
| --- | --- | --- |
| API cannot start | SQL Server unavailable or connection string invalid | Check Docker SQL health and connection string. |
| Artifact generation returns Azure configuration error | Azure OpenAI provider selected without endpoint, key, or deployment name | Switch to mock provider or configure secure values. |
| Frontend cannot call API | API URL or CORS mismatch | Confirm API is running and frontend environment points to the correct base URL. |
| Docker SQL unhealthy | Password policy or startup delay | Use `.env.example` password pattern and wait for health retries. |

## Logs

Use correlation ID to connect frontend errors, API logs, and AI provider telemetry metadata. Do not search for or expose full prompts, full requirements, secrets, or full AI responses.

## Incident Triage

1. Check `/health/live`.
2. Check `/health/ready`.
3. Review API logs for correlation ID and error summary.
4. Confirm provider selection.
5. Confirm database connectivity.
6. Escalate to security review if sensitive data exposure is suspected.
