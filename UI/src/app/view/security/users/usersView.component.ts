import {Component, Input} from '@angular/core';
import {BlockModel} from '../../../model/template/blockModel';

@Component({
  selector: 'app-view-security-users',
  templateUrl: './usersView.component.html'
})
export class UsersViewComponent {
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando'};
}
