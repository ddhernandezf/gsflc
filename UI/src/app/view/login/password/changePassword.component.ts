import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ChangePasswordModel} from '../../../model/api/security/changePasswordModel';
import {UserInfoModel} from '../../../model/api/security/userInfoModel';

@Component({
  selector: 'app-view-login-change-password',
  templateUrl: './changePassword.component.html'
})
export class ChangePasswordComponent {
  @Input() Form: ChangePasswordModel;
  @Input() DataUser: UserInfoModel;

  @Output() FormResult = new EventEmitter<ChangePasswordModel>();

  Submit(): void {
    this.Form.email = this.DataUser.email
    this.FormResult.emit(this.Form);
  }
}
