import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ApiOptions } from '../../../general/apiOptions';
import {RoleModel} from '../../../model/api/security/roleModel';

@Injectable()

export class RoleService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public Get(): Observable<RoleModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.role.root;
    return this.http.get<RoleModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
