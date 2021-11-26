import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PilotModel} from '../../../../../model/api/catalog/vehicle/pilotModel';

@Component({
  selector: 'app-component-pilot-form',
  templateUrl: './pilotForm.component.html'
})
export class PilotFormComponent {
  @Input() Visible: boolean;
  @Input() Form: PilotModel;
  @Input() Title: string;

  @Output() Closing = new EventEmitter();
  @Output() FormResult = new EventEmitter<PilotModel>();

  constructor() {}

  Submit(): void {
    this.FormResult.emit(this.Form);
  }

  Close(): void {
    this.Closing.emit();
  }
}
