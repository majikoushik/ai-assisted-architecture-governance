import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

interface HealthResponse {
  status: string;
  totalDuration: string;
  entries: {
    [key: string]: {
      data: any;
      duration: string;
      status: string;
      tags: string[];
    };
  };
}

interface ReadinessResponse {
  service: string;
  status: string;
  aiProvider: string;
}

@Component({
  selector: 'app-system-health',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="system-health-container">
      <h2>System Health</h2>
      
      <div class="health-cards">
        <!-- Liveness Card -->
        <div class="card" [ngClass]="livenessStatus === 'Healthy' ? 'status-healthy' : 'status-unhealthy'">
          <h3>Liveness</h3>
          <p>Status: <strong>{{ livenessStatus }}</strong></p>
          <button (click)="checkLiveness()">Check Now</button>
        </div>

        <!-- Readiness Card -->
        <div class="card" [ngClass]="readinessStatus === 'Healthy' ? 'status-healthy' : 'status-unhealthy'">
          <h3>Readiness (Database)</h3>
          <p>Status: <strong>{{ readinessStatus }}</strong></p>
          <p *ngIf="dbDuration">Duration: {{ dbDuration }}</p>
          <button (click)="checkReadiness()">Check Now</button>
        </div>

        <!-- Platform Info Card -->
        <div class="card status-info">
          <h3>Platform Info</h3>
          <p>Service: {{ platformInfo?.service || '...' }}</p>
          <p>Status: <strong>{{ platformInfo?.status || '...' }}</strong></p>
          <p>Active AI Provider: <strong>{{ platformInfo?.aiProvider || '...' }}</strong></p>
          <button (click)="checkPlatformInfo()">Check Now</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .system-health-container {
      padding: 20px;
    }
    .health-cards {
      display: flex;
      gap: 20px;
      margin-top: 20px;
    }
    .card {
      border: 1px solid #ddd;
      border-radius: 8px;
      padding: 20px;
      width: 300px;
      background: #fff;
      box-shadow: 0 2px 4px rgba(0,0,0,0.05);
    }
    .status-healthy {
      border-top: 4px solid #28a745;
    }
    .status-unhealthy {
      border-top: 4px solid #dc3545;
    }
    .status-info {
      border-top: 4px solid #17a2b8;
    }
    h3 {
      margin-top: 0;
    }
    button {
      margin-top: 10px;
      padding: 8px 16px;
      background: #f8f9fa;
      border: 1px solid #ddd;
      border-radius: 4px;
      cursor: pointer;
    }
    button:hover {
      background: #e2e6ea;
    }
  `]
})
export class SystemHealthComponent implements OnInit {
  livenessStatus: string = 'Checking...';
  readinessStatus: string = 'Checking...';
  dbDuration: string = '';
  platformInfo: ReadinessResponse | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.checkAll();
  }

  checkAll() {
    this.checkLiveness();
    this.checkReadiness();
    this.checkPlatformInfo();
  }

  checkLiveness() {
    this.livenessStatus = 'Checking...';
    this.http.get(`${environment.apiUrl}/health/live`, { responseType: 'text' }).subscribe({
      next: (res) => { this.livenessStatus = res; },
      error: (err) => { this.livenessStatus = 'Unhealthy'; }
    });
  }

  checkReadiness() {
    this.readinessStatus = 'Checking...';
    this.dbDuration = '';
    this.http.get(`${environment.apiUrl}/health/ready`, { responseType: 'text' }).subscribe({
      next: (res) => { this.readinessStatus = res; },
      error: (err) => { this.readinessStatus = 'Unhealthy'; }
    });
  }

  checkPlatformInfo() {
    this.http.get<ReadinessResponse>(`${environment.apiUrl}/api/v1/platform/readiness`).subscribe({
      next: (res) => { this.platformInfo = res; },
      error: (err) => { console.error('Platform check failed'); }
    });
  }
}
