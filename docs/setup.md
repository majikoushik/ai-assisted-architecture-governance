# Setup Guide

## Prerequisites

- .NET 8 SDK.
- Node.js 20, 21, or 22.
- Docker Desktop for Docker Compose and SQL Server local container.
- Azure CLI for optional Bicep validation.

## Clone and Restore

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln
```

## Frontend Dependencies

```powershell
cd src/web/architecture-governance-portal
npm install
```

## Environment

Copy the safe local template:

```powershell
copy .env.example .env
```

Keep the local AI provider as:

```text
AI_PROVIDER=Mock
```

Azure OpenAI is optional. Do not place real secrets in source control.

## Run Locally

API:

```powershell
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

Web:

```powershell
cd src/web/architecture-governance-portal
npm start
```

Docker:

```powershell
docker compose build
docker compose up
```
