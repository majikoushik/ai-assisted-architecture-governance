import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Artifact, GenerateArtifactCommand, Review, CreateReviewRequest, UpdateArtifactStatusRequest } from '../models/artifact.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class ArtifactsService {
  private apiUrl = `${environment.apiBaseUrl}/api/v1/artifacts`;
  private projectsUrl = `${environment.apiBaseUrl}/api/v1/projects`;
  private requirementsUrl = `${environment.apiBaseUrl}/api/v1/requirements`;

  constructor(private http: HttpClient) { }

  generateArtifact(command: GenerateArtifactCommand): Observable<Artifact> {
    return this.http.post<ApiResponse<Artifact>>(`${this.apiUrl}/generate`, command)
      .pipe(map(response => response.data));
  }

  getArtifact(id: string): Observable<Artifact> {
    return this.http.get<ApiResponse<Artifact>>(`${this.apiUrl}/${id}`)
      .pipe(map(response => response.data));
  }

  getArtifactsByProject(projectId: string): Observable<Artifact[]> {
    return this.http.get<ApiResponse<Artifact[]>>(`${this.projectsUrl}/${projectId}/artifacts`)
      .pipe(map(response => response.data));
  }

  getArtifactsByRequirement(requirementId: string): Observable<Artifact[]> {
    return this.http.get<ApiResponse<Artifact[]>>(`${this.requirementsUrl}/${requirementId}/artifacts`)
      .pipe(map(response => response.data));
  }

  getMarkdownExportUrl(id: string): string {
    return `${this.apiUrl}/${id}/export/markdown`;
  }

  getReviews(id: string): Observable<Review[]> {
    return this.http.get<ApiResponse<Review[]>>(`${this.apiUrl}/${id}/reviews`)
      .pipe(map(response => response.data));
  }

  createReview(id: string, request: CreateReviewRequest): Observable<Review> {
    return this.http.post<ApiResponse<Review>>(`${this.apiUrl}/${id}/reviews`, request)
      .pipe(map(response => response.data));
  }

  updateStatus(id: string, request: UpdateArtifactStatusRequest): Observable<boolean> {
    return this.http.patch<ApiResponse<boolean>>(`${this.apiUrl}/${id}/status`, request)
      .pipe(map(response => response.data));
  }

  getVersions(id: string): Observable<Artifact[]> {
    return this.http.get<ApiResponse<Artifact[]>>(`${this.apiUrl}/${id}/versions`)
      .pipe(map(response => response.data));
  }
}
