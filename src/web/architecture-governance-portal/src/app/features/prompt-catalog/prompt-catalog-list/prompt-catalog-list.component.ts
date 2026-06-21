import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PromptTemplate } from '../../../core/models/prompt.model';
import { PromptsService } from '../../../core/services/prompts.service';

@Component({
  selector: 'app-prompt-catalog-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './prompt-catalog-list.component.html'
})
export class PromptCatalogListComponent implements OnInit {
  prompts: PromptTemplate[] = [];
  isLoading = true;

  constructor(private promptsService: PromptsService) {}

  ngOnInit(): void {
    this.promptsService.getPrompts().subscribe({
      next: (data) => {
        this.prompts = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }
}
