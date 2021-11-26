import {VehicleTypeModel} from './vehicleTypeModel';
import {RegistrationTypeModel} from './registrationTypeModel';
import {BrandModel} from './brandModel';
import {BrandModelModel} from './brandModelModel';

export class VehicleModel {
  id: number;
  registration: string;
  year: number;
  name: string;
  active: boolean;
  text: string;
  vehicleType: VehicleTypeModel;
  registrationType: RegistrationTypeModel;
  brand: BrandModel;
  brandModel: BrandModelModel;
}
