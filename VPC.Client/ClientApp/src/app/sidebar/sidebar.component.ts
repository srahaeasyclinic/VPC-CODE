import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { MenuService } from '../services/menu.service';
import { MenuGroup } from '../model/menuGroup';
import { LoginService } from '../login/login.service';

import { PicklistUiService } from '../picklist-ui/picklist-ui.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { TosterService } from '../services/toster.service';
import { NewMenuItem } from '../model/menuItem';
import { Router, ActivatedRoute } from '@angular/router';
import { Resource } from '../model/resource';
//import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
// outputs: [" topMenuClickEvent:generateSideMenu ", " topMenuNewClickEvent:generateSideMenuNew "]
export class SidebarComponent implements OnInit {

  @Input() menus: NewMenuItem[];
  @Input() currentGroup: NewMenuItem;
  @Input() isMenuopen: boolean;

  @Output() menuClickEvent: EventEmitter<any> = new EventEmitter();




  // public topMenuClickEvent: EventEmitter<any>;
  // public topMenuNewClickEvent: EventEmitter<any>;

  // public topMenu: Array<MenuGroup> = [];
  // public topMenuNew: Array<MenuGroup> = [];


  // private layoutType: number = 3;
  // public defaultLayout: LayoutModel = new LayoutModel();
  // private orderBy: string = '';
  // public sort: SortDescriptor[];
  // public selectedFields: string = '';
  // public results: any;
  // public totalRecords: number = 0;
  // public pageindex: number = 1;
  // private pageSize: number = 10;



  isLog: boolean;
  isConfigToggle: boolean = true;
  //isMenuopen: boolean = false;
  isSubMenuopen: boolean = false;
  menuName: string;
  public menuItemName: string = "";

  // public groupName: string;
  public resource: Resource;



  constructor(public menuService: MenuService,
    public authService: LoginService,
    private picklistService: PicklistUiService,
    private toster: TosterService,
    private router: Router,
    private activatedRoute: ActivatedRoute,

  ) {
    // this.topMenuClickEvent = new EventEmitter();
    // this.topMenuNewClickEvent = new EventEmitter();
  }

  ngOnInit() {
    //console.log('menu ngOnInit Called');
    if(this.menuService.getCacheMenu())
    {
      this.menuItemName = this.menuService.getCacheMenu().name;
      //console.log('this.menuItemName ', this.menuItemName);
    }
  
    this.menuToggle();
    //console.log(this.router);
  }

  configToggle() {
    this.isConfigToggle = !this.isConfigToggle;
  }

  menuToggle() {
    this.isMenuopen = !this.isMenuopen;
  }

  childMenuToggle() {
    this.isSubMenuopen = !this.isSubMenuopen;
  }

  navigateUrl(item) {
    this.menuClickEvent.emit(item);
    this.menuItemName = item.name;
  }
  onLogout() {
    this.authService.logout();
  }

  generateResourceName(word)
 {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }
}