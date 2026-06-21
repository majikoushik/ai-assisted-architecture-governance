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
        <p>Track requirements, AI-assisted draft artifacts, reviews, risks, prompt governance, and Azure readiness from one workspace.</p>
      </div>
      <span class="status-badge" *ngIf="platformReadiness">
        {{ platformReadiness.aiProvider }} AI ready
      </span>
    </section>

    <section class="notice-panel">
      <strong>Human review required.</strong>
      Generated artifacts are AI-assisted drafts and must be reviewed by a qualified architect before production decisions.
    </section>

    <section class="panel-grid">
      <article class="card">
        <h2>Projects</h2>
        <p>Manage solution workspaces, business domains, owners, statuses, and requirement traceability.</p>
        <a routerLink="/projects" class="btn btn-secondary">View Projects</a>
      </article>
      <article class="card">
        <h2>Requirements</h2>
        <p>Capture synthetic or approved requirements and select expected architecture artifacts.</p>
        <a routerLink="/requirements" class="btn btn-secondary">View Requirements</a>
      </article>
      <article class="card">
        <h2>Generate</h2>
        <p>Use the governed workflow to generate HLD, LLD, ADR, NFR, security, API, and risk drafts.</p>
        <a routerLink="/artifact-generation" class="btn btn-secondary">Open Workflow</a>
      </article>
      <article class="card">
        <h2>Prompt Catalog</h2>
        <p>Review versioned prompt templates and responsible AI constraints.</p>
        <a routerLink="/prompts" class="btn btn-secondary">View Prompts</a>
      </article>
      <article class="card">
        <h2>System Health</h2>
        <p>Inspect API readiness, health checks, and AI provider configuration posture.</p>
        <a routerLink="/system-health" class="btn btn-secondary">View Health</a>
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
