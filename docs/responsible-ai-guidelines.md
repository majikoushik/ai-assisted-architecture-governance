# Responsible AI Guidelines

This platform generates architecture governance artifacts using AI. Due to the high stakes of architectural decision-making, the following Responsible AI (RAI) guidelines are strictly enforced across the codebase.

## 1. Human-in-the-Loop is Mandatory
AI-generated content is **draft content only**. 
The system does not possess final authority over system design, security compliance, or deployment readines. Every generated artifact—whether from the mock provider or Azure OpenAI—contains a prominent notice:
> *This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.*

## 2. Deterministic Local Development
To prevent accidental data leakage during local testing and to allow offline development, the default AI provider is the **Mock AI Provider**.
- It does not require real API keys or an internet connection.
- It generates deterministic, safe mock outputs.
- No sensitive requirement text is ever transmitted outside of the local process while using the mock provider.

## 3. Data Privacy and Confidentiality
- **No Secrets**: Never commit real API keys or secrets to the repository.
- **No Sensitive Logging**: Full requirement text submitted by users must **not** be logged in application logs (e.g., Serilog or Application Insights) to prevent PII/confidential data spills.

## 4. Mitigating Hallucinations
- Prompt templates are specifically engineered to instruct the model to separate **Facts** from **Assumptions**.
- If context is missing, the model is instructed to list items under **Open Questions** rather than inventing a system design.
- The UI surfaces these sections prominently so human reviewers can easily spot and validate the AI's assumptions.
