import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserInfoService } from '../services/userInfo.service';
import { AuthService } from '../auth/auth.service';
import { RoutelocalizationService } from '../services/routelocalization.service';

@Injectable({
  providedIn: 'root',
 
})
export class AuthorizationCheck implements CanActivate {

  constructor(private router: Router,
    private userInfoService: UserInfoService,
     private localize:RoutelocalizationService,
     
     ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, user may login to application
    if (this.userInfoService.gettokenInfo().value && !this.userInfoService.isTokenExpired())
    {

      return true;
    }

    // otherwise redirect to login page with the return url
    this.router.navigate([''], { queryParams: { returnUrl: this.localize.getlocalizeUrl(state.url) } });
    return false;
  }

  // private loadMenus()
  // {
  //   this.menus = this.menuService.getCacheMenus();
    
  //   if (this.menus == null) {
  //        this.menuService.getAllMenu().subscribe(result => {
  //          if (result) {
  //            this.menus = result;
  //       }
  //   });
  //   } 
  // }
}
