import {HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {LocalStorageService} from '../service/storage/localStorage.service';

export abstract class ApiOptions {
  Headers(): HttpHeaders {
    return new HttpHeaders({
      Token: environment.appSettings.appKey,
      'Content-Type': 'application/json',
      Authorization: new LocalStorageService().GetUser()
    });
  }

  ApiLocation(): string {
    return window.location.hostname;
  }

  ReportLocation(): string {
    return window.location.hostname;
  }
}
