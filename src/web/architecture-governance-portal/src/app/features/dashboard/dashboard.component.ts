import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlatformService } from '../../core/services/platform.service';
import { PlatformReadiness } from '../../core/models/platform-readiness';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'ag-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <section class="page-header">
      <div>
        <div class="eyebrow">Governance workspace</div>
        <h1>Architecture review command center</h1>
        <p>Track requirements, draft artifacts, reviews, risks, and prompt governance from one workspace.</p>
      </div>
      <span class="status-badge" *ngIf="platformReadiness">
        {{ platformReadiness.aiProvider }} AI ready
      </span>
    </section>
    <section class="panel-grid">
      <article class="card">
        <h2>Projects</h2>
        <p>Manage architecture review workspaces.</p>
        <a routerLink="/projects" class="btn btn-secondary">View Projects</a>
      </article>
      <article class="card">
        <h2>Requirements</h2>
        <p>Structured requirement intake.</p>
      </article>
      <article class="card">
        <h2>Prompts</h2>
        <p>Review prompt engineering catalog.</p>
        <a routerLink="/prompts" class="btn btn-secondary">View Prompts</a>
      </article>
    </section>
  `
})
export class DashboardComponent implements OnInit {
  platformReadiness: PlatformReadiness | null = null;

  constructor(private readonly platformService: PlatformService) {}

  ngOnInit(): void {
    this.platformService.getReadiness().subscribe({
      next: (readiness) => this.platformReadiness = readiness,
      error: (err) => console.error('Failed to load platform readiness', err)
    });
  }
}
