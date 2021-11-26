import {VehicleModel} from '../catalog/vehicle/vehicleModel';
import {TransactionTypeModel} from './transactionTypeModel';
import {MovementModel} from './movementModel';

export class TransactionGridModel {
  id: number;
  type: TransactionTypeModel;
  vehicle: VehicleModel;
  movement: MovementModel;
  registerDate: Date;
  transactionDate: Date;
  userName: string;
  userId: number;
  total: number;
  valueTotal: number;
}
