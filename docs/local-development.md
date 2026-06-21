# Local Development

## Local Defaults

- Mock AI provider is the default.
- SQL Server is the local database target.
- Swagger is enabled only in development.
- Generated artifacts are drafts and require human review.

## Backend Commands

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln
dotnet test ArchitectureGovernance.sln
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

## Frontend Commands

```powershell
cd src/web/architecture-governance-portal
npm install
npm start
npm run build
npm test
```

## Useful URLs

- Web portal: `http://localhost:4200`
- API: `http://localhost:5080`
- Swagger: `http://localhost:5080/swagger`
- Liveness: `http://localhost:5080/health/live`
- Readiness: `http://localhost:5080/health/ready`

## Demo Flow

1. Create a project.
2. Add a requirement using one of the synthetic samples.
3. Submit the requirement for analysis.
4. Generate requirement analysis, HLD, LLD, ADR, NFR, security, API, or risk review artifacts.
5. Open the artifact viewer.
6. Export Markdown or record a review.
