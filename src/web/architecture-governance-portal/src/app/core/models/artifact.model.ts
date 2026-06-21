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
  
  export interface Review {
    id: string;
    artifactId: string;
    reviewerName: string;
    status: string;
    comments: string;
    createdAt: string;
  }

  export interface CreateReviewRequest {
    reviewerName: string;
    reviewStatus: string;
    comments: string;
  }

  export interface UpdateArtifactStatusRequest {
    status: string;
    reason: string;
    updatedBy: string;
  }
