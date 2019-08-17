import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CommonService } from './common.service';

import * as jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})

export class UserInfoService {
  _username: any;
  _checkPasswordChangeAccess: any
  sessionExpiryPopupRendered1 = false;
  constructor(private http: HttpClient, private commonService: CommonService) { }
  url = "api/access";
  test() {
    return "test"
  }


  get sessionExpiryPopupRendered() {
    return localStorage.getItem('sessionExpiryPopupRendered')
  }

  set sessionExpiryPopupRendered(data) {
    localStorage.setItem('sessionExpiryPopupRendered', data)
  }
  get userInfo() {
    var data: any

    if (this.commonService.getCookie('userInfo')) {
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
      this.commonService.setCookie('userInfo', JSON.stringify(data), 1);
    } else {
      sessionStorage.setItem('userInfo', JSON.stringify(data))
    }

  }
  gettokenInfo() {
    var data = { value: "", isRemember: false }
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
    this.commonService.deleteOneCookie('userInfo')
    sessionStorage.removeItem('userInfo');
  }

  public deleteAllCookieAndSessionValues() {
    if (this.gettokenInfo().isRemember) {
      this.commonService.deleteOneCookie('TokenInfo')
    } else {
      this.commonService.deleteOneCookie('TokenInfo')
      this.commonService.deleteOneCookie('userInfo')
    }
    sessionStorage.removeItem('TokenInfo');
    sessionStorage.removeItem('userInfo');
  }
  removeToken() {
    this.commonService.deleteOneCookie('TokenInfo')
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

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);
    if (decoded.exp === undefined) return null;
    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }
  get getSessionExpiryTime() {
    var token = this.gettokenInfo().value;
    if (token) {
      var decoded = jwt_decode(token);
      if (decoded.exp === undefined) return null;
      var todayseconds = new Date().getTime() / 1000;
      // if (todayseconds - decoded.exp < 10) {
      return todayseconds - decoded.exp
      // }
    }
  }
  public isTokenExpired(token?: string): boolean {
    if (!token) token = this.gettokenInfo().value;
    if (!token) return true;
    const date = this.getTokenExpirationDate(token);
    if (date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

  
}
