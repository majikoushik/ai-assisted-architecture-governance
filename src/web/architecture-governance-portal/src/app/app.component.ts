import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'ag-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="shell">
      <aside class="sidebar">
        <div class="brand">Architecture Governance</div>
        <nav class="nav" aria-label="Main navigation">
          <a routerLink="/dashboard" routerLinkActive="active">Dashboard</a>
          <a routerLink="/projects" routerLinkActive="active">Projects</a>
          <a routerLink="/requirements" routerLinkActive="active">Requirements</a>
          <a routerLink="/artifact-generation" routerLinkActive="active">Generate</a>
          <a routerLink="/artifact-viewer" routerLinkActive="active">Artifacts</a>
          <a routerLink="/prompt-catalog" routerLinkActive="active">Prompts</a>
          <a routerLink="/reviews" routerLinkActive="active">Reviews</a>
        </nav>
      </aside>
      <main class="content">
        <router-outlet />
      </main>
    </div>
  `
})
export class AppComponent {}
