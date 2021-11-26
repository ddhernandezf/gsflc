import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {BrandModelModel} from '../../../../model/api/catalog/vehicle/brandModelModel';

@Injectable()

export class BrandModelService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<BrandModelModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brandModel.root;
    return this.http.get<BrandModelModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: BrandModelModel): Observable<BrandModelModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brandModel.root;
    return this.http.post<BrandModelModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: BrandModelModel): Observable<BrandModelModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brandModel.root;
    return this.http.put<BrandModelModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: BrandModelModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brandModel.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
