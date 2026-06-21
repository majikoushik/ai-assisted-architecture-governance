# Data Model

## Core Entities

| Entity | Purpose |
| --- | --- |
| `ArchitectureProject` | Governance workspace for one initiative or solution. |
| `RequirementSubmission` | Business or technical requirement submitted for analysis. |
| `GeneratedArtifact` | Versioned AI-assisted architecture artifact. |
| `PromptTemplate` | Source-controlled prompt metadata loaded from Markdown. |
| `ReviewRecord` | Human review status and comments for an artifact. |

## ArchitectureProject

Key fields include project ID, name, business domain, description, owner, status, created timestamp, and updated timestamp.

## RequirementSubmission

Key fields include requirement ID, project ID, title, requirement text, domain context, submitter, expected artifact types, status, and timestamps.

## GeneratedArtifact

Key fields include artifact ID, project ID, requirement submission ID, artifact type, title, Markdown content, version, status, provider name, prompt template name, prompt template version, correlation ID, and timestamps.

## ReviewRecord

Key fields include review ID, artifact ID, reviewer name, review status, comments, and created timestamp.

## Traceability

Generated artifacts connect requirement input, prompt template, prompt version, AI provider, generated Markdown, review status, and export workflow. This traceability is central to the architecture governance value proposition.
