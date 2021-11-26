import {AfterViewInit, Component, Inject, Input} from '@angular/core';
import {BlockModel} from '../../model/template/blockModel';
import {appWindow} from '../../provider/appWindowProvider';
import {UserService} from '../../service/api/security/user.service';
import {ActionModel} from '../../model/api/security/actionModel';
import notify from 'devextreme/ui/notify';
import {Router} from '@angular/router';
import {environment} from '../../../environments/environment';
import {LocalStorageService} from '../../service/storage/localStorage.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html'
})
export class LayoutComponent implements AfterViewInit {
  width: number = null;
  screenSize: string = null;
  dataActions: ActionModel[];
  showKeepAlive: boolean;
  sessionAlive: any;
  completeName: string;

  @Input() Blocker: BlockModel = { block: false, message: 'Cargando'};

  constructor(@Inject(appWindow) private window: Window,
              private router: Router,
              private userService: UserService,
              private localService: LocalStorageService) {
    this.screenSize = this.screen(this.width);
    this.width = window.screen.width;
    this.dataActions = [];
    this.Blocker.block = true;
    this.Blocker.message = 'Cargando';
    this.showKeepAlive = false;
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.GetActions();
      this.KeepingAlive();
      this.completeName = this.localService.GetUserInfo().name;
    });
  }

  screen(width) {
    if (width < 768) {
      return 'xs';
    } else if (width < 992) {
      return 'sm';
    } else if (width < 1200) {
      return 'md';
    } else {
      return 'lg';
    }
  }

  ItemClick(data: any) {
    if (data.itemData.url !== null) {
      if (data.itemData.url === 'GetOut') {
        this.Blocker.block = true;
        this.Blocker.message = 'Cerrando sesión';
        localStorage.clear();
        setTimeout(() => { window.location.reload(); }, 500);
      } else {
        this.Blocker.block = true;
        this.Blocker.message = 'Redireccionando';
        setTimeout(() => { this.router.navigateByUrl(data.itemData.url); }, 500);
      }
    }
  }

  CloseAlive(): void {
    this.showKeepAlive = false;
    this.KeepingAlive();
  }

  private GetActions() {
    const errorMsg = 'Error al cargar la información de acciones para menú. Comuníquese con el administrador del sistema';

    this.userService.GetActions().subscribe(
      ok => { this.dataActions = ok; },
      error => { notify(errorMsg, 'error', 10000); }
    ).add(() => { this.dataActions.push({ id: 999, icon: 'close', items: null, name: 'exit', text: 'Salir', url: 'GetOut'}); });
  }

  private KeepingAlive(): void {
    setTimeout(() => {
      this.sessionAlive = setInterval(() => {
        this.showKeepAlive = true;
      }, environment.appSettings.session.keepAlive);
    });
  }
}
