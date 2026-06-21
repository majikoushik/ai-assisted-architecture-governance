# Prompt Engineering Strategy

## Strategy

Prompt templates are treated as governed architecture assets. They are stored in the repository, versioned with code, exposed through the prompt catalog, and referenced by generated artifacts.

## Prompt Template Standard

Each prompt template includes:

- Title and version.
- Purpose.
- Expected input.
- Role and task instructions.
- Output format.
- Constraints.
- Anti-hallucination guidance.
- Assumptions, risks, and open questions sections.
- Human review notice.

## Catalog

| Prompt | Artifact |
| --- | --- |
| `requirement-analysis.md` | Requirement analysis |
| `hld-generation.md` | High-level design |
| `lld-generation.md` | Low-level design |
| `adr-generation.md` | Architecture decision records |
| `nfr-review.md` | Non-functional requirements review |
| `security-review.md` | Security review |
| `api-contract-review.md` | API contract review |
| `risk-assumption-review.md` | Risk and assumption review |

## Hallucination Reduction

Prompts require the model to:

- Use only provided requirement context.
- Identify assumptions explicitly.
- Ask open questions for missing information.
- Avoid invented regulatory, integration, SLA, or organizational facts.
- Distinguish recommendation from fact.

## Evaluation Readiness

Future evaluation can compare generated artifacts against golden outputs for:

- Required section presence.
- Human review notice presence.
- Assumption/risk/open question quality.
- No production-approval claims.
- No obvious prompt safety violations.
