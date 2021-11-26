import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BlockModel} from '../../../../../model/template/blockModel';
import {TransactionDetailModel} from '../../../../../model/api/operation/transactionDetailModel';
import {environment} from '../../../../../../environments/environment';
import {TransactionDetailService} from '../../../../../service/api/operation/transactionDetail.service';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-component-transaction-detail-form',
  templateUrl: './detailTransactionForm.component.html'
})
export class DetailTransactionFormComponent {
  currencyFormat: string;

  @Input() Blocker: BlockModel = { block: null, message: null };
  @Input() Form: TransactionDetailModel;
  @Input() TransactionId: number;
  @Input() IsItEmpty: boolean;

  @Output() CloseForm = new EventEmitter();
  @Output() FormResult = new EventEmitter<TransactionDetailModel>();

  constructor(private detailService: TransactionDetailService) {
    this.Form = { id: null, quantity: null, description: null, unitPrice: null, totalPrice: null, transaction: this.TransactionId };
    this.currencyFormat = environment.stringFormats.currency;
  }

  Submit(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      if (this.Form.id == null) {
        this.Blocker.message = 'Guardando';
        this.Form.transaction = this.TransactionId;
        this.detailService.Add(this.Form).subscribe(
          () => { this.FormResult.emit(); },
          error => {
            notify(error.error, 'error', 10000);
            this.Blocker.block = false;
          }
        );
      } else {
        this.Blocker.message = 'Actualizando';
        this.detailService.Update(this.Form).subscribe(
          () => { this.FormResult.emit(); },
          error => {
            notify(error.error, 'error', 10000);
            this.Blocker.block = false;
          }
        );
      }
    });
  }

  Close(): void {
    if (!this.IsItEmpty) {
      this.CloseForm.emit();
    }
  }

  FormFieldChange(e): void {
    if (this.Form !== undefined) {
      const quantity: number = this.Form.quantity === null ? 0 : this.Form.quantity;
      const price: number = this.Form.unitPrice === null ? 0 : this.Form.unitPrice;
      this.Form.totalPrice = quantity * price;
    }
  }
}
