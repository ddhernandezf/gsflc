import {CanActivate, Router} from '@angular/router';
import {Inject, Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import {appWindow} from '../provider/appWindowProvider';
import {LocalStorageService} from './storage/localStorage.service';
import {UserInfoModel} from '../model/api/security/userInfoModel';

@Injectable()
export class SecurityService implements CanActivate {

  constructor(@Inject(appWindow) private window: Window,
              private router: Router,
              private storageService: LocalStorageService) {}

  canActivate(): Observable<boolean>|boolean {
    const userInfo: UserInfoModel = this.storageService.GetUserInfo();

    if (userInfo !== null) {
      return new Observable<boolean>((obs) => {
        obs.next(true);
      });
    } else {
      this.router.navigateByUrl('Login');
    }
  }
}
