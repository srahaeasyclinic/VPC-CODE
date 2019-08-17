import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { CommonModule, APP_BASE_HREF } from '@angular/common'
import { SecondaryAppRoutingModule } from './secondaryapp-routing.module';


import { ComponentInitializer } from "../initialiser/genericinitializer/component-initializer/dynamic.component";
import { PicklistListComponent } from "../picklist-ui/picklist-list/picklist-list.component";
import { GeneralUiPreviewComponent } from "../general/preview/GeneralUiPreview.component";
import { PicklistPreviewComponent } from "../picklist-ui/picklist-preview/picklist-preview.component";
import { GeneralUiEditComponent } from "../general/edit/GeneralUiEdit.component";
import { PicklistEditComponent } from "../picklist-ui/picklist-edit/picklist-edit.component";
import { GeneralUiNewComponent } from "../general/new/GeneralUiNew.component";
import { PicklistNewComponent } from "../picklist-ui/picklist-new/picklist-new.component";
import { MenuItemComponent } from "../menu-item/menu-item.component";
import { SubscriptionComponent } from "../subscription/subscription.component";
import { SubscriptionDetailComponent } from "../subscription/subscription-detail.component";
import { RoleComponent } from "../role/role.component";
import { RoleDetailComponent } from "../role/role-detail.component";
import { CommunicationComponent } from "../communication/communication.component";
import { BatchTypeComponent } from "../batchtype/batchtype.component";
import { ResourceComponent } from "../resource/resource.component";
import { CounterComponent } from "../counter/counter.component";
import { PagenotfoundComponent } from "../pagenotfound/pagenotfound.component";

//import { getBaseLocation } from "../utility/setLanguageBaseUrl";
import { BreadcrumbsService } from "../bread-crumb/BreadcrumbsService";
import { MenuService } from "../services/menu.service";
import { LanguageIntialiserComponent } from "../initialiser/languageintialiser.component";
import { RoutelocalizationService } from '../services/routelocalization.service';


@NgModule({
  // bootstrap: [
  //   SecondaryAppComponent
  // ],
  declarations: [
    ComponentInitializer,
    PagenotfoundComponent,
    LanguageIntialiserComponent
    
  ],
  entryComponents: [
    PicklistListComponent,
    GeneralUiPreviewComponent,
    PicklistPreviewComponent,
    GeneralUiEditComponent,
    PicklistEditComponent,
    GeneralUiNewComponent,
    PicklistNewComponent,
    ComponentInitializer,
    MenuItemComponent,
    SubscriptionComponent,
    SubscriptionDetailComponent,
    RoleComponent,
    RoleDetailComponent,
    CommunicationComponent,
    BatchTypeComponent,
    ResourceComponent,
    CounterComponent,
    PagenotfoundComponent,

  ],
  imports: [
    BrowserModule,
    CommonModule,
    SecondaryAppRoutingModule,
    
  ],
  exports: [
    ComponentInitializer,
    PagenotfoundComponent
  ],
  providers: [
    BreadcrumbsService,
    MenuService,
    RoutelocalizationService,
    // {
    //     provide: APP_BASE_HREF,
    //     useFactory: getBaseLocation,
      
    // },
  ]
})

export class SecondaryAppModule { }

