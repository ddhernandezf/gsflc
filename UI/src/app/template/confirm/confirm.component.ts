import { Component, Input, Output, EventEmitter } from '@angular/core';
import {ConfirmModel} from '../../model/template/confirmModel';

@Component({
  selector: 'app-layout-confirm',
  templateUrl: './confirm.component.html'
})
export class ConfirmComponent {
  @Input() Properties: ConfirmModel;
  @Output() Confirm = new EventEmitter();
  @Output() CloseCofnirm = new EventEmitter();

  Click(): void {
    this.Confirm.emit();
  }

  Close(): void {
    this.CloseCofnirm.emit();
  }
}
