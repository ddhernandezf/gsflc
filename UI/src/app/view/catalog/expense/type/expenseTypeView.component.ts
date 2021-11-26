import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-expense-type',
  templateUrl: './expenseTypeView.component.html'
})
export class ExpenseTypeViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
