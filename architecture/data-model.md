# Data Model

This document outlines the core entities in the SQL Server database.

## 1. ArchitectureProject

| Field | Type | Description |
|-------|------|-------------|
| `Id` | `UNIQUEIDENTIFIER` (PK) | Unique identifier for the project workspace. |
| `Name` | `NVARCHAR(200)` | The name of the project. |
| `BusinessDomain` | `NVARCHAR(100)` | The domain context. |
| `Description` | `NVARCHAR(MAX)` | Description of the project goals. |
| `Owner` | `NVARCHAR(100)` | Primary owner or sponsor. |
| `Status` | `NVARCHAR(50)` | `Draft`, `Active`, `UnderReview`, `Archived`. |
| `CreatedAt` | `DATETIMEOFFSET` | When the project was created. |
| `UpdatedAt` | `DATETIMEOFFSET` | When the project was last updated. |

## 2. RequirementSubmission

Represents the raw business need or technical requirements for AI-assisted analysis.

## 3. GeneratedArtifact

Represents an AI-assisted architecture artifact with type, version, status, prompt version, and review metadata.

| Field | Type | Description |
|-------|------|-------------|
| `Id` | `UNIQUEIDENTIFIER` (PK) | Unique identifier for the artifact. |
| `ProjectId` | `UNIQUEIDENTIFIER` (FK) | Links to ArchitectureProject. |
| `RequirementSubmissionId` | `UNIQUEIDENTIFIER` (FK) | Links to RequirementSubmission. |
| `ArtifactType` | `NVARCHAR(100)` | The type of the artifact. |
| `Title` | `NVARCHAR(200)` | The title of the artifact. |
| `MarkdownContent` | `NVARCHAR(MAX)` | The AI-generated markdown content. |
| `Version` | `NVARCHAR(50)` | Version string. |
| `Status` | `NVARCHAR(50)` | `Draft`, `Reviewed`, `Approved`. |
| `ProviderName` | `NVARCHAR(100)` | The AI provider used. |
| `PromptTemplateName` | `NVARCHAR(100)` | The name of the prompt template used. |
| `PromptTemplateVersion` | `NVARCHAR(50)` | The version of the prompt template used. |
| `CreatedAt` | `DATETIMEOFFSET` | When the artifact was generated. |
| `UpdatedAt` | `DATETIMEOFFSET` | When the artifact was last updated. |

## PromptTemplate

Represents a versioned prompt template.

## AIInteractionLog

Stores safe metadata about provider calls. It must not store secrets or full sensitive prompts.

## ReviewRecord

Captures human review status and comments for generated artifacts.
