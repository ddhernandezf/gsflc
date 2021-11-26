import {VehicleModel} from '../catalog/vehicle/vehicleModel';
import {ServiceModel} from '../catalog/service/serviceModel';
import {ExpenseModel} from '../catalog/expense/expenseModel';

export class TransactionModel {
  id: number;
  vehicle: VehicleModel;
  service: ServiceModel;
  expense: ExpenseModel;
  transactionDate: Date;
}
