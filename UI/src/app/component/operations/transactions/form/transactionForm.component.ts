import {AfterViewInit, Component, EventEmitter, Input, Output} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';
import {VehicleModel} from '../../../../model/api/catalog/vehicle/vehicleModel';
import {TransactionForm} from '../../../../model/forms/transactionForm';
import {GeneralService} from '../../../../service/api/general.service';
import notify from 'devextreme/ui/notify';
import DataSource from 'devextreme/data/data_source';
import {ServiceModel} from '../../../../model/api/catalog/service/serviceModel';
import {ExpenseModel} from '../../../../model/api/catalog/expense/expenseModel';
import {ServiceService} from '../../../../service/api/catalog/service/service.service';
import {ExpenseService} from '../../../../service/api/catalog/expense/expense.service';
import {Transform} from '../../../../general/transform';
import {TransactionModel} from '../../../../model/api/operation/transactionModel';
import {TransactionService} from '../../../../service/api/operation/transaction.service';
import {TransactionTypeModel} from '../../../../model/api/operation/transactionTypeModel';

@Component({
  selector: 'app-component-transaction-form',
  templateUrl: './transactionForm.component.html'
})
export class TransactionFormComponent implements AfterViewInit{
  dataTransactionType: TransactionTypeModel[];
  dataServices: DataSource;
  private services: ServiceModel[];
  dataExpenses: DataSource;
  private  expenses: ExpenseModel[];
  minDate: Date;
  maxDate: Date;

  @Input() Blocker: BlockModel = { block: null, message: null };
  @Input() Visible: boolean;
  @Input() Title: string;
  @Input() Vehicle: VehicleModel;
  @Input() Form: TransactionForm;
  @Input() Month: number;
  @Input() Year: number;

  @Output() Closing = new EventEmitter();
  @Output() Saved = new EventEmitter();

  constructor(private generalService: GeneralService,
              private serviceService: ServiceService,
              private expenseService: ExpenseService,
              private transactionService: TransactionService) {
    this.Form = { id: null, vehicle: null, type: 1, service: null, expense: null, transactionDate: new Date() };
    this.dataTransactionType = [];
    this.dataServices = new DataSource({});
    this.services = [];
    this.dataExpenses = new DataSource({});
    this.expenses = [];
    this.DateLimits();
  }

  ngAfterViewInit(): void {
    this.GetTranType();
    this.GetServices();
    this.GetExpenses();
  }

  Submit(): void {
    setTimeout(() => {
      this.Form.vehicle = this.Vehicle.id;
      const item: TransactionModel = {
        id: this.Form.id,
        vehicle: this.Vehicle,
        service: this.Form.service === null ? null : this.services.filter(x => x.id === this.Form.service)[0],
        expense: this.Form.expense === null ? null : this.expenses.filter(x => x.id === this.Form.expense)[0],
        transactionDate: this.Form.transactionDate
      };

      this.Blocker.block = true;
      if (item.id === null) {
        this.Blocker.message = 'Guardando';
        this.transactionService.Add(item).subscribe(
          () => { this.Saved.emit(); },
          error => {
            notify(error.error, 'error', 10000);
            this.Blocker.block = false;
          }
        );
      } else {
        this.Blocker.message = 'Actualizando';
        this.transactionService.Update(item).subscribe(
          () => { this.Saved.emit(); },
          error => {
            notify(error.error, 'error', 10000);
            this.Blocker.block = false;
          }
        );
      }
    });
  }

  Close(): void {
    this.Form = { id: null, vehicle: null, type: null, service: null, expense: null, transactionDate: null };
    this.Closing.emit();
  }

  FormFieldChange(e): void {
    this.DateLimits();
    if (e.dataField === 'type' && this.Form.id === null) {
      this.Form.service = null;
      this.Form.expense = null;
    }
  }

  DisableTransaction(): boolean {
    if (this.Vehicle.vehicleType === undefined) {
      return false;
    } else {
      return !(this.Vehicle.vehicleType.canExpense && this.Vehicle.vehicleType.canService);
    }
  }

  private DateLimits(): void {
    if (this.Month !== undefined && this.Year !== undefined) {
      this.minDate = new Date(this.Year, this.Month - 1, 1);
      this.maxDate = new Date(this.Year, this.Month, 0);
    }
  }

  private GetTranType() {
    this.generalService.GetTransactionType().subscribe(
      ok => { this.dataTransactionType = ok; },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetServices() {
    this.serviceService.GetGrouped() .subscribe(
      ok => {
        this.dataServices = Transform.ServiceGroupedToCategoryModel(ok);
        this.services = Transform.ServiceGroupedToServiceModel(ok);
      },
      error => { notify(error.error, 'error', 10000); }
    );
  }

  private GetExpenses() {
    this.expenseService.GetGrouped().subscribe(
      ok => {
        this.dataExpenses = Transform.ExpenseGroupedToCategoryModel(ok);
        this.expenses = Transform.ExpenseGroupedToExpenseModel(ok);
      },
      error => { notify(error.error, 'error', 10000); }
    );
  }
}
