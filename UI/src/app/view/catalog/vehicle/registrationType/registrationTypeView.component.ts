import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../../model/template/blockModel';

@Component({
  selector: 'app-view-catalog-vehicle-registration-type',
  templateUrl: './registrationTypeView.component.html'
})
export class RegistrationTypeViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
