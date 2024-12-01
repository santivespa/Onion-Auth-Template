import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, RouterLink } from '@angular/router';
import { SHARED_MATERIAL } from '../../../helpers/shared_modules/shared-material';
import { AuthService } from '../../../services/auth.service';
import { Response } from '../../../models/Response';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [SHARED_MATERIAL, ReactiveFormsModule, RouterLink],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
  
  form = this.formBuilder.group({
    fullName: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  loading = false;
  constructor(private formBuilder: FormBuilder, private _authService: AuthService, private _snackbar: MatSnackBar, private router: Router) {}

  onSubmit = () => {
    if(this.form.valid){
      this.loading = true;
      this._authService.signup(this.form.value).subscribe({
        next: (res: Response) => {
          if(res.succeeded){
            localStorage.setItem("token", res.data.token);
            localStorage.setItem("email", res.data.email);
            this.router.navigate(['/home']);
          }
        }
      }).add(() => this.loading = false);
    }
  }
}
