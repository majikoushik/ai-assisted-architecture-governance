import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProjectService } from '../../core/services/project.service';
import { Project, ProjectStatus } from '../../core/models/project';

@Component({
  selector: 'ag-project-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div *ngIf="loading" class="loading-state">Loading project details...</div>
    <div *ngIf="error" class="error-state">{{ error }}</div>

    <div *ngIf="project" class="project-detail">
      <div class="page-header">
        <div>
          <a routerLink="/projects" class="back-link">← Back to Projects</a>
          <div class="title-row">
            <h1>{{ project.name }}</h1>
            <span class="badge" [ngClass]="project.status.toLowerCase()">{{ project.status }}</span>
          </div>
        </div>
        <div class="actions">
          <button class="btn btn-secondary" (click)="cycleStatus()">Change Status</button>
        </div>
      </div>

      <div class="card detail-card">
        <div class="detail-grid">
          <div class="detail-item">
            <strong>Business Domain</strong>
            <span>{{ project.businessDomain }}</span>
          </div>
          <div class="detail-item">
            <strong>Owner</strong>
            <span>{{ project.owner }}</span>
          </div>
          <div class="detail-item">
            <strong>Created</strong>
            <span>{{ project.createdAt | date:'medium' }}</span>
          </div>
          <div class="detail-item" *ngIf="project.updatedAt">
            <strong>Last Updated</strong>
            <span>{{ project.updatedAt | date:'medium' }}</span>
          </div>
        </div>
        
        <div class="detail-section">
          <h3>Description</h3>
          <p class="description">{{ project.description }}</p>
        </div>
      </div>
      
      <div class="tabs">
        <div class="tab active">Requirements</div>
        <div class="tab">Artifacts</div>
        <div class="tab">Reviews</div>
      </div>
      
      <div class="card empty-state">
        <p>Future Epic: Requirements and artifacts will be displayed here.</p>
      </div>
    </div>
  `,
  styles: [`
    .page-header { display: flex; justify-content: space-between; align-items: flex-start; margin-bottom: 2rem; }
    .title-row { display: flex; align-items: center; gap: 1rem; margin-top: 0.5rem; }
    .title-row h1 { margin: 0; }
    .back-link { color: #4f46e5; text-decoration: none; font-size: 0.9rem; }
    .back-link:hover { text-decoration: underline; }
    
    .badge { padding: 0.25rem 0.75rem; border-radius: 999px; font-size: 0.8rem; font-weight: 500; }
    .badge.draft { background-color: #f3f4f6; color: #374151; }
    .badge.active { background-color: #d1fae5; color: #065f46; }
    .badge.underreview { background-color: #fef3c7; color: #92400e; }
    .badge.archived { background-color: #fee2e2; color: #b91c1c; }
    
    .detail-card { padding: 2rem; margin-bottom: 2rem; }
    .detail-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(200px, 1fr)); gap: 1.5rem; margin-bottom: 2rem; padding-bottom: 2rem; border-bottom: 1px solid #e5e7eb; }
    .detail-item { display: flex; flex-direction: column; gap: 0.25rem; }
    .detail-item strong { color: #6b7280; font-size: 0.875rem; text-transform: uppercase; letter-spacing: 0.05em; }
    
    .detail-section h3 { margin-top: 0; margin-bottom: 1rem; font-size: 1.125rem; color: #374151; }
    .description { color: #4b5563; line-height: 1.6; white-space: pre-wrap; }
    
    .tabs { display: flex; border-bottom: 1px solid #e5e7eb; margin-bottom: 1.5rem; }
    .tab { padding: 0.75rem 1.5rem; color: #6b7280; font-weight: 500; cursor: pointer; border-bottom: 2px solid transparent; }
    .tab:hover { color: #374151; }
    .tab.active { color: #4f46e5; border-bottom-color: #4f46e5; }
    
    .empty-state { text-align: center; color: #6b7280; font-style: italic; }
  `]
})
export class ProjectDetailComponent implements OnInit {
  project: Project | null = null;
  loading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.loadProject(id);
      }
    });
  }

  loadProject(id: string): void {
    this.loading = true;
    this.projectService.getProject(id).subscribe({
      next: (data) => {
        this.project = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load project details.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  cycleStatus(): void {
    if (!this.project) return;
    
    const statuses = Object.values(ProjectStatus);
    const currentIndex = statuses.indexOf(this.project.status);
    const nextIndex = (currentIndex + 1) % statuses.length;
    const nextStatus = statuses[nextIndex];
    
    this.projectService.updateProjectStatus(this.project.id, { status: nextStatus }).subscribe({
      next: (updatedProject) => {
        this.project = updatedProject;
      },
      error: (err) => {
        console.error('Failed to update status', err);
      }
    });
  }
}
