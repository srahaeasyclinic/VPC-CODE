 
import { Component, Output, EventEmitter, Input, HostListener } from '@angular/core';
//import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgbModal, NgbModalOptions, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs/Observable';
import { WorkFlowStep } from '../../../model/workflow/workflow-step';
import { WorkFlowInnerStep } from '../../../model/workflow/workflow-innerstep';
import { ModalWorkFlowInnerStepService } from '../../workflow/modal-innerstep/modal-innerstep.service';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../../../global-resource/global-resource.service'
import { Resource } from '../../../model/resource';


@Component({
  selector: 'app-modal-innerstep',
  templateUrl: './modal-innerstep.component.html',
  //styleUrls: ['./modal-innerstep.component.css']
})
export class ModalInnerStepComponent {
  workFlowInnerStep=new WorkFlowInnerStep();
  @Input() public entityName;
  @Input() public transactionType;
  @Input() public workFlowId;
  @Input() public workFlowStepId;
  @Output() emitData = new EventEmitter();

  workFlowSteps:any;
  public title : any;
  public resource: Resource;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router, 
    private workFlowInnerStepService: ModalWorkFlowInnerStepService,
    public activeModal: NgbActiveModal,
    public globalResourceService: GlobalResourceService,

    ) { }

   
   //entityName: string;

   ngOnInit() { 
    //this.resource = this.globalResourceService.getGlobalResources();
    //  this.activatedRoute.parent.params.subscribe((params: Params) => {
    //    this.entityName = params['name'];
    //  });
     this.getInnerStep();
   } 
 
   getInnerStep()
   {
     this.workFlowInnerStepService.getInnerStep(this.entityName,this.transactionType,this.workFlowId).pipe(first()).subscribe(
       data => {
        // console.log("data", data);
         if (data && data) {            
           this.workFlowSteps = data;
         }
       },
       error => {
         console.log(error);
       });
   }

   addInnerStep(workFlowStep)
   {
    this.workFlowInnerStep.TransitionType={};
    this.workFlowInnerStep.TransitionType.Id = workFlowStep.id ;
    this.workFlowInnerStep.TransitionType.Name = workFlowStep.value ;
    this.workFlowInnerStep.WorkFlowId = this.workFlowId;
    this.workFlowInnerStep.WorkFlowStepId = this.workFlowStepId;
    this.workFlowInnerStepService.addInnerStep(this.workFlowInnerStep).pipe(first()).subscribe(      
      data => {   
        const index: number = this.workFlowSteps.indexOf(workFlowStep);
          if (index !== -1) {
            this.workFlowSteps.splice(index, 1);
          } 
        this.emitData.next(data); 
            //console.log("data", data);      
      },
      error => {
        console.log(error);
      });

   }
  //  generateResourceName(word)
  //  {
  //     if (!word) return word;
  //     return word[0].toLowerCase() + word.substr(1);
  //   }
 
}
