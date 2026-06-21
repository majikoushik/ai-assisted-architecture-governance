import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { inject } from '@angular/core';

export const errorInterceptor: HttpInterceptorFn = (request, next) => {
  return next(request).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unexpected error occurred.';
      let correlationId = error.headers?.get('X-Correlation-ID') || 'Unknown';

      if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // Server-side error
        if (error.status === 400 && error.error?.errors) {
          errorMessage = 'Validation failed. Please check the form and try again.';
        } else if (error.error?.detail) {
          errorMessage = error.error.detail;
        } else if (error.status === 404) {
          errorMessage = 'The requested resource was not found.';
        } else if (error.status >= 500) {
          errorMessage = 'A server error occurred. Please try again later.';
        }

        // Extract correlation ID from the Problem Details payload if available
        if (error.error?.correlationId) {
          correlationId = error.error.correlationId;
        }
      }

      console.error(`[Correlation ID: ${correlationId}] HTTP Error:`, error);
      
      // In a real application, inject a Snackbar/Toast service here and display `errorMessage`
      // For this MVP, we log cleanly and throw.
      return throwError(() => new Error(errorMessage));
    })
  );
};
