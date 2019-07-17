import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from "@angular/common/http";
import { Observable } from "rxjs/Rx";
import { catchError } from "rxjs/operators";
import {
  TosterService,
  globalErrorMessage
} from "src/app/services/toster.service";
import { LoginService } from "../login/login.service";
import { Router } from "@angular/router";
import { of } from "rxjs/internal/observable/of";

@Injectable({
  providedIn: "root"
})
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private loginService: LoginService,
    private router: Router,
    private toster: TosterService
  ) { }

  intercept(
    request: HttpRequest<any>,
    newRequest: HttpHandler
  ): Observable<HttpEvent<any>> {
    return newRequest.handle(request).pipe(
      catchError(err => {
        //console.log('Error Code- ' + err.status);
        if (err.status === 403) {
          this.toster.showWarning("Unauthorized.");
          this.loginService.logout();
          return Observable.throw(err);
        } else if (err.status === 501) {
          return Observable.throw(err);
        } else if (err.status === 401) {
          //this.toster.showError(errorreponse); invalid
          err.message = "Invalid credential!";
          return Observable.throw(err);
        } else if (err.status === 208) {
          this.toster.showError(err.error.text);
          return Observable.throw(err);
        }
        else if (err.status === 404) {
          this.toster.showError(err.error);
          return Observable.throw(err);
        }
        else if (err.status === 520) {
          //console.log('520',JSON.stringify(err));
          this.toster.showError(err.error);
          return Observable.throw(err);
        }
        else {
          let errorreponse = err.error.message || err.statusText;

          //console.log(' errorreponse intercept' + errorreponse);
          //console.log(' err intercept' + JSON.stringify(err));

          if (
            errorreponse.includes("Unknown Error") ||
            errorreponse == undefined) {
            errorreponse = globalErrorMessage;
          }

          this.toster.showError(errorreponse);
          return Observable.throw(err);
        }

        // if (err.status === 403) {
        //   this.toster.showWarning('Unauthorized.');
        // }

        // if (err.status===500)
        // {
        //   this.toster.showError('Something went wrong, please try again later!');
        // }
        // if (err.status===0 || err.status===403)
        // {
        //   this.toster.showError('Something went wrong, please login and try again!');
        // }

        //console.log('Error Code- ' + err.status);
        //console.log(' err intercept'+JSON.stringify(err));
        // const error = err.error.message || err.statusText;
        //console.log('Error intercept'+JSON.stringify(error));
        // return Observable.throw(err);
      })
    );
  }
}
