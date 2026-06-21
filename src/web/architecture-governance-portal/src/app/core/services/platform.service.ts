import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PlatformReadiness } from '../models/platform-readiness';

@Injectable({ providedIn: 'root' })
export class PlatformService {
  constructor(private readonly http: HttpClient) {}

  getReadiness(): Observable<PlatformReadiness> {
    return this.http.get<PlatformReadiness>(`${environment.apiBaseUrl}/api/v1/platform/readiness`);
  }
}
