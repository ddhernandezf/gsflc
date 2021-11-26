import {ItemModel} from './balance/itemModel';
import {ResumeChartModel} from './balance/resumeChartModel';

export class BalanceModel {
  serviceTotal: number;
  expenseTotal: number;
  total: number;
  data: ItemModel[];
  resumeChart: ResumeChartModel[];
}
