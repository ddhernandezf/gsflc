import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-service',
  templateUrl: './serviceView.component.html'
})
export class ServiceViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
