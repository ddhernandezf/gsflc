import {Component, EventEmitter, Input, Output} from '@angular/core';
import {UserModel} from '../../../../model/api/security/userModel';
import {RoleModel} from '../../../../model/api/security/roleModel';

@Component({
  selector: 'app-component-user-form',
  templateUrl: './userForm.component.html'
})
export class UserFormComponent {
  @Input() Visible: boolean;
  @Input() Form: UserModel;
  @Input() Title: string;
  @Input() DataRole: RoleModel[];

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<UserModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
