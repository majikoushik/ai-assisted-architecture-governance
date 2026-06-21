# System Context Diagram

```mermaid
flowchart LR
    Architect["Solution Architect"]
    Reviewer["Architecture Reviewer"]
    Engineer["Engineering Lead"]

    Portal["Architecture Governance Portal"]
    Api["Architecture Governance API"]
    PromptCatalog["Versioned Prompt Catalog"]
    MockProvider["Mock AI Provider"]
    AzureOpenAI["Azure OpenAI Provider"]
    Database["SQL Server / Azure SQL"]
    Export["Markdown Export"]
    Observability["Logs, Health Checks, AI Telemetry Metadata"]

    Architect --> Portal
    Reviewer --> Portal
    Engineer --> Portal
    Portal --> Api
    Api --> PromptCatalog
    Api --> MockProvider
    Api -. optional .-> AzureOpenAI
    Api --> Database
    Api --> Export
    Api --> Observability

    classDef person fill:#f5f7fa,stroke:#5f6f7d,color:#17202a;
    classDef system fill:#e8f2f4,stroke:#16697a,color:#17202a;
    classDef external fill:#fff7e6,stroke:#b75d69,color:#17202a;

    class Architect,Reviewer,Engineer person;
    class Portal,Api,PromptCatalog,Database,Export,Observability system;
    class MockProvider,AzureOpenAI external;
```

The platform sits between architecture stakeholders and AI providers. It turns requirements into governed draft artifacts while preserving prompt versioning, traceability, review status, and safe telemetry boundaries.
