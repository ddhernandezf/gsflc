import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-vehicle-type',
  templateUrl: './vehicleTypeView.component.html'
})
export class VehicleTypeViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
