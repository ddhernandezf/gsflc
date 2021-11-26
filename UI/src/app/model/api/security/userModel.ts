import {RoleModel} from './roleModel';

export class UserModel {
  id: number;
  email: string;
  password: string;
  name: string;
  reset: boolean;
  active: boolean;
  role: RoleModel;
}
