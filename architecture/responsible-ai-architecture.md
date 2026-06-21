# Responsible AI Architecture

## Positioning

The platform uses AI to assist architects, not to replace them. Generated artifacts are draft content intended to accelerate review, discussion, and documentation. A qualified architect must review outputs before they influence production decisions.

## Human-in-the-Loop Model

- AI generates draft artifacts from submitted requirements and source-controlled prompts.
- Artifacts are persisted with draft status and review metadata.
- Reviewers record status and comments.
- Approval in the app means accepted for the demo workflow, not enterprise production approval.

## Prompt Safety Controls

Prompt templates instruct the model to:

- Separate known facts from assumptions.
- Mark missing context as open questions.
- Avoid inventing external facts.
- Avoid claiming production certainty.
- Include assumptions, risks, and human review notice.
- Treat security and API reviews as advisory drafts.

## Data Privacy Guidance

- Public demos should use synthetic requirements.
- Real confidential business requirements should not be entered into a non-approved environment.
- Full prompts, full requirement text, and full AI responses should not be logged by default.
- AI provider secrets and connection strings must never be logged.

## Provider Abstraction

`IArchitectureAiProvider` decouples artifact generation from provider implementation. The mock provider is deterministic and works locally without credentials. Azure OpenAI is optional and configuration-driven.

## Safe Telemetry

Allowed metadata:

- Provider name.
- Artifact type.
- Prompt template name and version.
- Duration.
- Status.
- Correlation ID.

Excluded by default:

- Full prompts.
- Full requirement text.
- Full AI responses.
- Secrets, keys, tokens, and connection strings.

## Production Readiness Caveats

Production use requires formal data classification, privacy review, prompt injection controls, model evaluation, monitoring, access control, retention policy, and enterprise compliance approval.
