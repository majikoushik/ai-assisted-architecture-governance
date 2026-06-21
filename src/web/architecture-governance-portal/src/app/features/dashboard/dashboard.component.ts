import { Component } from '@angular/core';

@Component({
  selector: 'ag-dashboard',
  standalone: true,
  template: `
    <section class="page-header">
      <div>
        <div class="eyebrow">Governance workspace</div>
        <h1>Architecture review command center</h1>
        <p>Track requirements, draft artifacts, reviews, risks, and prompt governance from one workspace.</p>
      </div>
      <span class="status-badge">Mock AI ready</span>
    </section>
    <section class="panel-grid">
      <article class="card"><h2>Projects</h2><p>Workspace management will arrive in Epic 1.</p></article>
      <article class="card"><h2>Requirements</h2><p>Structured requirement intake will arrive in Epic 2.</p></article>
      <article class="card"><h2>Artifacts</h2><p>AI-assisted draft generation starts in Epic 4.</p></article>
    </section>
  `
})
export class DashboardComponent {}
