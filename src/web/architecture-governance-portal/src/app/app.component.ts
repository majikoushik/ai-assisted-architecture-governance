import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'ag-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="shell">
      <aside class="sidebar">
        <div class="brand">
          <span class="brand-mark">AG</span>
          <span>Architecture Governance</span>
        </div>
        <p class="sidebar-note">AI-assisted drafts with human architect review.</p>
        <nav class="nav" aria-label="Main navigation">
          <a routerLink="/dashboard" routerLinkActive="active">Dashboard</a>
          <a routerLink="/projects" routerLinkActive="active">Projects</a>
          <a routerLink="/requirements" routerLinkActive="active">Requirements</a>
          <a routerLink="/artifact-generation" routerLinkActive="active">Generate</a>
          <a routerLink="/artifact-viewer" routerLinkActive="active">Artifacts</a>
          <a routerLink="/prompts" routerLinkActive="active">Prompts</a>
          <a routerLink="/reviews" routerLinkActive="active">Reviews</a>
          <a routerLink="/system-health" routerLinkActive="active">Health</a>
        </nav>
      </aside>
      <main class="content">
        <router-outlet />
      </main>
    </div>
  `
})
export class AppComponent {}
