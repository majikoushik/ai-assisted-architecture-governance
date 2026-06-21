# Azure Deployment Diagram

```mermaid
flowchart TB
    Dev["Developer / GitHub Actions"] --> ACR["Azure Container Registry"]
    Dev --> SWA["Azure Static Web Apps"]

    subgraph Azure["Azure Subscription"]
        ACR --> ACA["Azure Container Apps\nBackend API"]
        SWA --> Browser["User Browser"]
        Browser --> SWA
        SWA --> ACA
        ACA --> SQL["Azure SQL Database"]
        ACA --> KV["Azure Key Vault"]
        ACA -. optional .-> AOAI["Azure OpenAI"]
        ACA --> AI["Application Insights"]
        AI --> LAW["Log Analytics Workspace"]
        ACA --> LAW
    end

    KV --> ACA
```

The Bicep blueprint models the target Azure topology. Production deployment still requires environment-specific networking, identity, security, cost, and compliance review.
