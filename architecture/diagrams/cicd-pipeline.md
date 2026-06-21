# CI/CD Pipeline Diagram

```mermaid
flowchart LR
    Push["Push / Pull Request"] --> Checkout["Checkout"]
    Checkout --> Dotnet[".NET restore, build, test"]
    Checkout --> Node["npm install, Angular build, tests"]
    Dotnet --> Docker["Docker Compose build validation"]
    Node --> Docker
    Docker --> Bicep["Optional Bicep build validation"]
    Bicep --> Publish["Future: publish images and deploy to Azure"]
```

The current CI validates backend, frontend, and Docker build readiness. Azure deployment workflows are intentionally blueprint-oriented and require environment secrets before use.
