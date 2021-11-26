import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ExpenseTypeModel} from '../../../../../model/api/catalog/expense/expenseTypeModel';

@Component({
  selector: 'app-component-expense-type-form',
  templateUrl: './expenseTypeForm.component.html'
})
export class ExpenseTypeFormComponent {
  @Input() Visible: boolean;
  @Input() Form: ExpenseTypeModel;
  @Input() Title: string;

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<ExpenseTypeModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
