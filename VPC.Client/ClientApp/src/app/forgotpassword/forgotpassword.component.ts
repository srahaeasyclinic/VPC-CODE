import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { TosterService } from '../services/toster.service';
import { UserInfoService } from '../services/userInfo.service';
@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css'],
  providers: [LoginService]
})

export class ForgotPasswordComponent implements OnInit {
  changePassForm: FormGroup;
  submitClick = false;
  submitted = false;
  returnUrl: string;
  error = '';
  templateEmail: string = ''
  IsPasswordChanged = false
  submitClicked = false;

  constructor(private formBuilder: FormBuilder,
    private loginService: LoginService, private router: Router, private toster: TosterService,private userInfoService:UserInfoService) { }

  ngOnInit() {
    this.userInfoService.removeToken();
    this.changePassForm = this.formBuilder.group({
      tenantcode: ['', Validators.required],
      username: ['', Validators.required]
    });
  }
  get formData() { return this.changePassForm.controls; }

  onsubmit() {
    if (this.changePassForm.valid) {
      this.loginService.forgotpass(this.changePassForm.value).subscribe(x => this.afterReset(x),
        error => {
          if (error.status == 500) {
            var errorbuilder = error.error.message;
            this.toster.showError(errorbuilder)
          } else if (error.status == 401){
            this.toster.showErrorWithTitle('Invalid credential', 'The credential provided are invalid')
          }
        })
    } else {
      var errorMsg = ""
      if (this.formData.tenantcode.errors && this.formData.tenantcode.errors.required) {
        errorMsg = "Workspace is required" + "<br>"
      }
      if (this.formData.username.errors && this.formData.username.errors.required) {
        errorMsg += "Username is required" + "<br>"
      }
      if (errorMsg) {
        this.toster.showError(errorMsg)
      }
      this.submitClicked = true;

    }

  }
  navigateBack() {
    this.router.navigate([''])
  }
  afterReset(data) {
    this.toster.showSuccess(data.message)
    setTimeout(() => {
      this.router.navigate([''])
    }, 1000);
  }
}
