import {VehicleModel} from '../api/catalog/vehicle/vehicleModel';

export class SearchTransactionForm {
  month: number;
  year: number;
  vehicleId: number;
  vehicle: VehicleModel;
}
