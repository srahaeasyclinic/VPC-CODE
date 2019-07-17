import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { LoginService } from '../login/login.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Passwordpolicyinfo } from '../model/passwordpolicyinfo';
import { UserInfoService } from '../services/userInfo.service';
import { Location } from '@angular/common';
import { ResourceService } from '../services/resource.service';
import { TosterService } from '../services/toster.service';
import { MenuService } from '../services/menu.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { truncateSync } from 'fs';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css'],
  providers: [LoginService]
})

export class ChangePasswordComponent implements OnInit {
  changePassForm: FormGroup;
  submitClick = false;
  submitted = false;
  returnUrl: string;
  error = [];
  errormsg = []
  templateEmail: string = ''
  IsPasswordChanged = false
  IsAfterLogin = false
  tokenInfo: any
  resourceinfo: any
  passwordPolicyInfo: Passwordpolicyinfo
  passwordLastChngedOn: Date
  submitClicked = false;
  userInfo: any
  user: { passwordChangedOn: Date; isNew: boolean };
  constructor(private formBuilder: FormBuilder, private loginService: LoginService, private router: Router,
    public userinfoService: UserInfoService, private location: Location, private resourceService: ResourceService
    , private toster: TosterService, private menuService: MenuService, private route: ActivatedRoute,
    private globalResourceService: GlobalResourceService) { }

  ngOnInit() {
    this.returnUrl = 'home';
    var data: any = this.userinfoService.userInfo
    this.userInfo = this.userinfoService.userInfo
    if (data == null) {
      this.router.navigate([''])
    }
    this.changePassForm = this.formBuilder.group({
      oldpass: ["", Validators.required],
      newpass: ["", Validators.required],
      newpassconfirm: ["", Validators.required]
    });
    var tokeninfo: any;
    if (this.userinfoService.gettokenInfo().value) {
      this.tokenInfo = this.userinfoService.gettokenInfo().value    
      this.userinfoService.removeToken();    
      this.getPasswordPolicy(data);
      this.getUserPasswordChangedOn(data);
    }
    else {
      this.router.navigate(['']);
    }

  }

  getUserPasswordChangedOn(data) {
    this.loginService.getUserPasswordChangedOn(data).subscribe(x => {
      this.user = x
    })
  }
  getPasswordPolicy(data) {
    this.loginService.getPasswordPolicy(data.tenantcode).subscribe(x => {
      if (x.data && Array.isArray(x.data) && x.data.length > 0) {
        this.setData(x.data[0])
        this.changePassForm.controls['newpass'].setValidators([Validators.minLength(this.passwordPolicyInfo.passwordLength), Validators.required]);
        this.changePassForm.controls['newpassconfirm'].setValidators([Validators.minLength(this.passwordPolicyInfo.passwordLength), Validators.required]);
      }
    })
  }

  get formData() { return this.changePassForm.controls; }
  onsubmit() {
    if (this.changePassForm.valid) {
      this.error = [];
      var data = this.changePassForm.getRawValue()
      var userinfo: any = this.userinfoService.userInfo
      var data1 = { "TenantCode": userinfo.tenantcode, "UserName": userinfo.username, "NewPassword": data.newpass, "OldPassword": data.oldpass }
      this.loginService.changepass(data1).subscribe(x => {
        this.toster.showSuccess(this.getResourceValue('lblPassChangesSuccessfully'))
        setTimeout(() => {
          var data = JSON.parse(this.tokenInfo)
          this.userinfoService.setTokenInfo(data, this.userinfoService.gettokenInfo().isRemember)
          this.loadMenu();
        }, 1000);
      }, error => {
        if (error.status == 500) {
          this.error = error.error.errorList;
          var errormsg = ""
          this.error.forEach(element => {
            errormsg += this.getResourceValue(element) + "<br>"
          });

          this.toster.showError(errormsg)
        } else {
          this.toster.showError(this.getResourceValue('ChangePassword.InvalidCurrentPasssword'))
        }

      })
    } else {
      this.submitClicked = true;
      var errorMsg = ""
      if (this.formData.oldpass.errors && this.formData.oldpass.errors.required) {
        errorMsg = this.getResourceValue('Validation_OldPassword_Message') + "<br>"
      }
      if (this.formData.newpass.errors && this.formData.newpass.errors.required) {
        errorMsg += this.getResourceValue('Validation_NewPassword_Message') + "<br>"
      }
      if (this.formData.newpassconfirm.errors && this.formData.newpassconfirm.errors.required) {
        errorMsg += this.getResourceValue('Validation_ConfirmPassword_Message') + "<br>"
      }
      if (errorMsg) {
        this.toster.showError(errorMsg)
      }

    }
  }
  private loadMenu() {
    this.menuService.getAllMenu().subscribe(result => {
      if (result) {
        if (!result) this.toster.showWarning('Url tempered! or no entity name found!');
        this.menuService.setCacheMenus(result);
        this.router.navigate([this.returnUrl]);
      }
    });
  }
  onfocusout() {

    if (this.changePassForm.controls['newpassconfirm'].value) {
      if (this.changePassForm.controls['newpass'].value == this.changePassForm.controls['newpassconfirm'].value) {

        this.changePassForm.controls['newpassconfirm'].setErrors(null);
      } else {
        this.changePassForm.controls['newpassconfirm'].setErrors({
          notsame: true
        })

      }
    }


  }
  checkOldPassVsNewPass() {
    if (this.errormsg.length > 0 && this.errormsg.includes('validationOldNewPass')) {
      return this.getResourceValue('validationOldNewPass')
    }

  }
  validate() {
    if (this.passwordPolicyInfo) {
      this.isValidNewPassword(this.changePassForm.controls['newpass'].value)
      this.onfocusout();
    }
  }

  validationMessage(type) {
    if (this.changePassForm.controls['newpass'].value) {
      if (this.errormsg && this.errormsg.length > 0) {
        if (this.errormsg.includes(type)) {
          return 'red-tick';
        } else {
          return 'green-tick';
        }
      } else {
        return 'green-tick';
      }
    } else {
      return 'red-tick';
    }


  }
  isValidNewPassword(ctrlValue) {
    var error = []
    if (ctrlValue && ctrlValue.length > 0) {
      if (this.changePassForm.controls['oldpass'].value == ctrlValue) {
        error.push('validationOldNewPass')
      }
      if (ctrlValue.length < this.passwordPolicyInfo.passwordLength) {

        error.push('validationMinLength');
      }
      if (this.passwordPolicyInfo.isUppercase) {
        if (!/(?=.*[A-Z])/.test(ctrlValue)) {
          error.push('validationUppercase');
        }
      }
      if (this.passwordPolicyInfo.isLowercase) {
        if (!/(?=.*[a-z])/.test(ctrlValue)) {
          error.push('validationLowercase');
        }
      }

      if (this.passwordPolicyInfo.isNumber) {
        if (!/(?=.*[0-9])/.test(ctrlValue)) {
          error.push('validationNumber');
        }
      }

      if (this.passwordPolicyInfo.isNonAlphaNumeric) {
        if (!/(?=.*[#$@!%&*?])/.test(ctrlValue)) {
          error.push('validationNonAlphaNumeric');
        }
      }

      if (error.length > 0) {
        this.changePassForm.controls['newpass'].setErrors({
          customerror: true
        })
      } else {
        this.changePassForm.controls['newpass'].setErrors(null);
      }
      this.changePassForm.controls['newpass'];

    }
    this.errormsg = error
  }
  setData(data) {
    this.passwordPolicyInfo = {
      isUppercase: data.isUppercase != null && data.isUppercase != '' ? data.isUppercase.toLowerCase() == 'true' ? true : false : false,
      isLowercase: data.isLowercase != null && data.isLowercase != '' ? data.isLowercase.toLowerCase() == 'true' ? true : false : false,
      isNumber: data.isNumber != null && data.isNumber != '' ? data.isNumber.toLowerCase() == 'true' ? true : false : false,
      isNonAlphaNumeric: data.isNonAlphaNumeric != null && data.isNonAlphaNumeric != '' ? data.isNonAlphaNumeric.toLowerCase() == 'true' ? true : false : false,
      passwordLength: data.passwordLength ? data.passwordLength : 0
    }
  }
  navigateBack() {
    
    if (this.tokenInfo) {
      var data = JSON.parse(this.tokenInfo)
      if (data.isChangedPassEnabled) {

      } else {        
        this.userinfoService.setTokenInfo(data, this.userinfoService.gettokenInfo().isRemember)
      }
    }
    this.location.back();
  }
  checkValidators() {
    if (this.passwordPolicyInfo) {
      if (this.passwordPolicyInfo.isUppercase || this.passwordPolicyInfo.isLowercase || this.passwordPolicyInfo.isNumber || this.passwordPolicyInfo.isNonAlphaNumeric || this.passwordPolicyInfo.passwordLength > 0) {
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }


  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
