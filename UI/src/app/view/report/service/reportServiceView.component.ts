import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-reports-service',
  templateUrl: './reportServiceView.component.html'
})
export class ReportServiceViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando reporte de servicios'};
}
