import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-pilot',
  templateUrl: './pilotView.component.html'
})
export class PilotViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
