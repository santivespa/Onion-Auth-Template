import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export const authGuard: CanActivateFn = (route, state) => {

  const _authService = inject(AuthService)
  const _router = inject(Router)
  const _snackbar = inject(MatSnackBar)

  const getRolesFromRoute = (route: ActivatedRouteSnapshot): Array<string> => {
    if (route.data?.['roles']) {
      return route.data['roles'];
    }
  
    for (const childRoute of route.children) {
      const roles = getRolesFromRoute(childRoute);
      if (roles.length > 0) {
        return roles;
      }
    }
  
    return [];
  };

  if (localStorage.getItem('token') != null) {
    const roles = getRolesFromRoute(route);
    if (roles?.length > 0) {
      if (_authService.roleMatch(roles)) {
        return true;
      } 
      else {
        localStorage.clear();
        _snackbar.open('You are not authorized to access this page!', 'OK', { duration: 4000 });
        _router.navigate(['/auth/login']);
        return false;
      }
    }
    else {
      return false;
    }
  } else {
    return false;
  }
}