import { Component, inject, signal } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { Response } from '../../../models/Response';
import { FormBuilder, Validators } from '@angular/forms';
import { FORM_MODULES } from '../../../helpers/shared_modules/FormModules';
import { NgIf } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatRippleModule} from '@angular/material/core';

@Component({
  selector: 'app-my-account',
  standalone: true,
  imports: [FORM_MODULES, NgIf, MatProgressSpinnerModule],
  templateUrl: './my-account.component.html',
  styleUrl: './my-account.component.scss'
})
export class MyAccountComponent {

  readonly _accountService = inject(AccountService);
  readonly _fb = inject(FormBuilder);
  readonly _snackbar = inject(MatSnackBar);

  loading = signal(false);
  loadingUser = signal(true);

  readonly form = this._fb.group({
    email: this._fb.control<string>({ value: '', disabled: true}),
    fullName: this._fb.control<string>('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
  });


  constructor() {
    this.getAccount();
  }

  getAccount = () => {
    this._accountService.getAccount().subscribe({
      next: (res: Response) => {
        if(res.succeeded){
          this.form.patchValue({
            email: res.data.email,
            fullName: res.data.fullName
          });
        }
      }
    }).add(()=>this.loadingUser.set(false));
  }

  onSubmit = () => {
    if(this.form.valid){
      const data = {
        fullName: this.form.value.fullName
      }
      this.loading.set(true);
      this._accountService.updateAccount(data).subscribe({
        next:(res: Response) => {
          if(res.succeeded){
            this._snackbar.open('Account successfully updated', 'OK', { duration: 3000 });
          }
        }
      }).add(()=>this.loading.set(false));
    }
  }
}
