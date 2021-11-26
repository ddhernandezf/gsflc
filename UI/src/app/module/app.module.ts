import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from '../component/startup/app.component';
import {RouteModule} from './route.module';
import {HttpClientModule} from '@angular/common/http';
import {WindowProviders} from '../provider/appWindowProvider';

import { LocalStorageService } from '../service/storage/localStorage.service';
import { SecurityService } from '../service/security.service';

import { GeneralService } from '../service/api/general.service';
import { UserService } from '../service/api/security/user.service';
import { RoleService } from '../service/api/security/role.service';
import { VehicleService } from '../service/api/catalog/vehicle/vehicle.service';
import { ServiceService } from '../service/api/catalog/service/service.service';
import { ServiceTypeService } from '../service/api/catalog/service/serviceType.service';
import { ExpenseService } from '../service/api/catalog/expense/expense.service';
import { ExpenseTypeService } from '../service/api/catalog/expense/expenseType.service';
import { RegistrationTypeService } from '../service/api/catalog/vehicle/registrationType.Service';
import { VehicleTypeService } from '../service/api/catalog/vehicle/vehicleType.service';
import { BrandService } from '../service/api/catalog/vehicle/brand.service';
import { BrandModelService } from '../service/api/catalog/vehicle/brandModel.service';
import { TransactionService } from '../service/api/operation/transaction.service';
import { TransactionDetailService } from '../service/api/operation/transactionDetail.service';
import { PilotService } from '../service/api/catalog/vehicle/pilot.service';
import { ReportService } from '../service/api/report/report.service';

import { BalanceService } from '../service/report/balance.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouteModule,
    HttpClientModule
  ],
  providers: [
    WindowProviders,
    LocalStorageService,
    SecurityService,
    UserService, RoleService, GeneralService, VehicleService, ServiceService, ServiceTypeService, ExpenseService,
    ExpenseTypeService, TransactionService, TransactionDetailService, RegistrationTypeService, VehicleTypeService,
    BrandService, BrandModelService, PilotService, ReportService,
    BalanceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
