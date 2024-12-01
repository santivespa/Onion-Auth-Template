import { Injectable, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router, CanActivateFn } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';



export const noAuthGuard: CanActivateFn = (route, state) => {

  const _router = inject(Router)

  if (localStorage.getItem('token') != null) {
    _router.navigate(['/home']);
    return false;
  } else {
    return true;
  }

};


