import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PromptTemplate } from '../../../core/models/prompt.model';
import { PromptsService } from '../../../core/services/prompts.service';

@Component({
  selector: 'app-prompt-catalog-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './prompt-catalog-detail.component.html'
})
export class PromptCatalogDetailComponent implements OnInit {
  prompt: PromptTemplate | null = null;
  isLoading = true;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private promptsService: PromptsService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.promptsService.getPromptById(id).subscribe({
        next: (data) => {
          this.prompt = data;
          this.isLoading = false;
        },
        error: (err) => {
          this.errorMessage = 'Failed to load prompt template.';
          this.isLoading = false;
        }
      });
    } else {
      this.errorMessage = 'Invalid prompt ID.';
      this.isLoading = false;
    }
  }
}
