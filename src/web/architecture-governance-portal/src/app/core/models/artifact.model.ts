export interface Artifact {
    id: string;
    projectId: string;
    requirementSubmissionId: string;
    artifactType: string;
    title: string;
    markdownContent: string;
    version: string;
    status: string;
    providerName: string;
    promptTemplateName: string;
    promptTemplateVersion: string;
    createdAt: string;
    updatedAt?: string;
    correlationId: string;
  }
  
  export interface GenerateArtifactCommand {
    projectId: string;
    requirementSubmissionId: string;
    artifactType: string;
  }
