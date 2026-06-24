import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Project, CreateProjectRequest, UpdateProjectRequest, UpdateProjectStatusRequest } from '../models/project';
import { environment } from '../../../environments/environment';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private apiUrl = `${environment.apiBaseUrl}/api/v1/projects`;

  constructor(private http: HttpClient) {}

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[] | ApiResponse<Project[]>>(this.apiUrl)
      .pipe(map(response => this.unwrap(response)));
  }

  getProject(id: string): Observable<Project> {
    return this.http.get<Project | ApiResponse<Project>>(`${this.apiUrl}/${id}`)
      .pipe(map(response => this.unwrap(response)));
  }

  createProject(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project | ApiResponse<Project>>(this.apiUrl, request)
      .pipe(map(response => this.unwrap(response)));
  }

  updateProject(id: string, request: UpdateProjectRequest): Observable<Project> {
    return this.http.put<Project | ApiResponse<Project>>(`${this.apiUrl}/${id}`, request)
      .pipe(map(response => this.unwrap(response)));
  }

  updateProjectStatus(id: string, request: UpdateProjectStatusRequest): Observable<Project> {
    return this.http.patch<Project | ApiResponse<Project>>(`${this.apiUrl}/${id}/status`, request)
      .pipe(map(response => this.unwrap(response)));
  }

  private unwrap<T>(response: T | ApiResponse<T>): T {
    return response && typeof response === 'object' && 'data' in response
      ? (response as ApiResponse<T>).data
      : response as T;
  }
}
