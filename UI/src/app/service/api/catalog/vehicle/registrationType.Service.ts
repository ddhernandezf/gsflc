import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {RegistrationTypeModel} from '../../../../model/api/catalog/vehicle/registrationTypeModel';

@Injectable()

export class RegistrationTypeService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<RegistrationTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.registrationType.root;
    return this.http.get<RegistrationTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: RegistrationTypeModel): Observable<RegistrationTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.registrationType.root;
    return this.http.post<RegistrationTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: RegistrationTypeModel): Observable<RegistrationTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.registrationType.root;
    return this.http.put<RegistrationTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: RegistrationTypeModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.registrationType.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
