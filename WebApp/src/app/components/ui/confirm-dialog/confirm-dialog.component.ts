import { Component, inject, signal } from '@angular/core';
import { DIALOG_MODULES } from '../../../helpers/shared_modules/DialogModules';
import { FORM_MODULES } from '../../../helpers/shared_modules/FormModules';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [ReactiveFormsModule,
    CommonModule,
    NgIf,
    NgFor,
    FORM_MODULES,
    DIALOG_MODULES],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss'
})
export class ConfirmDialogComponent {

  readonly dialogRef = inject(MatDialogRef<ConfirmDialogComponent>);
  readonly data = inject<{title: string, subTitle: string}>(MAT_DIALOG_DATA);
  title = signal(this.data.title);
  subTitle = signal(this.data.subTitle);


  onConfirm = (confirmValue: boolean) => {
    this.dialogRef.close(confirmValue);
  }

}
