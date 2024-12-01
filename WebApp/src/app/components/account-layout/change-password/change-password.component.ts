import { Component, inject, signal } from '@angular/core';
import { FORM_MODULES } from '../../../helpers/shared_modules/FormModules';
import { NgIf } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AccountService } from '../../../services/account.service';
import { AbstractControl, FormBuilder, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Response } from '../../../models/Response';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [FORM_MODULES, NgIf, MatProgressSpinnerModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {

  readonly _accountService = inject(AccountService);
  readonly _fb = inject(FormBuilder);
  readonly _snackbar = inject(MatSnackBar);

  loading = signal(false);

  showPasswords = false; // Variable para controlar el estado del checkbox
  toggleShowPasswords() {
    this.showPasswords = !this.showPasswords; // Cambia el estado de mostrar/ocultar contraseñas
  }

  readonly form = this._fb.group({
    currentPassword: this._fb.control<string>(
      '', 
      [Validators.required, Validators.minLength(4), Validators.maxLength(40)]
    ),
    newPassword: this._fb.control<string>(
      '', 
      [Validators.required, Validators.minLength(4), Validators.maxLength(40)]
    ),
    newPasswordConfirmation: this._fb.control<string>(
      '', 
      [Validators.required, Validators.minLength(4), Validators.maxLength(40)]
    )
  }, { validators: this.passwordMatchValidator() });
  
  passwordMatchValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const newPassword = control.get('newPassword')?.value;
      const newPasswordConfirmation = control.get('newPasswordConfirmation')?.value;
      return newPassword === newPasswordConfirmation ? null : { passwordMismatch: true };
    };
  }

  onSubmit = () => {
    if(this.form.valid){
      this.loading.set(true);
      this._accountService.changePassword(this.form.value).subscribe({
        next:(res: Response) => {
          if(res.succeeded){
            this._snackbar.open('Password successfully updated', 'OK', { duration: 3000 });
          }
        }
      }).add(()=>this.loading.set(false));
    }
  }
}
