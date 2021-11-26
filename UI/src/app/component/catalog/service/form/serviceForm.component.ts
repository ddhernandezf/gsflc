import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ServiceModel} from '../../../../model/api/catalog/service/serviceModel';
import {ServiceTypeModel} from '../../../../model/api/catalog/service/serviceTypeModel';

@Component({
  selector: 'app-component-service-form',
  templateUrl: './serviceForm.component.html'
})
export class ServiceFormComponent {
  @Input() Visible: boolean;
  @Input() Form: ServiceModel;
  @Input() Title: string;
  @Input() DataType: ServiceTypeModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<ServiceModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
