import { Component, OnInit, Type } from '@angular/core';
import { Router, ActivatedRoute, Params, Route, UrlTree, UrlSegmentGroup, UrlSegment, UrlSerializer } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuService,MenuContextObject } from '../services/menu.service';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import { CommonService } from '../services/common.service';
import{GlobalResourceService} from '../global-resource/global-resource.service'
import * as _ from 'lodash';
import { DebugRenderer2 } from '@angular/core/src/view/services';

import { PicklistIntialiser } from '../initialiser/picklistintialiser.component';
import { MetadataIntialiser } from '../initialiser/metadataIntialiser.component';
import { Ibreadcrumbs, BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';
import { RoutelocalizationService } from '../services/routelocalization.service';

// import { PlatformLocation } from '@angular/common';



@Component({
  selector: 'intialiser-base',
  template: 'NOT REQUIRED',
})
export class BaseIntialiser implements OnInit {
  name: string;
  public spinkit = Spinkit;
  public leftmenuItems: Array<MenuItem>;
  public leftmenuItemsNew: any;
  group: string;
  isMenuopen: boolean = false;
  public groupName: string = "";
  public menuItem: string = "";
  public componentName: any;
  public childMenu: any;
  public menus: Array<NewMenuItem>;
  public currentGroup: NewMenuItem;
  public breadcum: Array<Ibreadcrumbs>;
  public currentMenu: NewMenuItem;
  public topcurrentMenu: NewMenuItem;
  public currentmenuId: string;
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public menuService: MenuService,
    public serializer: UrlSerializer,
	  public commonService:CommonService,
    public globalResource: GlobalResourceService,
    public breadcrumsService: BreadcrumbsService,
    public localization:RoutelocalizationService
  ) { 
    
    }

  ngOnInit() {
    var cacheMenus = this.menuService.getCacheMenus();
    if (cacheMenus == null) {
      this.loadMenu();
    } else {
      this.menus = cacheMenus;
      this.currentGroup = this.menuService.getCacheGroup();
      this.breadcum = this.breadcrumsService.getbreadcums();
    }
    // for resoure initialization
    var cacheResource=this.globalResource.getGlobalResources();
    if(cacheResource==null){
      // console.log('base resour call');
      this.globalResource.getResource();
    }

   
  }

  private loadMenu() {
    this.menuService.getAllMenuBytenant().subscribe(result => {
      if (result) {
        this.menus = result;
        //this.nevigateGroup(this.menus[0]);
        this.menuService.setCacheMenus(this.menus);
        this.router.navigate([this.localization.getDefaultUrl()]);
      }
    });
  }
  nevigateGroup(group: any) {
    this.currentGroup = group;
    this.menuService.setCacheMenu(null);
    this.menuService.setCacheGroup(this.currentGroup);
    this.topcurrentMenu=this.menus.find(w=>w.id==this.currentGroup.parentId);
    let topGroup = this.commonService.getTrimmenuStr(this.topcurrentMenu.name);
    var LeftmaingroupName = this.commonService.getTrimmenuStr(this.currentGroup.name);
    var firstMenu = this.firstElementFromGroup(this.currentGroup);
    this.currentMenu =firstMenu;  //this.currentGroup
    var menuName = this.getMenuName(this.currentMenu);
    //console.log('this.currentMenu ', this.currentMenu);
    this.menuService.setCacheMenu(this.currentMenu);
    let leftgroup = this.commonService.getTrimmenuStr(this.currentMenu.name);
    this.setmenuContext(this.currentMenu,this.topcurrentMenu, menuName, LeftmaingroupName, this.currentGroup.id, leftgroup, this.currentMenu.id);
    this.breadcum = this.breadcrumsService.getbreadcums();
    this.navigate(topGroup,LeftmaingroupName,leftgroup, menuName);
  }
  getMenuName(firstMenu: NewMenuItem): string {
    var menuName = "";
   // if (this.currentGroup.actionTypeId == 2 || this.currentGroup.actionTypeId == 3) {
    if (firstMenu.actionTypeId == 2 || firstMenu.actionTypeId == 3) {
      menuName = this.commonService.getTrimmenuStr(firstMenu.name);
    } 
    else {
      menuName = (firstMenu.referenceEntityId != "") ? this.commonService.getTrimmenuStr(firstMenu.referenceEntityId) : this.commonService.getTrimmenuStr(firstMenu.wellKnownLink);
    }
    //console.log('this.menuName ', menuName);
    return menuName;
  }
  firstElementFromGroup(group: NewMenuItem): NewMenuItem {
   
    var menuName: NewMenuItem = null;

    //  this.menus.forEach(element => {
    //   if (element.groupName == group.groupName) {
    //     menuName = element;
    //     return;
    //   }});

    if (this.menus.find(d => d.groupName == group.groupName && d.parentId == group.id))
    {
      let menuobj = _.sortBy(this.menus.filter(d => d.groupName == group.groupName && d.parentId == group.id), 'sortItem');

    //  console.log('result '+ JSON.stringify(menuobj));

       return menuobj[0];
    }
   if (this.menus.find(d => d.parentId == group.id))
    {
      let menuobj = _.sortBy(this.menus.filter(d =>d.parentId == group.id), 'sortItem');

    //  console.log('result '+ JSON.stringify(menuobj));

       return menuobj[0];
    }
    return group;
  }
  navigate(topgroup:string,leftMaingroupName: string,leftgroupName: string, menuName: string) {
    if (topgroup == "" || leftMaingroupName == "" || menuName == "") {
      console.log("group name or menu name not found");
     // throw new Error("group name or menu name not found");
      this.router.navigate(['aboutpage']);
    } else {
         var url = this.getUrl(topgroup,leftMaingroupName, leftgroupName,menuName);;
    //console.log("url", url);
     this.router.navigate([url]);
    }
 
  }


  getUrl(topgroup:string,leftMaingroupName: string, leftgroupName: string, menuName: string): string {
    if (this.currentGroup == null) return;
    let url = "";
     var objectManager = this.menuService.getVirtualGroup(this.currentMenu.menuTypeId, this.currentMenu.actionTypeId);
  if(this.currentMenu.menuTypeId==3)
  {
   
    url = objectManager + "/" +topgroup.toLowerCase()+"/"+ leftMaingroupName.toLowerCase() + "/" + leftgroupName.toLocaleLowerCase();
  }
  else {
    url =topgroup.toLowerCase()+"/"+leftMaingroupName.toLowerCase() + "/" + leftgroupName.toLocaleLowerCase(); //"/" + menuName.toLowerCase();
    }

    return this.localization.getlocalizeUrl(url);
  }

  public nevigateChildren_old(menu: NewMenuItem) {
    // console.log('menu ', this.activatedRoute.parent);
    // debugger;
    this.currentMenu = menu;
    this.currentmenuId = menu.id;
   this.menuService.setCacheMenu(menu);//this.currentGroup.groupName
    let groupName = this.commonService.getTrimStr(this.currentGroup.groupName);
    let menuNameUpper = this.getMenuName(menu);
    let menuName = this.commonService.getTrimStr(menuNameUpper).toLocaleLowerCase();
    this.menuItem = menu.name;

    if (menu.menuTypeId == 4) {
      this.router.navigate([menuName], { relativeTo: this.activatedRoute });
    } else {
      if (menu.actionTypeId == 2) {
        this.router.navigate(["metadata", groupName, menuName]);
      } else if (menu.actionTypeId == 3) {
        this.router.navigate(["picklistmetadata", groupName, menuName]);
      } else {
        
        this.router.navigate(["../" + menuName], { relativeTo: this.activatedRoute });
      }
    }
    //console.log('this.menuName ', this.menuItem);
  }
  public nevigateChildren(menu: NewMenuItem) {
    
    this.currentMenu = menu;
    this.currentmenuId = menu.id;
    this.menuService.setCacheMenu(menu);//this.currentGroup.groupName
    let menuNameUpper = this.getMenuName(menu);
    let menuName = this.commonService.getTrimmenuStr(menuNameUpper).toLocaleLowerCase();
    this.menuItem = menu.name;
    this.childnavigation(menu,menuName);
   
  }
  childnavigation(menu:NewMenuItem, menuName:string)
  {
    if (menu == null || menuName == "") {
      console.log("menu or menu name not found");
      this.router.navigate(['aboutpage']);
    }
    else {
      let url = this.getchildURL(menu, menuName);

      this.breadcum = this.breadcrumsService.getbreadcums();
    
      this.router.navigate([url]);
    }
  }

  getchildURL(menu: NewMenuItem,menuName:string):string
  {
    if ((menu == null && menu == undefined))
    {
      return this.localization.getDefaultUrl();
    }
    let groupname = this.menus.find(d => d.groupId == menu.groupId && d.id == menu.parentId);
    this.topcurrentMenu=this.menus.find(d => d.groupId == menu.groupId && d.id == groupname.parentId);
    
    let url = "";
    var objectManager = this.menuService.getVirtualGroup(this.currentMenu.menuTypeId, this.currentMenu.actionTypeId);
    
   if(menu.menuTypeId==3)
   {
     this.setmenuContext(menu,this.topcurrentMenu,menuName,groupname.name,groupname.id,menu.name,menu.id);
     url = objectManager+"/"+this.commonService.getTrimmenuStr(this.topcurrentMenu.name).toLocaleLowerCase() + "/" +this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase();
    }
    // else if (menu.menuTypeId==4)
    // {
    //    url = objectManager+"/"+this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase()  +"/" + menuName.toLowerCase();
    //   }
   else {
     this.setmenuContext(menu,this.topcurrentMenu,menuName,groupname.name,groupname.id,menu.name,menu.id);
     url = this.commonService.getTrimmenuStr(this.topcurrentMenu.name).toLocaleLowerCase() + "/" +this.commonService.getTrimmenuStr(groupname.name).toLocaleLowerCase() + "/" + this.commonService.getTrimmenuStr(menu.name).toLocaleLowerCase();
    }
    return this.localization.getlocalizeUrl(url);
  }
  setmenuContext(menu:NewMenuItem,topMenu:NewMenuItem,menuName:string,topgroupname:string,topgorupId:string,sidemenuname:string,sidemenuId:string)
  {
    if (menu!=undefined && menu!=null && menuName!="")
    {
      let menucontext = new MenuContextObject();
      menucontext.topmainGroupId = topMenu.id;
      menucontext.topmainGroup = topMenu.name;

      menucontext.menuType = menu.menuTypeId;
      menucontext.param_name = menuName;
      menucontext.topgroupId = topgorupId;
      menucontext.leftgroupId = sidemenuId;
      menucontext.leftgroupName = this.commonService.getTrimmenuStrWithSpace(sidemenuname);
      menucontext.topgroupName=this.commonService.getTrimmenuStrWithSpace(topgroupname);
      this.menuService.setMenuContext(menucontext);
    } else {
    
      this.breadcrumsService.seterrorbreadcrums("Page not found");
    }

  }

  
 
}
