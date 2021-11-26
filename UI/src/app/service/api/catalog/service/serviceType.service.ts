import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {ServiceTypeModel} from '../../../../model/api/catalog/service/serviceTypeModel';

@Injectable()

export class ServiceTypeService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<ServiceTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.type.root;
    return this.http.get<ServiceTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: ServiceTypeModel): Observable<ServiceTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.type.root;
    return this.http.post<ServiceTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: ServiceTypeModel): Observable<ServiceTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.type.root;
    return this.http.put<ServiceTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: ServiceTypeModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.type.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
