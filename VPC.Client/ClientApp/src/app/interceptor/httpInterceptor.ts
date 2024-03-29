import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserInfoService } from '../services/userInfo.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class httpInterceptor implements HttpInterceptor {
  constructor(private userInfoService: UserInfoService, private route: Router) { }
  intercept(request: HttpRequest<any>, newRequest: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header to request

    //Get Token data from local storage
    let tokenInfo: any
    if (this.route.url != "/" && !request.url.includes("api/login")) {      
      if (this.userInfoService.gettokenInfo().value && this.userInfoService.isTokenExpired()) {
        this.route.navigate(['']);
      }
    }

    if (this.userInfoService.gettokenInfo().value) {
      tokenInfo = JSON.parse(this.userInfoService.gettokenInfo().value)
    }

    if (tokenInfo && tokenInfo.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${tokenInfo.token}`,
          'Content-Type': 'application/json'
        }
      });
    }
    return newRequest.handle(request);

  }
}
