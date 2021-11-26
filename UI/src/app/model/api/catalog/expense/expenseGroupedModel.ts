import {ExpenseTypeModel} from './expenseTypeModel';
import {ExpenseModel} from './expenseModel';

export class ExpenseGroupedModel extends ExpenseTypeModel{
  expenses: ExpenseModel[];
}
