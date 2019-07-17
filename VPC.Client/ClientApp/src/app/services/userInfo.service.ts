import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CommonService } from './common.service';
@Injectable({
  providedIn: 'root'
})

export class UserInfoService {
  _username: any;
  _checkPasswordChangeAccess: any
  constructor(private http: HttpClient, private commonService: CommonService) { }
  url = "api/access";
  test() {
    return "test"
  }
  get userInfo() {
    var data: any
   
    if ( this.commonService.getCookie('userInfo')) {
      data = this.commonService.getCookie('userInfo')
    }
    else if (sessionStorage.getItem('userInfo')) {
      data = sessionStorage.getItem('userInfo')
    }
    if (data) {
      return JSON.parse(data);
    }
  }
  setUserInfo(data, isRemember) {
    
    // this.removeUserInfo();
    if (isRemember) {
      this.commonService.setCookie('userInfo', JSON.stringify(data),1);      
    } else {
      sessionStorage.setItem('userInfo', JSON.stringify(data))
    }

  }
  gettokenInfo() {
    var data = { value: "", isRemember: true }
    if (this.commonService.getCookie('TokenInfo')) {
      data.value = this.commonService.getCookie('TokenInfo')
      data.isRemember = true
    }
    else if (sessionStorage.getItem('TokenInfo')) {
      data.value = sessionStorage.getItem('TokenInfo')
      data.isRemember = false
    }
    // if (data.value) {
      return data;
    // }
  }
  setTokenInfo(data, isRemember) {
    
    // this.removeToken();
    if (isRemember) {
      this.commonService.setCookie('TokenInfo', JSON.stringify(data), 1)
    } else {
      sessionStorage.setItem('TokenInfo', JSON.stringify(data))
    }

  }
  removeUserInfo() {
    this.commonService.deleteCookie('userInfo')
    sessionStorage.removeItem('userInfo');
  }

  public deleteAllCookieAndSessionValues() {  
    this.commonService.deleteAllCookie()
    sessionStorage.removeItem('TokenInfo');
    sessionStorage.removeItem('userInfo');
  }
  removeToken() {
    this.commonService.deleteCookie('TokenInfo')
    sessionStorage.removeItem('TokenInfo');
  }
  get checkPasswordChangeAccess() {
    return this._checkPasswordChangeAccess;

  }
  set checkPasswordChangeAccess(data) {
    this._checkPasswordChangeAccess = data;
  }

  get username() {
    return this._username;

  }
  set username(data) {
    this._username = data;
  }
}
