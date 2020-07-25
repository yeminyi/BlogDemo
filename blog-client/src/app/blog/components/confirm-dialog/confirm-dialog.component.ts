import {ChangeDetectionStrategy, Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

export interface ConfirmDialog {
  title: string;
  content: string;
  confirmAction: string;
}

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <h1 mat-dialog-title>{{dialog.title}}</h1>
    <div mat-dialog-content>{{dialog.content}}</div>
    <div mat-dialog-actions>
      <button mat-raised-button color="primary" (click)="handleAction(true)">{{dialog.confirmAction}}</button>
      <button mat-raised-button mat-dialog-close type="button" (click)="handleAction(false)">Close</button>
    </div>
  `,
  styles: [``],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConfirmDialogComponent {

  dialog: ConfirmDialog;

  constructor(@Inject(MAT_DIALOG_DATA) private data: any,
              private dialogRef: MatDialogRef<ConfirmDialogComponent>) {
    if (this.data.dialog !== undefined || this.data.dialog !== null) {
      this.dialog = this.data.dialog;
    }
  }

  handleAction(result: boolean) {
    this.dialogRef.close(result);
  }
}
