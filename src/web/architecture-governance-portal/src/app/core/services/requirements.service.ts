import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { 
  RequirementSubmission, 
  CreateRequirementRequest, 
  UpdateRequirementRequest, 
  UpdateRequirementStatusRequest 
} from '../models/requirement.model';

@Injectable({
  providedIn: 'root'
})
export class RequirementsService {
  private apiUrl = `${environment.apiBaseUrl}/v1/Requirements`;
  private projectsApiUrl = `${environment.apiBaseUrl}/v1/Projects`;

  constructor(private http: HttpClient) {}

  getAllRequirements(): Observable<RequirementSubmission[]> {
    return this.http.get<RequirementSubmission[]>(this.apiUrl);
  }

  getRequirementsByProject(projectId: string): Observable<RequirementSubmission[]> {
    return this.http.get<RequirementSubmission[]>(`${this.projectsApiUrl}/${projectId}/requirements`);
  }

  getRequirement(id: string): Observable<RequirementSubmission> {
    return this.http.get<RequirementSubmission>(`${this.apiUrl}/${id}`);
  }

  createRequirement(request: CreateRequirementRequest): Observable<RequirementSubmission> {
    return this.http.post<RequirementSubmission>(this.apiUrl, request);
  }

  updateRequirement(id: string, request: UpdateRequirementRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  updateRequirementStatus(id: string, request: UpdateRequirementStatusRequest): Observable<RequirementSubmission> {
    return this.http.patch<RequirementSubmission>(`${this.apiUrl}/${id}/status`, request);
  }
}
