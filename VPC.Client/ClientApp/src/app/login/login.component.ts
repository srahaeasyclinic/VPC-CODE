import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { LoginService } from './login.service';
import { MenuService } from '../services/menu.service';
import { NewMenuItem } from '../model/menuItem';
import { UserInfoService } from '../services/userInfo.service';
import { TosterService } from '../services/toster.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitClick = false;
  submitted = false;
  returnUrl: string;
  error = '';
  url: any;
  tenantcode: string;
  isfirst = false;
  containsTenantCode = false
  isRemember: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private menuService: MenuService,
    private loginService: LoginService,
    private userinfoService: UserInfoService,
    private toster: TosterService,
    private location: Location
  ) { }

  ngOnInit() {
    this.userinfoService.username = null
    this.userinfoService.checkPasswordChangeAccess = null
    this.loginForm = this.formBuilder.group({
      tenantcode: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      isRemember: [true, Validators.required]

    });
    this.route.queryParams.subscribe(params => {
      if (!this.isfirst) {
        var tenantcode = params['tenant'];
        if (tenantcode) {
          this.loginForm.controls['tenantcode'].setValue(tenantcode)
          this.url = '?tenant=' + tenantcode
          this.isfirst = true
          this.containsTenantCode = true
        } else {
          this.isfirst = true
          this.containsTenantCode = true
        }
      }
      else {
        if (this.url && this.containsTenantCode)
          this.location.go(this.url);
      }

    });


    // reset login status
    this.loginService.logout();
    // get return url from route parameters or default to '/'
    //this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/metadata';
    //this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/view/metadata';
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'home';
  }

  // convenience getter for easy access to form fields
  get formData() { return this.loginForm.controls; }

  onLogin() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {

      var errorMsg = ""
      if (this.formData.tenantcode.errors && this.formData.tenantcode.errors.required) {
        errorMsg = "Workspace is required" + "<br>"
      }
      if (this.formData.username.errors && this.formData.username.errors.required) {
        errorMsg += "Username is required" + "<br>"
      }
      if (this.formData.password.errors && this.formData.password.errors.required) {
        errorMsg += "Password is required" + "<br>"
      }
      if (errorMsg) {
        this.toster.showError(errorMsg)
      }

      return;
    }

    this.submitClick = true;
    
    this.loginService.login(this.formData.tenantcode.value, this.formData.username.value, this.formData.password.value, this.formData.isRemember.value)
      .pipe(first())
      .subscribe(
        data => {
          this.userinfoService.setUserInfo({ tenantcode: this.formData.tenantcode.value, username: this.formData.username.value }, this.formData.isRemember.value);
          if (data.isChangedPassEnabled) {
            // this.toster.showInfo(data.message)
            this.router.navigate(['changepassword']);
          } else {

            this.loadMenu();
          }

        },
        error => {
          if (error.status == 401) {
            this.toster.showErrorWithTitle('Invalid credential', 'The credential provided are invalid')
          }
          else if (error.status == 500) {

          }
          // this.error = error.message;
          this.submitClick = false;
        });
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

  navigate() {
    this.router.navigate(['forgotpassword'])
  }

}
