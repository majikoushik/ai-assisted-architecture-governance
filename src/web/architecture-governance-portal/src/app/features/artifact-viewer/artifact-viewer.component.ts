import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Artifact, Review } from '../../core/models/artifact.model';
import { ArtifactsService } from '../../core/services/artifacts.service';

@Component({
  selector: 'app-artifact-viewer',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './artifact-viewer.component.html'
})
export class ArtifactViewerComponent implements OnInit {
  artifact: Artifact | null = null;
  versions: Artifact[] = [];
  reviews: Review[] = [];
  isLoading = true;
  errorMessage = '';
  
  reviewForm: FormGroup;
  isSubmittingReview = false;
  reviewErrorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private artifactsService: ArtifactsService,
    private fb: FormBuilder
  ) {
    this.reviewForm = this.fb.group({
      reviewerName: ['', [Validators.required, Validators.maxLength(200)]],
      reviewStatus: ['', Validators.required],
      comments: ['', Validators.maxLength(4000)]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadArtifactData(id);
    } else {
      this.errorMessage = 'Invalid artifact ID.';
      this.isLoading = false;
    }
  }

  loadArtifactData(id: string): void {
    this.isLoading = true;
    this.artifactsService.getArtifact(id).subscribe({
      next: (data) => {
        this.artifact = data;
        this.loadVersions(data.id);
        this.loadReviews(data.id);
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load artifact.';
        this.isLoading = false;
      }
    });
  }

  loadVersions(id: string): void {
    this.artifactsService.getVersions(id).subscribe({
      next: (data) => this.versions = data,
      error: () => console.error('Failed to load versions')
    });
  }

  loadReviews(id: string): void {
    this.artifactsService.getReviews(id).subscribe({
      next: (data) => this.reviews = data,
      error: () => console.error('Failed to load reviews')
    });
  }

  switchVersion(event: any): void {
    const versionId = event.target.value;
    if (versionId && this.artifact && versionId !== this.artifact.id) {
      this.loadArtifactData(versionId);
    }
  }

  submitReview(): void {
    if (this.reviewForm.invalid || !this.artifact) return;

    this.isSubmittingReview = true;
    this.reviewErrorMessage = '';

    const request = this.reviewForm.value;
    
    this.artifactsService.createReview(this.artifact.id, request).subscribe({
      next: (review) => {
        this.reviews.unshift(review);
        // Update the current artifact status
        this.artifact!.status = review.status;
        this.reviewForm.reset({ reviewStatus: '' });
        this.isSubmittingReview = false;
      },
      error: () => {
        this.reviewErrorMessage = 'Failed to submit review.';
        this.isSubmittingReview = false;
      }
    });
  }


  downloadMarkdown(): void {
    if (this.artifact) {
      const url = this.artifactsService.getMarkdownExportUrl(this.artifact.id);
      window.open(url, '_blank');
    }
  }
}
