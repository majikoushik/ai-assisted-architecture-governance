import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'ag-artifact-generation',
  standalone: true,
  imports: [RouterLink],
  template: `
    <section class="page-header">
      <div>
        <div class="eyebrow">Artifact generation</div>
        <h1>Governed draft generation workflow</h1>
        <p>Generate architecture artifacts from submitted requirements through versioned prompts, provider abstraction, traceability, and human review.</p>
      </div>
    </section>

    <section class="workflow-steps">
      <article class="workflow-step">
        <span>1</span>
        <h2>Create a project</h2>
        <p>Start with an architecture workspace that captures domain, owner, status, and solution context.</p>
      </article>
      <article class="workflow-step">
        <span>2</span>
        <h2>Submit a requirement</h2>
        <p>Use synthetic or approved business requirements and select the expected artifact types.</p>
      </article>
      <article class="workflow-step">
        <span>3</span>
        <h2>Generate drafts</h2>
        <p>Create requirement analysis, HLD, LLD, ADR, NFR, security, API, and risk review drafts.</p>
      </article>
      <article class="workflow-step">
        <span>4</span>
        <h2>Review and version</h2>
        <p>Inspect Markdown output, confirm prompt metadata, export artifacts, and record human review status.</p>
      </article>
    </section>

    <section class="notice-panel">
      <strong>Responsible AI boundary.</strong>
      Mock AI is the local default. Azure OpenAI is optional and configuration-driven. Generated content is not production-approved.
    </section>

    <div class="action-row">
      <a routerLink="/projects" class="btn btn-primary">Start from Projects</a>
      <a routerLink="/requirements" class="btn btn-secondary">View Requirements</a>
      <a routerLink="/prompts" class="btn btn-secondary">Inspect Prompts</a>
    </div>
  `
})
export class ArtifactGenerationComponent {}
