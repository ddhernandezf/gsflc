import { Injectable } from '@angular/core';
import {LoginForm} from '../../model/forms/loginForm';
import {environment} from '../../../environments/environment';
import {UserInfoModel} from '../../model/api/security/userInfoModel';
import * as CryptoJS from 'crypto-js';

@Injectable()

export class LocalStorageService {
  public SetUser(user: LoginForm): void {
    let data: string = btoa(user.userName + ':' + user.password);
    data = CryptoJS.AES.encrypt(data, environment.appSettings.lockPassword).toString();
    localStorage.setItem(environment.appSettings.btoaKey, data);
  }

  public GetUser(): string {
    let data = localStorage.getItem(environment.appSettings.btoaKey);
    data = CryptoJS.AES.decrypt(data, environment.appSettings.lockPassword).toString(CryptoJS.enc.Utf8);
    return data;
  }

  public SetUserInfo(user: UserInfoModel): void {
    let data: string = JSON.stringify(user);
    data = CryptoJS.AES.encrypt(data, environment.appSettings.lockPassword).toString();
    localStorage.setItem(environment.appSettings.userInfo, data);
  }

  public GetUserInfo(): UserInfoModel {
    let data: string = localStorage.getItem(environment.appSettings.userInfo);

    if (data === null || data === '') {
      return null;
    } else {
      data = CryptoJS.AES.decrypt(data, environment.appSettings.lockPassword).toString(CryptoJS.enc.Utf8);
      return JSON.parse(data);
    }
  }
}
