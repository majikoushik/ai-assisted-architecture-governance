# Mock Output: Security Review

> This artifact is AI-assisted draft content and must be reviewed by a qualified architect before use in production decisions.

## Authentication

Use enterprise identity with OIDC/OAuth2. Borrower and employee experiences may require separate application registrations or policies.

## Authorization

Use RBAC for borrower, loan officer, supervisor, auditor, and admin roles. Enforce object-level authorization for loan application access.

## Sensitive Data Handling

Treat applicant identity, financial data, uploaded documents, and audit events as sensitive. Encrypt data at rest and in transit.

## Secret Management

Store database, storage, and provider credentials in Key Vault. Do not expose provider keys to the frontend.

## Risks

- Unauthorized document access.
- Overly broad employee permissions.
- Sensitive data leakage through logs.

## Open Questions

- What identity provider and conditional access rules apply?
- What retention and deletion rules apply to uploaded documents?
