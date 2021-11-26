import {Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {TransactionDetailModel} from '../../../../model/api/operation/transactionDetailModel';
import {BlockModel} from '../../../../model/template/blockModel';
import {environment} from '../../../../../environments/environment';
import {DxDataGridComponent} from 'devextreme-angular';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-component-transaction-detail',
  templateUrl: './detailTransaction.component.html'
})
export class DetailTransactionComponent {
  currencyFormat: string;
  item: TransactionDetailModel;
  showForm: boolean;

  @Input() Blocker: BlockModel = { block: null, message: null };
  @Input() Visible: boolean;
  @Input() Data: TransactionDetailModel[];
  @Input() Title: string;
  @Input() SubTitle: Date;
  @Input() TransactionId: number;
  @Input() IsItEmpty: boolean;

  @Input() set ShowForm(value: boolean) {
    this.showForm = value;
    if (value) {
      this.item = { id: null, quantity: null, description: null, unitPrice: null, totalPrice: null, transaction: null };
    }
  } get ShowForm(): boolean { return this.showForm; }

  @Output() Closing = new EventEmitter();
  @Output() DetailSaved = new EventEmitter();
  @Output() DeleteSelected = new EventEmitter<TransactionDetailModel>();

  @ViewChild('DxgDetailTran', { static: false}) DxgDetailTran: DxDataGridComponent;

  constructor() {
    this.Data = [];
    this.currencyFormat = environment.stringFormats.currency;
    this.item = { id: null, quantity: null, description: null, unitPrice: null, totalPrice: null, transaction: null };
    this.ShowForm = false;
  }

  New(): void {
    setTimeout(() => {
      this.ShowForm = true;
      this.item = { id: null, quantity: null, description: null, unitPrice: null, totalPrice: null, transaction: this.TransactionId };
    });
  }

  Edit(): void {
    setTimeout(() => {
      if (this.DxgDetailTran.instance.getSelectedRowsData().length === 0) {
        notify('No ha seleccionado ningún registro', 'error', 3000);
        return;
      }
      this.ShowForm = true;
      this.item = this.GetSelectedItem();
    });
  }

  Delete(): void {
    setTimeout(() => {
      if (this.DxgDetailTran.instance.getSelectedRowsData().length === 0) {
        notify('No ha seleccionado ningún registro', 'error', 3000);
        return;
      }

      this.DeleteSelected.emit(this.GetSelectedItem());
    });
  }

  Cancel(): void {
    setTimeout(() => { this.ShowForm = false; });
  }

  Close(): void {
    this.Closing.emit();
  }

  DetailItem() {
    this.DetailSaved.emit();
    this.ShowForm = false;
  }

  private GetSelectedItem(): TransactionDetailModel {
    return this.DxgDetailTran.instance.getSelectedRowsData()[0];
  }
}
