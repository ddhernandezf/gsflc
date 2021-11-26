import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';
import {DxDataGridComponent} from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import {ConfirmModel} from '../../../model/template/confirmModel';
import {PopUpModel} from '../../../model/template/popUpModel';
import {VehicleModel} from '../../../model/api/catalog/vehicle/vehicleModel';
import {VehicleTypeModel} from '../../../model/api/catalog/vehicle/vehicleTypeModel';
import {VehicleService} from '../../../service/api/catalog/vehicle/vehicle.service';
import {VehicleTypeService} from '../../../service/api/catalog/vehicle/vehicleType.service';
import {RegistrationTypeService} from '../../../service/api/catalog/vehicle/registrationType.Service';
import {RegistrationTypeModel} from '../../../model/api/catalog/vehicle/registrationTypeModel';
import {BrandService} from '../../../service/api/catalog/vehicle/brand.service';
import {BrandModelService} from '../../../service/api/catalog/vehicle/brandModel.service';
import {PilotService} from '../../../service/api/catalog/vehicle/pilot.service';
import {BrandModel} from '../../../model/api/catalog/vehicle/brandModel';
import {BrandModelModel} from '../../../model/api/catalog/vehicle/brandModelModel';
import {PilotModel} from '../../../model/api/catalog/vehicle/pilotModel';

@Component({
  selector: 'app-component-vehicle',
  templateUrl: './vehicle.component.html'
})
export class VehicleComponent implements AfterViewInit{
  data: VehicleModel[];
  dataType: VehicleTypeModel[];
  dataRegType: RegistrationTypeModel[];
  dataBrand: BrandModel[];
  dataModel: BrandModelModel[];
  dataPilot: PilotModel[];
  confirm: ConfirmModel;
  formPop: PopUpModel;
  selectedItem: VehicleModel;

  @Input() Blocker: BlockModel = { block: null, message: null };

  @ViewChild('DxgData', { static: false}) DxgData: DxDataGridComponent;

  constructor(private service: VehicleService,
              private typeService: VehicleTypeService,
              private regTypeService: RegistrationTypeService,
              private brandService: BrandService,
              private modelService: BrandModelService,
              private pilotService: PilotService) {
    this.data = [];
    this.dataType = [];
    this.dataRegType = [];
    this.dataBrand = [];
    this.dataModel = [];
    this.dataPilot = [];
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
    this.GetDataRegType();
    this.GetDataBrand();
    this.GetDataModel();
    this.GetDataPilot();
  }

  Refresh(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Cargando';

      this.GetData();
    });
  }

  New(): void {
    this.selectedItem = {
      id: null,
      brandModel: null,
      brand: null,
      registration: null,
      registrationType: null,
      vehicleType: null,
      active: true,
      name: null,
      year: null,
      text: null
    };
    this.formPop = { visible: true, title: 'Nuevo registro'};
  }

  Edit(): void {
    const item: VehicleModel = this.GetSelectedItem();

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

  Save(model: VehicleModel): void {
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
    this.typeService.Get().subscribe(
      ok => { this.dataType = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetDataRegType(): void {
    this.regTypeService.Get().subscribe(
      ok => { this.dataRegType = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetDataBrand(): void {
    this.brandService.Get().subscribe(
      ok => { this.dataBrand = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetDataModel(): void {
    this.modelService.Get().subscribe(
      ok => { this.dataModel = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetDataPilot(): void {
    this.pilotService.Get().subscribe(
      ok => { this.dataPilot = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private SaveData(item: VehicleModel): void {
    this.service.Add(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private UpdateData(item: VehicleModel): void {
    this.service.Update(item).subscribe(
      () => {
        this.GetData();
        this.formPop.visible = false;
      },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private DeleteData(item: VehicleModel): void {
    this.service.Delete(item).subscribe(
      () => { this.GetData(); },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { this.Blocker.block = false; });
  }

  private GetSelectedItem(): VehicleModel {
    const selected = this.DxgData.instance.getSelectedRowsData();
    return selected.length === 0 ? null : selected[0];
  }
}
