import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {ServiceGroupedModel} from '../../../../model/api/catalog/service/serviceGroupedModel';
import {ServiceModel} from '../../../../model/api/catalog/service/serviceModel';

@Injectable()

export class ServiceService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetGrouped(): Observable<ServiceGroupedModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.getGrouped;
    return this.http.get<ServiceGroupedModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Get(): Observable<ServiceModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.root;
    return this.http.get<ServiceModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: ServiceModel): Observable<ServiceModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.root;
    return this.http.post<ServiceModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: ServiceModel): Observable<ServiceModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.root;
    return this.http.put<ServiceModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: ServiceModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.service.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
