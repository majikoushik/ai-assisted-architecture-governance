import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { RequirementsService } from '../../../core/services/requirements.service';
import { RequirementSubmission, RequirementStatus } from '../../../core/models/requirement.model';

@Component({
  selector: 'app-requirement-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './requirement-detail.component.html',
  styleUrls: ['./requirement-detail.component.css']
})
export class RequirementDetailComponent implements OnInit {
  requirement: RequirementSubmission | null = null;
  projectId: string = '';
  isLoading = false;
  isSubmitting = false;
  error: string | null = null;

  RequirementStatus = RequirementStatus;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private requirementsService: RequirementsService
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
      },
      error: (err) => {
        this.error = 'Failed to load requirement details.';
        this.isLoading = false;
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
}
