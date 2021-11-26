import {VehicleTypeModel} from './vehicleTypeModel';
import {VehicleModel} from './vehicleModel';

export class VehicleGroupedModel extends VehicleTypeModel{
  vehicles: VehicleModel[];
}
