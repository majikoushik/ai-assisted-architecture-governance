import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { PromptTemplate } from '../models/prompt.model';

interface ApiResponse<T> {
  data: T;
}

@Injectable({
  providedIn: 'root'
})
export class PromptsService {
  private apiUrl = `${environment.apiBaseUrl}/api/v1/prompts`;

  constructor(private http: HttpClient) {}

  getPrompts(): Observable<PromptTemplate[]> {
    return this.http.get<ApiResponse<PromptTemplate[]>>(this.apiUrl)
      .pipe(map(response => response.data));
  }

  getPromptById(id: string): Observable<PromptTemplate> {
    return this.http.get<ApiResponse<PromptTemplate>>(`${this.apiUrl}/${id}`)
      .pipe(map(response => response.data));
  }
}
