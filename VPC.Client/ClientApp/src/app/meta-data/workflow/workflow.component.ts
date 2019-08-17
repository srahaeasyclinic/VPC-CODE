import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { WorkFlowService } from './workflow.service';
import { Entities } from '../../model/entities';
import { WorkFlowStep } from '../../model/workflow/workflow-step';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ModalInnerStepComponent } from './modal-innerstep/modal-innerstep.component';
import { MetadataService } from '../metadata.service';
import { ModalWorkflowprocessComponent } from './modal-workflowprocess/modal-workflowprocess.component';
import { Resource } from 'src/app/model/resource';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import swal from 'sweetalert2';
import { MenuService } from '../../services/menu.service';

@Component({
  selector: 'app-workflow',
  templateUrl: './workflow.component.html',
  styleUrls: ['./workflow.component.css']
})
export class WorkFlowComponent implements OnInit {
  
  private entity: Entities;
  public view: Observable<GridDataResult>;
  //public gridData: any = this.entity;
  workFlowInfo: any;
  public workFlowInnerSteps=[];
  workFlowStep=new WorkFlowStep();
  entityName: string;
  subTypes:any[];
  subTypeSelected:string;
  public resource: Resource;
  
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private workFlowService: WorkFlowService, 
    private modalService: NgbModal,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService,
    private menuService: MenuService
    ) { }

 

  ngOnInit() { 
    this.activatedRoute.parent.params.subscribe((params: Params) => {
      this.entityName = params['entityName'];
    });
    // let result=this.menuService.getMenuconext();
    //   this.entityName = result.param_name;
    this.getMetadataFieldsByName(this.entityName)
    this.resource = this.globalResourceService.getGlobalResources();     
  }

  private getMetadataFieldsByName(name) {

    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      this.subTypes = data.subtypes;
      this.subTypeSelected = this.subTypes[0];
      this.getWorkFlow(this.subTypeSelected);
      //console.log('if');
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              this.subTypes = data.subtypes;
              this.subTypeSelected = this.subTypes[0];
              this.getWorkFlow(this.subTypeSelected);
            }
          },
          error => {
            console.log(error);
          });
      //console.log('else');
    }




    // this.metadataService.getMetadataByName(name)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       //console.log("data", data);
    //       if (data) {
    //         this.subTypes = data.subtypes;
    //         this.subTypeSelected = this.subTypes[0];
    //         this.getWorkFlow(this.subTypeSelected);
    //       }
    //     },
    //     error => {
    //       console.log(error);
    //     });
  }

  private getWorkFlow(subTypeName) {
    this.workFlowService.getWorkFlow(this.entityName,subTypeName).pipe(first()).subscribe(
        data => {
          //console.log("data", data);
          if (data) {            
            this.workFlowInfo = data;
            var totalCount=0;
            var lastItemWorkFlowStepId='';

            this.workFlowInfo.steps.forEach(obj => {           
                if(totalCount<obj.sequenceNumber && obj.workFlowStepId!='00000000-0000-0000-0000-000000000000')
                {
                  totalCount=obj.sequenceNumber ;
                  lastItemWorkFlowStepId=obj.workFlowStepId;
                }          
          });

          this.workFlowInfo.steps.forEach(obj => {           
            if(obj.workFlowStepId==lastItemWorkFlowStepId )
            {
              obj.lastItem=true;
            }          
      });

          }
        },
        error => {
          console.log(error);
        });
  }

  getInnerStep(mainStep)
  {
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };

    const modalRef = this.modalService.open(ModalInnerStepComponent, ngbModalOptions);
    modalRef.componentInstance.title = this.getResourceValue("workflow_metadata_addtransition");
    modalRef.componentInstance.innerstep = this;
    modalRef.componentInstance.entityName = this.entityName;
    modalRef.componentInstance.transactionType = mainStep.transitionType.id;   
    modalRef.componentInstance.workFlowId = mainStep.workFlowId; 
    modalRef.componentInstance.workFlowStepId = mainStep.workFlowStepId; 

    modalRef.componentInstance.emitData.subscribe(($e) => {
     // console.log('$e', $e);      
      mainStep.innerSteps.push($e);
    })

  }

  addWorkFlowStep(mainStep)
  {
    if(mainStep.workFlowStepId=='00000000-0000-0000-0000-000000000000')
    {
      this.workFlowStep.WorkFlowId=mainStep.workFlowId;
      this.workFlowStep.TransitionType={};
      this.workFlowStep.TransitionType.id=mainStep.transitionType.id;
     // this.workFlowStep.WorkFlowStepId=mainStep.workFlowId;
      this.workFlowService.addWorkFlowStep(this.workFlowStep).pipe(first()).subscribe(
        data => {
          mainStep.workFlowStepId=data;  
          this.getWorkFlow(this.subTypeSelected);      
        },
        error => {
          console.log(error);
        });
    }
    else{
      this.workFlowService.deleteWorkFlowStep(mainStep.workFlowStepId,mainStep.workFlowId).pipe(first()).subscribe(
        data => {
          mainStep.workFlowStepId='00000000-0000-0000-0000-000000000000';  
          this.getWorkFlow(this.subTypeSelected);       
        },
        error => {
          console.log(error);
        });
    }
  }

  deleteWorkFlowInnerStep(workFlowInnerStep,innerSteps)
  {

    //if (actionName.toLowerCase() == 'Delete'.toLowerCase()) {

      this.globalResourceService.openDeleteModal.emit()

      this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
        this.workFlowService.deleteWorkFlowInnerStep(workFlowInnerStep.innerStepId).pipe(first()).subscribe(
          data => {
            const index: number = innerSteps.indexOf(workFlowInnerStep);
            if (index !== -1) {
              innerSteps.splice(index, 1);
            }      
          },
          error => {
            console.log(error);
          });
         
        })


      // swal({
      //   title: this.getResourceValue("common_message_areyousure"),
      //   text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
      //   type: 'warning',
      //   showCancelButton: true,
      //   confirmButtonColor: '#3085d6',
      //   cancelButtonColor: '#d33',
      //   confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
      //   showLoaderOnConfirm: true,
      // })
      //   .then((willDelete) => {
      //     if (willDelete.value) {
      //       this.workFlowService.deleteWorkFlowInnerStep(workFlowInnerStep.innerStepId).pipe(first()).subscribe(
      //         data => {
      //           const index: number = innerSteps.indexOf(workFlowInnerStep);
      //           if (index !== -1) {
      //             innerSteps.splice(index, 1);
      //           }      
      //         },
      //         error => {
      //           console.log(error);
      //         });

      //     } else {
      //       //write the code for cancel click
      //     }

      //   });
    //}

    // this.workFlowService.deleteWorkFlowInnerStep(workFlowInnerStep.innerStepId).pipe(first()).subscribe(
    //   data => {
    //     const index: number = innerSteps.indexOf(workFlowInnerStep);
    //     if (index !== -1) {
    //       innerSteps.splice(index, 1);
    //     }      
    //   },
    //   error => {
    //     console.log(error);
    //   });
  }

  moveDown_Step(allSteps, index) {
    var temp = allSteps[index];
    allSteps[index] = allSteps[index + 1];
    allSteps[index + 1] = temp;
    this.workFlowStep_UpdateSequence_Steps(allSteps);

    
};

  moveUp_Step(allSteps, index) {
      var temp = allSteps[index];
      allSteps[index] = allSteps[index - 1];
      allSteps[index - 1] = temp;
      this.workFlowStep_UpdateSequence_Steps(allSteps);
  };

  workFlowStep_UpdateSequence_Steps(allSteps)
  {
    this.workFlowService.workFlowStep_UpdateSequence_Steps(allSteps).pipe(first()).subscribe(
      data => {
           
      },
      error => {
        console.log(error);
      });
  }

  moveDown_InnerStep (allInnerSteps, index) {
    var temp = allInnerSteps[index];
    allInnerSteps[index] = allInnerSteps[index + 1];
    allInnerSteps[index + 1] = temp;
    this.workFlowStep_UpdateSequence_InnerSteps(allInnerSteps);

    
};

  moveUp_InnerStep(allInnerSteps, index) {
      var temp = allInnerSteps[index];
      allInnerSteps[index] = allInnerSteps[index - 1];
      allInnerSteps[index - 1] = temp;
      this.workFlowStep_UpdateSequence_InnerSteps(allInnerSteps);
  };

  workFlowStep_UpdateSequence_InnerSteps(allSteps)
  {
    this.workFlowService.workFlowStep_UpdateSequence_InnerSteps(allSteps).pipe(first()).subscribe(
      data => {
           
      },
      error => {
        console.log(error);
      });
  }
  
  addProcess(mainStep,innerStep)
  {
    const modalRef = this.modalService.open(ModalWorkflowprocessComponent, { size: 'lg' });
    modalRef.componentInstance.title = this.getResourceValue("metadata_workflow_processconfiguration");
    modalRef.componentInstance.innerSteps = innerStep;
    modalRef.componentInstance.entityName = this.entityName;
    modalRef.componentInstance.fromStepId = mainStep.transitionType.id;
    modalRef.componentInstance.toStepId = innerStep.transitionType.id;
  }

  configToggle(mainStep) {
    mainStep.isConfigToggle = !mainStep.isConfigToggle;
  }

  onChangeSubType(subType)
  {
    this.getWorkFlow(subType);
  }
  // generateResourceName(word)
  // {
  //    if (!word) return word;
  //    return word[0].toLowerCase() + word.substr(1);
  //  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }



}
