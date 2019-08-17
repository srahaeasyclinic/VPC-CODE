import { NgModule } from '@angular/core';
import { RouterModule, Routes, ActivatedRoute } from '@angular/router';



//Metadata component references
import { MetadataComponent } from '../meta-data/metadata.component';
import { MetadataDetailComponent } from '../meta-data/metadatadetail.component';
import { LayoutComponent } from '../meta-data/layout/layout.component';
import { FieldsComponent } from '../meta-data/fields/fields.component';
import { RelationsComponent } from '../meta-data/relations/relations.component';
import { QueryComponent } from '../meta-data/query/query.component';
import { TaskoperationComponent } from '../meta-data/taskoperation/taskoperation.component';
import { RuleComponent } from '../meta-data/rule/rule.component';


import { ModalLayoutComponent } from '../meta-data/layout/modal-layout/modal-layout.component';
import { ModalMessageComponent } from '../meta-data/layout/modal-message/modal-message.component';
import { LayoutdetailComponent } from '../meta-data/layout/layoutdetail/layoutdetail.component';
import { WorkFlowComponent } from '../meta-data/workflow/workflow.component';
import { ModalInnerStepComponent } from '../meta-data/workflow/modal-innerstep/modal-innerstep.component';
import { ModalWorkflowprocessComponent } from '../meta-data/workflow/modal-workflowprocess/modal-workflowprocess.component';
import { ModalOperationProcessComponent } from '../meta-data/workflow/modal-operationprocess/modal-operationprocess.component';
import { LayoutDetailListComponent } from '../meta-data/layout/layout-detail-list/layout-detail-list.component';
import { LayoutPreviewComponent } from '../meta-data/layout/layoutpreview/layoutpreview.component';
import { LayoutfieldsComponent } from '../meta-data/layout/layout-detail-list/layoutfields/layoutfields.component';
import { LayouttoolbarComponent } from '../meta-data/layout/layout-detail-list/layouttoolbar/layouttoolbar.component';
import { LayoutactionComponent } from "../meta-data/layout/layout-detail-list/layoutaction/layoutaction.component";
import { FreetextSearchComponent } from "../meta-data/layout/layout-detail-list/freetextsearch/freetextsearch.component";
import { SimpleSearchComponent } from "../meta-data/layout/layout-detail-list/simplesearch/simplesearch.component";
import { AdvanceSearchComponent } from "../meta-data/layout/layout-detail-list/advancesearch/advancesearch.component";
import { LayoutDetailFormComponent } from '../meta-data/layout/layout-detail-form/layout-detail-form.component';
import { LayoutDetailViewComponent } from '../meta-data/layout/layout-detail-view/layout-detail-view.component';
import { LayoutDetailViewFieldsComponent } from '../meta-data/layout/layout-detail-view/layout-detail-view-fields/layout-detail-view-fields.component';
import { LayoutDetailViewActionComponent } from '../meta-data/layout/layout-detail-view/layout-detail-view-action/layout-detail-view-action.component';
import { LayoutResolver } from '../meta-data/layout/layout-resolver.service';

//picklist 
import { PicklistComponent } from '../picklist/picklist.component';
import { PicklistDetailComponent } from '../picklist/picklistdetail.component';
import { PicklistFieldComponent } from '../picklist/picklist-field/picklist-field.component';
import { PicklistLayoutComponent } from '../picklist/picklist-layout/picklist-layout.component';
import { PicklistOperationComponent } from '../picklist/picklist-operation/picklist-operation.component';
import { PicklistQueryComponent } from '../picklist/picklist-query/picklist-query.component';
import { ListComponent } from '../picklist/picklist-layout/list/list.component';
import { PicklistLayoutResolverService } from '../picklist/picklist-layout/list/picklist-layout-resolver.service';
import { ListFreeTextSearchComponent } from '../picklist/picklist-layout/list/list-free-text-search/list-free-text-search.component';
import { ListActionComponent } from '../picklist/picklist-layout/list/list-action/list-action.component';
import { ListSimpleSearchComponent } from '../picklist/picklist-layout/list/list-simple-search/list-simple-search.component';
import { ListToolbarComponent } from '../picklist/picklist-layout/list/list-toolbar/list-toolbar.component';
import { ListFieldsComponent } from '../picklist/picklist-layout/list/list-fields/list-fields.component';
import { ViewComponent } from '../picklist/picklist-layout/view/view.component';
import { ViewFieldsComponent } from '../picklist/picklist-layout/view/view-fields/view-fields.component';
import { FormComponent } from '../picklist/picklist-layout/form/form.component';


// dynamic form builder  


import { RoleComponent } from '../role/role.component';
import { RoleDetailComponent } from '../role/role-detail.component';
import { DynamicGridComponent } from '../dynamic-grid/dynamic-grid.component';
import { EntitySecurityComponent } from "../entitysecurity/entitysequrity.component";
import { WorkFlowSecurityComponent } from "../entitysecurity/workflowsequrity.component";
import { RoleSecurityComponent } from "../entitysecurity/rolesequrity.component";

//example
import { CounterComponent } from '../counter/counter.component';
import { NotfoundcomponentComponent } from '../notfoundcomponent/notfoundcomponent.component';

//filter
import { FilterPipe } from '../services/filter.pipe';

import { GeneralUiNewComponent } from '../general/new/GeneralUiNew.component';
import { GeneralUiEditComponent } from '../general/edit/GeneralUiEdit.component';
import { GeneralUiPreviewComponent } from '../general/preview/GeneralUiPreview.component';
import { FunctionSecurityComponent } from "../entitysecurity/functionsequrity.component";

//Menu Item
import { MenuItemComponent } from '../menu-item/menu-item.component';

import { GeneralUiDisplayComponent } from "../general-ui-display/general-ui-display.component";

import { SubscriptionComponent } from "../subscription/subscription.component";
import { SubscriptionDetailComponent } from "../subscription/subscription-detail.component";
import { HomeComponent } from '../home/home.component';
import { BatchTypeComponent } from "../batchtype/batchtype.component";
import { CommunicationComponent } from '../communication/communication.component';
import { SchedulerComponent } from "../batchtype/scheduler.component";
import { ForgotPasswordComponent } from '../forgotpassword/forgotpassword.component';
// import { ChangePasswordComponent } from '../changepassword/changepassword.component';
import { RuleUpsertComponent } from '../meta-data/rule/ruleupsert.component';
import { PicklistIntialiser } from '../initialiser/picklistintialiser.component';
import { MetadataIntialiser } from '../initialiser/metadataIntialiser.component';
import { ConfigurationIntialiser } from '../initialiser/configurationintialiser.component';
import { PicklistDesignerIntialiser } from '../initialiser/picklistdesignerintialiser.component';
import { MetadataDesignerIntialiser } from '../initialiser/metadatadesignerIntialiser.component';
import { LayoutIntialiserComponent } from '../meta-data/layout/layoutinitialiser.component';
import { ResourceComponent } from '../resource/resource.component';



import { AuthorizationCheck } from '../interceptor/authorizationCheck';
import { ComponentInitializer } from '../initialiser/genericinitializer/component-initializer/dynamic.component';
import { IntializeMetadataConfigurer } from '../meta-data/layout/layout-detail-list/intializemetadataconfigurer/intializemetadataconfigurer.component';
import { RelatedEntitiesComponent } from '../meta-data/relatedentities/relatedentities.component';
import { PagenotfoundComponent } from '../pagenotfound/pagenotfound.component';
import { FormLayoutDesignerComponent } from '../meta-data/layout/layout-detail-form/form-layout-designer/form-layout-designer.component';
import { LanguageIntialiserComponent } from '../initialiser/languageintialiser.component';
import { pagenotfoundCheck } from '../interceptor/pagenotfoundCheck';

//const componentResolver:RoutingComponentFactry;
const Secondaryroutes: Routes = [
  ////////////////////////////////////the aboutpage is used to show error message when authentication token is not expired////////////////////////////////////////////////// 
 {
    path: 'aboutpage', component: ConfigurationIntialiser, pathMatch: 'full',
    children: [
      {path:'',component:PagenotfoundComponent}
    ]
  },
////////////////////////////////////////////////END/////////////////////////////////////////////////////////////////////////////////////////////////////////
  {
    path: ':language', component: LanguageIntialiserComponent, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
    children: [
    { path: '', redirectTo:"dashboard", canActivate: [AuthorizationCheck], pathMatch: 'full' },
  {
    path: 'dashboard', component: MetadataIntialiser, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
    children: [ { path: '', component: HomeComponent, canActivate: [AuthorizationCheck] },]
  },
  
  {
    path: 'metadata/:topgroup/:leftmaingroup/:leftgroup', component: MetadataIntialiser, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
    children: [
      { path: '', component: MetadataComponent, canActivate: [AuthorizationCheck] },
      {
        path: ':entityName',
        component: MetadataDetailComponent,
        children: [
          { path: '', redirectTo: 'fields', pathMatch: 'full', canActivate: [AuthorizationCheck] },
          { path: 'workflow', component: WorkFlowComponent, canActivate: [AuthorizationCheck] },
          {
            path: 'layout', component: LayoutIntialiserComponent, canActivate: [AuthorizationCheck],
            children: [
              { path: '', component: LayoutComponent, canActivate: [AuthorizationCheck] },
              {
                path: 'list/:id', component: LayoutDetailListComponent,canActivate: [AuthorizationCheck],
                resolve: {
                  layoutDetails: LayoutResolver
                },
                children: [
                   { path: '', redirectTo: 'fields', pathMatch: 'full',canActivate: [AuthorizationCheck] },
                  { path: ':type', component: IntializeMetadataConfigurer,canActivate: [AuthorizationCheck] }
                  // { path: 'fields', component: LayoutfieldsComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'textsearch', component: FreetextSearchComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'simplesearch', component: SimpleSearchComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'advancesearch', component: AdvanceSearchComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'toolbar', component: LayouttoolbarComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'action', component: LayoutactionComponent,canActivate: [AuthorizationCheck] }
                                  
                ]
              },
              {
                path: 'form/:id',
                // component: LayoutDetailFormComponent,canActivate: [AuthorizationCheck],
                component: FormLayoutDesignerComponent,canActivate: [AuthorizationCheck],                
                resolve: {
                  layoutDetails: LayoutResolver
                }
              },
              {
                path: 'view/:id',
                component: LayoutDetailViewComponent,canActivate: [AuthorizationCheck],
                resolve: {
                  layoutDetails: LayoutResolver
                },
                children: [
                  { path: '', redirectTo: 'fields', pathMatch: 'full',canActivate: [AuthorizationCheck] },
                  { path: ':type', component: IntializeMetadataConfigurer,canActivate: [AuthorizationCheck] }  
                  // { path: 'fields', component: LayoutDetailViewFieldsComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'action', component: LayoutDetailViewActionComponent,canActivate: [AuthorizationCheck] }
                ]
              },
            ]
          },

          { path: 'fields', component: FieldsComponent,canActivate: [AuthorizationCheck] },
          { path: 'relations', component: RelationsComponent,canActivate: [AuthorizationCheck] },
          { path: 'relatedentities', component: RelatedEntitiesComponent,canActivate: [AuthorizationCheck] },
          { path: 'operations', component: TaskoperationComponent,canActivate: [AuthorizationCheck] },
          { path: 'rules', component: RuleComponent,canActivate: [AuthorizationCheck] },
          { path: 'query', component: QueryComponent,canActivate: [AuthorizationCheck] },
          {
            path: 'rolesecurity', component: RoleSecurityComponent, canActivate: [AuthorizationCheck],children: [
              { path: '', redirectTo: 'entitysecurity', pathMatch: 'full',canActivate: [AuthorizationCheck] },
              { path: 'entitysecurity', component: EntitySecurityComponent,canActivate: [AuthorizationCheck] },
              { path: 'workflowsecurity', component: WorkFlowSecurityComponent,canActivate: [AuthorizationCheck] }
            ]
          },
        ]
      }
    ]
  },
  {
    path: 'picklistmetadata/:topgroup/:leftmaingroup/:leftgroup', component: MetadataIntialiser, pathMatch: 'prefix',canActivate: [AuthorizationCheck],
     children: [
      { path: '', component: PicklistComponent,canActivate: [AuthorizationCheck] },
      {
        path: ':picklistName', component: PicklistDetailComponent,canActivate: [AuthorizationCheck],
        children: [
          { path: '', redirectTo: 'fields', pathMatch: 'full',canActivate: [AuthorizationCheck] },
          {
            path: 'layouts', component: LayoutIntialiserComponent,canActivate: [AuthorizationCheck],
            children: [
              { path: '', component: PicklistLayoutComponent,canActivate: [AuthorizationCheck] },
              {
                path: 'list/:id', component: ListComponent,canActivate: [AuthorizationCheck],
                resolve: {
                  layoutDetails: PicklistLayoutResolverService
                },
                children: [
                  { path: ':type', component: IntializeMetadataConfigurer,canActivate: [AuthorizationCheck] }  
                  , { path: '', redirectTo: 'fields', pathMatch: 'full',canActivate: [AuthorizationCheck] },
                  // { path: 'textsearch', component: ListFreeTextSearchComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'simplesearch', component: ListSimpleSearchComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'toolbars', component: ListToolbarComponent,canActivate: [AuthorizationCheck] },
                  // { path: 'actions', component: ListActionComponent,canActivate: [AuthorizationCheck] }
                  
                ]
              },
              {
                // path: 'form/:id', component: FormComponent,canActivate: [AuthorizationCheck],
                path: 'form/:id', component: FormLayoutDesignerComponent,canActivate: [AuthorizationCheck]  ,              
                resolve: {
                  layoutDetails: PicklistLayoutResolverService
                }
              },
              {
                path: 'view/:id',
                component: ViewComponent,
                canActivate: [AuthorizationCheck],
                resolve: {
                  layoutDetails: PicklistLayoutResolverService
                },
                children: [
                  { path: '', redirectTo: 'fields', pathMatch: 'full',canActivate: [AuthorizationCheck] },
                  // { path: 'fields', component: ViewFieldsComponent,canActivate: [AuthorizationCheck] }
                  { path: ':type', component: IntializeMetadataConfigurer,canActivate: [AuthorizationCheck] }  
                ]
              },
            ]
          },
          { path: 'fields', component: PicklistFieldComponent,canActivate: [AuthorizationCheck] },
          { path: 'operations', component: PicklistOperationComponent,canActivate: [AuthorizationCheck] },
        ]
      },
    ]
  },
  {
    path: ':topgroup/:leftmaingroup/:leftgroup', component: ConfigurationIntialiser, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
    children: [
      { path: '', component: ComponentInitializer, canActivate: [AuthorizationCheck] },
      { path: 'new', component: ComponentInitializer , canActivate: [AuthorizationCheck]},
      { path: 'preview/:id', component: ComponentInitializer, canActivate: [AuthorizationCheck] },
      { path: 'edit/:id', component: ComponentInitializer, canActivate: [AuthorizationCheck] },
      {
        path: ':id', component: ComponentInitializer, canActivate: [AuthorizationCheck],
        children: [
          { path: 'entitysecurity/:name', component: EntitySecurityComponent , canActivate: [AuthorizationCheck]},
          { path: 'workflowsecurity/:name', component: WorkFlowSecurityComponent, canActivate: [AuthorizationCheck] }
        ]
      },
    ]
      },

    ]
  },
 { path: '**', component: PagenotfoundComponent , canActivate:[pagenotfoundCheck]}
  
]


@NgModule({
  imports: [RouterModule.forChild(Secondaryroutes)],
  exports: [RouterModule]
})
export class SecondaryAppRoutingModule { }



 /////////////////////////////////////////OLD Routing///////////////////////////////////////////////////////////////////////
  // // { path: 'changepassword', component: ChangePasswordComponent, pathMatch: 'full' },
  // { path: 'dashboard', component: HomeComponent, pathMatch: 'full', canActivate: [AuthorizationCheck] },
  
  // {
  //   path: 'picklist-manager/:group/:name', component: MetadataIntialiser, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
  //   children: [
  //     { path: '', component: PicklistListComponent, canActivate: [AuthorizationCheck] },
  //     { path: 'new', component: PicklistNewComponent , canActivate: [AuthorizationCheck]},
  //     { path: 'preview/:id', component: PicklistPreviewComponent, canActivate: [AuthorizationCheck] },
  //     { path: 'edit/:id', component: PicklistEditComponent, canActivate: [AuthorizationCheck] },
  //   ]
  // },
  // {
  //   path: 'object-manager/:group/:name', component: MetadataIntialiser, pathMatch: 'prefix', canActivate: [AuthorizationCheck],
  //   children: [
  //     { path: '', component: GeneralUiDisplayComponent, canActivate: [AuthorizationCheck] },
  //     { path: 'new', component: GeneralUiNewComponent , canActivate: [AuthorizationCheck]},
  //     { path: 'preview/:id', component: GeneralUiPreviewComponent, canActivate: [AuthorizationCheck] },
  //     { path: 'edit/:id', component: GeneralUiEditComponent, canActivate: [AuthorizationCheck] },
  //   ]
  // },

  