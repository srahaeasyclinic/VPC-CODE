import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatTabsModule } from '@angular/material';
import { CommonModule } from '@angular/common'

//Material module
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatExpansionModule } from '@angular/material/expansion';
//import { AuthGuard } from './auth/auth.guard';
//import { AuthService } from './auth/auth.service';

//SweetAlert2Module 
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

//Kendo grid
import { GridModule, ExcelModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

//multiselect dropdown
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

//// Global component references
import { AppComponent } from './app.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NgHttpLoaderModule } from 'ng-http-loader';

//Angular 6 Toastr Notifications Module
import { ToastrModule } from 'ng6-toastr-notifications';

//Home component
import { httpInterceptor } from './interceptor/httpInterceptor';
import { ErrorInterceptor } from './interceptor/errorInterceptor';
import { AuthorizationCheck } from './interceptor/authorizationCheck'

//Metadata component references
import { MetadataComponent } from './meta-data/metadata.component';
import { MetadataDetailComponent } from './meta-data/metadatadetail.component';
import { FieldsComponent } from './meta-data/fields/fields.component';
import { ModalLayoutComponent } from './meta-data/layout/modal-layout/modal-layout.component';
import { ModalMessageComponent } from './meta-data/layout/modal-message/modal-message.component';
import { ModalInnerStepComponent } from './meta-data/workflow/modal-innerstep/modal-innerstep.component';
import { ModalWorkflowprocessComponent } from './meta-data/workflow/modal-workflowprocess/modal-workflowprocess.component';
import { ModalOperationProcessComponent } from './meta-data/workflow/modal-operationprocess/modal-operationprocess.component';
import { LayoutResolver } from './meta-data/layout/layout-resolver.service';

// dynamic form builder  
import { TreeModule } from './dynamic-form-builder/tree.module';
import { CheckboxSettingComponent } from "./dynamic-form-builder/settings/checkbox-setting.component";
import { DropdownSettingComponent } from "./dynamic-form-builder/settings/dropdown-setting.component";
import { FileSettingComponent } from "./dynamic-form-builder/settings/file-setting.component";

//------ Common Grid Data Module This Grid will show all data, currently users'
//import { CommonGridDataModule } from './common-grid-data/common-grid-data.module';

// //RichTextbox module
// import { AngularEditorModule } from '@kolkov/angular-editor';
// //end

import { RichtextboxSettingComponent } from "./dynamic-form-builder/settings/richtextbox-setting.component";
import { RadioSettingComponent } from "./dynamic-form-builder/settings/radio-setting.component";
import { SectionSettingComponent } from "./dynamic-form-builder/settings/section-setting.component";
import { TabSettingComponent } from "./dynamic-form-builder/settings/tab-setting.component";
import { TextboxSettingComponent } from "./dynamic-form-builder/settings/textbox-setting.component";
import { AddSettingComponent } from "./dynamic-form-builder/settings/add-setting.component";
import { CustomSettingComponent } from "./dynamic-form-builder/settings/custom-setting.component";
import { DetailEntityComponent } from "./dynamic-form-builder/settings/custom/detailentity-setting.component";
import { MultiSelectDropdownSettingComponent } from "./dynamic-form-builder/settings/multiselectdropdown-setting.component";

import { RequiredValidatorComponent } from "./dynamic-form-builder/validator/requried-validator.component";
import { LengthValidatorComponent } from "./dynamic-form-builder/validator/length-validator.component";
import { RangeValidatorComponent } from "./dynamic-form-builder/validator/range-validator.component";
import { EmailFormatValidatorComponent } from './dynamic-form-builder/validator/emailformat-validator.component'
import { Data } from './services/storage.data';

//Menu Item
import { MenuItemComponent } from './menu-item/menu-item.component';

import { GeneralUiDisplayComponent } from "./general-ui-display/general-ui-display.component";
import { GeneralUiListModule } from "./general/list/general-ui-list.module";
import { UtilityTopBarModule } from './utility-top-bar/utility-top-bar.module';
import { RightSearchBarModule } from './right-search-bar/right-search-bar.module';
import { SchedulerComponent } from "./batchtype/scheduler.component";
import { AppRoutingModule, routableComponents } from './app-routing.module';
import { MetadataModule } from './meta-data/metadata.module';
import { LayoutDetailListModule } from './meta-data/layout/layout-detail-list/layout-detail-list.module';
import { RuleUpsertComponent } from "./meta-data/rule/ruleupsert.component";
import { UniquePipe } from "./filter/UniquePipe";
import { UserInfoService } from "./services/userInfo.service";

// Resource Item
import{ResourceModule} from './resource/resource.module'
//Counter Component
import{CounterComponent} from './counter/counter.component';

import { BaseIntialiser } from "./initialiser/baseintialiser.component";
//import { LayoutTopBarComponent } from "./meta-data/layout/layout-top-bar/layout-top-bar.component";
//Dynamic Menu
import{BreadcrumbComponent} from './bread-crumb/breadcrumb.component';
import{LanguageComponent} from './global-resource/language/language.component';
import { SecondaryAppModule } from "./secondaryModule/secondary-app.module";
import { LinkSettingComponent } from "./dynamic-form-builder/settings/link-setting.component";
import { CalendarSettingComponent } from "./dynamic-form-builder/settings/calendar-setting.component";

import { QuickAddSettingComponent } from "./dynamic-form-builder/settings/custom/quickadd-setting.component";
import { MetaDataConfigurer } from "./meta-data/layout/layout-detail-list/metadataconfigurer/metadataconfigurer.component";
import { SessionExpiryPopupComponent } from "./sessionexpirypopup/sessionexpirypopup.component";
import { MenuGroupComponent } from "./menu-group/menu-group.component";
import { DeletepopupComponent } from "./deletepopup/deletepopup.component";
//import { MenuGroupComponent } from './top-menu/menu-group.component';

//import { MenuFilterPipe } from './utility/menuFilter';

@NgModule({
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    ModalLayoutComponent,
    ModalMessageComponent,
    ModalInnerStepComponent,
    ModalWorkflowprocessComponent,
    ModalOperationProcessComponent,
    CheckboxSettingComponent,
    RichtextboxSettingComponent,
    DropdownSettingComponent,
    FileSettingComponent,
    MultiSelectDropdownSettingComponent,
    RadioSettingComponent,
    SectionSettingComponent,
    TabSettingComponent,
    TextboxSettingComponent,
	LinkSettingComponent,			 
    AddSettingComponent,
    RequiredValidatorComponent,
    LengthValidatorComponent,
    RangeValidatorComponent,
    EmailFormatValidatorComponent,
    CustomSettingComponent,
    DetailEntityComponent,
    GeneralUiDisplayComponent,
    MenuItemComponent,
    MetadataComponent,
    MetadataDetailComponent,
    FieldsComponent,
    SchedulerComponent,
    RuleUpsertComponent,
	QuickAddSettingComponent,
    CalendarSettingComponent,						 
    LinkSettingComponent,	
  MenuGroupComponent

  ],
  imports: [
    BrowserModule,
    DropDownsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    MatTabsModule,
    CommonModule,
    //Ng2OrderModule,
    GridModule,
    ExcelModule,
    BrowserAnimationsModule,
    TreeModule,
    GeneralUiListModule,
    NgMultiSelectDropDownModule,
    //Ng2SearchPipeModule,

    //Material module
    MatToolbarModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatInputModule,
    MatFormFieldModule,
    MatExpansionModule,
    MetadataModule,
    LayoutDetailListModule,
    ResourceModule,
    // //richtextbox module
    // AngularEditorModule,
    // Module for Grid Data
    //  CommonGridDataModule,
    //GeneralUiListModule,

    UtilityTopBarModule,
    RightSearchBarModule,

    AppRoutingModule,

    SweetAlert2Module.forRoot({
      //=> Or provide default SweetAlert2-native options
      //   (here, we make Swal more Bootstrap-friendly)
      buttonsStyling: false,
      customClass: 'modal-content',
      confirmButtonClass: 'btn btn-primary',
      cancelButtonClass: 'btn'
    }),
    ToastrModule.forRoot(),
    NgHttpLoaderModule.forRoot(),
    SecondaryAppModule
  ],

  exports: [
    MenuItemComponent,
    MetadataComponent,
    MetadataDetailComponent,
    FieldsComponent
  ],

  declarations: [
    AppComponent,
    routableComponents,
    UniquePipe,
    BreadcrumbComponent,
    CounterComponent,
    LanguageComponent,
    MetaDataConfigurer,
    SessionExpiryPopupComponent,
    DeletepopupComponent,
    //MenuFilterPipe
    //MenuGroupComponent
  ],

  providers: [
    LayoutResolver,
    SidebarComponent,
    Data,
    AuthorizationCheck,
    UserInfoService,

    //LoginService,
    { provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ]
})

export class AppModule { }

