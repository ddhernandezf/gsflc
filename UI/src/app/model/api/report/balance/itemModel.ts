import {HeaderModel} from './headerModel';

export class ItemModel {
  id: number;
  transaction: HeaderModel;
  quantity: number;
  description: string;
  unitPrice: number;
  totalPrice: number;
}
