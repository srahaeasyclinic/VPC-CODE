import { Component, OnInit, Pipe, PipeTransform, OnChanges, ViewChild, ChangeDetectorRef } from '@angular/core';
import { CommonService } from '../services/common.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { UserInfoService } from '../services/userInfo.service';
import * as jwt_decode from 'jwt-decode';
import { convertActionBinding } from '@angular/compiler/src/compiler_util/expression_converter';
import { Router } from '@angular/router';
import { SessionexpirypopupService } from './sessionexpirypopup.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
@Component({
  selector: 'app-sessionexpirypopup',
  templateUrl: './sessionexpirypopup.component.html',
  styleUrls: ['./sessionexpirypopup.component.css'],
  providers: [SessionexpirypopupService]
})
export class SessionExpiryPopupComponent implements OnInit, OnChanges {
  private modalReference: any;
  isLoginModal = false;
  @ViewChild("sessionExpiry") sessionExpiry;
  data: any
  popuptimeout: NodeJS.Timer;
  constructor(
    public commonService: CommonService,
    private userInfoService: UserInfoService,
    private modalService: NgbModal,
    private refChangedetect: ChangeDetectorRef,
    private sessionexpirypopupService: SessionexpirypopupService,
    private router: Router,
    private globalResourceService: GlobalResourceService
  ) { }
  ngOnDestroy() {
    if (this.popuptimeout) {
      clearInterval(this.popuptimeout);
    }
  }
  ngOnInit() {

    this.userInfoService.sessionExpiryPopupRendered == null ? 'false' : this.userInfoService.sessionExpiryPopupRendered
    this.popuptimeout = setInterval(() => {
      if (this.getSessionExpiryTime()) {
        var data = Number((this.getSessionExpiryTime()).toFixed(0))
        var minutes = Math.floor(data / 60);
        var seconds = data - minutes * 60;
        var totalseconds = data
        this.data = {
          minutes: minutes,
          seconds: seconds,
          totalseconds: totalseconds
        }
      }
      if (this.data && this.data.totalseconds <= 0) {
        this.isLoginModal = true;
      } 
      else {
        if (this.userInfoService.sessionExpiryPopupRendered === 'false') {
          if (this.getSessionExpiryTime()) {
            this.sessionExpiryPopup();
            this.userInfoService.sessionExpiryPopupRendered = 'true';
            this.isLoginModal = false;
            this.refChangedetect.detectChanges()
          }
        }
      }

    }
      , 1);

  }
  public sessionExpiryPopup(): void {

    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(this.sessionExpiry, ngbModalOptions);
  }
  ngOnChanges() {

  }
  getSessionExpiryTime() {
    var token = this.userInfoService.gettokenInfo().value;
    if (token) {
      var decoded = jwt_decode(token);
      if (decoded.exp === undefined) return null;
      var todayseconds = new Date().getTime() / 1000;
      if (decoded.exp - todayseconds < 60) {
        return parseInt(decoded.exp) - todayseconds
      }
    }
  }
  revokeAuthorization() {
    this.sessionexpirypopupService.revokeAuthorization().subscribe(result => {
      if (this.userInfoService.gettokenInfo().isRemember) {
        this.userInfoService.setTokenInfo(result, this.userInfoService.gettokenInfo().isRemember)
        this.userInfoService.sessionExpiryPopupRendered = 'false'
        // this.modalService.dismissAll();
        this.modalReference.close();
      }
    })
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  setIsModal() {
    this.isLoginModal = false;
  }
}