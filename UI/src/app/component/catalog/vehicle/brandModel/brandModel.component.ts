import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';
import {DxDataGridComponent} from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import {ConfirmModel} from '../../../../model/template/confirmModel';
import {PopUpModel} from '../../../../model/template/popUpModel';
import {BrandModelModel} from '../../../../model/api/catalog/vehicle/brandModelModel';
import {BrandModelService} from '../../../../service/api/catalog/vehicle/brandModel.service';
import {BrandService} from '../../../../service/api/catalog/vehicle/brand.service';
import {BrandModel} from '../../../../model/api/catalog/vehicle/brandModel';
import {Transform} from '../../../../general/transform';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'app-component-brand-model',
  templateUrl: './brandModel.component.html'
})
export class BrandModelComponent implements AfterViewInit{
  data: BrandModelModel[];
  dataType: DataSource;
  type: BrandModel[];
  confirm: ConfirmModel;
  formPop: PopUpModel;
  selectedItem: BrandModelModel;

  @Input() Blocker: BlockModel = { block: null, message: null };

  @ViewChild('DxgData', { static: false}) DxgData: DxDataGridComponent;

  constructor(private service: BrandModelService,
              private brandService: BrandService) {
    this.data = [];
    this.dataType = new DataSource({});
    this.type = [];
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
    this.GetDataType();
  }

  Refresh(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Cargando';

      this.GetData();
    });
  }

  New(): void {
    this.selectedItem = { id: null, name: null, brand: null };
    this.formPop = { visible: true, title: 'Nuevo registro'};
  }

  Edit(): void {
    const item: BrandModelModel = this.GetSelectedItem();

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

  Save(model: BrandModelModel): void {
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

  private GetDataType(): void {
    this.brandService.GetGrouped().subscribe(
      ok => {
        this.dataType = Transform.BrandGroupedToCategoryModel(ok);
        this.type = Transform.BrandGroupedToBrandModel(ok);
      },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private SaveData(item: BrandModelModel): void {
    this.service.Add(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private UpdateData(item: BrandModelModel): void {
    this.service.Update(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private DeleteData(item: BrandModelModel): void {
    this.service.Delete(item).subscribe(
      () => { this.GetData(); },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private GetSelectedItem(): BrandModelModel {
    const selected = this.DxgData.instance.getSelectedRowsData();

    return selected.length === 0 ? null : selected[0];
  }
}
