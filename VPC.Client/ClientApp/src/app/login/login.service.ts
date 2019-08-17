import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, first } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { UserInfoService } from '../services/userInfo.service';
import { MenuService } from '../services/menu.service';
import { Resource } from '../model/resource';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { MetadataService } from '../meta-data/metadata.service';
@Injectable({
  providedIn: 'root'
})



export class LoginService {


  private loginRoute: string = '/api/security';
  private defaultlangaugeRoute: string = '/api/resources/defaultlanguage';
  constructor(private http: HttpClient,
    private router: Router,
    private menuService: MenuService,
    private userinfoService: UserInfoService,
    private GlobalResourceService: GlobalResourceService,
    private metadataService: MetadataService

  ) { }




  //private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  // get isLoggedIn() {
  //   return this.loggedIn.asObservable();
  // }

  //   login(tenantname :string,username: string, password: string): Observable<any> {
  //     var loginUrl = `${environment.apiUrl}` + this.loginRoute;
  //     return this.http.post(loginUrl, { tenantname,username, password });
  //     }

  login(tenantcode: string, username: string, password: string, isModal, isRemember) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute+'/login';
    return this.http.post<any>(loginUrl, { tenantcode, username, password })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes

          if (isModal) {
            this.loginModal(user)
            this.getUserDefaultLanguage();
          } else {
            this.loginNoModal(user, isRemember)
             this.getUserDefaultLanguage();
          }

          //this.loggedIn.next(true);
        }
        return user;
      }));
  }
  loginModal(data) {

    
    // this.userinfoService.removeToken()
    this.userinfoService.setTokenInfo(data, this.userinfoService.gettokenInfo().isRemember)
    this.userinfoService.sessionExpiryPopupRendered = 'false'
  }
  loginNoModal(data, isRemember) {
    if (data && data.token) {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      this.userinfoService.setTokenInfo(data, isRemember)
      this.GlobalResourceService.getResource();
    }
  }
  logout() {

    // remove user from local storage to log user out
    this.userinfoService.deleteAllCookieAndSessionValues();
    localStorage.removeItem('langInfo');
    localStorage.removeItem('editableColumnname');
    this.GlobalResourceService.setGlobalresources(new Resource());
    this.menuService.clearAllCacheItem();
    this.metadataService.clearCacheMetadata();  
      this.router.navigate(['']);
  }


  get isLoggedIn(): boolean {
    return this.userinfoService.gettokenInfo().value != null ? true : false

  }

  forgotpass(postData: any) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/forgotpassword';
    return this.http.post(loginUrl, postData)
  }
  changepass(postData: any) {
    var loginUrl = `${environment.apiUrl}` + this.loginRoute + '/changepassword';
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

  getUserDefaultLanguage()
  {
    let defaultlanguageUrl = `${environment.apiUrl}` + this.defaultlangaugeRoute;
    this.http.get(defaultlanguageUrl).pipe(first())
      .subscribe(
        data => { 
          if (data)
          {
            localStorage.setItem('langInfo', JSON.stringify(data));;
          }
        }
      );
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


