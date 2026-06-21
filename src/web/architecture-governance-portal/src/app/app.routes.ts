import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ProjectsComponent } from './features/projects/projects.component';
import { RequirementsComponent } from './features/requirements/requirements.component';
import { ArtifactGenerationComponent } from './features/artifact-generation/artifact-generation.component';
import { ArtifactViewerComponent } from './features/artifact-viewer/artifact-viewer.component';
import { PromptCatalogListComponent } from './features/prompt-catalog/prompt-catalog-list/prompt-catalog-list.component';
import { PromptCatalogDetailComponent } from './features/prompt-catalog/prompt-catalog-detail/prompt-catalog-detail.component';
import { ReviewsComponent } from './features/reviews/reviews.component';
import { ProjectCreateComponent } from './features/projects/project-create.component';
import { ProjectDetailComponent } from './features/projects/project-detail.component';
import { RequirementCreateComponent } from './features/requirements/requirement-create/requirement-create.component';
import { RequirementDetailComponent } from './features/requirements/requirement-detail/requirement-detail.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: 'projects/new', component: ProjectCreateComponent },
  { path: 'projects/:id', component: ProjectDetailComponent },
  { path: 'projects/:projectId/requirements/new', component: RequirementCreateComponent },
  { path: 'projects/:projectId/requirements/:reqId', component: RequirementDetailComponent },
  { path: 'requirements', component: RequirementsComponent },
  { path: 'artifact-generation', component: ArtifactGenerationComponent },
  { path: 'artifact-viewer', component: ArtifactViewerComponent },
  { path: 'prompts', component: PromptCatalogListComponent },
  { path: 'prompts/:id', component: PromptCatalogDetailComponent },
  { path: 'reviews', component: ReviewsComponent },
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  { path: '**', redirectTo: 'dashboard' }
];
