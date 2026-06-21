# Responsible AI Architecture

## Human Review Model

AI-generated architecture artifacts are drafts. A qualified architect must review, revise, and approve output before it influences production decisions.

## AI Output Limitations

The system must not present model output as authoritative. Artifacts must separate facts, recommendations, assumptions, risks, and open questions.

## Sensitive Data Guidance

Users should avoid submitting confidential or regulated information unless approved controls are in place. The system must not log secrets or full sensitive prompts.

## Prompt Safety

Prompt templates instruct the model to avoid unsupported facts, identify assumptions, and ask clarifying questions.

## Provider Abstraction

The mock provider supports safe local development. Azure OpenAI is planned through configuration and managed identity readiness.
