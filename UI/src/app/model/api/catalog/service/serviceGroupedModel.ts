import {ServiceTypeModel} from './serviceTypeModel';
import {ServiceModel} from './serviceModel';

export class ServiceGroupedModel extends ServiceTypeModel{
  services: ServiceModel[]
}
