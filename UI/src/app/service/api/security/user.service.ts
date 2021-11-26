import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ApiOptions } from '../../../general/apiOptions';
import {ActionModel} from '../../../model/api/security/actionModel';
import {UserInfoModel} from '../../../model/api/security/userInfoModel';
import {UserModel} from '../../../model/api/security/userModel';
import {ChangePasswordModel} from '../../../model/api/security/changePasswordModel';

@Injectable()

export class UserService extends ApiOptions {
  private URL: string = environment.api.transport.url.replace('{0}', this.ApiLocation());
  private TIMEOUT = environment.api.transport.timeout;

  constructor(private http: HttpClient) {
    super();
  }

  public GetActions(): Observable<ActionModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.action;
    return this.http.get<ActionModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Authenticate(): Observable<UserInfoModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.authenticate;
    return this.http.get<UserInfoModel>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Get(): Observable<UserModel[]> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.root;
    return this.http.get<UserModel[]>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Add(model: UserModel): Observable<UserModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.root;
    return this.http.post<UserModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Update(model: UserModel): Observable<UserModel> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.root;
    return this.http.put<UserModel>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public Delete(model: UserModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.delete
      .replace('{0}', model.id.toString());
    return this.http.delete<any>(endpoint, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }

  public ChangePassword(model: ChangePasswordModel): Observable<any> {
    const endpoint = this.URL + environment.api.transport.endpoint.security.user.changePassword;
    return this.http.post<any>(endpoint, model, { headers: this.Headers()}).pipe(timeout(this.TIMEOUT));
  }
}
