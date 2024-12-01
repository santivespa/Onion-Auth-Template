import { Routes } from '@angular/router';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/auth-layout/login/login.component';
import { SignupComponent } from './components/auth-layout/signup/signup.component';
import { RecoverPasswordComponent } from './components/auth-layout/recover-password/recover-password.component';
import { SetPasswordComponent } from './components/auth-layout/set-password/set-password.component';
import { noAuthGuard } from './guards/noauth.guard';
import { AuthLayoutComponent } from './components/auth-layout/auth-layout.component';
import { authGuard } from './guards/auth.guard';
import { AccountLayoutComponent } from './components/account-layout/account-layout.component';
import { MyAccountComponent } from './components/account-layout/my-account/my-account.component';
import { ChangePasswordComponent } from './components/account-layout/change-password/change-password.component';

const roleAdmin = 'Admin';
const roleManager = 'Manager';
const allRoles = [roleAdmin, roleManager]


export const routes: Routes = [
  {
    path: '',
    redirectTo: '/auth/login',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    canActivate: [noAuthGuard],
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'sign-up', component: SignupComponent },
      { path: 'recover-password', component: RecoverPasswordComponent },
      { path: 'reset-password', component: SetPasswordComponent },
    ]
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivateChild: [authGuard],
    children: [
      { path: 'home', component: HomeComponent, data: { roles: allRoles } },
      {
        path: 'account',
        component: AccountLayoutComponent,
        canActivateChild: [authGuard],
        children: [
          { path: '', component: MyAccountComponent, data: { roles: allRoles } },
          { path: 'change-password', component: ChangePasswordComponent, data: { roles: allRoles } }
        ],
      },
      { path: '', redirectTo: '/home', pathMatch: "full" },
    ]
  }

];
