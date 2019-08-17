import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { MetadataService } from '../metadata.service';
import { Entities } from '../../model/entities';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalOperationProcessComponent } from '../workflow/modal-operationprocess/modal-operationprocess.component';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-taskoperation',
  templateUrl: './taskoperation.component.html',
  styleUrls: ['./taskoperation.component.css']
})
export class TaskoperationComponent implements OnInit {
  private entity: Entities;
  public gridData: any = this.entity;
  public operationList = [];
  public resource: Resource;
  public name: string;

  constructor(
    private route: ActivatedRoute,
    private metadataService: MetadataService,
    private modalService: NgbModal,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.route.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
    });

    this.getResource();
  }

  private getMetadataByName(name) {


    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      if (data.operations) {
        if (data.operations.length > 0) {
          data.operations.forEach(value => {
            //Delete this later
            this.deleteThis_Function_WhenOperation_Class_getfixed(value);
            value.type = "Operation";
            value.isOperation = true;
            this.operationList.push(value);
          });
        }

      }
      if (data.tasks) {
        if (data.tasks.length > 0) {
          data.tasks.forEach(value => {
            this.operationList.push(value);
          });
        }
      }

     // console.table(this.operationList);
      this.gridData = this.operationList;
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              if (data.operations) {
                if (data.operations.length > 0) {
                  data.operations.forEach(value => {
                    //Delete this later
                    this.deleteThis_Function_WhenOperation_Class_getfixed(value);
                    value.type = "Operation";
                    value.isOperation = true;
                    this.operationList.push(value);
                  });
                }
  
              }
              if (data.tasks) {
                if (data.tasks.length > 0) {
                  data.tasks.forEach(value => {
                    this.operationList.push(value);
                  });
                }
              }
  
              console.table(this.operationList);
              this.gridData = this.operationList;              
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

    //       if (data && data) {
    //         if (data.operations) {
    //           if (data.operations.length > 0) {
    //             data.operations.forEach(value => {
    //               //Delete this later
    //               this.deleteThis_Function_WhenOperation_Class_getfixed(value);
    //               value.type = "Operation";
    //               value.isOperation = true;
    //               this.operationList.push(value);
    //             });
    //           }

    //         }
    //         if (data.tasks) {
    //           if (data.tasks.length > 0) {
    //             data.tasks.forEach(value => {
    //               this.operationList.push(value);
    //             });
    //           }
    //         }

    //         console.table(this.operationList);
    //         this.gridData = this.operationList;
    //       }

    //     },
    //     error => {
    //       console.log(error);
    //     });
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getMetadataByName(this.name);
  }

 

  addOperationProcess(operationInfo) {
    const modalRef = this.modalService.open(ModalOperationProcessComponent, { size: 'lg' });
    modalRef.componentInstance.title = this.getResourceValue("metadata_workflow_processconfiguration");
    modalRef.componentInstance.entityName = this.name;
    modalRef.componentInstance.operationName = operationInfo.name;
    modalRef.componentInstance.operationType = operationInfo.operationType;
  }

  deleteThis_Function_WhenOperation_Class_getfixed(value) {
    if (value.name == "Create") {
      value.operationType = 1;
    }
    if (value.name == "Delete") {
      value.operationType = 3;
    }
    if (value.name == "Update") {
      value.operationType = 2;
    }
    if (value.name == "UpdateStatus") {
      value.operationType = 4;
    }



  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }



}
