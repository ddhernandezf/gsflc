import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TransactionComponent } from '../component/operations/transactions/transaction.component';
import { TransactionFormComponent } from '../component/operations/transactions/form/transactionForm.component';
import { DetailTransactionComponent } from '../component/operations/transactions/detail/detailTransaction.component';
import { DetailTransactionFormComponent } from '../component/operations/transactions/detail/form/detailTransactionForm.component';
import { RegistrationTypeComponent } from '../component/catalog/vehicle/registrationType/registrationType.component';
import { RegistrationTypeFormComponent } from '../component/catalog/vehicle/registrationType/form/registrationTypeForm.component';
import { ExpenseComponent } from '../component/catalog/expense/expense.component';
import { ExpenseFormComponent } from '../component/catalog/expense/form/expenseForm.component';
import { ExpenseTypeComponent } from '../component/catalog/expense/type/expenseType.component';
import { ExpenseTypeFormComponent } from '../component/catalog/expense/type/form/expenseTypeForm.component';
import { ServiceComponent } from '../component/catalog/service/service.component';
import { ServiceFormComponent } from '../component/catalog/service/form/serviceForm.component';
import { ServiceTypeComponent } from '../component/catalog/service/type/serviceType.component';
import { ServiceTypeFormComponent } from '../component/catalog/service/type/form/serviceTypeForm.component';
import { VehicleTypeFormComponent } from '../component/catalog/vehicle/type/form/vehicleTypeForm.component';
import { VehicleTypeComponent } from '../component/catalog/vehicle/type/vehicleType.component';
import { VehicleFormComponent } from '../component/catalog/vehicle/form/vehicleForm.component';
import { VehicleComponent } from '../component/catalog/vehicle/vehicle.component';
import { BrandComponent } from '../component/catalog/vehicle/brand/brand.component';
import { BrandFormComponent } from '../component/catalog/vehicle/brand/form/brandForm.component';
import { BrandModelFormComponent } from '../component/catalog/vehicle/brandModel/form/brandModelForm.component';
import { BrandModelComponent } from '../component/catalog/vehicle/brandModel/brandModel.component';
import { PilotFormComponent } from '../component/catalog/vehicle/pilot/form/pilotForm.component';
import { PilotComponent } from '../component/catalog/vehicle/pilot/pilot.component';
import { UserFormComponent } from '../component/security/users/form/userForm.component';
import { UserComponent } from '../component/security/users/user.component';

import { BalanceReportComponent } from '../component/report/balance/balanceReport.component';

import { LayoutComponent } from '../template/layout/layout.component';
import { KeepAliveComponent } from '../template/keepAlive/keepAlive.component';
import { ConfirmComponent } from '../template/confirm/confirm.component';

import { LoginComponent } from '../view/login/login.component';
import { ChangePasswordComponent } from '../view/login/password/changePassword.component';
import { IndexViewComponent } from '../view/index/indexView.component';
import { TransactionsViewComponent } from '../view/operations/transactions/transactionsView.component';
import { UsersViewComponent } from '../view/security/users/usersView.component';
import { RegistrationTypeViewComponent } from '../view/catalog/vehicle/registrationType/registrationTypeView.component';
import { PilotViewComponent } from '../view/catalog/vehicle/pilot/pilotView.component';
import { UnitComponent } from '../view/catalog/vehicle/units/unit.component';
import { ExpenseTypeViewComponent } from '../view/catalog/expense/type/expenseTypeView.component';
import { ExpenseViewComponent } from '../view/catalog/expense/expenseView.component';
import { ServiceTypeViewComponent } from '../view/catalog/service/type/serviceTypeView.component';
import { ServiceViewComponent } from '../view/catalog/service/serviceView.component';
import { VehicleTypeViewComponent } from '../view/catalog/vehicle/type/vehicleTypeView.component';
import { VehicleViewComponent } from '../view/catalog/vehicle/vehicleView.component';
import { BrandViewComponent } from '../view/catalog/vehicle/brand/brandView.component';
import { BrandModelViewComponent } from '../view/catalog/vehicle/brandModel/brandModelView.component';
import { ReportBalanceViewComponent } from '../view/report/balance/reportBalanceView.component';
import { ReportServiceViewComponent } from '../view/report/service/reportServiceView.component';
import { ReportExpenseViewComponent } from '../view/report/expense/reportExpenseView.component';

import {SecurityService} from '../service/security.service';

import {
  DxResponsiveBoxModule, DxLoadPanelModule, DxScrollViewModule, DxTemplateModule, DxBoxModule, DxPopupModule,
  DxButtonModule, DxFormModule, DxTextBoxModule, DxNumberBoxModule, DxListModule, DxTextAreaModule,
  DxSlideOutModule, DxToolbarModule, DxMenuModule, DxSelectBoxModule, DxDropDownButtonModule, DxSwitchModule,
  DxDateBoxModule, DxDataGridModule, DxDropDownBoxModule
} from 'devextreme-angular';
import {DxoToolbarModule} from 'devextreme-angular/ui/nested';

const routes: Routes = [
  { path: '', component: IndexViewComponent, canActivate: [SecurityService] },
  { path: 'Login', component: LoginComponent},
  { path: 'Operaciones/Transacciones', component: TransactionsViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Seguridad/Usuarios', component: UsersViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/TipoMatricula', component: RegistrationTypeViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/Pilotos', component: PilotViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/Unidades', component: UnitComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/Vehiculo', component: VehicleViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/TipoVehiculo', component: VehicleTypeViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/Marca', component: BrandViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Vehiculos/Modelo', component: BrandModelViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Gastos/TipoGasto', component: ExpenseTypeViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Gastos/Gasto', component: ExpenseViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Servicios/TipoServicio', component: ServiceTypeViewComponent, canActivate: [SecurityService] },
  { path: 'Catalogos/Servicios/Servicio', component: ServiceViewComponent, canActivate: [SecurityService] },
  { path: 'Reportes/Balance', component: ReportBalanceViewComponent, canActivate: [SecurityService] },
  { path: 'Reportes/Servicios', component: ReportServiceViewComponent, canActivate: [SecurityService] },
  { path: 'Reportes/Gastos', component: ReportExpenseViewComponent, canActivate: [SecurityService] }
];

@NgModule({
  declarations: [
    TransactionComponent, TransactionFormComponent, DetailTransactionComponent, DetailTransactionFormComponent,
    RegistrationTypeComponent, RegistrationTypeFormComponent, ExpenseComponent, ExpenseFormComponent, ExpenseTypeComponent,
    ExpenseTypeFormComponent, ServiceComponent, ServiceFormComponent, ServiceTypeComponent, ServiceTypeFormComponent,
    VehicleTypeFormComponent, VehicleTypeComponent, VehicleFormComponent, VehicleComponent, BrandComponent, BrandFormComponent,
    BrandModelFormComponent, BrandModelComponent, PilotFormComponent, PilotComponent,
    UserFormComponent, UserComponent,
    LayoutComponent, ConfirmComponent, KeepAliveComponent,
    IndexViewComponent, TransactionsViewComponent, UsersViewComponent, RegistrationTypeViewComponent, PilotViewComponent,
    UnitComponent, ExpenseTypeViewComponent, ExpenseViewComponent, ServiceTypeViewComponent, ServiceViewComponent,
    VehicleTypeViewComponent, VehicleViewComponent, BrandViewComponent,
    BrandModelViewComponent, ReportBalanceViewComponent, ReportServiceViewComponent, ReportExpenseViewComponent,
    LoginComponent, ChangePasswordComponent,
    BalanceReportComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    DxResponsiveBoxModule, DxLoadPanelModule, DxScrollViewModule, DxTemplateModule, DxBoxModule, DxPopupModule, DxButtonModule,
    DxFormModule, DxTextBoxModule, DxNumberBoxModule, DxListModule, DxTextAreaModule, DxSlideOutModule, DxToolbarModule, DxMenuModule,
    DxSelectBoxModule, DxDropDownButtonModule, DxSwitchModule, DxDateBoxModule, DxDataGridModule, DxoToolbarModule, DxDropDownBoxModule
  ],
  exports: [RouterModule]
})
export class RouteModule { }
