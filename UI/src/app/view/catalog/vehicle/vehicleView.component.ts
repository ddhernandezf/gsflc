import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-vehicle',
  templateUrl: './vehicleView.component.html'
})
export class VehicleViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
