# Deployment

## Local

Use direct tooling for development:

```powershell
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
cd src/web/architecture-governance-portal
npm start
```

## Docker Compose

```powershell
copy .env.example .env
docker compose build
docker compose up
```

## Azure Blueprint

Bicep files are under `infra/bicep/`. Validate syntax with:

```powershell
az bicep build --file infra/bicep/main.bicep
```

The blueprint models the target deployment but is not automatically deployed by this repository without environment-specific parameters and approved secrets.

## Cleanup

For Docker:

```powershell
docker compose down
docker volume rm ai-assisted-architecture-governance_sqlserver-data
```

For Azure demos, delete the resource group after validation to avoid ongoing cost.
