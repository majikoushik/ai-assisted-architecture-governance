import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { 
  RequirementSubmission, 
  CreateRequirementRequest, 
  UpdateRequirementRequest, 
  UpdateRequirementStatusRequest 
} from '../models/requirement.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class RequirementsService {
  private apiUrl = `${environment.apiBaseUrl}/api/v1/requirements`;
  private projectsApiUrl = `${environment.apiBaseUrl}/api/v1/projects`;

  constructor(private http: HttpClient) {}

  getAllRequirements(): Observable<RequirementSubmission[]> {
    return this.http.get<RequirementSubmission[] | ApiResponse<RequirementSubmission[]>>(this.apiUrl)
      .pipe(map(response => this.mapRequirements(this.unwrap(response))));
  }

  getRequirementsByProject(projectId: string): Observable<RequirementSubmission[]> {
    return this.http.get<RequirementSubmission[] | ApiResponse<RequirementSubmission[]>>(`${this.projectsApiUrl}/${projectId}/requirements`)
      .pipe(map(response => this.mapRequirements(this.unwrap(response))));
  }

  getRequirement(id: string): Observable<RequirementSubmission> {
    return this.http.get<RequirementSubmission | ApiResponse<RequirementSubmission>>(`${this.apiUrl}/${id}`)
      .pipe(map(response => this.mapRequirement(this.unwrap(response))));
  }

  createRequirement(request: CreateRequirementRequest): Observable<RequirementSubmission> {
    return this.http.post<RequirementSubmission | ApiResponse<RequirementSubmission>>(this.apiUrl, request)
      .pipe(map(response => this.mapRequirement(this.unwrap(response))));
  }

  updateRequirement(id: string, request: UpdateRequirementRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  updateRequirementStatus(id: string, request: UpdateRequirementStatusRequest): Observable<RequirementSubmission> {
    return this.http.patch<RequirementSubmission | ApiResponse<RequirementSubmission>>(`${this.apiUrl}/${id}/status`, request)
      .pipe(map(response => this.mapRequirement(this.unwrap(response))));
  }

  private unwrap<T>(response: T | ApiResponse<T>): T {
    return response && typeof response === 'object' && 'data' in response
      ? (response as ApiResponse<T>).data
      : response as T;
  }

  private mapRequirements(requirements: RequirementSubmission[]): RequirementSubmission[] {
    return requirements.map(requirement => this.mapRequirement(requirement));
  }

  private mapRequirement(requirement: RequirementSubmission): RequirementSubmission {
    return {
      ...requirement,
      createdTimestamp: requirement.createdTimestamp ?? (requirement as unknown as { createdAt?: string }).createdAt ?? '',
      updatedTimestamp: requirement.updatedTimestamp ?? (requirement as unknown as { updatedAt?: string }).updatedAt
    };
  }
}
