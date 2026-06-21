# AI-Assisted Architecture Governance

An enterprise architecture governance accelerator that demonstrates how a Solution Architect can use AI-assisted engineering to turn business requirements into structured, reviewable, and version-controlled architecture artifacts.

This is not a chatbot demo. The repository is organized around requirement intake, architecture analysis, artifact generation, decision capture, risk review, prompt governance, and human review.

## Current Scope

- [x] Epic 0: Repository Foundation
- [x] Epic 1: Architecture Project Workspace
- [ ] Epic 2: Requirement Submission and Parsing.

## Responsible AI Notice

AI-generated artifacts are draft content. They must be reviewed by a qualified architect before use in production decisions. The system must not claim AI output is production-approved.

## Tech Stack

- Backend: ASP.NET Core Web API, C#, layered architecture.
- Frontend: Angular with strict TypeScript.
- AI: Mock provider by default, Azure OpenAI target provider through abstraction.
- Data target: SQL Server locally and Azure SQL for cloud alignment.
- Azure direction: Azure Static Web Apps, Azure Container Apps, Azure SQL, Azure OpenAI, Key Vault, Application Insights.

## Repository Structure

```text
src/
  api/
  application/
  domain/
  infrastructure/
  ai/
  building-blocks/
  web/
tests/
prompts/
samples/
architecture/
docs/
infra/
.github/workflows/
```

## Run Locally

Install the .NET 8 SDK, Node.js 20/22 LTS, and Angular dependencies first.

```powershell
dotnet run --project src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj
```

```powershell
cd src/web/architecture-governance-portal
npm install
npm start
```

## Build and Test

```powershell
dotnet restore ArchitectureGovernance.sln
dotnet build ArchitectureGovernance.sln --configuration Release
dotnet test ArchitectureGovernance.sln --configuration Release
```

```powershell
cd src/web/architecture-governance-portal
npm install
npm run build
npm test
```

## Documentation

- [Architecture overview](architecture/README.md)
- [Responsible AI architecture](architecture/responsible-ai-architecture.md)
- [Prompt engineering strategy](architecture/prompt-engineering-strategy.md)
- [Setup guide](docs/setup.md)
- [Roadmap](docs/roadmap.md)
