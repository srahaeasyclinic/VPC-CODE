import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params, Route, UrlTree, UrlSegmentGroup, UrlSegment, UrlSerializer } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuService } from '../services/menu.service';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import { CommonService } from '../services/common.service';
import{GlobalResourceService} from '../global-resource/global-resource.service'


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
  public currentMenu: NewMenuItem;

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public menuService: MenuService,
    public serializer: UrlSerializer,
	public commonService:CommonService,
    public globalResource:GlobalResourceService,  ) { }

  ngOnInit() {
    var cacheMenus = this.menuService.getCacheMenus();
    if (cacheMenus == null) {
      this.loadMenu();
    } else {
      this.menus = cacheMenus;
      this.currentGroup = this.menuService.getCacheGroup();
    }
    // for resoure initialization
    var cacheResource=this.globalResource.getGlobalResources();
    if(cacheResource==null){
      console.log('base resour call');
      this.globalResource.getResource();
    }
  }

  private loadMenu() {
    this.menuService.getAllMenu().subscribe(result => {
      if (result) {
        this.menus = result;
        this.nevigateGroup(this.menus[0]);
        this.menuService.setCacheMenus(this.menus);
      }
    });
  }
  nevigateGroup(group: any) {
    this.currentGroup = group;
    this.menuService.setCacheMenu(null);
    this.menuService.setCacheGroup(this.currentGroup);
    var groupName = this.commonService.getTrimStr(this.currentGroup.groupName);
    var firstMenu = this.firstElementFromGroup(this.currentGroup.groupName);
    this.currentMenu = firstMenu;
    var menuName = this.getMenuName(firstMenu);
    //console.log('this.currentMenu ', this.currentMenu);
    this.menuService.setCacheMenu(this.currentMenu);
    this.nevigate(groupName, menuName);
  }
  getMenuName(firstMenu: NewMenuItem): string {
    var menuName = "";
    if (this.currentGroup.actionTypeId == 2 || this.currentGroup.actionTypeId == 3) {
      menuName = this.commonService.getTrimStr(firstMenu.name);
    } else {
      menuName = (firstMenu.referenceEntityId != "") ? this.commonService.getTrimStr(firstMenu.referenceEntityId) : this.commonService.getTrimStr(firstMenu.wellKnownLink);
    }
    return menuName;
  }
  firstElementFromGroup(groupName: string): NewMenuItem {
    var menuName: NewMenuItem = null;
    this.menus.forEach(element => {
      if (element.groupName == groupName) {
        menuName = element;
        return;
      }
    });
    return menuName;
  }
  nevigate(groupName: string, menuName: string) {
    if (groupName == "" || menuName == "") {
      throw new Error("group name or menu name not found");
    }
    var url = this.getUrl(groupName, menuName);;
    console.log("url", url);
    this.router.navigate([url]);
  }

  getUrl(groupName: string, menuName: string): string {
    if (this.currentGroup == null) return;
    var objectManager = this.commonService.getVirtualGroup(this.currentGroup.menuTypeId, this.currentMenu.actionTypeId);
    var url = objectManager + "/" + groupName.toLowerCase() + "/" + menuName.toLowerCase();
    return url;
  }

  public nevigateChildren(menu: NewMenuItem) {
    //console.log('menu ', menu);
    this.currentMenu = menu;
    this.menuService.setCacheMenu(menu);
    var groupName = this.commonService.getTrimStr(this.currentGroup.groupName);
    var menuNameUpper = this.getMenuName(menu);
    var menuName = this.commonService.getTrimStr(menuNameUpper).toLocaleLowerCase();
    this.menuItem = menu.name;
    if (menu.menuTypeId == 4) {
      this.router.navigate([menuName], { relativeTo: this.activatedRoute });
    } else {
      if (menu.actionTypeId == 2) {
        this.router.navigate(["entity-designer", groupName, menuName]);
      } else if (menu.actionTypeId == 3) {
        this.router.navigate(["picklist-designer", groupName, menuName]);
      } else {
        this.router.navigate(["../" + menuName], { relativeTo: this.activatedRoute });
      }
    }
    //console.log('this.menuName ', this.menuItem);
  }
}
