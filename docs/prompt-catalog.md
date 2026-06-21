# Prompt Catalog

Prompt templates live under `prompts/` and are loaded by the backend at runtime. They are versioned architecture assets and should be reviewed like code.

## Template Standard

Each prompt should include:

- Title.
- Version.
- Purpose.
- Expected input.
- Instructions.
- Output format.
- Constraints.
- Anti-hallucination guidance.
- Assumptions.
- Risks.
- Open questions.
- Human review notice.

## Current Templates

| File | Artifact |
| --- | --- |
| `requirement-analysis.md` | Requirement analysis |
| `hld-generation.md` | High-level design |
| `lld-generation.md` | Low-level design |
| `adr-generation.md` | ADR candidates |
| `nfr-review.md` | NFR checklist |
| `security-review.md` | Security review |
| `api-contract-review.md` | API contract review |
| `risk-assumption-review.md` | Risk and assumption review |

## Review Rules

- Do not ask the model to invent facts.
- Do not claim outputs are production-approved.
- Keep outputs structured and reviewable.
- Ask for assumptions, risks, and stakeholder questions.
- Keep model-facing instructions free from secrets.
