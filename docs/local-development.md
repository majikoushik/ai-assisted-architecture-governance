# Local Development

The local default AI provider is `Mock`, so no Azure OpenAI credentials are required.

## API

Run the ASP.NET Core API and check:

- `GET /health`
- `GET /api/v1/platform/readiness`

## Portal

Run the Angular app from `src/web/architecture-governance-portal`.

## Secrets

Use `.env` or user secrets locally when future epics introduce sensitive settings. Commit only `.env.example`.
