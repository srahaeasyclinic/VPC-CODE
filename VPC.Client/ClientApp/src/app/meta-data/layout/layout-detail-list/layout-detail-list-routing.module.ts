import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutDetailListComponent } from '../layout-detail-list/layout-detail-list.component';
import { LayoutfieldsComponent } from '../layout-detail-list/layoutfields/layoutfields.component';
import { FreetextSearchComponent } from "../layout-detail-list/freetextsearch/freetextsearch.component";
import { SimpleSearchComponent } from "../layout-detail-list/simplesearch/simplesearch.component";
import { AdvanceSearchComponent } from "../layout-detail-list/advancesearch/advancesearch.component";
import { LayouttoolbarComponent } from '../layout-detail-list/layouttoolbar/layouttoolbar.component';
import { LayoutactionComponent } from "../layout-detail-list/layoutaction/layoutaction.component";
import { LayoutResolver } from '../layout-resolver.service';

const routes: Routes = [
    {
        path: 'metadata/:name/layout/:id/List',
        component: LayoutDetailListComponent,
        resolve: {
          layoutDetails: LayoutResolver
        },
        data: { path: 'metadata/:name/layout/:id/List' },
        children: [
          { path: '', redirectTo: 'fields', pathMatch: 'full' },
          { path: 'fields', component: LayoutfieldsComponent },
          { path: 'textsearch', component: FreetextSearchComponent },
          { path: 'simplesearch', component: SimpleSearchComponent },
          { path: 'advancesearch', component: AdvanceSearchComponent },
          { path: 'toolbar', component: LayouttoolbarComponent },
          { path: 'action', component: LayoutactionComponent }
        ]
      }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  
  export class LayoutDetailListRoutingModule { }
  
  export const layoutdetaillistComponents = [
    //MetadataComponent,
    //MetadataDetailComponent
  ];
  