import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BrandModel} from '../../../../../model/api/catalog/vehicle/brandModel';
import {VehicleTypeModel} from '../../../../../model/api/catalog/vehicle/vehicleTypeModel';

@Component({
  selector: 'app-component-brand-form',
  templateUrl: './brandForm.component.html'
})
export class BrandFormComponent {
  @Input() Visible: boolean;
  @Input() Form: BrandModel;
  @Input() Title: string;
  @Input() DataType: VehicleTypeModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<BrandModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
