import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-brand',
  templateUrl: './brandView.component.html'
})
export class BrandViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
