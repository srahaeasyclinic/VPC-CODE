import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutDetailListComponent } from '../layout-detail-list/layout-detail-list.component';
import { LayoutResolver } from '../layout-resolver.service';
import { IntializeMetadataConfigurer } from './intializemetadataconfigurer/intializemetadataconfigurer.component';
const routes: Routes = [
    {
        path: 'metadata/:name/layout/:id/List',
        component: LayoutDetailListComponent,
        resolve: {
          layoutDetails: LayoutResolver
        },
        data: { path: 'metadata/:name/layout/:id/List' },
        children: [        
          // { path: 'fields', component: LayoutfieldsComponent },
          // { path: 'textsearch', component: FreetextSearchComponent },
          // { path: 'simplesearch', component: SimpleSearchComponent },
          // { path: 'advancesearch', component: AdvanceSearchComponent },
          // { path: 'toolbar', component: LayouttoolbarComponent },
          // { path: 'action', component: LayoutactionComponent }
          { path: ':type', component: IntializeMetadataConfigurer }      
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
  