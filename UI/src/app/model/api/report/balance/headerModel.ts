import {VehicleModel} from '../../catalog/vehicle/vehicleModel';
import {TransactionTypeModel} from '../../operation/transactionTypeModel';
import {OptionModel} from './optionModel';

export class HeaderModel {
  id: number;
  vehicle: VehicleModel;
  type: TransactionTypeModel;
  option: OptionModel;
  registerDate: Date;
  transactionDate: Date;
  total: number;
}
