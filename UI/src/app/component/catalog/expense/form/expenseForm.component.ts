import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ExpenseModel} from '../../../../model/api/catalog/expense/expenseModel';
import {ExpenseTypeModel} from '../../../../model/api/catalog/expense/expenseTypeModel';

@Component({
  selector: 'app-component-expense-form',
  templateUrl: './expenseForm.component.html'
})
export class ExpenseFormComponent {
  @Input() Visible: boolean;
  @Input() Form: ExpenseModel;
  @Input() Title: string;
  @Input() DataType: ExpenseTypeModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<ExpenseModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
