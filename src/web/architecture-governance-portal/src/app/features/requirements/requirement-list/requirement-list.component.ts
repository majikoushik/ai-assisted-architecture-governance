import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RequirementsService } from '../../../core/services/requirements.service';
import { RequirementSubmission } from '../../../core/models/requirement.model';

@Component({
  selector: 'app-requirement-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './requirement-list.component.html',
  styleUrls: ['./requirement-list.component.css']
})
export class RequirementListComponent implements OnInit, OnChanges {
  @Input() projectId!: string;
  requirements: RequirementSubmission[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(private requirementsService: RequirementsService) {}

  ngOnInit(): void {
    if (this.projectId) {
      this.loadRequirements();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['projectId'] && !changes['projectId'].firstChange) {
      this.loadRequirements();
    }
  }

  loadRequirements(): void {
    this.isLoading = true;
    this.error = null;
    this.requirementsService.getRequirementsByProject(this.projectId).subscribe({
      next: (data) => {
        this.requirements = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load requirements.';
        this.isLoading = false;
      }
    });
  }
}
