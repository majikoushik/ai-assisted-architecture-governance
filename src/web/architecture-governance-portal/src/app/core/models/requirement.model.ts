export enum RequirementStatus {
  Draft = 'Draft',
  Submitted = 'Submitted',
  InAnalysis = 'InAnalysis',
  Analyzed = 'Analyzed',
  Error = 'Error'
}

export enum ArtifactType {
  RequirementAnalysis = 'RequirementAnalysis',
  HighLevelDesign = 'HighLevelDesign',
  LowLevelDesign = 'LowLevelDesign',
  ArchitectureDecisionRecord = 'ArchitectureDecisionRecord',
  NonFunctionalRequirementReview = 'NonFunctionalRequirementReview',
  ApiContractReview = 'ApiContractReview',
  SecurityReview = 'SecurityReview',
  RiskAndAssumptionReview = 'RiskAndAssumptionReview'
}

export interface RequirementSubmission {
  id: string;
  projectId: string;
  title: string;
  requirementText: string;
  domainContext: string;
  expectedArtifactTypes: ArtifactType[];
  status: RequirementStatus;
  submittedBy: string;
  createdTimestamp: string;
  updatedTimestamp?: string;
}

export interface CreateRequirementRequest {
  projectId: string;
  title: string;
  requirementText: string;
  domainContext: string;
  expectedArtifactTypes: ArtifactType[];
  submittedBy: string;
}

export interface UpdateRequirementRequest {
  id: string;
  title: string;
  requirementText: string;
  domainContext: string;
  expectedArtifactTypes: ArtifactType[];
}

export interface UpdateRequirementStatusRequest {
  id: string;
  status: RequirementStatus;
}
