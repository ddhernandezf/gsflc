import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-brand-model',
  templateUrl: './brandModelView.component.html'
})
export class BrandModelViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
