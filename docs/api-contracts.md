# API Contracts

This document defines the REST API contracts for the backend system.

## 1. Projects API

### 1.1 Create Project

`POST /api/v1/projects`

**Request:**

```json
{
  "name": "Core Banking Modernization",
  "businessDomain": "Retail Banking",
  "description": "Modernization of legacy mainframe core banking system to microservices.",
  "owner": "Jane Doe"
}
```

**Response (201 Created):**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Core Banking Modernization",
  "businessDomain": "Retail Banking",
  "description": "Modernization of legacy mainframe core banking system to microservices.",
  "owner": "Jane Doe",
  "status": "Draft",
  "createdAt": "2026-06-21T00:00:00Z",
  "updatedAt": null
}
```

### 1.2 Get Projects

`GET /api/v1/projects`

**Response (200 OK):**

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Core Banking Modernization",
    "businessDomain": "Retail Banking",
    "description": "Modernization of legacy mainframe core banking system to microservices.",
    "owner": "Jane Doe",
    "status": "Draft",
    "createdAt": "2026-06-21T00:00:00Z",
    "updatedAt": null
  }
]
```

### 1.3 Get Project by ID

`GET /api/v1/projects/{projectId}`

**Response (200 OK):**

Returns the same object as the Create response.

### 1.4 Update Project

`PUT /api/v1/projects/{projectId}`

**Request:**

```json
{
  "name": "Core Banking Modernization V2",
  "businessDomain": "Retail Banking",
  "description": "Modernization of legacy mainframe.",
  "owner": "Jane Doe"
}
```

**Response (204 No Content)**

### 1.5 Update Project Status

`PATCH /api/v1/projects/{projectId}/status`

**Request:**

```json
{
  "status": "Active"
}
```

**Response (200 OK):**

Returns the updated project object.

---

## 2. Artifacts API

### 2.1 Generate Artifact

`POST /api/v1/artifacts/generate`

**Request:**

```json
{
  "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "requirementSubmissionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "artifactType": "RequirementAnalysis" // or "HighLevelDesign", "LowLevelDesign", "ArchitectureDecisionRecord", "NonFunctionalRequirementReview", "SecurityReview", "ApiContractReview", "RiskAndAssumptionReview"
}
```

**Response (200 OK):**

Returns the generated `Artifact` object.

### 2.2 Get Artifact by ID

`GET /api/v1/artifacts/{artifactId}`

**Response (200 OK):**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "requirementSubmissionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "artifactType": "RequirementAnalysis",
  "title": "Project - Requirement Analysis",
  "markdownContent": "# Draft...",
  "version": "1.0.0",
  "status": "Draft",
  "providerName": "MockProvider",
  "promptTemplateName": "requirement-analysis",
  "promptTemplateVersion": "1.0.0",
  "createdAt": "2026-06-21T00:00:00Z"
}
```

### 2.3 Get Artifacts by Project

`GET /api/v1/projects/{projectId}/artifacts`

### 2.4 Get Artifacts by Requirement

`GET /api/v1/requirements/{requirementId}/artifacts`

### 2.5 Export Artifact as Markdown

`GET /api/v1/artifacts/{artifactId}/export/markdown`

Returns `text/markdown` file download.

### 2.6 Get Artifact Versions

`GET /api/v1/artifacts/{artifactId}/versions`

**Response (200 OK):**
Returns a list of all historical versions of the given artifact, sorted descending by version number.

### 2.7 Update Artifact Status

`PATCH /api/v1/artifacts/{artifactId}/status`

**Request:**

```json
{
  "status": "Reviewed",
  "reason": "Initial manual review",
  "updatedBy": "John Doe"
}
```

**Response (200 OK):**
Returns a boolean indicating success.

---

## 3. Reviews API

### 3.1 Get Reviews for Artifact

`GET /api/v1/artifacts/{artifactId}/reviews`

**Response (200 OK):**

```json
[
  {
    "id": "1fa85f64-5717-4562-b3fc-2c963f66afa6",
    "artifactId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "reviewerName": "Jane Doe",
    "status": "NeedsReview",
    "comments": "The HLD looks good but is missing the security section.",
    "createdAt": "2026-06-21T10:00:00Z"
  }
]
```

### 3.2 Create Review

`POST /api/v1/artifacts/{artifactId}/reviews`

**Request:**

```json
{
  "reviewerName": "Jane Doe",
  "reviewStatus": "NeedsReview",
  "comments": "The HLD looks good but is missing the security section."
}
```

**Response (201 Created):**
Returns the created review object.

---

The API is version-ready under `/api/v1`.

Current foundation endpoint:

```http
GET /api/v1/platform/readiness
```

Future endpoints will cover projects, requirements, artifacts, prompt templates, reviews, and AI interaction metadata.

Errors should use Problem Details and include a correlation ID where possible.
