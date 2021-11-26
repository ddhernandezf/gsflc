import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-expense',
  templateUrl: './expenseView.component.html'
})
export class ExpenseViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
