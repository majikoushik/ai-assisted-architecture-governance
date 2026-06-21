import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProjectService } from '../../core/services/project.service';
import { Project } from '../../core/models/project';

@Component({
  selector: 'ag-projects',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="page-header">
      <h1>Projects</h1>
      <button class="btn btn-primary" routerLink="/projects/new">New Project</button>
    </div>

    <div *ngIf="loading" class="loading-state">Loading projects...</div>
    <div *ngIf="error" class="error-state">{{ error }}</div>

    <div *ngIf="!loading && !error && projects.length === 0" class="empty-state">
      <p>No projects found. Create a new architecture project to get started.</p>
    </div>

    <div class="grid" *ngIf="!loading && !error && projects.length > 0">
      <div class="card project-card" *ngFor="let project of projects">
        <div class="card-header">
          <h3>{{ project.name }}</h3>
          <span class="badge" [ngClass]="project.status.toLowerCase()">{{ project.status }}</span>
        </div>
        <div class="card-body">
          <p class="description">{{ project.description }}</p>
          <div class="metadata">
            <span class="meta-item"><strong>Domain:</strong> {{ project.businessDomain }}</span>
            <span class="meta-item"><strong>Owner:</strong> {{ project.owner }}</span>
          </div>
        </div>
        <div class="card-footer">
          <a class="btn btn-secondary" [routerLink]="['/projects', project.id]">View Details</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .page-header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 2rem; }
    .grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(350px, 1fr)); gap: 1.5rem; }
    .project-card { display: flex; flex-direction: column; }
    .card-header { display: flex; justify-content: space-between; align-items: flex-start; margin-bottom: 1rem; }
    .card-header h3 { margin: 0; font-size: 1.25rem; }
    .card-body { flex: 1; margin-bottom: 1.5rem; }
    .description { color: #666; margin-bottom: 1rem; display: -webkit-box; -webkit-line-clamp: 3; -webkit-box-orient: vertical; overflow: hidden; }
    .metadata { display: flex; flex-direction: column; gap: 0.5rem; font-size: 0.9rem; }
    .card-footer { display: flex; justify-content: flex-end; }
    
    .badge { padding: 0.25rem 0.75rem; border-radius: 999px; font-size: 0.8rem; font-weight: 500; }
    .badge.draft { background-color: #f3f4f6; color: #374151; }
    .badge.active { background-color: #d1fae5; color: #065f46; }
    .badge.underreview { background-color: #fef3c7; color: #92400e; }
    .badge.archived { background-color: #fee2e2; color: #b91c1c; }
  `]
})
export class ProjectsComponent implements OnInit {
  projects: Project[] = [];
  loading = true;
  error: string | null = null;

  constructor(private projectService: ProjectService) {}

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.loading = true;
    this.projectService.getProjects().subscribe({
      next: (data) => {
        this.projects = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load projects.';
        this.loading = false;
        console.error(err);
      }
    });
  }
}
