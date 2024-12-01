import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const _snackbar = inject(MatSnackBar);

  const authToken = localStorage.getItem("token");

  
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authToken}`
    }
  });

  return next(authReq).pipe(
    tap((res: any) => {
      if (res.body && !res.body.succeeded) {
        _snackbar.open(res.body.message, 'OK', { duration: 4000 });
      }
    }),
    catchError((error) => {
      let message = 'Something went wrong. Please try again later.'; 
        
        if (error.status === 403) {
          message = 'You are not authorized to access this resource.';
        } else if (error.error && error.error.message) {
          message = error.error.message;
        }

        _snackbar.open(message, 'OK', { duration: 4000 });
 
      return throwError(() => error);
    })
  );
};