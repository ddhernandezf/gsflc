import {Component, EventEmitter, Input, Output} from '@angular/core';
import {VehicleTypeModel} from '../../../../model/api/catalog/vehicle/vehicleTypeModel';
import {RegistrationTypeModel} from '../../../../model/api/catalog/vehicle/registrationTypeModel';
import {BrandModel} from '../../../../model/api/catalog/vehicle/brandModel';
import {BrandModelModel} from '../../../../model/api/catalog/vehicle/brandModelModel';
import {PilotModel} from '../../../../model/api/catalog/vehicle/pilotModel';
import {VehicleModel} from '../../../../model/api/catalog/vehicle/vehicleModel';

@Component({
  selector: 'app-component-vehicle-form',
  templateUrl: './vehicleForm.component.html'
})
export class VehicleFormComponent {
  disableBrand: boolean;
  disableModel: boolean;
  srcBrand: BrandModel[];
  srcModel: BrandModelModel[];

  @Input() Visible: boolean;
  @Input() Form: VehicleModel;
  @Input() Title: string;
  @Input() DataType: VehicleTypeModel[];
  @Input() DataRegType: RegistrationTypeModel[];
  @Input() DataBrand: BrandModel[];
  @Input() DataModel: BrandModelModel[];
  @Input() DataPilot: PilotModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<VehicleModel>();

  constructor() {
    this.disableBrand = true;
    this.disableModel = true;
    this.srcBrand = [];
    this.srcModel = [];
  }

  Submit(): void {
    this.Form.brand = this.DataBrand.filter(x => x.id === this.Form.brand.id)[0];
    this.Form.brandModel = this.DataModel.filter(x => x.id === this.Form.brandModel.id)[0];
    this.Form.registrationType = this.DataRegType.filter(x => x.id === this.Form.registrationType.id)[0];
    this.Form.vehicleType = this.DataType.filter(x => x.id === this.Form.vehicleType.id)[0];

    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }

  FieldChanged(e): void {
    if (e.dataField === 'vehicleType' || e.dataField === 'vehicleType.id') {

      if (this.Form !== null && this.Form !== undefined && this.Form.vehicleType !== null) {
        this.srcBrand = this.DataBrand.filter(x => x.vehicleType.id === this.Form.vehicleType.id);
        this.disableBrand = false;
      } else {
        this.srcBrand = [];
        this.disableBrand = true;
      }

      if (this.Form.id === null) {
        this.Form.brand = null;
      }
    }

    if (e.dataField === 'brand' || e.dataField === 'brand.id') {
      if (this.Form !== null && this.Form !== undefined && this.Form.brand !== null) {
        this.srcModel = this.DataModel.filter(x => x.brand.id === this.Form.brand.id);
        this.disableModel = false;
      } else {
        this.srcModel = [];
        this.disableModel = true;
      }

      if (this.Form.id === null) {
        this.Form.brandModel = null;
      }
    }
  }
}
