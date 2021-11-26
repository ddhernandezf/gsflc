import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {BalanceFilter} from '../../../model/forms/report/balanceFilter';
import {MonthModel} from '../../../model/api/general/monthModel';
import {GeneralService} from '../../../service/api/general.service';
import {VehicleService} from '../../../service/api/catalog/vehicle/vehicle.service';
import {Transform} from '../../../general/transform';
import notify from 'devextreme/ui/notify';
import DataSource from 'devextreme/data/data_source';
import {BlockModel} from '../../../model/template/blockModel';
import {DxDataGridComponent, DxListComponent} from 'devextreme-angular';
import {ServiceService} from '../../../service/api/catalog/service/service.service';
import {ExpenseService} from '../../../service/api/catalog/expense/expense.service';
import {DocTypeModel} from '../../../model/api/general/docTypeModel';
import {HttpEventType} from '@angular/common/http';
import {BalanceService} from '../../../service/report/balance.service';
import {ReportService} from '../../../service/api/report/report.service';
import {BalanceModel} from '../../../model/api/report/balanceModel';
import {environment} from '../../../../environments/environment';

@Component({
  selector: 'app-component-report-balance',
  templateUrl: './balanceReport.component.html'
})
export class BalanceReportComponent implements AfterViewInit{
  form: BalanceFilter;
  dataMonths: MonthModel[];
  dataDocType: DocTypeModel[];
  dataVehicle: DataSource;
  dataServices: DataSource;
  dataExpenses: DataSource;
  startDateColspan: number;
  startDateLabel: string;
  minDate: Date;
  selectedVehicles: number[];
  selectedServices: number[];
  selectedExpenses: number[];
  vehicleValue: string[];
  serviceValue: string[];
  expenseValue: string[];
  dataReport: BalanceModel;
  currencyFormat: string;
  reportName: string;

  @Input() ReportOption: string;
  @Input() ReportTitle: string;
  @Input() Blocker: BlockModel = { block: null, message: null };

  @ViewChild('VehicleList', { static: false}) VehicleList: DxListComponent;
  @ViewChild('ServiceList', { static: false}) ServiceList: DxListComponent;
  @ViewChild('ExpenseList', { static: false}) ExpenseList: DxListComponent;
  @ViewChild('DxgData', { static: false}) DxgData: DxDataGridComponent;

  constructor(private generalService: GeneralService,
              private vehicleService: VehicleService,
              private serviceService: ServiceService,
              private expenseService: ExpenseService,
              private balanceService: BalanceService,
              private reportService: ReportService) { this.Init(); }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.GetMonths();
      this.GetDocTypes();
      this.GetVehicles();
      this.GetServices();
      this.GetExpenses();

      const date: Date = new Date();
      this.form.month = date.getMonth() + 1;
      this.form.year = date.getFullYear();
      this.form.startDate = null;
      this.minDate = null;

      switch (this.ReportOption) {
        case 'BALANCE': {
          this.reportName = 'Reporte balance';
          break;
        }
        case 'SERVICE': {
          this.reportName = 'Reporte de servicios';
          break;
        }
        case 'EXPENSE': {
          this.reportName = 'Reporte de gastos';
          break;
        }
      }
    });
  }

  FormFieldChange(e): void {
    setTimeout(() => {
      if (e.dataField === 'dateRange') {
        this.Blocker.block = true;
        this.Blocker.message = 'Cargando';

        setTimeout(() => {
          this.startDateColspan = this.form.dateRange ? 3 : 6;
          this.startDateLabel = this.form.dateRange ? 'Fecha' : 'Fecha inicial';
          this.Blocker.block = false;
        }, 1000);
      }

      if (e.dataField === 'startDate') {
        this.minDate = this.form.startDate;
      }
    });
  }

  Submit(): void {
    this.Blocker.block = true;
    this.Blocker.message = 'Generando archivo';

    setTimeout(() => {
      if (this.form.docType === 'XLS') {
        this.GetReportData();
      } else {
        this.balanceService.Report(this.form.docType, this.ReportOption, this.form).subscribe(
          data => {
            if (data.type === HttpEventType.Response) {
              const downloadedFile = new Blob([data.body], { type: data.body.type });
              const a = document.createElement('a');
              a.setAttribute('style', 'display:none;');
              document.body.appendChild(a);

              let fileExt = '';
              switch (this.form.docType) {
                case 'PDF': {
                  fileExt = '.pdf';
                  break;
                }
                case 'XLS': {
                  fileExt = '.xlsx';
                  break;
                }
              }

              a.download = this.reportName + fileExt;
              a.href = URL.createObjectURL(downloadedFile);
              a.target = '_blank';
              a.click();
              document.body.removeChild(a);
              notify('Reporte generado exitosamente', 'success', 30000);
            }
          },
          (error) => { notify(error.message, 'error', 30000); }
        ).add(() => { this.Blocker.block = false; });
      }
    });
  }

  VehicleListChanged(): void {
    if (this.VehicleList.selectedItems.length === 0) {
      this.vehicleValue = [];
      this.form.vehicles = [];
    } else {
      this.vehicleValue = [];
      this.form.vehicles = [];

      this.VehicleList.selectedItems.forEach(x => {
        x.items.forEach(y => {
          this.vehicleValue.push(y.name);
          this.form.vehicles.push(y.id.toString());
        });
      });
    }
  }

  ServiceListChanged(): void {
    if (this.ServiceList.selectedItems.length === 0) {
      this.serviceValue = [];
      this.form.services = [];
    } else {
      this.serviceValue = [];
      this.form.services = [];

      this.ServiceList.selectedItems.forEach(x => {
        x.items.forEach(y => {
          this.serviceValue.push(y.name);
          this.form.services.push(y.id.toString());
        });
      });
    }
  }

  ExpenseListChanged(): void {
    if (this.ExpenseList.selectedItems.length === 0) {
      this.expenseValue = [];
      this.form.expenses = [];
    } else {
      this.expenseValue = [];
      this.form.expenses = [];

      this.ExpenseList.selectedItems.forEach(x => {
        x.items.forEach(y => {
          this.expenseValue.push(y.name);
          this.form.expenses.push(y.id.toString());
        });
      });
    }
  }

  Refresh(): void {
    this.Blocker.block = true;
    this.Blocker.message = 'Refrescando';

    setTimeout(() => {
      this.Init();
      this.ngAfterViewInit();
    });
  }

  XlsDownload(): void {
    if (this.dataReport.data.length > 0) {
      this.DxgData.instance.exportToExcel(false);
      this.Blocker.block = false;
    }
  }

  private Init(): void {
    this.form = {
      vehicles: [],
      services: [],
      expenses: [],
      dateRange: false,
      startDate: null,
      endDate: null,
      monthAndYear: false,
      month: null,
      year: null,
      docType: 'PDF'
    };
    this.dataMonths = [];
    this.dataDocType = [];
    this.minDate = null;
    this.startDateColspan = 6;
    this.startDateLabel = 'Fecha';
    this.selectedVehicles = [];
    this.selectedServices = [];
    this.selectedExpenses = [];
    this.vehicleValue = [];
    this.serviceValue = [];
    this.expenseValue = [];
    this.dataReport =  { serviceTotal: 0, expenseTotal: 0, total: 0, resumeChart: [], data: [] };
    this.currencyFormat = environment.stringFormats.currency;
  }

  private GetMonths(): void {
    this.generalService.GetMonths().subscribe(
      months => { this.dataMonths = months; },
      errorMonth => { notify(errorMonth.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1500); });
  }

  private GetDocTypes(): void {
    this.generalService.GetDocTypes().subscribe(
      x => { this.dataDocType = x; console.log(x); },
      errorMonth => { notify(errorMonth.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1500); });
  }

  private GetVehicles(): void {
    this.vehicleService.GetGroupedReport(this.ReportOption) .subscribe(
      vehicles => {
        this.dataVehicle = Transform.VehicleGroupedToCategoryModel(vehicles);
        this.selectedVehicles = [];
        this.form.vehicles = [];
        this.vehicleValue = [];

        vehicles.forEach(x => {
          x.vehicles.forEach(y => {
            this.selectedVehicles.push(y.id);
            this.form.vehicles.push(y.id.toString());
            this.vehicleValue.push(y.text);
          });
        });
      },
      errorVehicle => { notify(errorVehicle.error, 'error', 10000); }
    ).add(() => { setTimeout(() => { this.Blocker.block = false; }, 1500); });
  }

  private GetServices(): void {
    this.serviceService.GetGrouped() .subscribe(
      services => {
        this.dataServices = Transform.ServiceGroupedToCategoryModel(services);
        this.selectedServices = [];
        this.form.services = [];
        this.serviceValue = [];

        services.forEach(x => {
          x.services.forEach(y => {
            this.selectedServices.push(y.id);
            this.form.services.push(y.id.toString());
            this.serviceValue.push(y.name);
          });
        });
      },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetExpenses(): void {
    this.expenseService.GetGrouped().subscribe(
      expenses => {
        this.dataExpenses = Transform.ExpenseGroupedToCategoryModel(expenses);
        this.selectedExpenses = [];
        this.form.expenses = [];
        this.expenseValue = [];

        expenses.forEach(x => {
          x.expenses.forEach(y => {
            this.selectedExpenses.push(y.id);
            this.form.expenses.push(y.id.toString());
            this.expenseValue.push(y.name);
          });
        });
      },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetReportData(): void {
    this.reportService.GetBalanceData(this.ReportOption, this.form) .subscribe(
      ok => {
        this.dataReport = ok;

        if (ok.data.length === 0) {
          notify('No hay datos para exportar', 'warning', 10000);
          this.Blocker.block = false;
        }
      },
      error => {
        notify(error.error, 'error', 10000);
        this.Blocker.block = false;
      }
    );
  }
}
