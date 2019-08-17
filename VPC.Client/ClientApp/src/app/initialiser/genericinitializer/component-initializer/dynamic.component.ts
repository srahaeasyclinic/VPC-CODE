import {
  Component,
  OnInit,
  ViewChild,
  ViewContainerRef,
  ComponentFactoryResolver,
  ComponentRef,
  ComponentFactory,
  OnDestroy,
  OnChanges,
  SimpleChanges
} from '@angular/core';
  
import { NewMenuItem } from '../../../model/menuItem';
import { MenuService, MenuType } from '../../../services/menu.service';
import { Router, ActivatedRoute, NavigationStart,NavigationEnd, NavigationError,UrlTree, PRIMARY_OUTLET, UrlSegment,Event } from '@angular/router';
import { filter } from 'rxjs/operators';
import { CommonService } from 'src/app/services/common.service';

//All component which will init dynamically;
import { GeneralUiNewComponent } from '../../../general/new/GeneralUiNew.component';
import { GeneralUiEditComponent } from '../../../general/edit/GeneralUiEdit.component';
import { GeneralUiPreviewComponent } from '../../../general/preview/GeneralUiPreview.component';
import { GeneralUiDisplayComponent } from "../../../general-ui-display/general-ui-display.component";

import { PicklistListComponent } from '../../../picklist-ui/picklist-list/picklist-list.component';
import { PicklistNewComponent } from '../../../picklist-ui/picklist-new/picklist-new.component';
import { PicklistEditComponent } from '../../../picklist-ui/picklist-edit/picklist-edit.component';
import { PicklistPreviewComponent } from '../../../picklist-ui/picklist-preview/picklist-preview.component';

import { MenuItemComponent } from '../../../menu-item/menu-item.component';
import { SubscriptionComponent } from '../../../subscription/subscription.component';
import { RoleComponent } from '../../../role/role.component';
import { CommunicationComponent } from '../../../communication/communication.component';
import { ResourceComponent } from '../../../resource/resource.component';
import { BatchTypeComponent } from '../../../batchtype/batchtype.component';
import { CounterComponent } from '../../../counter/counter.component';
import { SubscriptionDetailComponent } from '../../../subscription/subscription-detail.component';
import { RoleDetailComponent } from '../../../role/role-detail.component';
import { PagenotfoundComponent } from '../../../pagenotfound/pagenotfound.component';
import { MenuGroupComponent } from '../../../menu-group/menu-group.component';


@Component({
  selector: 'app-component-initializer',
  template: `<ng-container #loadComponent></ng-container>`,
})
export class ComponentInitializer implements OnInit, OnDestroy, OnChanges {
  
  
  public componentRef: ComponentRef<{}>;
  @ViewChild('loadComponent', { read: ViewContainerRef }) entry: ViewContainerRef;
  private topgroup: string;
  private leftgroup: string;
  private name: string;
  private guid: string;
  private actionMode: string;
  private virtutalGroupName: string;
  private menutype: number;
  private urltree: UrlTree;

  constructor(
    private _menuservice: MenuService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private commonService: CommonService,
    private resolver: ComponentFactoryResolver
  ) {}

  ngOnInit() {
    this.actionMode = "";
    this.virtutalGroupName = "";

     this.processUrl(this.router.url);
    this.processMenuContext();

    this.router.events.pipe(filter(value => value instanceof NavigationStart)).subscribe((event: NavigationStart) => {
      //console.log('NavigationStart '+ event.url);
      this.processUrl(event.url);
      this.processMenuContext();

    });
   
  }

  ngOnChanges(changes: SimpleChanges): void {
      //this.actionMode = "";
    //this.virtutalGroupName = "";

    //this.processUrl(this.router.url);
    //  this.processUrl(this.router.url);

    // this.router.events.pipe(filter(value => value instanceof NavigationStart)).subscribe((value: any) => {
    //   this.processUrl(value.url);
    // });
  }
  processUrl(url: any) {
    this.urltree = this.router.parseUrl(url);

    if (this.urltree.root.numberOfChildren > 0) { 

      let primaryurl = this.urltree.root.children[PRIMARY_OUTLET];

      if (primaryurl!=null)
      {
        const segment: UrlSegment[] = primaryurl.segments;
        //console.log('Param: ', primaryurl);
        if (segment!=undefined && segment != null && segment.length > 3 )
        {
          
          this.topgroup = segment[2].path;
          this.leftgroup = segment[3].path;

          if (segment.length>4)
          {
            this.guid = segment[4].path;
             this.actionMode = segment[4].path;;
          }
          
          //this.setVirtualGroupname();
        }
        
      }

    }
  }
processMenuContext() {
  let menuitem=this._menuservice.getMenuconext();
       
//let menucontex=
    if (menuitem!=null && menuitem!=undefined)
    {
      this.menutype = menuitem.menuType;
      this.name = menuitem.param_name;
     this.destroyComponent();
    if (this.menutype==MenuType.Entity ||this.menutype==MenuType.Picklist)
    {
      this.loadpicklist_Entity();
    }
    if (this.menutype==MenuType.wellkown)
    {
      this.loadWellkown();
    }
   
    }
    
    
  }
  setVirtualGroupname_old()
  {
    let menu = this._menuservice.getVirtualGroupbyname(this.commonService.getTrimmenuStrWithSpace(this.topgroup), this.commonService.getTrimmenuStrWithSpace(this.leftgroup));
    //let menucontex=
   // console.log('virtual_ ',menu);
    if (menu!=null &&menu!=undefined)
    {
      this.virtutalGroupName = this._menuservice.getVirtualGroup(menu.menuTypeId, menu.actionTypeId);
      //this.createComponent();
    }
  }

  loadpicklist_Entity()
  {
    let factory: any;
     if (this.menutype==MenuType.Entity && (this.actionMode=="" || this.actionMode==null)) {
     factory = this.resolver.resolveComponentFactory(GeneralUiDisplayComponent);
      this.componentRef= this.entry.createComponent(factory);
    
    }
    else if ( this.menutype==MenuType.Entity && this.actionMode=="preview") {
      factory = this.resolver.resolveComponentFactory(GeneralUiPreviewComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
     else if ( this.menutype==MenuType.Entity && this.actionMode=="edit") {
      factory = this.resolver.resolveComponentFactory(GeneralUiEditComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
   else if (this.menutype==MenuType.Entity && this.actionMode=="new") {
      factory = this.resolver.resolveComponentFactory(GeneralUiNewComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
      
   else if (this.menutype==MenuType.Picklist && (this.actionMode == "" || this.actionMode == null))
    {
      factory = this.resolver.resolveComponentFactory(PicklistListComponent);
     this.componentRef= this.entry.createComponent(factory);
    } 
    
    else if (this.menutype==MenuType.Picklist && this.actionMode=="preview")
    {
     factory = this.resolver.resolveComponentFactory(PicklistPreviewComponent);
      this.componentRef=this.entry.createComponent(factory);
    } 
     
    else if (this.menutype==MenuType.Picklist && this.actionMode=="edit")
    {
      factory = this.resolver.resolveComponentFactory(PicklistEditComponent);
      this.componentRef=this.entry.createComponent(factory);
    } 
   
    else if (this.menutype==MenuType.Picklist && this.actionMode=="new" && this.actionMode!=null)
    {
     factory = this.resolver.resolveComponentFactory(PicklistNewComponent);
      this.componentRef=this.entry.createComponent(factory);
     } else {
       factory = this.resolver.resolveComponentFactory(PagenotfoundComponent);
      this.componentRef=this.entry.createComponent(factory);
    } 
  }

  loadWellkown() {
    let factory: any;
    if (this.name=="" || this.name.toLowerCase()=="menu-item" ) {
     factory = this.resolver.resolveComponentFactory(MenuItemComponent);
      this.componentRef= this.entry.createComponent(factory);
    
    }
    else if ( this.name.toLowerCase()=="subscriptions" && !this._menuservice.isGUID(this.guid))
    {
      factory = this.resolver.resolveComponentFactory(SubscriptionComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else if ( this.name.toLowerCase()=="roles" && !this._menuservice.isGUID(this.guid))
    {
      factory = this.resolver.resolveComponentFactory(RoleComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
     else if ( this.name.toLowerCase()=="communication")
    {
      factory = this.resolver.resolveComponentFactory(CommunicationComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else if (this.name.toLowerCase()=="batchtypes")
    {
      factory = this.resolver.resolveComponentFactory(BatchTypeComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else if (this.name.toLowerCase()=="resource")
    {
      factory = this.resolver.resolveComponentFactory(ResourceComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else if ( this.name.toLowerCase()=="counter")
    {
      factory = this.resolver.resolveComponentFactory(CounterComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
     else if ( this.name.toLowerCase()=="subscriptions" && this._menuservice.isGUID(this.guid))
    {
      factory = this.resolver.resolveComponentFactory(SubscriptionDetailComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else if ( this.name.toLowerCase()=="roles" && this._menuservice.isGUID(this.guid))
    {
      factory = this.resolver.resolveComponentFactory(RoleDetailComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
     else if ( this.name.toLowerCase()=="menugroup")
    {
      factory = this.resolver.resolveComponentFactory(MenuGroupComponent);
      this.componentRef= this.entry.createComponent(factory);
    }
    else {
       factory = this.resolver.resolveComponentFactory(PagenotfoundComponent);
      this.componentRef=this.entry.createComponent(factory);
    } 

  }

  destroyComponent() {

   if (this.componentRef) {
      this.componentRef.destroy();
      this.componentRef = null;
    }
  }
ngOnDestroy(): void {
    this.destroyComponent();
  }
}
