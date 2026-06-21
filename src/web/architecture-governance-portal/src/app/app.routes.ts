import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ProjectsComponent } from './features/projects/projects.component';
import { RequirementsComponent } from './features/requirements/requirements.component';
import { ArtifactGenerationComponent } from './features/artifact-generation/artifact-generation.component';
import { ArtifactViewerComponent } from './features/artifact-viewer/artifact-viewer.component';
import { PromptCatalogComponent } from './features/prompt-catalog/prompt-catalog.component';
import { ReviewsComponent } from './features/reviews/reviews.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: 'requirements', component: RequirementsComponent },
  { path: 'artifact-generation', component: ArtifactGenerationComponent },
  { path: 'artifact-viewer', component: ArtifactViewerComponent },
  { path: 'prompt-catalog', component: PromptCatalogComponent },
  { path: 'reviews', component: ReviewsComponent },
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  { path: '**', redirectTo: 'dashboard' }
];
