import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-operations-transactions',
  templateUrl: './transactionsView.component.html'
})
export class TransactionsViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando transacciones'};
}
