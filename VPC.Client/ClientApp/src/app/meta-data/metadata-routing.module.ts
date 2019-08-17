import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorkFlowSecurityComponent } from '../entitysecurity/workflowsequrity.component';
import { EntitySecurityComponent } from '../entitysecurity/entitysequrity.component';
import { RoleSecurityComponent } from '../entitysecurity/rolesequrity.component';
import { QueryComponent } from './query/query.component';
import { RuleComponent } from './rule/rule.component';
import { TaskoperationComponent } from './taskoperation/taskoperation.component';
import { RelationsComponent } from './relations/relations.component';
import { FieldsComponent } from './fields/fields.component';
import { LayoutComponent } from './layout/layout.component';
import { WorkFlowComponent } from './workflow/workflow.component';
import { MetadataDetailComponent } from './metadatadetail.component';
import { MetadataComponent } from './metadata.component';
import { RelatedEntitiesComponent } from './relatedentities/relatedentities.component';

const routes: Routes = [
  { path: 'metadata', component: MetadataComponent },
  {
    path: 'metadata/:name',
    component: MetadataDetailComponent,
    children: [
      { path: '', redirectTo: 'fields', pathMatch: 'full' },
      { path: 'workflow', component: WorkFlowComponent },
      { path: 'layout', component: LayoutComponent },
      { path: 'fields', component: FieldsComponent },
      { path: 'relations', component: RelationsComponent },
      { path: 'relatedentities', component: RelatedEntitiesComponent },
      { path: 'operations', component: TaskoperationComponent },
      { path: 'rules', component: RuleComponent },
      { path: 'query', component: QueryComponent },
      {
        path: 'rolesecurity', component: RoleSecurityComponent, children: [
          { path: '', redirectTo: 'entitysecurity', pathMatch: 'full' },
          { path: 'entitysecurity', component: EntitySecurityComponent },
          { path: 'workflowsecurity', component: WorkFlowSecurityComponent }
        ]
      }
    ]
  }
];

@NgModule({
   //imports: [RouterModule.forRoot(routes)],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class MetadataRoutingModule { }

export const metadataComponents = [
  //MetadataComponent,
  //MetadataDetailComponent
];
