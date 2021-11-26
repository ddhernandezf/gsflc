import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-reports-balance',
  templateUrl: './reportBalanceView.component.html'
})
export class ReportBalanceViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando reporte balance'};
}
