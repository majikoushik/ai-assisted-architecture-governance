# Testing Strategy

## Current Test Areas

- Domain review status rules.
- Project creation command handling.
- Artifact generation command handling.
- Mock AI provider output and metadata.
- Azure OpenAI provider configuration readiness without real credentials.
- Global exception handling behavior.
- Angular component smoke tests for core screens.

## Commands

Backend:

```powershell
dotnet test ArchitectureGovernance.sln --configuration Release
```

Frontend:

```powershell
cd src/web/architecture-governance-portal
npm test
```

Docker:

```powershell
docker compose build
docker compose config
```

Bicep:

```powershell
az bicep build --file infra/bicep/main.bicep
```

## Test Data Policy

Use synthetic data only. Tests and samples must not contain real customer requirements, production API keys, real tenant IDs, confidential URLs, or proprietary architecture content.

## Future Coverage

- API integration tests for all core endpoints.
- Prompt template schema/section validation.
- Golden-output tests for deterministic mock artifacts.
- Frontend service tests for error handling and correlation ID interceptor behavior.
- Bicep validation in CI.
