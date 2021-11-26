import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';
import {GeneralService} from '../../../service/api/general.service';
import notify from 'devextreme/ui/notify';
import {MonthModel} from '../../../model/api/general/monthModel';
import {VehicleService} from '../../../service/api/catalog/vehicle/vehicle.service';
import {Transform} from '../../../general/transform';
import DataSource from 'devextreme/data/data_source';
import {PopUpModel} from '../../../model/template/popUpModel';
import {SearchTransactionForm} from '../../../model/forms/searchTransactionForm';
import {VehicleModel} from '../../../model/api/catalog/vehicle/vehicleModel';
import {BrandModel} from '../../../model/api/catalog/vehicle/brandModel';
import {BrandModelModel} from '../../../model/api/catalog/vehicle/brandModelModel';
import {TransactionService} from '../../../service/api/operation/transaction.service';
import {TransactionGridModel} from '../../../model/api/operation/transactionGridModel';
import {environment} from '../../../../environments/environment';
import {DxDataGridComponent} from 'devextreme-angular';
import {TransactionForm} from '../../../model/forms/transactionForm';
import {TransactionDetailService} from '../../../service/api/operation/transactionDetail.service';
import {TransactionDetailModel} from '../../../model/api/operation/transactionDetailModel';
import {PopUpSubModel} from '../../../model/template/popUpSubModel';
import {ConfirmModel} from '../../../model/template/confirmModel';

@Component({
  selector: 'app-component-transaction',
  templateUrl: './transaction.component.html'
})
export class TransactionComponent implements AfterViewInit{
  dataMonths: MonthModel[];
  dataVehicle: DataSource;
  private vehicles: VehicleModel[];
  tranForm: PopUpModel;
  detailForm: PopUpSubModel;
  searchForm: SearchTransactionForm;
  dataTransaction: TransactionGridModel[];
  dataTranDetail: TransactionDetailModel[];
  currencyFormat: string;
  transactionForm: TransactionForm;
  transactionId: number;
  headerConfirm: ConfirmModel;
  detailConfirm: ConfirmModel;
  detailItemToDelete: TransactionDetailModel;

  @Input() Blocker: BlockModel = { block: null, message: null };

  @ViewChild('DxgTran', { static: false}) DxgTran: DxDataGridComponent;

  constructor(private generalService: GeneralService,
              private vehicleService: VehicleService,
              private transactionService: TransactionService,
              private transactionDetailService: TransactionDetailService) {
    this.Blocker = { block: true, message: 'Cargando transacciones' };
    this.dataMonths = [];
    this.dataVehicle = new DataSource({});
    this.vehicles = [];
    this.tranForm = { visible: false, title: null};
    this.detailForm = { visible: false, title: null, subTitle: null};
    this.searchForm = { month: null, year: null, vehicleId: null, vehicle: new VehicleModel() };
    this.searchForm.vehicle.brand = new BrandModel();
    this.searchForm.vehicle.brandModel = new BrandModelModel();
    this.dataTransaction = [];
    this.dataTranDetail = [];
    this.currencyFormat = environment.stringFormats.currency;
    this.transactionForm = { id: null, vehicle: null, type: 1, service: null, expense: null, transactionDate: new Date() };
    this.transactionId = null;
    this.headerConfirm = {
      message: '¿Se encuentra realmente seguro de borrar el registro?',
      buttonText: 'Confirmar',
      buttonIcon: 'trash',
      show: false
    };
    this.detailConfirm = {
      message: '¿Se encuentra realmente seguro de borrar el registro?',
      buttonText: 'Confirmar',
      buttonIcon: 'trash',
      show: false
    };
    this.detailItemToDelete = null;
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.LoadFilter();
      const date: Date = new Date();
      this.searchForm.month = date.getMonth() + 1;
      this.searchForm.year = date.getFullYear();
    });
  }

  CallNewForm(): void {
    setTimeout(() => {
      if (this.searchForm.vehicle.id === undefined) {
        notify('Debe seleccionar un vehículo', 'error', 3000);
        return;
      }

      if (this.searchForm.year === null) {
        notify('Debe ingresar el año', 'error', 3000);
        return;
      }

      if (this.searchForm.month === null) {
        notify('Debe seleccionar un mes', 'error', 3000);
        return;
      }

      let tranType: number = null;
      if (!this.searchForm.vehicle.vehicleType.canService && !this.searchForm.vehicle.vehicleType.canExpense) {
        notify('No se configuró ejecución alguna de transacción para este tipo de vehículos. Revisar la configuración del tipo de vehículo', 'error', 3000);
        return;
      } else if (this.searchForm.vehicle.vehicleType.canService || (this.searchForm.vehicle.vehicleType.canService && this.searchForm.vehicle.vehicleType.canExpense)) {
        tranType = 1;
      } else if (this.searchForm.vehicle.vehicleType.canExpense) {
        tranType = 2;
      }

      this.transactionForm = { id: null, vehicle: null, type: tranType, service: null, expense: null, transactionDate: null };
      this.tranForm = { visible: true, title: 'Nueva transacción'};
    });
  }

  CloseForm(): void { this.tranForm.visible = false; }

  HeaderSaved(): void {
    this.CloseForm();
    this.GetTransactions();
  }

  DetailSaved(): void {
    // this.Detail();
    this.GetTransactions();
    this.detailForm.visible = false;
  }

  CloseDetailForm(): void { this.detailForm.visible = false; }

  VehicleChange(): void {
    this.dataTranDetail = [];

    if (this.searchForm.vehicleId !== null) {
      this.searchForm.vehicle = this.vehicles.filter(x => x.id === this.searchForm.vehicleId)[0];
      this.GetTransactions();
    } else {
      this.searchForm.vehicle = new VehicleModel();
      this.searchForm.vehicle.brand = new BrandModel();
      this.searchForm.vehicle.brandModel = new BrandModelModel();
      this.dataTransaction = [];
    }
  }

  DateChange(from: string): void {
    const value: number = from === 'month' ? this.searchForm.month : (from === 'year' ? this.searchForm.year : null);
    if (value === null) {
      this.searchForm.vehicleId = null;
    } else {
      this.VehicleChange();
    }
  }

  GetTransactions(): void {
    if (this.searchForm.month === null) {
      notify('Seleccione un mes', 'error', 3000);
      return;
    }

    if (this.searchForm.year === null) {
      notify('Ingrese el año', 'error', 3000);
      return;
    }

    if (this.searchForm.vehicleId === null) {
      notify('Selecciones un vehículo', 'error', 3000);
      return;
    }

    this.Blocker.block = true;
    this.Blocker.message = 'Cargando transacciones';

    this.transactionService.GetFiltered(this.searchForm.year, this.searchForm.month, this.searchForm.vehicleId).subscribe(
      ok => { this.dataTransaction = ok; },
      error => { notify(error.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, this.dataTransaction.length === 0 ? 500 : 5000); });
  }

  Detail(): void {
    this.Blocker.block = true;
    this.Blocker.message = 'Cargando detalle de transacción';

    setTimeout(() => {
      if (this.DxgTran.instance.getSelectedRowsData().length === 0) {
        notify('No ha seleccionado ningún registro', 'error', 3000);
        return;
      }

      const tranItem: TransactionGridModel = this.GetSelectedTran();
      this.transactionId = tranItem.id;

      this.transactionDetailService.GetFiltered(tranItem.id).subscribe(
        ok => {
          this.dataTranDetail = ok;
          this.detailForm = {
            visible: true,
            title: 'Transacción: ' + tranItem.movement.name + ' (' + tranItem.type.name + ')',
            subTitle: tranItem.transactionDate
          };
        },
        error => { notify(error.error, 'error', 10000); }
      ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1500); });
    });
  }

  Edit(): void {
    setTimeout(() => {
      if (this.DxgTran.instance.getSelectedRowsData().length === 0) {
        notify('No ha seleccionado ningún registro', 'error', 3000);
        return;
      }

      this.transactionForm = this.GetTranItem();
      this.tranForm = { visible: true, title: 'Editar transacción'};
    });
  }

  HeaderSelected(): void {
    if (this.DxgTran.instance.getSelectedRowsData().length === 0) {
      notify('No ha seleccionado ningún registro', 'error', 3000);
      return;
    }

    this.headerConfirm.show = true;
  }

  DeleteHeader(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Eliminando';

      this.transactionService.Delete(this.GetSelectedTran()).subscribe(
        () => {
          this.CloseHeaderConfirm();
          this.GetTransactions();
        },
        error => {
          notify(error.error, 'error', 10000);
          this.Blocker.block = false;
        }
      );
    });
  }

  CloseHeaderConfirm(): void { this.headerConfirm.show = false; }

  DetailSelected(item: TransactionDetailModel): void {
    this.detailItemToDelete = item;
    this.detailConfirm.show = true;
  }

  DeleteDetail(): void {
    setTimeout(() => {
      this.Blocker.block = true;
      this.Blocker.message = 'Eliminando';

      this.transactionDetailService.Delete(this.detailItemToDelete).subscribe(
        () => {
          this.CloseDetailConfirm();
          this.GetTransactions();
          this.detailForm.visible = false;
        },
        error => {
          notify(error.error, 'error', 10000);
          this.Blocker.block = false;
        }
      );
    });
  }

  CloseDetailConfirm(): void { this.detailConfirm.show = false; }

  private LoadFilter() {
    this.generalService.GetMonths().subscribe(
      months => {
        this.dataMonths = months;
        this.vehicleService.GetGrouped().subscribe(
          vehicles => {
            this.dataVehicle = Transform.VehicleGroupedToCategoryModel(vehicles);
            this.vehicles = Transform.VehicleGroupedToVehicleModel(vehicles);
          },
          errorVehicle => { notify(errorVehicle.error, 'error', 10000); }
        );
      },
      errorMonth => { notify(errorMonth.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1500); });
  }

  private GetTranItem(): TransactionForm {
    const item: TransactionGridModel = this.GetSelectedTran();
    return {
      id: item.id,
      vehicle: item.vehicle.id,
      type: item.type.id,
      service: item.type.id === 1 ? item.movement.id : null,
      expense: item.type.id === 2 ? item.movement.id : null,
      transactionDate: item.transactionDate
    };
  }

  private GetSelectedTran(): TransactionGridModel {
    return this.DxgTran.instance.getSelectedRowsData()[0];
  }
}
