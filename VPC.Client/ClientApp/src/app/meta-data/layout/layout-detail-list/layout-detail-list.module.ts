import { NgModule } from '@angular/core';

// import { LayoutDetailListComponent } from './layout-detail-list.component';
// import { LayoutfieldsComponent } from './layoutfields/layoutfields.component';
// import { FreetextSearchComponent } from "./freetextsearch/freetextsearch.component";
// import { SimpleSearchComponent } from "./simplesearch/simplesearch.component";
// import { AdvanceSearchComponent } from "./advancesearch/advancesearch.component";
// import { LayouttoolbarComponent } from './layouttoolbar/layouttoolbar.component';
// import { LayoutactionComponent } from "./layoutaction/layoutaction.component";
import { LayoutDetailListRoutingModule, layoutdetaillistComponents } from './layout-detail-list-routing.module';

@NgModule({
    imports: [
        LayoutDetailListRoutingModule
    ],
    declarations: [
        layoutdetaillistComponents
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
  export class LayoutDetailListModule { }