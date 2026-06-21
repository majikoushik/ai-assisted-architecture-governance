# Setup

## Prerequisites

- .NET 8 SDK
- Node.js 20 LTS or 22 LTS
- npm
- Docker Desktop for container-based local development
- SQL Server container support for later data epics

## Backend

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

Swagger is available in Development once the API is running.

## Frontend

```powershell
cd src/web/architecture-governance-portal
npm install
npm start
```

## Configuration

Use environment variables or local development settings. Do not commit real secrets.
