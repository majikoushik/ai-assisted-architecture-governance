import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Artifact } from '../../core/models/artifact.model';
import { ArtifactsService } from '../../core/services/artifacts.service';

@Component({
  selector: 'app-artifact-viewer',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './artifact-viewer.component.html'
})
export class ArtifactViewerComponent implements OnInit {
  artifact: Artifact | null = null;
  isLoading = true;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private artifactsService: ArtifactsService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.artifactsService.getArtifact(id).subscribe({
        next: (data: any) => {
          this.artifact = data;
          this.isLoading = false;
        },
        error: (err: any) => {
          this.errorMessage = 'Failed to load artifact.';
          this.isLoading = false;
        }
      });
    } else {
      this.errorMessage = 'Invalid artifact ID.';
      this.isLoading = false;
    }
  }

  downloadMarkdown(): void {
    if (this.artifact) {
      const url = this.artifactsService.getMarkdownExportUrl(this.artifact.id);
      window.open(url, '_blank');
    }
  }
}
