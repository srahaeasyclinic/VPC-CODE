import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../model/layoutmodel';
import { LayoutService } from '../../meta-data/layout/layout.service';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { EntityValueService } from '../entityValue.service';
import { TosterService } from '../../services/toster.service';
import { ResourceService } from '../../services/resource.service';
import { Data } from '../../services/storage.data';
import { CommonService } from 'src/app/services/common.service';
import { LogService } from 'src/app/services/log.service';
import { ValidationService } from 'src/app/services/validation.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { WorkFlowService } from '../../meta-data/workflow/workflow.service';
import { GlobalResourceService } from '../../global-resource/global-resource.service';

@Component({
  selector: 'app-general-ui-edit',
  templateUrl: './GeneralUiEdit.component.html',
  styleUrls: ['./GeneralUiEdit.component.css']
})
export class GeneralUiEditComponent implements OnInit {
  //public layoutInfo: LayoutModel = new LayoutModel();
  public layoutInfo: LayoutModel;
  public entityInfo: any;
  public tree: ITreeNode;
  public selectedTreeNode: ITreeNode | null;
  public isTreeReady: boolean = false;
  public transitObject: any;
  public isPreviousIdAvailable: boolean = false;
  public isNexIdAvailable: boolean = false;
  public possibleSteps = [];
  private resource: any;
  private entityName: string;
  private layoutType: string = "Form";
  private layoutContext: string = "Edit";
  private layoutSubType: string = '';
  public validateMessages: Array<string> = [];
  currentUserWorkFlowInfo: any;

  public displayRule: any;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private resourceService: ResourceService,
    private toster: TosterService,
    private entityValueService: EntityValueService,
    private data: Data,
    private commonService: CommonService,
    private validateService: ValidationService,
    private workFlowService: WorkFlowService,
    private modalService: NgbModal, private logService: LogService,
    private globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.getResource();
    this.processUrl();
  }

  // Method sections
  private getResource(): void {
    //console.log("resource found", data);
    //this.resource =this.resourceService.getResources();
    this.resource = this.globalResourceService.getGlobalResources();
  }



  private getRuleList(entityName: string): void {

    this.commonService.getRuleList(entityName)
      .pipe(first())
      .subscribe(
        data => {
          //console.log('getRuleList GUIE ', data);
          if (data && data) {
            this.displayRule = data.retVal;
            if (this.displayRule) {
              this.displayRule.map(function (i) { return i["source"] = i.sourceList.map(function (e) { return e.name }).join() });
              this.displayRule.map(function (i) { return i["target"] = i.targetList.map(function (t) { return t.name }).join() });
            }
          }
        },
        error => {
          console.log(error)
        }
      );
  }


  private processUrl(): void {
    this.activatedRoute.parent.params.subscribe((urlPath) => {
      this.entityName = urlPath["name"];
      this.activatedRoute.queryParams
        .filter(params => params.subType)
        .subscribe(params => {
          this.layoutSubType = params.subType;
          this.getPreviousNextData();
          if (this.entityName && this.layoutSubType) {
            this.getDefaultLayout(this.entityName, this.layoutType, this.layoutSubType, this.layoutContext);
          } else {
            this.toster.showWarning('Url tempered! or no entity name found!');
          }
        });
    });
    this.getRuleList(this.entityName);
  }

  private getDefaultLayout(name: string, type: string, subtype: string, context: string): void {
    this.layoutService.getDefaultLayout(name, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutInfo = new LayoutModel();
            this.layoutInfo = data;
            this.activatedRoute.params.subscribe((params: Params) => {
              let entityValueId = params['id'];
              if (entityValueId) {
                this.getEntityDetails(this.entityName, entityValueId);
              }
            });
          }
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }

  private getEntityDetails(entityName: string, entityValueId: string) {
    this.entityValueService.getEntityDetails(entityName, entityValueId, this.layoutSubType)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.entityInfo = data;
            this.getCurrentUserWorkflows()
            //  this.getWorkflows();
            //console.log("this.entityInfo ====================================================== ", this.entityInfo);
            if (this.entityInfo && this.layoutInfo && this.layoutInfo.formLayoutDetails) {
              //console.log("this.layoutInfo.formLayoutDetails.fields", this.layoutInfo.formLayoutDetails.fields);
              //console.log("this.entityInfo", this.entityInfo);
              this.mapData(this.layoutInfo.formLayoutDetails.fields, this.entityInfo);
              this.tree = this.layoutInfo.formLayoutDetails;
              this.isTreeReady = true;
              //console.log("after mapping tree is ", this.tree);
            }
          }
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }

  private getCurrentUserWorkflows() {
    this.workFlowService.getCurrentUserWorkflows(this.entityName)
      .pipe(first()).subscribe(
        data => {
          if (data) {
            this.currentUserWorkFlowInfo = data;
          }
          this.getWorkflows();
        },
        error => {
          console.log(error);
        });

  }

  private getWorkflows() {
    this.possibleSteps = [];
    this.workFlowService.getWorkFlows(this.entityName).pipe(first()).subscribe(
      data => {
        if (data) {

          data.forEach(workFlow => {
            if (workFlow.subTypeCode === this.layoutSubType) {
              workFlow.steps.forEach(step => {
                if (step.transitionType.id === this.entityInfo.currentWorkFlowStep) {
                  //info.innerSteps=[];

                  step.innerSteps.forEach(innerStep => {
                    //Check valid transition assigned to user
                    var curentWorkFlow = this.currentUserWorkFlowInfo.filter(currentworkflow => currentworkflow.workFlowId === workFlow.workFlowId);
                    if (curentWorkFlow != null && curentWorkFlow.length > 0) {
                      var curentWorkFlowSteps = curentWorkFlow[0].steps.filter(allValidStep => allValidStep.transitionType.id === innerStep.transitionType.id);
                      if (curentWorkFlowSteps != null && curentWorkFlowSteps.length > 0) {
                        //Assign is mandatory or not
                        innerStep.isAssigmentMandatory = curentWorkFlowSteps[0].isAssigmentMandatory;

                        //Assign To user list according to role
                        var assignToThisRoles = curentWorkFlowSteps[0].roles.filter(assignToThisRole => assignToThisRole.assignmentOperationType === 3);
                        if (assignToThisRoles.length > 0) {
                          innerStep.roles = assignToThisRoles;
                        }
                        //-------------------

                        this.possibleSteps.push({
                          refId: this.entityInfo.internalId,
                          entityName: this.entityName,
                          subTypeName: workFlow.subTypeCode,
                          currentTransitionType: this.entityInfo.currentWorkFlowStep,
                          nextTransitionType: innerStep.transitionType.id,
                          nextTransitionName: innerStep.transitionType.name
                        })

                        //info.innerSteps.push(innerStep);
                      }
                    }
                  });
                }
              });
            }
          });


          //  data.forEach(workFlow => {
          //    if (workFlow.subTypeCode === this.layoutSubType) {
          //      workFlow.steps.forEach(step => {
          //        if (step.transitionType.id === this.entityInfo.currentWorkFlowStep) {
          //          //this.possibleSteps = step.innerSteps;        
          //          step.innerSteps.forEach(innerStep => {              
          //            workFlow.steps.forEach(allStep => {
          //              if (innerStep.transitionType.id === allStep.transitionType.id) { 
          //                  this.possibleSteps.push({
          //                    refId:this.entityInfo.internalId,
          //                    entityName:this.entityName,
          //                    subTypeName: workFlow.subTypeCode,
          //                    currentTransitionType:this.entityInfo.currentWorkFlowStep,
          //                    nextTransitionType:innerStep.transitionType.id,
          //                    nextTransitionName:innerStep.transitionType.name
          //                  })
          //              }
          //            });
          //          });
          //        }
          //      });
          //    }
          //  });
        }
      },
      error => {
        console.log(error);
      });
  }

  // private getWorkflows() {
  //    this.possibleSteps=[];
  //   this.workFlowService.getWorkFlows(this.entityName).pipe(first()).subscribe(
  //       data => {          
  //         if (data) {
  //           data.forEach(workFlow => {
  //             if (workFlow.subTypeCode === this.layoutSubType) {
  //               workFlow.steps.forEach(step => {
  //                 if (step.transitionType.id === this.entityInfo.currentWorkFlowStep) {
  //                   //this.possibleSteps = step.innerSteps;        
  //                   step.innerSteps.forEach(innerStep => {              
  //                     workFlow.steps.forEach(allStep => {
  //                       if (innerStep.transitionType.id === allStep.transitionType.id) { 
  //                           this.possibleSteps.push({
  //                             refId:this.entityInfo.internalId,
  //                             entityName:this.entityName,
  //                             subTypeName: workFlow.subTypeCode,
  //                             currentTransitionType:this.entityInfo.currentWorkFlowStep,
  //                             nextTransitionType:innerStep.transitionType.id,
  //                             nextTransitionName:innerStep.transitionType.name
  //                           })
  //                       }
  //                     });
  //                   });
  //                 }
  //               });
  //             }
  //           });
  //         }      
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // } 
  //refId:internalId,entityName:entityName,subTypeName:entitySubTypeName,currentTransitionType:currentWorkFlowTransitionId,nextTransitionType:nextTransitioinStepId

  private mapData(fields, whichObject) {
    fields.forEach((item, index) => {
      Object.keys(whichObject).forEach(function (key, index) {
        if (key.toLocaleLowerCase() == item.name.toLocaleLowerCase()) {
          item.value = whichObject[key];
        }
      });
      if (item.fields != null && item.fields.length > 0) {
        this.mapData(item.fields, whichObject);
      }
      if (item.tabs != null && item.tabs.length > 0) {
        this.mapData(item.tabs, whichObject);
      }
    });

  }


  private getPreviousNextData(): void {
    if (this.data) {
      //Get the data for the previous next operation
      this.transitObject = this.data.storage;

      let recordNo: number = 0;
      let currentPage: number = 0;
      currentPage = this.transitObject.pageIndex - 1;

      if (!this.transitObject.recordNo) {
        recordNo = (currentPage * 10) + (this.transitObject.itemIndex) + 1;
        this.transitObject.recordNo = recordNo;
      }
      else {
        recordNo = this.transitObject.recordNo;
      }

      this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
      this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

    }
  }


  private getEntityValueId(): string {
    let entityValueId: string = '';
    this.activatedRoute.params.subscribe((params: Params) => {
      entityValueId = params['id'];
    });
    return entityValueId;
  }

  private getQuery(): string {
    // "ItemName,Code,OrgNo,IsLegalEntity,IsOrganization,UpdatedOn,SubType,TenantType,Active&searchText=qua&pageIndex=1&pageSize=10&filters=Active,1"

    let pagesize: number = 1;
    let maxresult: number = 1;
    let query: string = '';

    if (this.transitObject && this.transitObject.fields) {
      query = this.transitObject.fields + '&';
    }

    // if (this.transitObject && this.transitObject.freetextsearch) {
    //   query += 'searchText=' + this.transitObject.freetextsearch + '&';
    // }

    if (this.transitObject && this.transitObject.searchText) {
      query += this.transitObject.searchText + '&';
    }

    // if (this.transitObject && this.transitObject.itemIndex) {
    //   query += 'pageIndex=' + this.transitObject.itemIndex + '&';
    // }
    if (this.transitObject.recordNo) {
      query += 'pageIndex=' + this.transitObject.recordNo + '&';
    }

    query += 'pageSize=' + pagesize + '&';

    if (this.transitObject && this.transitObject.filters) {
      query += 'filters=' + this.transitObject.filters + '&';
    }

    if (this.transitObject && this.transitObject.orderBy) {
      query += 'orderBy=' + this.transitObject.orderBy + '&';
    }

    query += 'maxResult=' + maxresult;

    return query;

  }
  // Events




  redirectToListPageWithError(): void {
    this.toster.showError(this.entityName + ' relation not created');
    this.router.navigate(['ui/' + this.entityName]);
  }

  private redirectToListPage() {
    this.toster.showSuccess(this.entityName + ' updated successfully.');
    this.router.navigate(['ui/' + this.entityName]);
  }

  public nextData(): void {
    if (this.transitObject && this.transitObject.fields) {

      this.transitObject.recordNo = this.transitObject.recordNo + 1;
      this.transitObject.nextClick = true;

      let query: string = this.getQuery();
      this.entityValueService.getEntityValues(this.entityName, query)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result.length > 0) {
                let id = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

                //this.getEntityDetails(this.entityName, id);

                this.data.storage = this.transitObject;
                //this.router.navigate(["ui/" + this.entityName + "/edit/" + id], { queryParams: { subType: this.layoutSubType } });
                this.router.navigate(["../../edit", id], { queryParams: { subType: this.layoutSubType }, relativeTo: this.activatedRoute });
              }

            }
          },
          error => {
            if (error.status === 501) {
              this.toster.showError(error.message);
            }

          });
    }
  }

  public previousData(): void {
    if (this.transitObject && this.transitObject.fields && this.transitObject.orderBy) {

      this.transitObject.recordNo = this.transitObject.recordNo - 1;
      this.transitObject.previousClick = true;

      let query: string = this.getQuery();
      this.entityValueService.getEntityValues(this.entityName, query)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result.length > 0) {
                let id = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

                //this.getEntityDetails(this.entityName,id);
                this.data.storage = this.transitObject;
                //this.router.navigate(["ui/" + this.entityName + "/edit/" + id], { queryParams: { subType: this.layoutSubType } });
                this.router.navigate(["../../edit", id], { queryParams: { subType: this.layoutSubType }, relativeTo: this.activatedRoute });
              }
            }
          },
          error => {
            if (error.status === 501) {
              this.toster.showError(error.message);
            }
          });
    }
  }



  public update(content: string): string {

    // console.log('Tree update '+JSON.stringify(this.tree.fields));
    this.validateMessages = [];
    this.validateMessages = this.validateService.validate(this.tree.fields);
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    if (this.validateMessages.length > 0) {
      this.modalService.open(content, ngbModalOptions);
      return
    }
    let value: any = {};
    value = this.commonService.createKeyValue(this.tree.fields, value);
     //console.log('Tree value '+JSON.stringify(value));
    //console.log('TestJSON '+JSON.stringify(value));
    let id = this.getEntityValueId();
    this.entityValueService.updateEntityValues(this.entityName, value, id, this.layoutSubType)
      .pipe(first())
      .subscribe(
        data => {
          this.redirectToListPage();
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }
  generateResourceName(word: string) {
    return this.commonService.generateResourceNameWithHierarchy(word);
  }


  public onToolBarWorkFlowClick(transitionWapper): void {
    this.workFlowService.managerTransition(transitionWapper).pipe(first()).subscribe(
      data => {
        this.toster.showSuccess('Step changed successfully.');
        this.processUrl();
      },
      error => {
        console.log(error);
      });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
