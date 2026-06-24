import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  showError(message: string, correlationId?: string): void {
    const correlationText = correlationId ? ` (Correlation ID: ${correlationId})` : '';
    console.error(`[Notification] ${message}${correlationText}`);
    // In a full implementation, this would show a snackbar or toast.
    // e.g. this.snackBar.open(message, 'Close', { duration: 5000 });
  }

  showSuccess(message: string): void {
    console.log(`[Notification] ${message}`);
  }
}
