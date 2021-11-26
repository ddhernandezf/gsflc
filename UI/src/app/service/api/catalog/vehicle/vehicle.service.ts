import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {VehicleGroupedModel} from '../../../../model/api/catalog/vehicle/vehicleGroupedModel';
import {VehicleModel} from '../../../../model/api/catalog/vehicle/vehicleModel';

@Injectable()

export class VehicleService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<VehicleModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.root;
    return this.http.get<VehicleModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public GetGrouped(): Observable<VehicleGroupedModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.getGrouped;
    return this.http.get<VehicleGroupedModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public GetGroupedReport(reportOption: string): Observable<VehicleGroupedModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.getGroupedReport
      .replace('{0}', reportOption);
    return this.http.get<VehicleGroupedModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: VehicleModel): Observable<VehicleModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.root;
    return this.http.post<VehicleModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: VehicleModel): Observable<VehicleModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.root;
    return this.http.put<VehicleModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: VehicleModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
