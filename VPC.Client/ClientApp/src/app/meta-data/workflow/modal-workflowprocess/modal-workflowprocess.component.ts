import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalWorkFlowProcessService } from '../modal-workflowprocess/modal-workflowprocess.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { first } from 'rxjs/operators';
import { WorkFlowProcessTaskInfo } from '../../../model/workflow/workFlowProcessStep';
import { WorkFlowResource } from '../../../model/workflow/workflowresource';
import { GlobalResourceService } from '../../../global-resource/global-resource.service'
import { Resource } from '../../../model/resource';

@Component({
  selector: 'app-modal-workflowprocess',
  templateUrl: './modal-workflowprocess.component.html',
  styleUrls: ['./modal-workflowprocess.component.css']
})
export class ModalWorkflowprocessComponent implements OnInit {

  @Input() public innerSteps;
  @Input() public fromStepId;
  @Input() public toStepId;
  @Input() public entityName;
  public allPreProcessTasks = [];
  public allProcessTasks = [];
  public allPostProcessTasks = [];

  preProcessSelected: any;
  processSelected: any;
  postProcessSelected: any;

  public preProcessList = [];
  public processList = [];
  public postProcessList = [];

  public title: string;
  public resource: Resource;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private modalWorkFlowProcessService: ModalWorkFlowProcessService,
    public activeModal: NgbActiveModal, public globalResourceService: GlobalResourceService,


  ) { }

  ngOnInit() {
   // this.resource = this.globalResourceService.getGlobalResources();
    //this.getAllDefinedProcessTask();  
    this.getAllInnerStepTask();
  }

  private getAllInnerStepTask() {
    this.modalWorkFlowProcessService.getAllInnerStepTask(this.innerSteps.innerStepId, this.entityName).pipe(first()).subscribe(
      data => {
        //console.log("data", data);
        if (data) {

          //keep all the task seprately (pre-processor,processor,post-processor)
          data.forEach(childObj => {
            if (childObj.processType === 1) {
              this.preProcessList.push(childObj);
            }
            else if (childObj.processType === 2) {
              this.processList.push(childObj);
            }
            else if (childObj.processType === 3) {
              this.postProcessList.push(childObj);
            }
          })

          this.getAllDefinedProcessTask();
        }
      },
      error => {
        console.log(error);
      });
  }

  private getAllDefinedProcessTask() {
    this.modalWorkFlowProcessService.getAllDefinedProcessTask(this.fromStepId, this.toStepId, this.entityName).pipe(first()).subscribe(
      data => {
        //console.log("data", data);
        if (data) {

          data.forEach(childObj => {

            if (childObj.processType === 1) {
              //check is already pre processor has been added or not (if added then remove from dropdown)
              var checkAlreadyAddedPreProcessor = this.preProcessList.filter(innerStep => innerStep.processCode === childObj.id);
              if (checkAlreadyAddedPreProcessor.length === 0) {
                this.allPreProcessTasks.push(childObj);
              }

            }
            else if (childObj.processType === 2) {
              //check is already processor has been added or not (if added then remove from dropdown)
              var checkAlreadyAddedProcessor = this.processList.filter(innerStep => innerStep.processCode === childObj.id);
              if (checkAlreadyAddedProcessor.length === 0) {
                this.allProcessTasks.push(childObj);
              }
            }
            else if (childObj.processType === 3) {
              //check is already post processor has been added or not (if added then remove from dropdown)
              var checkAlreadyAddedPostProcessor = this.postProcessList.filter(innerStep => innerStep.processCode === childObj.id);
              if (checkAlreadyAddedPostProcessor.length === 0) {
                this.allPostProcessTasks.push(childObj);
              }
            }
          })
        }
      },
      error => {
        console.log(error);
      });
  }


  onChangeProcess(processorId, processType) {
    if (processorId === '00000000-0000-0000-0000-000000000000') {

      this.preProcessSelected = '';
      this.processSelected = '';
      this.postProcessSelected = '';
      
      return false;
    }
    var filteredProcess = this.innerSteps.workFlowProcess.filter(innerStep => innerStep.processType === processType)[0];
    var workFlowProcessTaskInfo = new WorkFlowProcessTaskInfo();
    workFlowProcessTaskInfo.WorkFlowProcessId = filteredProcess.workFlowProcessId;
    workFlowProcessTaskInfo.WorkFlowId = this.innerSteps.workFlowId;
    workFlowProcessTaskInfo.ProcessCode = processorId;

    this.modalWorkFlowProcessService.addProcessSteps(workFlowProcessTaskInfo).pipe(first()).subscribe(
      data => {
        //console.log("data", data);

        this.preProcessSelected = '';
        this.processSelected = '';
        this.postProcessSelected = '';

        if (data) {
          if (processType === 1) {
            // delete from  pre-processor dropdown 
            this.allPreProcessTasks.forEach(childObj => {
              if (childObj.id === data.processCode) {
                data.processName = childObj.key;
                this.allPreProcessTasks.splice(this.allPreProcessTasks.indexOf(childObj), 1);
              }
            })

            // add to pre-processor grid            
            this.preProcessList.push(data);
          }

          else if (processType === 2) {
            // delete from  processor dropdown 
            this.allProcessTasks.forEach(childObj => {
              if (childObj.id === data.processCode) {
                data.processName = childObj.key;
                this.allProcessTasks.splice(this.allProcessTasks.indexOf(childObj), 1);
              }
            })
            // add to processor grid 
            this.processList.push(data);
          }
          else if (processType === 3) {
            // delete from  post-processor dropdown 
            this.allPostProcessTasks.forEach(childObj => {
              if (childObj.id === data.processCode) {
                data.processName = childObj.key;
                this.allPostProcessTasks.splice(this.allPostProcessTasks.indexOf(childObj), 1);
              }
            })
            // add to pre-processor grid 
            this.postProcessList.push(data);
          }
        }
      },
      error => {
        console.log(error);
      });
  }

  delProcessTask(processor, processType) {
    this.modalWorkFlowProcessService.deleteProcessSteps(processor.workFlowProcessTaskId).pipe(first()).subscribe(
      data => {
        //console.log("data", data);
        if (data) {

          var bindTask = new WorkFlowResource();
          bindTask.id = processor.processCode;
          bindTask.key = processor.processName;
          bindTask.processType = processor.processType;

          if (processType === 1) {
            // add to per-processor grid
            this.allPreProcessTasks.push(bindTask);
            // delete from pre-processor dropdown
            const index: number = this.preProcessList.indexOf(processor);
            if (index !== -1) {
              this.preProcessList.splice(index, 1);
            }

          } else if (processType === 2) {
            // add to processor grid
            this.allProcessTasks.push(bindTask);
            // delete from processor dropdown
            const index: number = this.processList.indexOf(processor);
            if (index !== -1) {
              this.processList.splice(index, 1);
            }
          } else if (processType === 3) {
            // add to post-processor grid
            this.allPostProcessTasks.push(bindTask);
            // delete from post-processor dropdown
            const index: number = this.postProcessList.indexOf(processor);
            if (index !== -1) {
              this.postProcessList.splice(index, 1);
            }
          }

        }
      },
      error => {
        console.log(error);
      });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
