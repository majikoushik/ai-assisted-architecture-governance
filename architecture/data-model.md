# Data Model

## ArchitectureProject

Represents a governance workspace for a solution or initiative.

## RequirementSubmission

Represents submitted business or technical requirements for AI-assisted analysis.

## GeneratedArtifact

Represents an AI-assisted architecture artifact with type, version, status, prompt version, and review metadata.

## PromptTemplate

Represents a versioned prompt template.

## AIInteractionLog

Stores safe metadata about provider calls. It must not store secrets or full sensitive prompts.

## ReviewRecord

Captures human review status and comments for generated artifacts.
