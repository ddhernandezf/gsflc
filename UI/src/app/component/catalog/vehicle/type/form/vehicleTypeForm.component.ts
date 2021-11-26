import {Component, EventEmitter, Input, Output} from '@angular/core';
import {VehicleTypeModel} from '../../../../../model/api/catalog/vehicle/vehicleTypeModel';

@Component({
  selector: 'app-component-vehicle-type-form',
  templateUrl: './vehicleTypeForm.component.html'
})
export class VehicleTypeFormComponent {
  @Input() Visible: boolean;
  @Input() Form: VehicleTypeModel;
  @Input() Title: string;

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<VehicleTypeModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
