import { Component, inject, signal } from '@angular/core';
import { NavbarComponent } from '../ui/navbar/navbar.component';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { SidebarComponent } from '../ui/sidebar/sidebar.component';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { Response } from '../../models/Response';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [NavbarComponent, RouterOutlet, RouterModule, MatButtonModule, SidebarComponent, NgIf, MatProgressSpinner],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {

  readonly _authService = inject(AuthService);
  readonly router = inject(Router);
  checkingAuth = signal(true);


  
  constructor(){
    if(this._authService.isSigned){
      this._authService.renewToken().subscribe({
        next: (res: Response) => {
          localStorage.setItem("token", res.data.token);
          localStorage.setItem("email", res.data.email);
          // this.router.navigate(['/home']);
        }
      }).add(() => this.checkingAuth.set(false));
    } else {
      this.checkingAuth.set(false);
    }
  }

}
