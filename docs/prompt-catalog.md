# Prompt Catalog

The Architecture Governance platform maintains a versioned catalog of prompts used to generate architecture artifacts. These prompts are stored as Markdown files in the `prompts/` directory.

## Core Principles
1. **Version Control**: Every prompt must specify its version (e.g. `v1.0.0`). This is crucial for traceability and reproducibility.
2. **Deterministic Fallback**: For local development and testing, a `MockArchitectureAiProvider` is used to provide deterministic, realistic outputs without requiring an active Azure OpenAI subscription.
3. **Structured Outputs**: Prompts mandate a specific Markdown format separating facts, assumptions, risks, and open questions.
4. **Human-in-the-Loop**: Every prompt explicitly includes a Human Review Notice.

## How Prompts are Loaded
During application startup or when an API request is made to `/api/v1/prompts`, the `FilePromptRepository` scans the `prompts/` directory. It parses the Markdown files to extract:
- Name (from the H1 header)
- Version (from the `Version:` block)
- Purpose (from the `## Purpose` section)

This catalog is then exposed to the Angular frontend via the `PromptsController`.

## Mock AI Provider
The Mock AI Provider (`ArchitectureGovernance.AI.Mock.MockArchitectureAiProvider`) is the default local implementation of `IArchitectureAiProvider`.
- It returns realistic Markdown specific to the `ArtifactType` requested.
- It is purely deterministic and does not make external HTTP calls.
- It includes the standard AI Human Review Notice in every generated output.
