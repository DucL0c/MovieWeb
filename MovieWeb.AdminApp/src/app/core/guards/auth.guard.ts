import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { SystemConstants } from '../../core/common/system.constants';
import { UrlConstants } from '../../core/common/url.constants';

const user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER)!);
const menuUsers = JSON.parse(localStorage.getItem(SystemConstants.USER_MENUS)!);

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private httpClient: HttpClient) {}
  canActivate(
    activateRoute: ActivatedRouteSnapshot,
    routerState: RouterStateSnapshot
  ) {
    if (localStorage.getItem('user')) {
      return true;
    } else {
      this.router.navigate([UrlConstants.LOGIN], {
        queryParams: {
          returnUrl: routerState.url,
        },
      });
      return false;
    }
  }
}
