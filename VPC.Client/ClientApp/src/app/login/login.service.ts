import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { UserInfoService } from '../services/userInfo.service';
import { MenuService } from '../services/menu.service';
import { Resource } from '../model/resource';
import { GlobalResourceService } from '../global-resource/global-resource.service'
@Injectable({
  providedIn: 'root'
})



export class LoginService {


  private loginRoute: string = '/api/login';
  constructor(private http: HttpClient,
    private router: Router,
    private menuService: MenuService,
    private userinfoService: UserInfoService,
    private GlobalResourceService: GlobalResourceService,

  ) { }




  //private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  // get isLoggedIn() {
  //   return this.loggedIn.asObservable();
  // }

  //   login(tenantname :string,username: string, password: string): Observable<any> {
  //     var loginUrl = `${environment.apiUrl}` + this.loginRoute;
  //     return this.http.post(loginUrl, { tenantname,username, password });
  //     }

  login(tenantcode: string, username: string, password: string, isRemember: boolean) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute;
    return this.http.post<any>(loginUrl, { tenantcode, username, password })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes

          this.userinfoService.setTokenInfo(user, isRemember)
          this.GlobalResourceService.getResource();
          //this.loggedIn.next(true);
        }
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    this.userinfoService.deleteAllCookieAndSessionValues();
    localStorage.removeItem('langInfo');
    this.GlobalResourceService.setGlobalresources(new Resource());
    this.menuService.clearAllCacheItem();
    this.router.navigate(['']);
  }


  get isLoggedIn(): boolean {
    return this.userinfoService.gettokenInfo().value != null ? true : false

  }

  forgotpass(postData: any) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/forgotpass';
    return this.http.post(loginUrl, postData)
  }
  changepass(postData: any) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/changepass';
    return this.http.post(loginUrl, postData)
  }
  getPasswordPolicy(data): any {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/passwordpolicy/' + data;
    return this.http.get(loginUrl)
  }
  changepassafterlogin() {

    // var data=this.userinfoService.userInfo
    // var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/changepassafterlogin';
    // this.http.post(loginUrl, data).subscribe(x => {
    this.router.navigate(['changepassword']);
    // })
  }
  // checkAccess(data: any): any {
  //   var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/checkaccess';
  //   return this.http.post(loginUrl, data)
  // }
  getUserPasswordChangedOn(data): any {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/credential/passwordchangedon/' + data.tenantcode + '/' + data.username;
    return this.http.get(loginUrl)
  }

}




// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { map } from 'rxjs/operators';

// @Injectable()
// export class LoginService {
//     constructor(private http: HttpClient) { }

//     login(tenantname :string,username: string, password: string) {
//         return this.http.post<any>('/api/login', { username, password })
//             .pipe(map(user => {
//                 // login successful if there's a jwt token in the response
//                 if (user && user.token) {
//                     // store user details and jwt token in local storage to keep user logged in between page refreshes
//                     localStorage.setItem('TokenInfo', JSON.stringify(user));
//                 }

//                 return user;
//             }));
//     }

//     logout() {
//         // remove user from local storage to log user out
//         localStorage.removeItem('TokenInfo');
//     }
// }


