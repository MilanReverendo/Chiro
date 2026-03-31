import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';
import { AuthService } from '../services/auth-service';
import { catchError, switchMap, throwError } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const platformId = inject(PLATFORM_ID);

  const token = isPlatformBrowser(platformId) ? localStorage.getItem('at') : null;

  // Clone the request to add the Bearer token
  let clonedReq = req;
  if (token) {
    clonedReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(clonedReq).pipe(
    catchError((error) => {
      // If we get a 401, it means the Access Token is likely expired
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return authService.refreshToken().pipe(
          switchMap((res) => {
            // Success! Save new tokens
            localStorage.setItem('at', res.accesToken);
            localStorage.setItem('rt', res.refreshToken);

            // Retry the original request with the NEW Access Token
            return next(req.clone({
              setHeaders: { Authorization: `Bearer ${res.accesToken}` }
            }));
          }),
          catchError((err) => {
            // Refresh token also failed/expired! Force logout
            authService.logout();
            return throwError(() => err);
          })
        );
      }
      return throwError(() => error);
    })
  );
};
