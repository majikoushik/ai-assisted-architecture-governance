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

The API is version-ready under `/api/v1`.

Current foundation endpoint:

```http
GET /api/v1/platform/readiness
```

Future endpoints will cover projects, requirements, artifacts, prompt templates, reviews, and AI interaction metadata.

Errors should use Problem Details and include a correlation ID where possible.
