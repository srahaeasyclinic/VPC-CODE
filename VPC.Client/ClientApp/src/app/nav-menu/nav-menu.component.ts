import { Component, OnInit } from '@angular/core';
//import { Observable } from 'rxjs';
import { LoginService } from '../login/login.service'; 
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  
  public searchText : any;
  isLog:boolean;
  constructor(public authService: LoginService) { }

  ngOnInit() {
   // this.isLog = this.authService.isLoggedIn;
  }

  onLogout() {
    this.authService.logout();
  }
}
