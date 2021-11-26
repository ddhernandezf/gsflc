import {Component, EventEmitter, Input, Output} from '@angular/core';
import {environment} from '../../../environments/environment';
import {BlockModel} from '../../model/template/blockModel';

@Component({
  selector: 'app-layout-keep-alive',
  templateUrl: './keepAlive.component.html'
})
export class KeepAliveComponent {
  countDown: any;
  counter: number;
  visible: boolean;

  @Input()
  set Visible(value: boolean){
    this.visible = value;
    if (value) {
      this.CountingDown();
      clearInterval(this.SessionAlive);
    }
  }
  get Visible(): boolean { return this.visible; }
  @Input() SessionAlive: any;
  @Input() Blocker: BlockModel = { block: null, message: null };

  @Output() Stopping = new EventEmitter();

  CountingDown(): void {
    this.counter = environment.appSettings.session.countDown;
    this.countDown = setInterval(() => {
      this.counter = this.counter - 1;

      if (this.counter === 0) {
        clearInterval(this.countDown);
        clearInterval(this.SessionAlive);

        this.Blocker.block = true;
        this.Blocker.message = 'Cerrando sesión';
        setTimeout(() => {
          localStorage.clear();
          window.location.reload();
        }, 3000);
      }
    }, 1000);
  }

  KeepItAlive() {
    clearInterval(this.countDown);
    this.counter = environment.appSettings.session.countDown;
    this.Stopping.emit();
  }
}
