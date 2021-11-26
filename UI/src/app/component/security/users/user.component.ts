import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';
import {DxDataGridComponent} from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import {ConfirmModel} from '../../../model/template/confirmModel';
import {PopUpModel} from '../../../model/template/popUpModel';
import {UserModel} from '../../../model/api/security/userModel';
import {RoleModel} from '../../../model/api/security/roleModel';
import {UserService} from '../../../service/api/security/user.service';
import {RoleService} from '../../../service/api/security/role.service';

@Component({
  selector: 'app-component-user',
  templateUrl: './user.component.html'
})
export class UserComponent implements AfterViewInit{
  data: UserModel[];
  dataRole: RoleModel[];
  confirm: ConfirmModel;
  formPop: PopUpModel;
  selectedItem: UserModel;

  @Input() Blocker: BlockModel = { block: null, message: null };

  @ViewChild('DxgData', { static: false}) DxgData: DxDataGridComponent;

  constructor(private service: UserService,
              private roleService: RoleService) {
    this.data = [];
    this.dataRole = [];
    this.confirm = {
      message: '¿Se encuentra realmente seguro de borrar el registro?',
      buttonText: 'Confirmar',
      buttonIcon: 'trash',
      show: false
    };
    this.formPop = { visible: false, title: null };
    this.selectedItem = null;
  }

  ngAfterViewInit(): void {
    this.GetData();
    this.GetDataRole();
  }

  Refresh(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Cargando';

      this.GetData();
    });
  }

  New(): void {
    this.selectedItem = { id: null, email: null, name: null, password: null, role: null, active: true, reset: true };
    this.formPop = { visible: true, title: 'Nuevo registro'};
  }

  Edit(): void {
    const item: UserModel = this.GetSelectedItem();

    if (item === null) {
      notify('No ha seleccionado ningún registro', 'error', 3000);
      return;
    }

    this.selectedItem = item;
    this.formPop = { visible: true, title: 'Actualizar registro'};
  }

  Delete(): void {
    if (this.GetSelectedItem() === null) {
      notify('No ha seleccionado ningún registro', 'error', 3000);
      return;
    }

    this.confirm.show = true;
  }

  Confirm(): void {
    this.Blocker.block = true;
    this.Blocker.message = 'Eliminando';

    this.DeleteData(this.GetSelectedItem());
    this.confirm.show = false;
  }

  CloseConfirm(): void { this.confirm.show = false; }

  CloseForm(): void {
    this.formPop.visible = false;
    this.Refresh();
  }

  Save(model: UserModel): void {
    this.Blocker.block = true;

    if (model.id === null) {
      this.Blocker.message = 'Guardando';
      this.SaveData(model);
    } else {
      this.Blocker.message = 'Actualizando';
      this.UpdateData(model);
    }
  }

  private GetData(): void {
    this.service.Get().subscribe(
      ok => { this.data = ok; },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 500); });
  }

  private GetDataRole(): void {
    this.roleService.Get().subscribe(
      ok => { this.dataRole = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private SaveData(item: UserModel): void {
    this.service.Add(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private UpdateData(item: UserModel): void {
    this.service.Update(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private DeleteData(item: UserModel): void {
    this.service.Delete(item).subscribe(
      () => { this.GetData(); },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private GetSelectedItem(): UserModel {
    const selected = this.DxgData.instance.getSelectedRowsData();

    return selected.length === 0 ? null : selected[0];
  }
}
