import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { SHARED_MATERIAL } from '../../../helpers/shared_modules/shared-material';
import { AuthService } from '../../../services/auth.service';
import { Response } from '../../../models/Response';


@Component({
  selector: 'app-set-password',
  standalone: true,
  imports: [SHARED_MATERIAL, ReactiveFormsModule, RouterLink],
  templateUrl: './set-password.component.html',
  styleUrl: './set-password.component.scss'
})
export class SetPasswordComponent {
  
  token: string;
  email: string;


  form = this.formBuilder.group({
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  });

  loading = false;

  constructor(private formBuilder: FormBuilder, private _authService: AuthService, private _snackbar: MatSnackBar, private route: ActivatedRoute, private router: Router) {
    route.params.subscribe(params=>{
      this.token = this.route.snapshot.queryParamMap.get('token');
      this.email = this.route.snapshot.queryParamMap.get('email');
      console.log(this.token);
      console.log(this.email);
    });
 
    
  }

  onSubmit = () => {
    if(this.form.valid){
      this.loading = true;
      var model = {
        email: this.email,
        token: this.token,
        password: this.form.value.password
      }

      this._authService.resetPassword(model).subscribe({
        next: (res: Response) => {
          if(res.succeeded){
            this._snackbar.open('Password updated', 'OK', { duration: 3000 });
            this.router.navigate(['/auth/login'])
          }
        }
      }).add(() => this.loading = false);
    }
  }
}
