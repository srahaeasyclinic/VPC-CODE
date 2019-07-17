import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params, Route, UrlTree, UrlSegmentGroup, UrlSegment, UrlSerializer } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuService } from '../services/menu.service';
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

  public groups: Array<NewMenuItem> = [];
  public menus: Array<NewMenuItem>;
  public currentGroup: NewMenuItem;
  public currentMenu: NewMenuItem;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private serializer: UrlSerializer,
    public commonService: CommonService,
    public globResource: GlobalResourceService,
  ) { }

  ngOnInit() {
    var cacheMenus = this.menuService.getCacheMenus();
    if (cacheMenus == null) {
      this.router.navigate(['']);
    } else {
      this.menus = cacheMenus;
      //console.log('data ', this.menus);
      //console.log('data ', this.menus);
      this.groups = _.uniqBy(this.menus, 'groupName');
    }
    // var cacheResource=this.globResource.getGlobalResources();
    // if(cacheResource==null){
    //   this.globResource.getResource();
      this.resource=this.globResource.getGlobalResources();
    // }
    // else{
    //   this.resource=cacheResource;
    // }
    //console.log('Home Resource '+JSON.stringify( this.resource));

  }
  nevigate(objectName: string, groupName: string, menuName: string) {
    var url = objectName + "/" + groupName + "/" + menuName;
    this.router.navigate([url]);
  }
  private navigateUrl(group: NewMenuItem) {
    //console.log('group ', group);
    this.menuService.setCacheGroup(group);
    var firstMenu = this.firstElementFromGroup(group);
    var groupName = this.commonService.getTrimStr(group.groupName);
    var menuName = this.getMenuName(firstMenu);
    this.menuService.setCacheMenu(firstMenu);
    var objectName = this.getObjectName(group, firstMenu);
    this.nevigate(objectName, groupName, menuName);
  }

  getObjectName(group: NewMenuItem, menu: NewMenuItem): string {
    var objectManager = this.commonService.getVirtualGroup(group.menuTypeId, menu.actionTypeId);
    // var objectManager = "";

    // if (group.menuTypeId == 1) {
    //   objectManager = "object-manager";
    // } else if (group.menuTypeId == 2) {
    //   objectManager = "picklist-manager";
    //   //  url =objectManager+"/"+groupName.toLowerCase()+"/"+menuName.toLowerCase();
    // } else if (group.menuTypeId == 3) {
    //   if (menu.actionTypeId == 2) {
    //     objectManager = "entity-designer";
    //   } else if (menu.actionTypeId == 3) {
    //     objectManager = "picklist-designer";
    //   }
    // } else if (group.menuTypeId == 4) {
    //   objectManager = "configuration-manager";
    // }
    return objectManager;
  }

  getMenuName(firstMenu: NewMenuItem): string {
    var menuName = "";
    if (firstMenu.actionTypeId == 2 || firstMenu.actionTypeId == 3) {
      menuName = this.commonService.getTrimStr(firstMenu.name);
    } else {
      menuName = (firstMenu.referenceEntityId != "") ? this.commonService.getTrimStr(firstMenu.referenceEntityId) : this.commonService.getTrimStr(firstMenu.wellKnownLink);
    }
    return menuName;
  }
  firstElementFromGroup(groupName: NewMenuItem): NewMenuItem {
    var menuName: NewMenuItem = null;
    this.menus.forEach(element => {
      if (element.groupName == groupName.groupName) {
        menuName = element;
        return;
      }
    });
    return menuName;
  }

}
