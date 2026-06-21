import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { RequirementsService } from '../../../core/services/requirements.service';
import { ArtifactsService } from '../../../core/services/artifacts.service';
import { RequirementSubmission, RequirementStatus } from '../../../core/models/requirement.model';
import { Artifact } from '../../../core/models/artifact.model';

@Component({
  selector: 'app-requirement-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './requirement-detail.component.html',
  styleUrls: ['./requirement-detail.component.css']
})
export class RequirementDetailComponent implements OnInit {
  requirement: RequirementSubmission | null = null;
  artifacts: Artifact[] = [];
  projectId: string = '';
  isLoading = false;
  isSubmitting = false;
  isGenerating = false;
  isGeneratingHLD = false;
  isGeneratingLLD = false;
  isGeneratingADR = false;
  error: string | null = null;

  RequirementStatus = RequirementStatus;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private requirementsService: RequirementsService,
    private artifactsService: ArtifactsService
  ) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('projectId') || '';
    const reqId = this.route.snapshot.paramMap.get('reqId');
    
    if (reqId) {
      this.loadRequirement(reqId);
    } else {
      this.error = 'Requirement ID not found in route.';
    }
  }

  loadRequirement(id: string): void {
    this.isLoading = true;
    this.error = null;
    this.requirementsService.getRequirement(id).subscribe({
      next: (data) => {
        this.requirement = data;
        this.isLoading = false;
        this.loadArtifacts();
      },
      error: (err) => {
        this.error = 'Failed to load requirement details.';
        this.isLoading = false;
      }
    });
  }

  loadArtifacts(): void {
    if (!this.requirement) return;
    this.artifactsService.getArtifactsByRequirement(this.requirement.id).subscribe({
      next: (data) => {
        this.artifacts = data;
      },
      error: (err) => {
        console.error('Failed to load artifacts', err);
      }
    });
  }

  submitForAnalysis(): void {
    if (!this.requirement) return;

    this.isSubmitting = true;
    this.error = null;

    this.requirementsService.updateRequirementStatus(this.requirement.id, {
      id: this.requirement.id,
      status: RequirementStatus.Submitted
    }).subscribe({
      next: (data) => {
        this.requirement = data;
        this.isSubmitting = false;
      },
      error: (err) => {
        this.error = 'Failed to submit requirement for analysis.';
        this.isSubmitting = false;
      }
    });
  }

  generateRequirementAnalysis(): void {
    if (!this.requirement || !this.projectId) return;

    this.isGenerating = true;
    this.error = null;

    this.artifactsService.generateArtifact({
      projectId: this.projectId,
      requirementSubmissionId: this.requirement.id,
      artifactType: 'RequirementAnalysis'
    }).subscribe({
      next: (artifact) => {
        this.isGenerating = false;
        this.artifacts.push(artifact);
        // Refresh requirement to potentially update status if it changed
        if (this.requirement) this.loadRequirement(this.requirement.id);
      },
      error: (err) => {
        this.error = 'Failed to generate artifact. ' + (err.error?.detail || err.message || '');
        this.isGenerating = false;
      }
    });
  }

  generateHighLevelDesign(): void {
    if (!this.requirement || !this.projectId) return;

    this.isGeneratingHLD = true;
    this.error = null;

    this.artifactsService.generateArtifact({
      projectId: this.projectId,
      requirementSubmissionId: this.requirement.id,
      artifactType: 'HighLevelDesign'
    }).subscribe({
      next: (artifact) => {
        this.isGeneratingHLD = false;
        this.artifacts.push(artifact);
        if (this.requirement) this.loadRequirement(this.requirement.id);
      },
      error: (err) => {
        this.error = 'Failed to generate HLD draft. ' + (err.error?.detail || err.message || '');
        this.isGeneratingHLD = false;
      }
    });
  }

  generateLowLevelDesign(): void {
    if (!this.requirement || !this.projectId) return;

    this.isGeneratingLLD = true;
    this.error = null;

    this.artifactsService.generateArtifact({
      projectId: this.projectId,
      requirementSubmissionId: this.requirement.id,
      artifactType: 'LowLevelDesign'
    }).subscribe({
      next: (artifact) => {
        this.isGeneratingLLD = false;
        this.artifacts.push(artifact);
        if (this.requirement) this.loadRequirement(this.requirement.id);
      },
      error: (err) => {
        this.error = 'Failed to generate LLD draft. ' + (err.error?.detail || err.message || '');
        this.isGeneratingLLD = false;
      }
    });
  }

  generateArchitectureDecisionRecord(): void {
    if (!this.requirement || !this.projectId) return;

    this.isGeneratingADR = true;
    this.error = null;

    this.artifactsService.generateArtifact({
      projectId: this.projectId,
      requirementSubmissionId: this.requirement.id,
      artifactType: 'ArchitectureDecisionRecord'
    }).subscribe({
      next: (artifact) => {
        this.isGeneratingADR = false;
        this.artifacts.push(artifact);
        if (this.requirement) this.loadRequirement(this.requirement.id);
      },
      error: (err) => {
        this.error = 'Failed to generate ADR draft. ' + (err.error?.detail || err.message || '');
        this.isGeneratingADR = false;
      }
    });
  }
}
