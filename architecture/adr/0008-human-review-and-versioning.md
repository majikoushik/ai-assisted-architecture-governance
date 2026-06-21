# ADR-0008: Human Review and Artifact Versioning

## Status

Accepted

## Context

AI-generated architecture artifacts must not be treated as final. The platform needs visible human review and versioning so draft outputs can be iterated safely.

## Decision

Generated artifacts will be versioned per requirement and artifact type. Review records will capture reviewer name, review status, comments, and timestamps.

## Consequences

- Users can distinguish draft versions.
- Human review is part of the workflow.
- Approval remains a demo workflow status, not production architecture board approval.
- Future work can add diff views and formal approval policies.

## Alternatives Considered

- Overwriting artifacts in place: rejected because it destroys traceability.
- Treating AI output as final: rejected because it violates responsible AI and architecture governance principles.
