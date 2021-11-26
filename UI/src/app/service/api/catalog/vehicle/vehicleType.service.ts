import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {VehicleTypeModel} from '../../../../model/api/catalog/vehicle/vehicleTypeModel';

@Injectable()

export class VehicleTypeService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<VehicleTypeModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.type.root;
    return this.http.get<VehicleTypeModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: VehicleTypeModel): Observable<VehicleTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.type.root;
    return this.http.post<VehicleTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: VehicleTypeModel): Observable<VehicleTypeModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.type.root;
    return this.http.put<VehicleTypeModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: VehicleTypeModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.type.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
