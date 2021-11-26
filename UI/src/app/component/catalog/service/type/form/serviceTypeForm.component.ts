import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ServiceTypeModel} from '../../../../../model/api/catalog/service/serviceTypeModel';

@Component({
  selector: 'app-component-service-type-form',
  templateUrl: './serviceTypeForm.component.html'
})
export class ServiceTypeFormComponent {
  @Input() Visible: boolean;
  @Input() Form: ServiceTypeModel;
  @Input() Title: string;

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<ServiceTypeModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
