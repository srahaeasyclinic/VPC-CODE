import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params, Route, UrlTree, UrlSegmentGroup, UrlSegment, UrlSerializer } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuService, MenuContextObject } from '../services/menu.service';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import * as _ from 'lodash';
import { first } from 'rxjs/operators';

import { MenuItemComponent } from '../menu-item/menu-item.component';
import { MetadataComponent } from '../meta-data/metadata.component';
import { MetadataDetailComponent } from '../meta-data/metadatadetail.component';
import { FieldsComponent } from '../meta-data/fields/fields.component';
import { AuthorizationCheck } from '../interceptor/authorizationCheck'

import { Type } from '@angular/compiler';
import { CommonService } from '../services/common.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import { forEach } from '@angular/router/src/utils/collection';
import { element } from '@angular/core/src/render3';
import { Ibreadcrumbs } from '../bread-crumb/BreadcrumbsService';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  name: string;
  public spinkit = Spinkit;
  public leftmenuItems: Array<MenuItem>;
  public leftmenuItemsNew: any;
  group: string;
  isMenuopen: boolean = false;
  public groupName: string = "";
  public menuItem: any;
  public componentName: any;
  public childMenu: any;
  public resource: Resource;

  public groups: NewMenuItem[] = [];
  public menus: Array<NewMenuItem>;
  public currentGroup: NewMenuItem;
  public currentMenu: NewMenuItem;
  currentmenuId: string;
  breadcum: Ibreadcrumbs[];
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private serializer: UrlSerializer,
    public commonService: CommonService,
    public globResource: GlobalResourceService,
  ) { }

  ngOnInit() {
    //debugger;
    this.setbreadcrums();
    // var cacheMenus = this.menuService.getCacheMenus();
    // if (cacheMenus == null) {
    //   this.loadMenu();
    // } else {
    //   this.menus = cacheMenus;
    //   //console.log('data ', this.menus);
    //   //console.log('data ', this.menus);
    //   this.groups = _.uniqBy(this.menus, 'groupName');
    // }
    // var cacheResource=this.globResource.getGlobalResources();
    // if(cacheResource==null){
    //   this.globResource.getResource();
    this.resource = this.globResource.getGlobalResources();
    // }
    // else{
    //   this.resource=cacheResource;
    // }
    //console.log('Home Resource '+JSON.stringify( this.resource));

  }
  setbreadcrums()
  {
     this.breadcum = [{
      elementName: 'Dashboard',
      elementURL: '',
      isGroup: false
    }]

  }
  // public nevigateChildren(menu: NewMenuItem) {
  //   this.breadcum = []
  //   this.currentMenu = menu;
  //   this.currentmenuId = menu.id;
  //   this.menuService.setCacheMenu(menu);//this.currentGroup.groupName
  //   let menuNameUpper = this.getMenuName(menu);
  //   let menuName = this.commonService.getTrimmenuStr(menuNameUpper).toLocaleLowerCase();
  //   this.menuItem = menu.name;
  //   let url = this.getchildURL(menu, menuName);

  //   this.breadcum = this.menuService.getbreadcums();

  //   this.router.navigate([url]);
  // }

  // getchildURL(menu: NewMenuItem, menuName: string): string {
  //   if (menu == null && menu == undefined) {
  //     return "dashboard";
  //   }
  //   let groupname = this.menus.find(d => d.groupId == menu.groupId && d.id == menu.parentId);

  //   let url = "";
  //   var objectManager = this.menuService.getVirtualGroup(this.currentMenu.menuTypeId, this.currentMenu.actionTypeId);

  //   if (menu.menuTypeId == 3) {
  //     this.setmenuContext(menu, menuName, groupname.name, groupname.id, menu.name, menu.id);
  //     url = objectManager + "/" + this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase();
  //   }
  //   // else if (menu.menuTypeId==4)
  //   // {
  //   //    url = objectManager+"/"+this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase()  +"/" + menuName.toLowerCase();
  //   //   }
  //   else {
  //     this.setmenuContext(menu, menuName, groupname.name, groupname.id, menu.name, menu.id);
  //     url = this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase();
  //   }
  //   return url;
  // }
  // getUrl(groupName: string, leftgroupName: string, menuName: string): string {
  //   if (this.currentGroup == null) return;
  //   let url = "";
  //   var objectManager = this.menuService.getVirtualGroup(this.currentMenu.menuTypeId, this.currentMenu.actionTypeId);
  //   if (this.currentMenu.menuTypeId == 3) {

  //     url = objectManager + "/" + groupName.toLowerCase() + "/" + leftgroupName.toLocaleLowerCase();
  //   }
  //   // else if (this.currentMenu.menuTypeId==4)
  //   // {
  //   //    url = objectManager+"/"+groupName.toLowerCase() + "/" + leftgroupName.toLocaleLowerCase()  +"/" + menuName.toLowerCase();
  //   //   }
  //   else {
  //     url = groupName.toLowerCase() + "/" + leftgroupName.toLocaleLowerCase(); //"/" + menuName.toLowerCase();
  //   }

  //   return url;
  // }

 

 // }

}
