import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BrandModelModel} from '../../../../../model/api/catalog/vehicle/brandModelModel';
import {BrandModel} from '../../../../../model/api/catalog/vehicle/brandModel';
import DevExpress from 'devextreme';
import DataSource = DevExpress.data.DataSource;

@Component({
  selector: 'app-component-brand-model-form',
  templateUrl: './brandModelForm.component.html'
})
export class BrandModelFormComponent {
  @Input() Visible: boolean;
  @Input() Form: BrandModelModel;
  @Input() Title: string;
  @Input() DataType: DataSource;
  @Input() Type: BrandModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<BrandModelModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
