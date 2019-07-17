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
@Injectable({
  providedIn: 'root'
})

export class MenuService {



  private menuItem: string = '/api/menu';
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


  constructor(private picklistService: PicklistUiService, private toster: TosterService, private http: HttpClient) { }

  // getMenu(): Array<MenuItem> {
  //   return [
  //     {
  //       "group": "Metadata",
  //       "name": "Metadata",
  //       "menuType": "Entity",
  //       "referenceEntity": "Metadata",
  //       "path": "/metadata",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Organization",
  //       "name": "User",
  //       "menuType": "Entity",
  //       "referenceEntity": "User",
  //       "path": "/ui/user",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Tenant",
  //       "name": "Tenant",
  //       "menuType": "Entity",
  //       "referenceEntity": "Tenant",
  //       "path": "/ui/tenant",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Organization",
  //       "name": "Roles",
  //       "menuType": "Entity",
  //       "referenceEntity": "Roles",
  //       "path": "/roles",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Organization",
  //       "name": "Organization unit",
  //       "menuType": "Entity",
  //       "referenceEntity": "OrganizationUnit",
  //       "path": "/ui/organizationunit",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Picklist layout",
  //       "name": "Picklist layout",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Picklist layout",
  //       "path": "/picklist",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Country",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Country",
  //       "path": "picklist/ui/country",
  //       "sequence": "1"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Currency",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Currency",
  //       "path": "picklist/ui/currency",
  //       "sequence": "2"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Timezone",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Timezone",
  //       "path": "picklist/ui/timezone",
  //       "sequence": "3"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "City",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "City",
  //       "path": "picklist/ui/city",
  //       "sequence": "4"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Language",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Language",
  //       "path": "picklist/ui/language",
  //       "sequence": "5"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Municipality",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Municipality",
  //       "path": "picklist/ui/municipality",
  //       "sequence": "6"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "State",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "State",
  //       "path": "picklist/ui/state",
  //       "sequence": "7"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Qualification",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Qualification",
  //       "path": "picklist/ui/qualification",
  //       "sequence": "8"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Unit type",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "UnitType",
  //       "path": "picklist/ui/unittype",
  //       "sequence": "9"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Profession",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "Profession",
  //       "path": "picklist/ui/profession",
  //       "sequence": "10"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Organization Type",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "OrganizationType",
  //       "path": "picklist/ui/organizationtype",
  //       "sequence": "11"
  //     },
  //     {
  //       "group": "Picklist",
  //       "name": "Time Calculation Type",
  //       "menuType": "Infrastructure context",
  //       "referenceEntity": "OrganizationType",
  //       "path": "picklist/ui/timecalculationtype",
  //       "sequence": "12"
  //     },
  //     {
  //       "group": "Subscription",
  //       "name": "Subscription",
  //       "menuType": "Entity",
  //       "referenceEntity": "Subscription",
  //       "path": "/subscriptions",
  //       "sequence": "13"
  //     },
  //   ];
  // }

  // private getMenuGroup(): Array<MenuGroup> {
  //   return [
  //     {
  //       "group": "Metadata",
  //       "sequence": "1",
  //       "icon": "fa fa-users"
  //     },

  //     {
  //       "group": "Organization",
  //       "sequence": "1",
  //       "icon": "fa fa-users"
  //     },
  //     {
  //       "group": "Picklist layout",
  //       "sequence": "2",
  //       "icon": "fa fa-list-ul"
  //     },
  //     {
  //       "group": "Picklist",
  //       "sequence": "3",
  //       "icon": "fa fa-list-ul"
  //     },
  //     {
  //       "group": "Settings",
  //       "sequence": "4",
  //       "icon": "fa fa-cogs"
  //     },
  //     {
  //       "group": "Tenant",
  //       "sequence": "5",
  //       "icon": "fa fa-university"
  //     },
  //     {
  //       "group": "Subscription",
  //       "sequence": "5",
  //       "icon": "fa fa-user"
  //     }
  //   ]
  // }

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
    return this.http.post(menuItemUrl, menuItemModel);
  }

  public updateMenuItem(name: string, id: string, menuItemModel: NewMenuItem): Observable<any> {
    var menuItemUrl = `${environment.apiUrl}` + this.menuItem + '/' + id;
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

}
