# Mock Output: Low-Level Design

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Component Responsibilities

- `LoanApplicationsController`: Accepts and retrieves loan applications.
- `CreateLoanApplicationHandler`: Validates input and creates application records.
- `DocumentUploadService`: Stores document metadata and routes file content to storage.
- `ApprovalWorkflowService`: Handles state transitions and reviewer assignment.
- `AuditEventWriter`: Records safe audit metadata.

## API Boundaries

```text
POST /api/v1/loan-applications
GET  /api/v1/loan-applications/{id}
POST /api/v1/loan-applications/{id}/documents
POST /api/v1/loan-applications/{id}/workflow-actions
GET  /api/v1/loan-applications/{id}/audit-events
```

## Validation Rules

- Application amount must be positive.
- Required applicant identity fields must be present.
- Uploaded document type must be supported.
- Workflow action must be valid for the current status.

## Error Handling

- Use Problem Details.
- Include correlation ID.
- Avoid exposing internal exception details.

## Assumptions

- File upload scanning is handled by a downstream service.

## Risks

- Document metadata and content storage can drift without transactional consistency patterns.

## Open Questions

- What document types are mandatory for each loan product?
