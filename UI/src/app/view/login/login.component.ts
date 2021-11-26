import {AfterViewInit, Component, Inject, Input} from '@angular/core';
import {BlockModel} from '../../model/template/blockModel';
import {appWindow} from '../../provider/appWindowProvider';
import {LoginForm} from '../../model/forms/loginForm';
import {LocalStorageService} from '../../service/storage/localStorage.service';
import notify from 'devextreme/ui/notify';
import {UserService} from '../../service/api/security/user.service';
import {Router} from '@angular/router';
import {ChangePasswordModel} from '../../model/api/security/changePasswordModel';
import {UserInfoModel} from '../../model/api/security/userInfoModel';

@Component({
  selector: 'app-view-index',
  templateUrl: './login.component.html'
})
export class LoginComponent  implements AfterViewInit {
  form: LoginForm;
  showLoginForm: boolean;
  showChangeForm: boolean;
  changeForm: ChangePasswordModel;
  selectedUser: UserInfoModel;

  @Input() Blocker: BlockModel = { block: false, message: 'Cargando'};

  constructor(@Inject(appWindow) private window: Window,
              private router: Router,
              private localService: LocalStorageService,
              private userService: UserService) {
    if (this.localService.GetUserInfo() !== null) {
      this.router.navigateByUrl('');
    } else {
      this.form = { userName: null, password: null };
      this.Blocker.block = true;
      this.Blocker.message = 'Iniciando aplicación';
      this.showChangeForm = false;
      this.showLoginForm = true;
      this.selectedUser = { email: null, name: null, reset: null, role: null };
      this.changeForm = { email: null, confirm: null, password: null };
    }
  }

  ngAfterViewInit(): void { setTimeout(() => { this.Blocker.block = false; }); }

  Login(): void {
    this.Blocker.block = true;
    this.Blocker.message = 'Autenticando';

    this.localService.SetUser(this.form);
    this.Authenticate();
  }

  Change(item: ChangePasswordModel): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Cambiando password';

      this.userService.ChangePassword(item).subscribe(
        ok => {
          this.form = { userName: null, password: null };
          this.showChangeForm = false;
          this.showLoginForm = true;
          this.selectedUser = { email: null, name: null, reset: null, role: null };
          this.changeForm = { email: null, confirm: null, password: null };

          notify('Ingrese sus nuevas credenciales', 'success', 5000);
        },
        error => { notify(error.error, 'error', 10000); }
      ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1000); });
    });
  }

  private Authenticate() {
    this.userService.Authenticate().subscribe(
      ok => {
        if (ok.reset) {
          this.selectedUser = ok;
          this.showLoginForm = false;
          this.showChangeForm = true;
        } else {
          this.localService.SetUserInfo(ok);
          this.router.navigateByUrl('');
        }
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1000); });
  }
}
