# DevOps Guide

## CI Workflow

`.github/workflows/ci.yml` validates:

- .NET restore, build, and tests.
- Angular dependency install, production build, and tests.
- Docker Compose build.

## Local Build Commands

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln --configuration Release
dotnet test ArchitectureGovernance.sln --configuration Release
```

```powershell
cd src/web/architecture-governance-portal
npm install
npm run build:prod
npm test
```

## Docker Commands

```powershell
copy .env.example .env
docker compose config
docker compose build
docker compose up
```

## Deployment Templates

The Azure deployment workflow is intentionally a template. Before enabling deployment, configure approved environment secrets, review resource names, validate Bicep, and confirm cost controls.

## CI AI Provider Policy

CI should use the mock provider. Azure OpenAI integration tests must not require real credentials by default.
