import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//// Global component references
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { SidebarComponent } from './sidebar/sidebar.component';

//Angular 6 Toastr Notifications Module

//Home component
import { LoginComponent } from './login/login.component';
import { AuthorizationCheck } from './interceptor/authorizationCheck'

//Metadata component references
import { MetadataComponent } from './meta-data/metadata.component';
import { MetadataDetailComponent } from './meta-data/metadatadetail.component';
import { LayoutComponent } from './meta-data/layout/layout.component';
import { FieldsComponent } from './meta-data/fields/fields.component';
import { RelationsComponent } from './meta-data/relations/relations.component';
import { QueryComponent } from './meta-data/query/query.component';
import { TaskoperationComponent } from './meta-data/taskoperation/taskoperation.component';
import { RuleComponent } from './meta-data/rule/rule.component';


import { ModalLayoutComponent } from './meta-data/layout/modal-layout/modal-layout.component';
import { ModalMessageComponent } from './meta-data/layout/modal-message/modal-message.component';
import { LayoutdetailComponent } from './meta-data/layout/layoutdetail/layoutdetail.component';
import { WorkFlowComponent } from './meta-data/workflow/workflow.component';
import { ModalInnerStepComponent } from './meta-data/workflow/modal-innerstep/modal-innerstep.component';
import { ModalWorkflowprocessComponent } from './meta-data/workflow/modal-workflowprocess/modal-workflowprocess.component';
import { ModalOperationProcessComponent } from './meta-data/workflow/modal-operationprocess/modal-operationprocess.component';
import { LayoutDetailListComponent } from './meta-data/layout/layout-detail-list/layout-detail-list.component';
import { LayoutPreviewComponent } from './meta-data/layout/layoutpreview/layoutpreview.component';
import { LayoutfieldsComponent } from './meta-data/layout/layout-detail-list/layoutfields/layoutfields.component';
import { LayouttoolbarComponent } from './meta-data/layout/layout-detail-list/layouttoolbar/layouttoolbar.component';
import { LayoutactionComponent } from "./meta-data/layout/layout-detail-list/layoutaction/layoutaction.component";
import { FreetextSearchComponent } from "./meta-data/layout/layout-detail-list/freetextsearch/freetextsearch.component";
import { SimpleSearchComponent } from "./meta-data/layout/layout-detail-list/simplesearch/simplesearch.component";
import { AdvanceSearchComponent } from "./meta-data/layout/layout-detail-list/advancesearch/advancesearch.component";
import { ListFreeTextSearchComponent } from './picklist/picklist-layout/list/list-free-text-search/list-free-text-search.component';
import { ListActionComponent } from './picklist/picklist-layout/list/list-action/list-action.component';
import { ListSimpleSearchComponent } from './picklist/picklist-layout/list/list-simple-search/list-simple-search.component';
import { ListToolbarComponent } from './picklist/picklist-layout/list/list-toolbar/list-toolbar.component';
import { ListFieldsComponent } from './picklist/picklist-layout/list/list-fields/list-fields.component';
import { ListAdvanceSearchComponent } from './picklist/picklist-layout/list/list-advance-search/list-advance-search.component';

import { LayoutDetailViewComponent } from './meta-data/layout/layout-detail-view/layout-detail-view.component';
import { LayoutDetailViewFieldsComponent } from './meta-data/layout/layout-detail-view/layout-detail-view-fields/layout-detail-view-fields.component';
import { LayoutDetailViewActionComponent } from './meta-data/layout/layout-detail-view/layout-detail-view-action/layout-detail-view-action.component';
import { LayoutResolver } from './meta-data/layout/layout-resolver.service';

//picklist 
import { PicklistComponent } from './picklist/picklist.component';
import { PicklistDetailComponent } from './picklist/picklistdetail.component';
import { PicklistFieldComponent } from './picklist/picklist-field/picklist-field.component';
import { PicklistLayoutComponent } from './picklist/picklist-layout/picklist-layout.component';
import { PicklistOperationComponent } from './picklist/picklist-operation/picklist-operation.component';
import { PicklistQueryComponent } from './picklist/picklist-query/picklist-query.component';
import { ListComponent } from './picklist/picklist-layout/list/list.component';
import { PicklistLayoutResolverService } from './picklist/picklist-layout/list/picklist-layout-resolver.service';

import { ViewComponent } from './picklist/picklist-layout/view/view.component';
import { ViewFieldsComponent } from './picklist/picklist-layout/view/view-fields/view-fields.component';
import { FormComponent } from './picklist/picklist-layout/form/form.component';
import { ListPreviewComponent } from './picklist/picklist-layout/list/list-preview/list-preview.component';


// dynamic form builder  
import { CheckboxSettingComponent } from "./dynamic-form-builder/settings/checkbox-setting.component";
import { DropdownSettingComponent } from "./dynamic-form-builder/settings/dropdown-setting.component";
import { FileSettingComponent } from "./dynamic-form-builder/settings/file-setting.component";
import { RichtextboxSettingComponent } from "./dynamic-form-builder/settings/richtextbox-setting.component";
import { RadioSettingComponent } from "./dynamic-form-builder/settings/radio-setting.component";
import { SectionSettingComponent } from "./dynamic-form-builder/settings/section-setting.component";
import { TabSettingComponent } from "./dynamic-form-builder/settings/tab-setting.component";
import { TextboxSettingComponent } from "./dynamic-form-builder/settings/textbox-setting.component";
import { LinkSettingComponent } from "./dynamic-form-builder/settings/link-setting.component";
import { CalendarSettingComponent } from './dynamic-form-builder/settings/calendar-setting.component';
import { AddSettingComponent } from "./dynamic-form-builder/settings/add-setting.component";
import { UserComponent } from "./user/user.component";
import { UserCreateComponent } from "./user/user-create.component";
import { UserEditComponent } from "./user/user-edit.component";
import { CustomSettingComponent } from "./dynamic-form-builder/settings/custom-setting.component";
import { DetailEntityComponent } from "./dynamic-form-builder/settings/custom/detailentity-setting.component";
import { MultiSelectDropdownSettingComponent } from "./dynamic-form-builder/settings/multiselectdropdown-setting.component";
import { CountryComponent } from './country/country.component';
import { CountryNewComponent } from './country/country-new/country-new.component';
import { CountryDetailComponent } from './country/country-detail/country-detail.component';
import { RequiredValidatorComponent } from "./dynamic-form-builder/validator/requried-validator.component";
import { LengthValidatorComponent } from "./dynamic-form-builder/validator/length-validator.component";



import { DefaultValidatorComponent } from "./dynamic-form-builder/validator/defaultvalue-validator.component";

import { RangeValidatorComponent } from "./dynamic-form-builder/validator/range-validator.component";
import { PicklistListComponent } from './picklist-ui/picklist-list/picklist-list.component';
import { PicklistNewComponent } from './picklist-ui/picklist-new/picklist-new.component';
import { PicklistEditComponent } from './picklist-ui/picklist-edit/picklist-edit.component';
import { PicklistPreviewComponent } from './picklist-ui/picklist-preview/picklist-preview.component';
import { EmailFormatValidatorComponent } from './dynamic-form-builder/validator/emailformat-validator.component'
import { RoleComponent } from './role/role.component';
import { RoleDetailComponent } from './role/role-detail.component';
import { DynamicGridComponent } from './dynamic-grid/dynamic-grid.component';
import { EntitySecurityComponent } from "./entitysecurity/entitysequrity.component";
import { WorkFlowSecurityComponent } from "./entitysecurity/workflowsequrity.component";
import { RoleSecurityComponent } from "./entitysecurity/rolesequrity.component";

//example
import { CounterComponent } from './counter/counter.component';
import { NotfoundcomponentComponent } from './notfoundcomponent/notfoundcomponent.component';

//filter
import { FilterPipe } from './services/filter.pipe';

import { GeneralUiNewComponent } from './general/new/GeneralUiNew.component';
import { GeneralUiEditComponent } from './general/edit/GeneralUiEdit.component';
import { GeneralUiPreviewComponent } from './general/preview/GeneralUiPreview.component';
import { FunctionSecurityComponent } from "./entitysecurity/functionsequrity.component";

//Menu Item
import { MenuItemComponent } from './menu-item/menu-item.component';

import { GeneralUiDisplayComponent } from "./general-ui-display/general-ui-display.component";

import { SubscriptionComponent } from "./subscription/subscription.component";
import { SubscriptionDetailComponent } from "./subscription/subscription-detail.component";
import { HomeComponent } from './home/home.component';
import { BatchTypeComponent } from "./batchtype/batchtype.component";
import { CommunicationComponent } from './communication/communication.component';
import { SchedulerComponent } from "./batchtype/scheduler.component";
import { ForgotPasswordComponent } from './forgotpassword/forgotpassword.component';
import { ChangePasswordComponent } from './changepassword/changepassword.component';
import { RuleUpsertComponent } from './meta-data/rule/ruleupsert.component';
import { PicklistIntialiser } from './initialiser/picklistintialiser.component';
import { MetadataIntialiser } from './initialiser/metadataIntialiser.component';
import { ConfigurationIntialiser } from './initialiser/configurationintialiser.component';
import { PicklistDesignerIntialiser } from './initialiser/picklistdesignerintialiser.component';
import { MetadataDesignerIntialiser } from './initialiser/metadatadesignerIntialiser.component';
import { LayoutIntialiserComponent } from './meta-data/layout/layoutinitialiser.component';
import { ResourceComponent } from './resource/resource.component';
import { BaseIntialiser } from './initialiser/baseintialiser.component';

import { RelatedEntitiesComponent } from './meta-data/relatedentities/relatedentities.component';
import { QuickAddSettingComponent } from './dynamic-form-builder/settings/custom/quickadd-setting.component';
import { IntializeMetadataConfigurer } from './meta-data/layout/layout-detail-list/intializemetadataconfigurer/intializemetadataconfigurer.component';
import { BatchItemComponent } from "./batchitem/batchitem.component";
import { LayoutDetailFormComponent } from './meta-data/layout/layout-detail-form/layout-detail-form.component';
import { MenuGroupComponent } from './menu-group/menu-group.component';
import { FormLayoutDesignerComponent } from './meta-data/layout/layout-detail-form/form-layout-designer/form-layout-designer.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';

//import { LayoutTopBarComponent } from './meta-data/layout/layout-top-bar/layout-top-bar.component';


//const componentResolver:RoutingComponentFactry;
const routes: Routes = [

  { path: '', component: LoginComponent, pathMatch: 'full' },

  { path: 'forgotpassword', component: ForgotPasswordComponent, pathMatch: 'full' },
  { path: 'changepassword', component: ChangePasswordComponent, pathMatch: 'full' },
  /////////////////////////////////////////The pagenotfound route is used to show 404 page error when user token is expired//////////////////////////////////////////////////
  { path: 'pagenotfound', component: PagenotfoundComponent, pathMatch:"full"},
 ///////////////////////////////////////////////////////////////////END////////////////////////////////////////////////////////////////////////////////
  //{ path: '**', component: LoginComponent }
]


@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // Tell the router to use the HashLocationStrategy.
    useHash: false,

    // We're going to dynamically set the param-inheritance strategy based
    // on the state of the browser location. This way, the user can jump back
    // and forth between the two different modes.
    paramsInheritanceStrategy:
      location.search.startsWith("?always")
        ? "always"
        : "emptyOnly"
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routableComponents = [
  NavMenuComponent,
  TopMenuComponent,
  SidebarComponent,
  LoginComponent,
  ForgotPasswordComponent,
  ChangePasswordComponent,
  MetadataComponent,
  MetadataDetailComponent,
  LayoutComponent,
  FieldsComponent,
  RelationsComponent,
  QueryComponent,
  TaskoperationComponent,
  RuleComponent,
  ModalLayoutComponent,
  ModalMessageComponent,
  LayoutdetailComponent,
  LayoutfieldsComponent,
  LayoutPreviewComponent,
  LayouttoolbarComponent,
  LayoutactionComponent,
  FreetextSearchComponent,
  SimpleSearchComponent,
  AdvanceSearchComponent,
  ListFreeTextSearchComponent,
  ListActionComponent,
  ListSimpleSearchComponent,
  ListToolbarComponent,
  ListFieldsComponent,
  ListAdvanceSearchComponent,

  PicklistComponent,
  PicklistDetailComponent,
  PicklistFieldComponent,
  PicklistLayoutComponent,
  PicklistOperationComponent,
  PicklistQueryComponent,
  //PicklistRelationComponent,
  //PicklistWorkflowComponent,
  WorkFlowComponent,
  ModalInnerStepComponent,
  ModalWorkflowprocessComponent,
  ModalOperationProcessComponent,
  LayoutDetailListComponent,
  LayoutDetailFormComponent,
  LayoutDetailViewComponent,
  LayoutDetailViewFieldsComponent,
  LayoutDetailViewActionComponent,
  ListComponent,
  FormComponent,
  ViewComponent,
  //ListAdvanceSearchComponent,
  //ListFreeTextSearchComponent,
  //ListActionComponent,
  //ListSimpleSearchComponent,
  //ListToolbarComponent,
  //ListFieldsComponent,
  ListPreviewComponent,
  ViewFieldsComponent,
  CheckboxSettingComponent,
  DropdownSettingComponent,
  FileSettingComponent,
  RadioSettingComponent,
  SectionSettingComponent,
  TabSettingComponent,
  TextboxSettingComponent,
  LinkSettingComponent,
  RichtextboxSettingComponent,
  CalendarSettingComponent,
  MultiSelectDropdownSettingComponent,
  AddSettingComponent,
  RequiredValidatorComponent,
  LengthValidatorComponent,
  RangeValidatorComponent,
  EmailFormatValidatorComponent,
  UserComponent,
  UserCreateComponent,
  UserEditComponent,
  CountryComponent,
  CountryDetailComponent,
  CountryNewComponent,
  PicklistListComponent,
  PicklistNewComponent,
  PicklistEditComponent,
  PicklistPreviewComponent,
  RoleComponent,
  RoleDetailComponent,
  DynamicGridComponent,
  EntitySecurityComponent,
  FunctionSecurityComponent,
  WorkFlowSecurityComponent,
  RoleSecurityComponent,
  CounterComponent,
  ListAdvanceSearchComponent,
  NotfoundcomponentComponent,
  FilterPipe,
  CustomSettingComponent,
  DetailEntityComponent,
  //GeneralUiListComponent,
  GeneralUiNewComponent,
  GeneralUiEditComponent,
  GeneralUiPreviewComponent,
  MenuItemComponent,
  GeneralUiDisplayComponent,
  SubscriptionComponent,
  SubscriptionDetailComponent,
  HomeComponent,
  BatchTypeComponent,
  CommunicationComponent,
  SchedulerComponent,
  RuleUpsertComponent,
  PicklistIntialiser,
  MetadataIntialiser,
  ConfigurationIntialiser,
  PicklistDesignerIntialiser,
  MetadataDesignerIntialiser,
  LayoutIntialiserComponent,
  BaseIntialiser,
  QuickAddSettingComponent,
  RelatedEntitiesComponent,
  IntializeMetadataConfigurer,
  DefaultValidatorComponent,
  BatchItemComponent,
  MenuGroupComponent,
  FormLayoutDesignerComponent
 // LayoutTopBarComponent
];

// function MyComponentFactory(route: ActivatedRoute): Type<any> {
//     if (route.snapshot.data['type']==='bug') {
//         return BugDetailComponent;
//     }
//     return FeatureRequestDetailComponent;
// }

 