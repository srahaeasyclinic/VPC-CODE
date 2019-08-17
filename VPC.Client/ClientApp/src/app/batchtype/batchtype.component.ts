import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { BatchTypeService } from './batchtype.service';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal,NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { BatchTypeInfo } from '../model/batchtypeinfo';
import { SchedulerComponent } from './scheduler.component';
import { MenuService } from '../services/menu.service';import{GlobalResourceService} from '../global-resource/global-resource.service';
import{Resource} from '../model/resource';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-batchtype',
  templateUrl: './batchtype.component.html',
  styleUrls: ['./batchtype.component.css']
})
export class BatchTypeComponent implements OnInit {
  
  public view: Observable<GridDataResult>;
  public gridDatas: BatchTypeInfo[];
  public batchInfo:BatchTypeInfo;
  public resource:Resource;
  public pageSize : number = this.commonService.defaultPageSize();
  
  constructor(private activatedRoute: ActivatedRoute, 
    private router: Router, 
    private batchTypeService: BatchTypeService, 
    private modalService: NgbModal,
    private toster: TosterService,   
    private formBuilder: FormBuilder,
	private globalResourceService:GlobalResourceService, private commonService:CommonService   ) { }

 

  ngOnInit() {    
    this.resource=this.globalResourceService.getGlobalResources(); 
    this.getBatches();
   
  }
  

  addUpdateBatch(info) {
    // let errorMessage: string = "";
    // if(this.batchInfo.priority == null){
    //   errorMessage += this.getResourceValue("PriorityIsRequired")+"</br>"
    //   }

    //   if(this.batchInfo.idleTime == null){
    //     errorMessage +=this.getResourceValue("IdleTimeIsRequired")+"</br>"
    //     }  

    // if (errorMessage != "") {
    //   this.toster.showError(errorMessage);
    //   return;
    // }


    if(info.batchTypeId=="00000000-0000-0000-0000-000000000000")
    {
      this.addBatch(info);
    }else{
      this.updateBatch(info);
    }
 
  }

  addUpdateStatusBatch(info)
  {
    if(info.batchTypeId=="00000000-0000-0000-0000-000000000000")
    {
      info.status=true;
      this.addBatch(info);
    }else{
      this.updateStatus(info.batchTypeId);
    }
 
  }

  addBatch(batchInfo){ 
    this.batchTypeService.addBatch(batchInfo).pipe(first()).subscribe(
      data => {   
        if (data) {
          this.getBatches();          
          this.toster.showSuccess(this.getResourceValue("BatchEnabledSuccessfully")); 
        }
      },
      error => {
        console.log(error);
      });
}

updateBatch(batchInfo){ 
  this.batchTypeService.updateBatch(batchInfo).pipe(first()).subscribe(
    data => {   
      if (data) {
        this.getBatches();          
        this.toster.showSuccess(this.getResourceValue("BatchUpdatedSuccessfully")); 
        this.modalService.dismissAll();
      }
    },
    error => {
      console.log(error);
    });
}

updateStatus(batchTypeId){ 
  this.batchTypeService.updateStatus(batchTypeId).pipe(first()).subscribe(
    data => {   
      if (data) {
        this.getBatches();          
        this.toster.showSuccess(this.getResourceValue("BatchUpdatedSuccessfully")); 
      }
    },
    error => {
      console.log(error);
    });
}

deleteBatch(batchTypeId){
  this.batchTypeService.deleteBatch(batchTypeId).pipe(first()).subscribe(
    data => {   
      if (data) {
        this.getBatches();          
        this.toster.showSuccess(this.getResourceValue("BatchDisabledSuccessfully")); 
      }
    },
    error => {
      console.log(error);
    });
}

updateBatchItemNextRunTime()
{
  this.batchTypeService.updateBatchItemNextRunTime().pipe(first()).subscribe(
    data => {   
      if (data) {            
        this.toster.showSuccess(this.getResourceValue("BatchUpdatedNextRunnTime")); 
      }
    },
    error => {
      console.log(error);
    });
}
 

  private getBatches() {
    this.batchTypeService.getBatches().pipe(first()).subscribe( data => {       
          if (data) {
            //console.log('data ', data);
           this.gridDatas = data;       
          }
        },
        error => {
          console.log(error);
        });
  }

  openUpdatePopUp(info,content)
  {
    this.batchInfo=info;
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };
    this.modalService.open(content,ngbModalOptions);

  }

  closePopUp()
  {
    this.modalService.dismissAll();
  }
 
  openScheduler(batchTypeId,content)
  {
    const modalRef = this.modalService.open(SchedulerComponent, { size: 'lg' });
    modalRef.componentInstance.batchId = batchTypeId;
    modalRef.componentInstance.title = this.getResourceValue("AddProcess");
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
