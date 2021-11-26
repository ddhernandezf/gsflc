import {ExpenseTypeModel} from './expenseTypeModel';

export class ExpenseModel {
  id: number;
  name: string;
  expenseType: ExpenseTypeModel;
}
