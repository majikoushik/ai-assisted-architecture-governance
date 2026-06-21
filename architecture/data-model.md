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

## 2. RequirementSubmission (Future)r technical requirements for AI-assisted analysis.

## GeneratedArtifact

Represents an AI-assisted architecture artifact with type, version, status, prompt version, and review metadata.

## PromptTemplate

Represents a versioned prompt template.

## AIInteractionLog

Stores safe metadata about provider calls. It must not store secrets or full sensitive prompts.

## ReviewRecord

Captures human review status and comments for generated artifacts.
