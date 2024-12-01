import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterLink } from '@angular/router';
import { SHARED_MATERIAL } from '../../../helpers/shared_modules/shared-material';
import { AuthService } from '../../../services/auth.service';
import { Response } from '../../../models/Response';
@Component({
  selector: 'app-recover-password',
  standalone: true,
  imports: [SHARED_MATERIAL, ReactiveFormsModule, RouterLink],
  templateUrl: './recover-password.component.html',
  styleUrl: './recover-password.component.scss'
})
export class RecoverPasswordComponent {

  form = this.formBuilder.group({
    email: ['', Validators.required]
  });

  loading = false;
  constructor(private formBuilder: FormBuilder, private _authService: AuthService, private _snackbar: MatSnackBar) {}

  onSubmit = () => {
    if(this.form.valid){
      this.loading = true;
      this._authService.recoverPassowrd(this.form.value).subscribe({
        next: (res: Response) => {
          if(res.succeeded){
            this._snackbar.open('Password recovery email sent.', 'OK', { duration: 3000 });
          }
        }
      }).add(() => this.loading = false);
    }
  }
}
