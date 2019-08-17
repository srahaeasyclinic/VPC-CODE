import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../services/userInfo.service';
import { environment } from '../../environments/environment';
import { errorPageModel } from '../model/errorPage';


@Component({
  selector: 'app-pagenotfound',
  templateUrl: './pagenotfound.component.html',
  styleUrls: ['./pagenotfound.component.css']
})
export class PagenotfoundComponent implements OnInit {

  public model=new errorPageModel();

  constructor(private userInfoService: UserInfoService) { }

  ngOnInit() {
    this.setModel();
}

  setModel()
  {
    this.model.body = "The page you are looking for might have been removed, had its name changed or is temporarily unavailable.";
    this.model.isAuthorize = this.userInfoService.gettokenInfo().value && !this.userInfoService.isTokenExpired();
    this.model.statusCode = 404;
    this.model.title = "404";
    this.model.subtitle = "Oops! nothing was found";
  }
}
