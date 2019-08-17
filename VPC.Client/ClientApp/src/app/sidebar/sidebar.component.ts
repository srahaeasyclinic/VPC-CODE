import { Component, OnInit, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { MenuService } from '../services/menu.service';
import { MenuGroup } from '../model/menuGroup';
import { LoginService } from '../login/login.service';

import { PicklistUiService } from '../picklist-ui/picklist-ui.service';
import { first, filter } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { TosterService } from '../services/toster.service';
import { NewMenuItem } from '../model/menuItem';
import { Router, ActivatedRoute, UrlSegment, PRIMARY_OUTLET, UrlTree, NavigationEnd, NavigationStart } from '@angular/router';
import { GlobalResourceService } from '../global-resource/global-resource.service'
import { Resource } from '../model/resource';
import * as _ from 'lodash';
import { CommonService } from '../services/common.service';
//import { Observable } from 'rxjs';
import { PlatformLocation } from '@angular/common'


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
// outputs: [" topMenuClickEvent:generateSideMenu ", " topMenuNewClickEvent:generateSideMenuNew "]
export class SidebarComponent implements OnInit, OnChanges {

  @Input() menus: NewMenuItem[];
  @Input() currentGroup: NewMenuItem;
  @Input() currentmenuId: any;

  @Output() menuClickEvent: EventEmitter<any> = new EventEmitter();

  public sidebarmenu:NewMenuItem[];

  isLog: boolean;
 isConfigToggle: boolean = true;
  //isMenuopen: boolean = false;
  // isSubMenuopen: boolean = false;
  menuName: string;
  public menuItemName: string = "";
  public currentmenuToggle: any = [];
  // public groupName: string;
  public resource: Resource;
  public currentmenuName: string = "";
  private urltree: UrlTree;
  

  constructor(public menuService: MenuService,
    public authService: LoginService,
    private picklistService: PicklistUiService,
    private toster: TosterService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private commonService: CommonService,
    private backbuttonevent: PlatformLocation,
    private globalResourceService: GlobalResourceService
  ) {
    // this.topMenuClickEvent = new EventEmitter();
    // this.topMenuNewClickEvent = new EventEmitter();
    
  }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    //console.log('menu ngOnInit Called');

    //this.generateSidebarMenuByCurrentGroup();

    // if(this.menuService.getCacheMenu())
    // {
    //   this.menuItemName = this.menuService.getCacheMenu().name;

    //   let subGroupobj = this.menus.find(e => e.id ==this.menuService.getCacheMenu().parentId);
    //   if (subGroupobj!=null)
    //   {
    //      this.currentmenuName=subGroupobj.name;
    //   }
    //   //console.log('this.menuItemName ', this.menuItemName);
    // }


      //console.log('init ng',this.currentGroup);
    // if (this.currentmenuId==null&&this.currentGroup != null && this.currentGroup != undefined && this.currentGroup)
    // {
    //   this.currentmenuName=this.currentGroup.name;
    // }
    // if (this.currentmenuId!=null&&this.currentmenuId!=undefined)
    // {
    //   let subGroupobj = this.menus.find(e => e.id == this.currentmenuId);
    //   if (subGroupobj!=null)
    //   {
    //      this.currentmenuName=this.menus.find(e => e.id == subGroupobj.parentId).name;
    //   }
     
    // }
    
    //console.log(this.router);

    //console.log('this.sidebarmenu ', JSON.stringify(this.sidebarmenu));
    //console.log('this.currentGroup ', JSON.stringify(this.currentGroup));
  }

  ngOnChanges(changes: SimpleChanges) {
    // changes.prop contains the old and the new value...
    
    this.generateSidebarMenuByCurrentGroup();

    if(this.menuService.getCacheMenu())
    {
      this.menuItemName = this.menuService.getCacheMenu().name;

      let subGroupobj = this.menus.find(e => e.id ==this.menuService.getCacheMenu().parentId);
      if (subGroupobj!=null)
      {
         this.currentmenuName=subGroupobj.name;
      }
      //console.log('this.menuItemName ', this.menuItemName);
    }

      // for (let propName in changes) {
   // console.log('sidebar ',this.router.url);
    //this.processBackButtonUrl();
        // this.backbuttonevent.onPopState(() => {
        //  this.router.events.pipe(filter(value => value instanceof NavigationStart)).subscribe((event: NavigationStart) => {
        //   //console.log('NavigationStart '+ event.url);
        //    this.processBackButtonUrl(event.url);
        //      console.log('pressed back!');
        //      this.generateSidebarMenuByCurrentGroup();

        // });
        // });
     
    // let chng = changes['currentGroup'];
    // if (chng != undefined && chng != null) {
    //   //this.processBackButtonUrl();
     
    //   let cur = chng.currentValue;
    //     let prev = chng.previousValue;
    //     if (cur != undefined && prev != undefined && cur.name != undefined && prev.name != undefined && cur.name != prev.name) {
    //       this.currentGroup = cur;
    //       this.currentmenuName = cur.name;
    //       if (this.menus.find(d => d.parentId == cur.id)) {
    //         let menuobj = _.sortBy(this.menus.filter(d => d.parentId == cur.id), 'sortItem');

    //         //  console.log('result '+ JSON.stringify(menuobj));

    //         this.menuItemName = menuobj[0].name;
    //       }
      
       
    // }
        
    // }
     
  
    
    
    // console.log('ngOnChanges');
  }
  configToggle() {

    this.isConfigToggle = !this.isConfigToggle;
  }

 
   menuToggle(items) {
  
    this.currentmenuName=items.name;
    
  }

  childMenuToggle() {
    //this.isSubMenuopen = !this.isSubMenuopen;
  }

  navigateUrl(item, groupname) {
   // this.currentmenuName = groupname;
    this.menuClickEvent.emit(item);
    this.menuItemName = item.name;
    item.groupName = groupname;
    this.setcurrentgroup(item);
  }
  onLogout() {
    this.authService.logout();
  }

  generateResourceName(word)
 {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
}

  generateSidebarMenuByCurrentGroup()
  {
    if (this.menus != null && this.menus != undefined &&this.currentGroup!=null && this.currentGroup!=undefined)
    {
      this.sidebarmenu = [];
      let menuitems = this.menus.filter(e => e.parentId == this.currentGroup.parentId && e.isMenuGroup==true);
      this.sidebarmenu=_.sortBy(menuitems,'sortItem');
      
      this.sidebarmenu.forEach(item => {
        item.subGroup = [];
        item.subGroup=_.sortBy(this.menus.filter(e => e.parentId == item.id && e.isMenuGroup!=true),'sortItem');;

      });
    
    }
   
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  setcurrentgroup(item:NewMenuItem)
  {
    this.currentGroup= this.menus.find(e=>e.id==item.parentId);
  }
  processBackButtonUrl(url:string) {

    let menucontextname = this.menuService.getMenuconext();

    this.urltree = this.router.parseUrl(url);

    if (this.urltree.root.numberOfChildren > 0) { 

      let primaryurl = this.urltree.root.children[PRIMARY_OUTLET];

      if (primaryurl!=null &&menucontextname!=null)
      {
        const segment: UrlSegment[] = primaryurl.segments;
        //console.log('Param: ', primaryurl);
        if (segment!=undefined && segment != null && segment.length > 1)
        {
          let topgroup = this.commonService.getTrimmenuStrWithSpace(segment[0].path);// (this.commonService.getTrimmenuStrWithSpace(segment[0].path).toLowerCase()=="configuration-manager")?this.commonService.getTrimmenuStrWithSpace(segment[1].path):;
          let subgroup = this.commonService.getTrimmenuStrWithSpace(segment[1].path);//(this.commonService.getTrimmenuStrWithSpace(segment[0].path).toLowerCase()=="configuration-manager")?this.commonService.getTrimmenuStrWithSpace(segment[2].path):this.commonService.getTrimmenuStrWithSpace(segment[1].path);

          if (subgroup.toLowerCase()!=menucontextname.leftgroupName.toLowerCase())
          {
            let topgroupmenu = this.menus.find(w => w.name.toLowerCase() == topgroup.toLowerCase());
            if (topgroupmenu!=undefined && topgroupmenu!=null) {
              let currentmenu = this.menus.find(w => w.name.toLowerCase() == subgroup.toLowerCase() && w.parentId == topgroupmenu.id && w.groupId==topgroupmenu.groupId);
              this.currentGroup = (currentmenu != undefined && currentmenu != null) ? currentmenu : this.currentGroup;
              this.menuService.setCacheMenu(null);
              this.menuService.setCacheGroup(this.currentGroup);
            }
          }
        }
        
      }

    }
  }
}