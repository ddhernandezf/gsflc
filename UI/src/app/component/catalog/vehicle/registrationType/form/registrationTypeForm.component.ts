import {Component, EventEmitter, Input, Output} from '@angular/core';
import {RegistrationTypeModel} from '../../../../../model/api/catalog/vehicle/registrationTypeModel';

@Component({
  selector: 'app-component-registration-type-form',
  templateUrl: './registrationTypeForm.component.html'
})
export class RegistrationTypeFormComponent {
  @Input() Visible: boolean;
  @Input() Form: RegistrationTypeModel;
  @Input() Title: string;

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<RegistrationTypeModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
