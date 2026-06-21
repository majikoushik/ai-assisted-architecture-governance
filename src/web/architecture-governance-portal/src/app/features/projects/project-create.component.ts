import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ProjectService } from '../../core/services/project.service';

@Component({
  selector: 'ag-project-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  template: `
    <div class="page-header">
      <h1>New Architecture Project</h1>
      <a routerLink="/projects" class="back-link">← Back to Projects</a>
    </div>

    <div class="card form-card">
      <form [formGroup]="projectForm" (ngSubmit)="onSubmit()">
        
        <div *ngIf="error" class="error-alert">{{ error }}</div>

        <div class="form-group">
          <label for="name">Project Name <span class="required">*</span></label>
          <input id="name" type="text" formControlName="name" class="form-control" placeholder="e.g. Core Banking Modernization" />
          <div *ngIf="f['name'].touched && f['name'].invalid" class="validation-error">
            <span *ngIf="f['name'].errors?.['required']">Project name is required.</span>
            <span *ngIf="f['name'].errors?.['maxlength']">Project name cannot exceed 200 characters.</span>
          </div>
        </div>

        <div class="form-group">
          <label for="businessDomain">Business Domain <span class="required">*</span></label>
          <input id="businessDomain" type="text" formControlName="businessDomain" class="form-control" placeholder="e.g. Retail Banking, Healthcare, etc." />
          <div *ngIf="f['businessDomain'].touched && f['businessDomain'].invalid" class="validation-error">
            <span *ngIf="f['businessDomain'].errors?.['required']">Business domain is required.</span>
          </div>
        </div>

        <div class="form-group">
          <label for="owner">Owner <span class="required">*</span></label>
          <input id="owner" type="text" formControlName="owner" class="form-control" placeholder="e.g. Jane Doe" />
          <div *ngIf="f['owner'].touched && f['owner'].invalid" class="validation-error">
            <span *ngIf="f['owner'].errors?.['required']">Owner is required.</span>
          </div>
        </div>

        <div class="form-group">
          <label for="description">Description <span class="required">*</span></label>
          <textarea id="description" formControlName="description" class="form-control" rows="4" placeholder="Briefly describe the goals of this architecture project..."></textarea>
          <div *ngIf="f['description'].touched && f['description'].invalid" class="validation-error">
            <span *ngIf="f['description'].errors?.['required']">Description is required.</span>
          </div>
        </div>

        <div class="form-actions">
          <button type="button" class="btn btn-secondary" routerLink="/projects">Cancel</button>
          <button type="submit" class="btn btn-primary" [disabled]="projectForm.invalid || submitting">
            {{ submitting ? 'Creating...' : 'Create Project' }}
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .page-header { margin-bottom: 2rem; }
    .back-link { color: #4f46e5; text-decoration: none; font-size: 0.9rem; }
    .back-link:hover { text-decoration: underline; }
    .form-card { max-width: 800px; padding: 2rem; }
    .form-group { margin-bottom: 1.5rem; }
    label { display: block; margin-bottom: 0.5rem; font-weight: 500; }
    .required { color: #ef4444; }
    .form-control { width: 100%; padding: 0.75rem; border: 1px solid #d1d5db; border-radius: 0.375rem; font-family: inherit; font-size: 1rem; }
    .form-control:focus { outline: none; border-color: #4f46e5; box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1); }
    .validation-error { color: #ef4444; font-size: 0.875rem; margin-top: 0.25rem; }
    .error-alert { background-color: #fee2e2; color: #b91c1c; padding: 1rem; border-radius: 0.375rem; margin-bottom: 1.5rem; }
    .form-actions { display: flex; justify-content: flex-end; gap: 1rem; margin-top: 2rem; }
  `]
})
export class ProjectCreateComponent {
  projectForm: FormGroup;
  submitting = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private router: Router
  ) {
    this.projectForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      businessDomain: ['', Validators.required],
      owner: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  get f() { return this.projectForm.controls; }

  onSubmit(): void {
    if (this.projectForm.invalid) {
      this.projectForm.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.error = null;

    this.projectService.createProject(this.projectForm.value).subscribe({
      next: (project) => {
        this.router.navigate(['/projects', project.id]);
      },
      error: (err) => {
        this.submitting = false;
        this.error = err.error?.detail || 'An error occurred while creating the project.';
        console.error(err);
      }
    });
  }
}
