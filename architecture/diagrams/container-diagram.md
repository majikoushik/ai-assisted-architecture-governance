# Container Diagram

```mermaid
flowchart TB
    subgraph Client["Client"]
        Browser["Browser"]
        Angular["Angular Portal"]
    end

    subgraph Backend["ASP.NET Core API"]
        Controllers["Thin Controllers"]
        Application["Application Layer\nCommands, Queries, Validators"]
        Domain["Domain Layer\nProjects, Requirements, Artifacts, Reviews"]
        Infra["Infrastructure Layer\nEF Core, Prompt Repository, Provider Wiring"]
        Observability["Observability Middleware\nCorrelation ID, Errors, Request Logs"]
    end

    subgraph Data["Data and Assets"]
        Sql["SQL Server / Azure SQL"]
        Prompts["Markdown Prompt Templates"]
    end

    subgraph AI["AI Providers"]
        Mock["Mock Deterministic Provider"]
        Azure["Azure OpenAI Provider"]
    end

    Browser --> Angular
    Angular --> Controllers
    Controllers --> Observability
    Controllers --> Application
    Application --> Domain
    Application --> Infra
    Infra --> Sql
    Infra --> Prompts
    Infra --> Mock
    Infra -. configuration-driven .-> Azure
```

The API preserves layered boundaries. Controllers coordinate HTTP concerns, the application layer owns use cases, the domain layer owns governance concepts, and infrastructure owns persistence, prompt loading, and AI provider adapters.
