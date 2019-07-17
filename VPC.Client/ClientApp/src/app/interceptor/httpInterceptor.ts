import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserInfoService } from '../services/userInfo.service';

@Injectable({
  providedIn: 'root'
})
export class httpInterceptor implements HttpInterceptor {
  constructor(private userInfoService: UserInfoService) { }
  intercept(request: HttpRequest<any>, newRequest: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header to request

    //Get Token data from local storage
    let tokenInfo: any
    

    if (this.userInfoService.gettokenInfo().value) {
      tokenInfo =JSON.parse(this.userInfoService.gettokenInfo().value)
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
