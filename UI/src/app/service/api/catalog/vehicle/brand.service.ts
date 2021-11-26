import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {BrandGroupedModel} from '../../../../model/api/catalog/vehicle/brandGroupedModel';
import {BrandModel} from '../../../../model/api/catalog/vehicle/brandModel';

@Injectable()

export class BrandService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<BrandModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brand.root;
    return this.http.get<BrandModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public GetGrouped(): Observable<BrandGroupedModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brand.grouped;
    return this.http.get<BrandGroupedModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: BrandModel): Observable<BrandModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brand.root;
    return this.http.post<BrandModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: BrandModel): Observable<BrandModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brand.root;
    return this.http.put<BrandModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: BrandModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.brand.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
