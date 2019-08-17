import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MetadataService } from '../meta-data/metadata.service';
import { WorkFlowService } from '../meta-data/workflow/workflow.service';
import { RoleService } from '../role/role.service';
import { WorkFlowSecurityService } from '../entitysecurity/workflowsequrity.service';
import { TosterService } from 'src/app/services/toster.service';
import {WorkFlowRole} from '../model/workflow/workflow-role';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import { MenuService } from '../services/menu.service';


@Component({
  selector: 'app-workflowsequrity',
  templateUrl: './workflowsequrity.component.html',
  styleUrls: ['./workflowsequrity.component.css']
})
export class WorkFlowSecurityComponent implements OnInit {  
  public gridData: any[];
  public roleList:any[];
  @Input() entityNameParam;  
  @Input() roleIdParam;  
  entityName:string;
  roleId:string;
  //workFlowInfos: any;
  workFlowRole=new WorkFlowRole();
  public view: Observable<GridDataResult>;
  public resource = Resource;

  private dropdownSettings = {
    singleSelection: false,
    idField: 'roleId',
    textField: 'name',
    // selectAllText: 'Select All',
    // unSelectAllText: 'UnSelect All',
    enableCheckAll:false,
    itemsShowLimit: '10',
    allowSearchFilter: true
  };

  
  constructor(private activatedRoute: ActivatedRoute,private router: Router,private metadataService: MetadataService,private workFlowService: WorkFlowService
    , private roleService: RoleService ,private workFlowSecurityService:WorkFlowSecurityService,private toster:TosterService,
    private globalResourceService: GlobalResourceService,private menuService: MenuService  ) {

   
     }

 

  ngOnInit() {  
    this.resource = this.globalResourceService.getGlobalResources();    

    this.entityName = this.entityNameParam;
    this.roleId=this.roleIdParam; 
    if(!this.entityName )
    {
      // let result=this.menuService.getMenuconext();
      // this.entityName = result.param_name;
        this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
          this.entityName = params['entityName'];
        });
      }

    this.getWorkFlowSecurity();    
  } 

  
    private getWorkFlowSecurity() {
    this.workFlowSecurityService.getWorkFlowSecurity(this.entityName).pipe(first()).subscribe(
        data => {   
          if (data)   
          this.gridData=data.workFlows; 
            if(this.roleId)
            {
              data.workFlows.forEach(workFlow=> {
                workFlow.steps.forEach(step=> {  
                step.activatedList= step.activatedList.filter(role => role.roleId === this.roleId);
                step.accessedList= step.accessedList.filter(role => role.roleId === this.roleId);
                step.assignedList= step.assignedList.filter(role => role.roleId === this.roleId);
                }) ; 
            }) ; 
              this.roleList= data.roleList.filter(role => role.roleId === this.roleId);
            }               
            else
            {
              this.roleList=data.roleList;
            }
                

        },
        error => {
          console.log(error);
        });
  }

  addWorkFlowRoleSecurity(workFlowStepId,type,workFlowId,model)
  {
    
    this.workFlowRole.workFlowStepId=workFlowStepId;
    this.workFlowRole.roleId=model.roleId;
    this.workFlowRole.workFlowId=workFlowId;
    this.workFlowRole.assignmentOperationType=type;
    this.workFlowSecurityService.addWorkFlowRoleSecurity(this.workFlowRole).pipe(first()).subscribe(
      data => {   
        this.workFlowRole.roleAssignmetId=data;
        this.toster.showSuccess(this.resource[this.getResourceValue("metadata_operation_workflow_save_success_message")]);           
      
      },
      error => {
        console.log(error);
      });
  }


  deleteWorkFlowRoleSecurity(workFlowStepId,type,workFlowId,model)
  {      
    this.workFlowSecurityService.deleteWorkFlowRoleSecurity(workFlowStepId,model.roleId,workFlowId,type).pipe(first()).subscribe(
      data => {   
       // this.workFlowRole.roleAssignmetId=data;
        this.toster.showSuccess(this.resource[this.getResourceValue("metadata_operation_workflow_delete_success_message")]);           
      
      },
      error => {
        console.log(error);
      });
  }

  onAssignemntMandatoryEvent(step)
  {

    this.workFlowService.updateWorkFlowStep(step).pipe(first()).subscribe(
      data => {
        this.toster.showSuccess(this.resource[this.getResourceValue("metadata_operation_workflow_update_success_message")]);
      },
      error => {
        console.log(error);
      });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
