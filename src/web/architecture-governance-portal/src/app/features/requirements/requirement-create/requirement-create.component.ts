import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { RequirementsService } from '../../../core/services/requirements.service';
import { CreateRequirementRequest, ArtifactType } from '../../../core/models/requirement.model';

@Component({
  selector: 'app-requirement-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './requirement-create.component.html',
  styleUrls: ['./requirement-create.component.css']
})
export class RequirementCreateComponent implements OnInit {
  requirementForm!: FormGroup;
  projectId!: string;
  isSubmitting = false;
  error: string | null = null;
  
  // Available artifact types to select
  artifactTypes = [
    { value: ArtifactType.RequirementAnalysis, label: 'Requirement Analysis' },
    { value: ArtifactType.HighLevelDesign, label: 'High-Level Design' },
    { value: ArtifactType.LowLevelDesign, label: 'Low-Level Design' },
    { value: ArtifactType.ArchitectureDecisionRecord, label: 'Architecture Decision Record' },
    { value: ArtifactType.NonFunctionalRequirementReview, label: 'NFR Review' },
    { value: ArtifactType.ApiContractReview, label: 'API Contract Review' },
    { value: ArtifactType.SecurityReview, label: 'Security Review' },
    { value: ArtifactType.RiskAndAssumptionReview, label: 'Risk & Assumption Review' }
  ];

  constructor(
    private fb: FormBuilder,
    private requirementsService: RequirementsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('projectId') || '';
    
    if (!this.projectId) {
      this.error = 'Project ID is missing.';
    }

    this.initForm();
  }

  initForm(): void {
    this.requirementForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      requirementText: ['', [Validators.required, Validators.maxLength(10000)]],
      domainContext: ['', [Validators.required, Validators.maxLength(50)]],
      submittedBy: ['user@example.com', [Validators.required]], // Mocked for now
      selectedArtifacts: this.fb.array([])
    });

    // Default select HLD and LLD
    const defaultTypes = [ArtifactType.RequirementAnalysis, ArtifactType.HighLevelDesign];
    this.artifactTypes.forEach(type => {
      this.selectedArtifactsFormArray.push(new FormControl(defaultTypes.includes(type.value)));
    });
  }

  get selectedArtifactsFormArray() {
    return this.requirementForm.controls['selectedArtifacts'] as FormArray;
  }

  getFormControl(i: number): FormControl {
    return this.selectedArtifactsFormArray.controls[i] as FormControl;
  }

  onSubmit(): void {
    if (this.requirementForm.invalid || !this.projectId) {
      return;
    }

    this.isSubmitting = true;
    this.error = null;

    const selectedTypes = this.requirementForm.value.selectedArtifacts
      .map((checked: boolean, i: number) => checked ? this.artifactTypes[i].value : null)
      .filter((v: any) => v !== null);

    const request: CreateRequirementRequest = {
      projectId: this.projectId,
      title: this.requirementForm.value.title,
      requirementText: this.requirementForm.value.requirementText,
      domainContext: this.requirementForm.value.domainContext,
      submittedBy: this.requirementForm.value.submittedBy,
      expectedArtifactTypes: selectedTypes
    };

    this.requirementsService.createRequirement(request).subscribe({
      next: (response) => {
        this.isSubmitting = false;
        // Redirect to project workspace or requirement details
        this.router.navigate(['/projects', this.projectId]);
      },
      error: (err) => {
        this.isSubmitting = false;
        this.error = err.error?.detail || 'An error occurred while submitting the requirement.';
      }
    });
  }
}
