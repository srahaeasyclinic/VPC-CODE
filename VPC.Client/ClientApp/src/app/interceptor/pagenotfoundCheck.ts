import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserInfoService } from '../services/userInfo.service';
import { AuthService } from '../auth/auth.service';
import { RoutelocalizationService } from '../services/routelocalization.service';

@Injectable({
  providedIn: 'root',
 
})
export class pagenotfoundCheck implements CanActivate {

  constructor(private router: Router,
    private userInfoService: UserInfoService,
  ) { 
      
     }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, redirect to pagenotfound
    if (this.userInfoService.gettokenInfo().value && !this.userInfoService.isTokenExpired())
    {
        this.router.navigate(['aboutpage']);
         return true;
    }

    // If token data not exist redirect to oopsNotfound 
     this.router.navigate(['pagenotfound']);
    return true;
  }

  
}
