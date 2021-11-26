import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { ApiOptions } from '../../../../general/apiOptions';
import {PilotModel} from '../../../../model/api/catalog/vehicle/pilotModel';

@Injectable()

export class PilotService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<PilotModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.pilot.root;
    return this.http.get<PilotModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: PilotModel): Observable<PilotModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.pilot.root;
    return this.http.post<PilotModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: PilotModel): Observable<PilotModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.pilot.root;
    return this.http.put<PilotModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: PilotModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.catalog.vehicle.pilot.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
