import {VehicleTypeModel} from './vehicleTypeModel';
import {BrandModel} from './brandModel';

export class BrandGroupedModel extends VehicleTypeModel{
  brands: BrandModel[];
}
