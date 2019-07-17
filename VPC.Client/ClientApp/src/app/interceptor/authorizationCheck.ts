import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserInfoService } from '../services/userInfo.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationCheck implements CanActivate {

  constructor(private router: Router, private userInfoService: UserInfoService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, user may login to application
    if (this.userInfoService.gettokenInfo().value) {
      return true;
    }

    // otherwise redirect to login page with the return url
    this.router.navigate([''], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
