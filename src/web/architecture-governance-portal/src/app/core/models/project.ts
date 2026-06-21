export interface Project {
  id: string;
  name: string;
  businessDomain: string;
  description: string;
  owner: string;
  status: ProjectStatus;
  createdAt: string;
  updatedAt?: string;
}

export enum ProjectStatus {
  Draft = 'Draft',
  Active = 'Active',
  UnderReview = 'UnderReview',
  Archived = 'Archived'
}

export interface CreateProjectRequest {
  name: string;
  businessDomain: string;
  description: string;
  owner: string;
}

export interface UpdateProjectRequest {
  name: string;
  businessDomain: string;
  description: string;
  owner: string;
}

export interface UpdateProjectStatusRequest {
  status: ProjectStatus;
}
