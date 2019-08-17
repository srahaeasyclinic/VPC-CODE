import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import { MenuGroup } from '../model/menuGroup'
import { PicklistUiService } from '../picklist-ui/picklist-ui.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { TosterService } from '../services/toster.service';
import { HttpHeaders } from '@angular/common/http';
import { CommonService } from './common.service';
import * as _ from 'lodash';
import { BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';


@Injectable({
  providedIn: 'root'
})

export class MenuService {



  private menuItem: string = '/api/menu';
  private allmenuItem: string = '/api/menu/all';
  private layout: string = "/api/meta-data";
  private pickList: string = "/api/picklists";
  query: string = '?pageIndex=1' + '&pageSize=100';


  private items: Array<MenuItem>;
  private itemsNew: Array<NewMenuItem>;
  private groups: Array<MenuGroup>;
  private layoutType: number = 3;
  public defaultLayout: LayoutModel = new LayoutModel();
  private orderBy: string = '';
  public sort: SortDescriptor[];
  public selectedFields: string = '';
  public results: any;
  public totalRecords: number = 0;
  public pageindex: number = 1;
  private pageSize: number = 10;


  constructor(private commonService: CommonService,
    private picklistService: PicklistUiService,
    private toster: TosterService,
    private http: HttpClient,
    private breadcrumsService: BreadcrumbsService
  ) { }

  

  // public getMenuItems(groupName: string): Array<MenuItem> {
  //   this.items = this.getMenu();
  //   this.items = this.items.filter(menu => menu.group == groupName);
  //   return this.items;
  // }

  public getMenuItemsNew(groupName: string): Observable<any> {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + this.query + '&groupName=' + groupName;
    return this.http.get<NewMenuItem[]>(menuItemUrl);
  }

  // public getTotalMenuItems(): Array<MenuItem> {
  //   return this.getMenu();
  // }

  // public getTopMenu(): Array<MenuGroup> {
  //   return this.getMenuGroup();
  // }




  public getMenuList(): Observable<any> {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + this.query;
    return this.http.get<NewMenuItem[]>(menuItemUrl);
  }

  public deleteMenu(id): Observable<any> {
    // const httpOptions = {
    //   headers: new HttpHeaders({
    //     'Content-Type': 'application/json',
    //     'Access-Control-Allow-Origin': '*'
    //   })
    // };

    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + '/' + id;
    return this.http.delete(menuItemUrl);
  }

  public getLayoutList(name: string, menuType: number): Observable<any> {
    var menuItemUrl = "";
    if (menuType === 1) {
      menuItemUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts?type=List';
    }
    else if (menuType === 2) {
      menuItemUrl = `${environment.apiUrl}` + this.pickList + '/' + name + '/layouts?type=List';
    }

    return this.http.get<any[]>(menuItemUrl);
  }

  public saveMenuItem(name: string, menuItemModel: NewMenuItem): Observable<any> {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem;
    //console.log('menu '+ JSON.stringify(menuItemModel));
    return this.http.post(menuItemUrl, menuItemModel);
  }

  public updateMenuItem(name: string, id: string, menuItemModel: NewMenuItem): Observable<any> {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + '/' + id;
      //console.log('menu '+ JSON.stringify(menuItemModel));
    return this.http.put(menuItemUrl, menuItemModel);
  }

  public getMenuById(id) {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + '/' + id;
    return this.http.get<NewMenuItem>(menuItemUrl);
  }


  //////////////////////////////////////////////////////////////////
  private cacheMenus: Array<NewMenuItem>;
  private cacheGroup: NewMenuItem;
  private cacheMenu: NewMenuItem;

  

  public getAllMenu(): Observable<any> {
    var picklistsUrl = `${environment.apiUrl}` + this.menuItem + this.query;
    return this.http.get<NewMenuItem[]>(picklistsUrl);
  }

  public getAllMenuBytenant(): Observable<any> {
    var picklistsUrl = `${environment.apiUrl}` + this.allmenuItem;
    return this.http.get<NewMenuItem[]>(picklistsUrl);
  }


  setCacheMenu(menu: NewMenuItem): void {
    //console.log('setCacheMenu Called');
    this.cacheMenu = menu;
  }
  getCacheMenu(): NewMenuItem {
    return this.cacheMenu;
  }
  public setCacheMenus(menus: Array<NewMenuItem>): void {
    this.cacheMenus = menus;
  }
  public getCacheMenus(): Array<NewMenuItem> {
    return this.cacheMenus;
  }

  setCacheGroup(currentGroup: NewMenuItem) {
    this.cacheGroup = currentGroup;
  }
  getCacheGroup(): NewMenuItem {
    return this.cacheGroup;
  }

  clearAllCacheItem() {
    this.cacheMenus = null;
    this.cacheGroup = null;
    this.cacheMenu = null;
    this.breadcrumsService.breadcrumb = null;
  }

  CheckPasswordChangeAccess(): any {
    var routeurl = "api/user";
    var routeUrl = `${environment.apiUrl}` + '/' + routeurl + '/checkpasswordchangeaccess';
    // console.log('routeUrl', routeUrl)
    return this.http.get<any>(routeUrl);
  }

  getUserName() {  
    var routeurl = "api/user";
    var routeUrl = `${environment.apiUrl}` + '/' + routeurl + '/username';
    // console.log('routeUrl', routeUrl)
    return this.http.get<any>(routeUrl);
  }

  clearAllcache():void
  {
    this.clearAllCacheItem();

    var picklistsUrl = `${environment.apiUrl}` + '/api/menu/clear-cache';
    this.http.get<NewMenuItem[]>(picklistsUrl).pipe(first())
      .subscribe(ele => {
        this.clearAllCacheItem();
      },
        error => {
          console.log(JSON.stringify(error));
        });

  }
    //----------------------------------------DYNAMIC MENU ---------------------------------\\\
  public virtualGroup: Array<string> = ["", "object-manager", "picklist-manager", "metadata#picklistmetadata", "configuration-manager"];
  public getVirtualGroup(menuTypeId: number, actionTypeId: number = null) {
    var virtualGroupStr = this.virtualGroup[menuTypeId];
    if (virtualGroupStr && actionTypeId != null && menuTypeId == 3) {
      var arr = virtualGroupStr.split("#");
      return arr[actionTypeId - 2];
    }
    return virtualGroupStr;
  }
  
  public getVirtualGroupbyname(topmenuGroupname:string,leftGroupname:string):NewMenuItem {
    let menuObj: NewMenuItem;
   
    if ( this.cacheMenus == null|| this.cacheMenus==undefined) {
      this.getAllMenu().subscribe(result => {
      if (result) {
         this.cacheMenus = result;
         return this.getmenu(topmenuGroupname, leftGroupname);
      }
     });
    }
    else {
      return this.getmenu(topmenuGroupname, leftGroupname);
    }

    return menuObj;
  }

  private getmenu(topmenuGroupname:string,leftGroupname:string):NewMenuItem
  {
    let parentobj = this.cacheMenus.find(w => w.name.toLocaleLowerCase() == topmenuGroupname.toLocaleLowerCase());
    if (parentobj != undefined && parentobj != null)
    {
      return this.cacheMenus.find(w => w.name.toLocaleLowerCase() == leftGroupname.toLocaleLowerCase() && w.parentId == parentobj.id);
    }
    
    return null;
  }

setMenuContext(obj: MenuContextObject)
{
  if (obj!=undefined && obj!=null)
    {
          //console.log('SetContext',obj);
    localStorage.setItem('currentmenuobject', JSON.stringify(obj)); 
    this.breadcrumsService.setGroupMenuBreadcums(obj);
    }
     
}
 

    // getMenuconext():Observable<MenuContextObject>
    // {
    //     var retrievedObject = localStorage.getItem('currentmenuobject');
    //     if (retrievedObject!=undefined && retrievedObject!=null)
    //     {
    //         return JSON.parse(retrievedObject);
    //     }
    //     return null;
    // }
    getMenuconext():MenuContextObject
    {
        var retrievedObject = localStorage.getItem('currentmenuobject');
        if (retrievedObject!=undefined && retrievedObject!=null)
        {
          //console.log("menuContext",JSON.parse(retrievedObject));
            return JSON.parse(retrievedObject);
        }
      
        return null;
    }
  
isGUID(expression:string)
{
    if (expression != null)
    {
        let pattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
        return pattern.test(expression);
    }
    return false;
}
  
  
  
  
}

////////// dynamic menu ////////////////////////////
export class MenuContextObject{
  topmainGroup: string;
  topmainGroupId: string;
  topgroupName: string;
  topgroupId: string;
  leftgroupName: string;
  leftgroupId: string;
  param_name: string;
  menuType: number;
}

export enum MenuType{

    Entity = 1,
    
    Picklist = 2,

    Context = 3,

    wellkown = 4
    
}

export class editableColumnname
{
  name: string;
  columnname: string;
}

