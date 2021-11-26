import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-reports-expense',
  templateUrl: './reportExpenseView.component.html'
})
export class ReportExpenseViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando reporte de gastos'};
}
