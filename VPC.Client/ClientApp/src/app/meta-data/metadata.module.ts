import { NgModule } from '@angular/core';

import { MetadataDetailComponent } from './metadatadetail.component';
import { FieldsComponent } from './fields/fields.component';
import { LayoutComponent } from './layout/layout.component';
import { LayoutdetailComponent } from './layout/layoutdetail/layoutdetail.component';
import { LayoutDetailListComponent } from './layout/layout-detail-list/layout-detail-list.component';
import { LayoutDetailFormComponent } from './layout/layout-detail-form/layout-detail-form.component';
import { LayoutDetailViewComponent } from './layout/layout-detail-view/layout-detail-view.component';
import { ModalWorkflowprocessComponent } from './workflow/modal-workflowprocess/modal-workflowprocess.component';
import { MetadataRoutingModule, metadataComponents } from './metadata-routing.module';


import { RuleUpsertComponent } from "./rule/ruleupsert.component";

@NgModule({
  imports: [
    MetadataRoutingModule
  ],
  declarations: [
    metadataComponents,
  ]
  // imports: [ CommonModule, MatTabsModule, RouterModule.forChild([
  //   { path: '', component: MetadataDetailComponent, children: [
  //     { path: '', redirectTo: 'fields' },
  //     { path: 'fields', component: FieldsComponent, data: { label: 'Fields' } },
  //     { path: 'layout', component: LayoutComponent, data: { label: 'Layout' } }
  //   ] }
  // ]) ],
  // declarations: [ MetadataDetailComponent, FieldsComponent, LayoutComponent, LayoutdetailComponent,LayoutDetailListComponent, LayoutDetailFormComponent, LayoutDetailViewComponent, ModalWorkflowprocessComponent ]
})
export class MetadataModule { }
