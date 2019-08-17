import { Component, OnInit, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { MenuService } from '../services/menu.service';
import { MenuGroup } from '../model/menuGroup';
import { LoginService } from '../login/login.service';
import{Router} from '@angular/router'
 
import { PicklistUiService } from '../picklist-ui/picklist-ui.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { TosterService } from '../services/toster.service';
import { NewMenuItem } from '../model/menuItem';
import * as _ from 'lodash';
import { UserInfoService } from '../services/userInfo.service';
import { GlobalResourceService } from '../global-resource/global-resource.service'
import { Resource } from '../model/resource';
import { RoutelocalizationService } from '../services/routelocalization.service';
//import { Observable } from 'rxjs';
//import { MenuFilterPipe} from '../utility/menuFilter'

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css'],
  // outputs: [" topMenuClickEvent:generateSideMenu ", " topMenuNewClickEvent:generateSideMenuNew "],
})
export class TopMenuComponent implements OnInit,OnChanges {

  @Input() menus: NewMenuItem[];
  @Input() onlyLogin: boolean;
  @Output() groupClickEvent: EventEmitter<any> = new EventEmitter();
  @Output() menuClickEvent:EventEmitter<any> = new EventEmitter();
  public groups: NewMenuItem[] = [];
  public groupMenu: NewMenuItem[] = [];
  public subgroups: any = [];
  public firstElement: NewMenuItem;


  public topMenuClickEvent: EventEmitter<any>;
  public topMenuNewClickEvent: EventEmitter<any>;
  public topMenu: Array<MenuGroup> = [];
  public topMenuNew: Array<MenuGroup> = [];
  //isLoggedIn$: Observable<boolean>;
  //isLog:boolean;
  private layoutType: number = 3;
  public defaultLayout: LayoutModel = new LayoutModel();
  // private orderBy: string = '';
  public sort: SortDescriptor[];
  public selectedFields: string = '';
  public results: any;
  public totalRecords: number = 0;
  public pageindex: number = 1;
  // private pageSize: number = 10;
  private canUserChangePassword = true;
  username: any;
  public resource: Resource;

  constructor(public localization:RoutelocalizationService,public menuService: MenuService, public authService: LoginService, private picklistService: PicklistUiService,
    private toster: TosterService, private userinfoService: UserInfoService,private globalResourceService: GlobalResourceService,private route:Router) {
    this.topMenuClickEvent = new EventEmitter();
    this.topMenuNewClickEvent = new EventEmitter();
  }

  ngOnInit() {
    
   // console.log('this.groups ',JSON.stringify(this.groups));
    this.resource = this.globalResourceService.getGlobalResources();
    
    //console.log('this.groups ', JSON.stringify(this.groups));
    //this.setSubgroup();
    
    //this.initTopmenu();
    //  console.log('this.subgroups ', JSON.stringify(this.subgroups));
    // this.menuService.getAllMenu().subscribe(result => {
    //   if (result) {

    //     this.topMenu = result;
    //     console.log("result", this.topMenu);
    //   }
    // });

  }
  ngOnChanges(changes: SimpleChanges) { 
    this.groups = _.uniqBy(this.menus, 'groupId');
    this.setData();
    this.LoadTopmenu();
    this.groupMenu = _.sortBy(this.groupMenu, 'groupIdSort');
    //console.log('new menus',this.groupMenu);
  }
  setData() {
    
    if (this.userinfoService.checkPasswordChangeAccess) {
      this.canUserChangePassword = this.userinfoService.checkPasswordChangeAccess
    } else {
      this.menuService.CheckPasswordChangeAccess().subscribe(x => {
        
        this.userinfoService.checkPasswordChangeAccess = x
        this.canUserChangePassword = this.userinfoService.checkPasswordChangeAccess
      }, error => console.log("TopMenuerror", error))
    }


    if (this.userinfoService.username) {
      this.username = this.userinfoService.username
    } else {
      this.menuService.getUserName().subscribe(x => {
        
        this.userinfoService.username = x.username
        this.username = this.userinfoService.username
      }, error => console.log("Topmenuerror", error))
    }

  }
  topMenuClick(group): void {
    this.getFirstelementfromGroup(group);
    this.groupClickEvent.emit(this.firstElement);
  }
  topSubMenuClick(subgroups):void {
    this.menuClickEvent.emit(subgroups);
}
  // topMenuNewClick(name): void {

  //   this.topMenuNewClickEvent.emit({ groupName: name });

  // }
// setSubgroup(groupname:string)
//   {
//     let suggroup = this.menus.filter(d => d.groupName == groupname && d.parentId == '00000000-0000-0000-0000-000000000000');

//     return _.sortBy(suggroup,'sortItem')
//   }
  setSubgroup(groupid:string)
  {
    //debugger;
    if (this.groups == undefined && this.groups == null) return null;
    let responseObj: NewMenuItem[]=[];

    let maingroup = this.menus.filter(d => d.groupId == groupid && d.isMenuGroup==true && d.parentId == '00000000-0000-0000-0000-000000000000');
    
    maingroup.forEach(subel => {

      let subgroup=this.menus.find(e => e.parentId == subel.id && e.isMenuGroup == true);
      if (subgroup!=null && subgroup!=undefined && this.menus.find(e=>e.parentId==subgroup.id && e.isMenuGroup!=true))
      {
        responseObj.push(subel);
      }

    });    

    //console.log('sss ',responseObj);
  
    return _.sortBy(responseObj, 'sortItem')
    
  }

  //////////////////////////// this is not using due to irregular behavior/////////////////////////////////////////////////
  LoadTopmenu()
  {
    this.groupMenu = [];
    if (this.groups == undefined && this.groups == null) return null;

    this.groups.forEach(element => {

      let menus = new NewMenuItem();
     
      menus.id = element.id;
      menus.name = element.name;
      menus.layoutId = element.layoutId;
      menus.menuIcon = element.menuIcon;
      menus.menuTypeId = element.menuTypeId;
      menus.menuTypeName = element.menuTypeName;
      menus.menucode = element.menucode;
      menus.parentId = element.parentId;
      menus.referenceEntityId = element.referenceEntityId;
      menus.sortItem = element.sortItem;
    
      menus.groupId = element.groupId;
      menus.groupName = element.groupName;
      menus.groupIdSort = element.groupIdSort;

      menus.actionTypeId = element.actionTypeId;
      menus.actionTypeName = element.actionTypeName;
      menus.wellKnownLink = element.wellKnownLink;

      menus.subGroup = this.setSubgroup(element.groupId)

      // let subgroup = this.menus.filter(d => d.groupName == element.groupName && d.parentId == '00000000-0000-0000-0000-000000000000');

      //  subgroup.forEach(subelement => {
      //     if (this.menus.find(d => d.groupName == element.groupName && d.parentId==subelement.id))
      //     {
      //       menus.subGroup.push(subelement);
      //     }
      //  });
      
      this.groupMenu.push(menus);
    });
    
    //console.log('new menus',this.groupMenu);
  }

  onLogout() {
    this.authService.logout();
  }

  // private getDefaultLayout(entityName: string): void {
  //   this.picklistService.getDefaultLayout(entityName, this.layoutType, 0)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.defaultLayout = data;
  //           if (this.defaultLayout) {

  // //             //generate the default orderby
  // //             if (this.defaultLayout.listLayoutDetails) {
  // //               if (this.defaultLayout.listLayoutDetails.defaultSortOrder) {

  // //                 this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
  // //                 //{dir: "asc", field: "text"}
  // //                 //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
  // //                 if (!this.sort)
  // //                   this.sort = [];

  // //                 this.sort.length = 0;
  // //                 this.sort.push({ dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
  // //               }
  // //             }
  // //             this.generateListlayout(this.defaultLayout, entityName);
  // //           }
  // //         }
  // //       },
  // //       error => {
  // //         console.log(error);
  // //       });
  // // }
  onChangePass() {
    this.authService.changepassafterlogin();
  }
  // private generateListlayout(layout: LayoutModel, entityName: string): void {

  //   let isvalid: boolean = true;

  //   let filters: string = '';

  //   let maxResult: number;

  //   if (layout.listLayoutDetails) {

  //     maxResult = layout.listLayoutDetails.maxResult;

  //     if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
  //       layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
  //         return a.sequence - b.sequence;
  //       });
  //       this.selectedFields = '';
  //       layout.listLayoutDetails.fields.forEach((item, index) => {
  //         if (!this.selectedFields) {
  //           this.selectedFields = item.name;
  //         }
  //         else {
  //           this.selectedFields += ',' + item.name;
  //         }
  //       });

  //     } else {
  //       isvalid = false;
  //       this.toster.showWarning('No fields found !');
  //     }

  //     if (layout.listLayoutDetails.searchProperties && layout.listLayoutDetails.searchProperties.length > 0) {
  //       layout.listLayoutDetails.searchProperties.forEach(element => {
  //         element.properties.forEach(prop => {
  //           if (prop.value != null) {
  //             filters += prop.name + ',' + prop.value + '|';
  //           }
  //         });
  //       });

  //   if (filters != "") {
  //     filters = filters.substring(0, filters.length - 1);
  //   }
  // }

  //     if (isvalid) {
  //       this.results = [];
  //       this.totalRecords = 0;
  //       //this.gridData = this.results;

  //       this.picklistService.getPicklistValues(entityName, this.selectedFields, filters, this.pageindex, this.pageSize, maxResult, this.orderBy, '')

  //         .pipe(first())
  //         .subscribe(
  //           data => {
  //             if (data && data) {
  //               this.results = data.result;

  //below values are requred for kendo grid dynamic paging
  // this.totalRecords = data.totalRow;
  // //this.gridDataResult = { data: this.results, total: this.totalRecords }
  // //this.gridData = this.results;        



  // this.results.forEach((item) => {
  //   var menu = new MenuGroup();
  //   menu.group = item.text;
  //   menu.icon = 'fa fa-user';

  //               this.results.forEach((item) => {
  //                 var menu = new MenuGroup();
  //                 menu.group = item.text;
  //                 menu.icon = 'fa fa-user';

  //                 this.topMenuNew.push(menu);
  //               });

  //             }
  //           },
  //           error => {
  //             console.log(error);
  //           });
  //     }
  //   } else {
  //     //No layout found
  //     this.results = [];
  //     this.results.length = 0;
  //     this.totalRecords = 0;
  //     // this.gridDataResult = { data: this.results, total: this.totalRecords }
  //     // this.gridData = this.gridDataResult;
  //   }
  // }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  logoClick(){
    this.route.navigate([this.localization.getDefaultUrl()]);

  }
  getFirstelementfromGroup(group:any)
  {
    if (this.groups == undefined && this.groups == null) return null;
    
    let subgroup = this.menus.filter(d => d.parentId == group.id && d.isMenuGroup==true);
    let firystelemnt=_.sortBy(subgroup, 'sortItem');
    if (firystelemnt!=undefined && firystelemnt!=null) {
      this.firstElement=firystelemnt[0];
    } else {
      return this.firstElement=group;
    }
  }
}
