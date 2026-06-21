# Azure OpenAI Provider

## Purpose

The Azure OpenAI provider demonstrates production-readiness through an `IArchitectureAiProvider` implementation. It is optional and not required for local development.

## Local Default

```text
AI_PROVIDER=Mock
```

## Optional Configuration

```text
AI_PROVIDER=AzureOpenAI
AZURE_OPENAI_ENDPOINT=https://<resource>.openai.azure.com/
AZURE_OPENAI_DEPLOYMENT_NAME=<deployment-name>
AZURE_OPENAI_KEY=<secret>
```

Use secure local secrets, CI/CD secrets, or Azure Key Vault. Do not commit real values.

## Safety Behavior

If Azure OpenAI configuration is missing, the provider returns a safe failure artifact rather than requiring credentials or crashing local demos.

## Production Direction

Production deployment should prefer Managed Identity and Key Vault, private networking where required, content safety controls, model evaluation, cost alerts, and data governance approval.
