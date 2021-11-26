import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-service-type',
  templateUrl: './serviceTypeView.component.html'
})
export class ServiceTypeViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
