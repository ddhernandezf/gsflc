import {AfterViewInit, Component, Input} from '@angular/core';
import {BlockModel} from '../../model/template/blockModel';

@Component({
  selector: 'app-view-index',
  templateUrl: './indexView.component.html'
})
export class IndexViewComponent implements AfterViewInit{
  @Input() Blocker: BlockModel = { block: true, message: 'Cargando transacciones'};

  ngAfterViewInit(): void { setTimeout(() => { this.Blocker.block = false; }); }
}
