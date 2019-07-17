import { Component, OnInit, ViewChild, ElementRef, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, NavigationExtras, Params, NavigationEnd } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';
import swal from 'sweetalert2';
import { NgbModal, NgbModalOptions, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { LayoutModel } from '../../model/layoutmodel';
import { LayoutService } from '../../meta-data/layout/layout.service';
import { TosterService } from '../../services/toster.service';
import { CommonService } from '../../services/common.service';
import { ResourceService } from '../../services/resource.service';
import { Data } from '../../services/storage.data';
import { EntityValueService } from '../entityValue.service';
import { WorkFlowService } from '../../meta-data/workflow/workflow.service';
import { MODALS } from '../../dynamic-form-builder/tree.config';
import { LogService } from 'src/app/services/log.service';
import {GlobalResourceService} from '../../global-resource/global-resource.service';
@Component({
  selector: 'app-general-ui-list',
  templateUrl: './general-ui-list.component.html',
  styleUrls: ['./general-ui-list.component.css'],
  inputs: ['entityName', 'displaysearchQueryString', 'layoutType', 'mode', 'resourceData', 'defaultLayoutData']
})
export class GeneralUiListComponent implements OnInit, OnChanges {

 // @ViewChild('contentExchange') modalRef: ElementRef;
  //@Input() entityName;

  @Input() mode: Number;

  constructor(private router: Router,
    private toster: TosterService,
    private resourceService: ResourceService,
    private commonService: CommonService,
    private route: ActivatedRoute,
    private data: Data,
    private layoutService: LayoutService,
    private entityValueService: EntityValueService,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private workFlowService: WorkFlowService,
    private globalResourceService:GlobalResourceService,
    ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
  }


  public view: Observable<GridDataResult>;
  public defaultLayout: LayoutModel = new LayoutModel();
  public results: any;
  public freetextsearchChanged: Subject<string> = new Subject<string>();
  public actions: Array<any>;
  public gridData: any;
  public gridDataResult: GridDataResult;
  public multiple = false;
  public allowUnsort = true;
  public freetextsearch: string;
  public columns = [];
  public sort: SortDescriptor[];
  public skip = 0;
  public isExpanded: boolean = false;
  public toolbarButtons = [];
  public sendToButtons = [];
  public printButtons = [];
  public dateFormat = this.commonService.defaultDateformat();
  public groups: GroupDescriptor[];
  public subTypes = [];
  public modalReference: any;
  public selectLayoutForm: FormGroup;
  public selectedSubType: string;
  workFlowInfo: any;
  currentUserWorkFlowInfo: any;



  private entityName: string = '';
  private layoutType: string = 'List'; // List page
  public pageindex: number = 1;
  private pageSize: number = 10;
  public totalRecords: number = 0;
  public resource: any;
  private orderBy: string = '';
  public storage: any;
  public filters: string = '';
  public selectedFields: string = '';

  private displaysearchQueryString: string = '';

  private freesearchQuery: string = '';
  private simplesearchQuery: string = '';
  private parentEntity: string = "";
  private parentId: string = "";
  private subType: string = "";
  public resourceData: any;
  public sortingOrder: string;
  public defaultLayoutData: LayoutModel = new LayoutModel();

  ngOnChanges() {
    //console.log('ngOnChanges GUIL :: '+this.displaysearchQueryString);
    this.generateListlayout(this.defaultLayout, this.entityName);
  }

  ngOnInit() {

    this.selectLayoutForm = this.formBuilder.group({ drpSubType: '' });
    //console.log('ngOnInit called in app-general-ui-list');

    //this.getResource();

    if (this.resourceData) {
      this.resource = this.resourceData;

      if (this.entityName == null || this.entityName == "") {
        this.route.parent.params.subscribe((urlPath) => {
          this.entityName = urlPath["name"];
        });
      }

      //console.log('getResource called with '+this.entityName);
      this.pageindex = 1;
      this.skip = 0;
      this.freetextsearch = '';

      this.getCurrentUserWorkflows();
    }
    else {
      this.getResource();
    }

    this.freetextsearchChanged
      .pipe(debounceTime(500), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;
        this.generateListlayout(this.defaultLayout, this.entityName);
      });

    this.subType = this.route.snapshot.queryParams["subType"];
  }

  /*
    public callFreeTextSearch(argEvent): void{
      //console.log('general-ui-list.component callFreeTextSearch called '+argEvent);
      this.onFreeTextSearch(argEvent);
    }
  */
  // for generate list

  public onFreeTextSearch(freesearchQuery: string): void {
    this.freetextsearchChanged.next(freesearchQuery);
    this.pageindex = 1;
  }
  // for generate list
  public onSimpleSearch(): void {
    this.generateListlayout(this.defaultLayout, this.entityName);
    this.pageindex = 1;
  }

  //for common data list
  public onActionClick(event): void {
    //console.log(actionName);
    //console.log(id); 
    if (event.actionName.toLowerCase() === 'Delete'.toLowerCase()) {
      swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        showLoaderOnConfirm: true,
      })
        .then((willDelete) => {
          if (willDelete.value) {
            this.entityValueService.deleteEntityValue(this.entityName, event.internalId).subscribe(result => {
              if (result) {
                this.generateListlayout(this.defaultLayout, this.entityName)
              }
            });

          } else {
            //write the code for cancel click
          }

        });
    }

    if (event.actionName.toLowerCase() == 'UpdateStatus'.toLowerCase()) {
      this.toster.showWarning('Method not implemented !');
    }

  }
  //WorkFlow step change

  public onActionWorkFlowClick(transitionWapper): void { 
    
    transitionWapper.entityName=this.entityName;
    var roleIds=[];
    if(transitionWapper.innerStep.roles)  
    {      
      transitionWapper.innerStep.roles.forEach(role => {
         roleIds.push(role.roleId);
      });
    }

    if(roleIds.length>0)
    {
      
    }
    delete transitionWapper.currentTransitionType;

    this.workFlowService.managerTransition(transitionWapper).pipe(first()).subscribe(
      data => {
        this.toster.showSuccess('Step changed successfully.');
        this.generateListlayout(this.defaultLayout, this.entityName);       
      },
      error => {
        console.log(error);
      });
  }

  public edit(obj: any): void {
    if (this.layoutType == "List") {
      this.nevigateToUrl(obj);
    } else {
      //console.log('dddd');
      if (obj != null && obj.internalId != null) {
        this.openDetailsEntityCreatePopup(obj.internalId);
      }
    }
  }

  private nevigateToUrl(obj: any) {
    //this.gridData
    let currentIndex: number = 0;
    let subType: string = "";
    this.gridData.forEach((item, index) => {
      if (item.internalId.toLowerCase() == obj.internalId.toLowerCase()) {
        currentIndex = index;
        subType = item.subType;
      }
    });


    let transitObject = {
      name: this.entityName,
      fields: this.selectedFields,
      //searchText: this.freetextsearch,
      searchText: this.displaysearchQueryString,
      orderBy: this.orderBy,
      filters: this.filters,
      pageIndex: this.pageindex,
      pageSize: this.pageSize,
      itemIndex: currentIndex,
      totalRecords: this.totalRecords
    };
    this.data.storage = transitObject;
    // var currentUrl = this.router.url;
    //  var url =  "./preview/" + obj.internalId;
    // console.log(url);
    //this.router.navigate(["preview", obj.internalId], { relativeTo: this.route, queryParams: { subType: subType } });

    // this.router.navigate([currentUrl + "/preview/" + obj.internalId], { queryParams: { subType: subType } });

    this.router.navigate(["./preview", obj.internalId], { queryParams: { subType: subType }, relativeTo: this.route });
  }

  public onSubTypeChange(value) {
    if (value) {
      this.selectedSubType = value;
    }
  }

  public navigate(): void {
    this.modalReference.close();
    // this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: this.selectedSubType } });

    this.router.navigate(["../../new"], { queryParams: { subType: this.selectedSubType }, relativeTo: this.route });
  }

  //for common data list
  // public onGridChangeEvent(event) {

  //   //this.isDataReady = false;
  //   this.totalRecords = 0;
  //   let maxResult: number;
  //   let isvalid: boolean = true;
  //   //let selectedFields: string = '';

  //   this.pageindex = event.pageIndex;
  //   this.pageSize = event.pageSize;

  //   maxResult = this.defaultLayout.listLayoutDetails.maxResult;

  //   if (this.defaultLayout.listLayoutDetails) {
  //     if (this.defaultLayout.listLayoutDetails.fields || this.defaultLayout.listLayoutDetails.fields.length > 0) {

  //       this.defaultLayout.listLayoutDetails.fields = this.defaultLayout.listLayoutDetails.fields.sort().sort(function (a, b) {
  //         return a.sequence - b.sequence;
  //       });

  //       this.selectedFields = '';

  //       this.defaultLayout.listLayoutDetails.fields.forEach((item, index) => {
  //         if (!this.selectedFields) {
  //           this.selectedFields = item.name;
  //         }
  //         else {
  //           this.selectedFields += ',' + item.name;
  //         }
  //       });

  //       if (this.defaultLayout.listLayoutDetails.searchProperties && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
  //         this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
  //           element.properties.forEach(prop => {
  //             if (prop.value != null) {
  //               this.filters += prop.name + ',' + prop.value + '|';
  //             }
  //           });
  //         });

  //         if (this.filters != "") {
  //           this.filters = this.filters.substring(0, this.filters.length - 1);
  //         }
  //       }


  //       if (isvalid) {
  //         let query: string = '';
  //         query = this.getQuery();

  //         this.entityValueService.getEntityValues(this.entityName, query)

  //           .pipe(first())
  //           .subscribe(
  //             data => {
  //               if (data && data) {

  //                 this.results = [];
  //                 this.results = data.result;
  //                 this.skip = event.skip;
  //                 this.pageindex = event.pageIndex;
  //                 this.pageSize = event.pageSize;

  //                 if (event.groupBy && event.groupBy.length > 0) {
  //                   this.groups = event.groupBy;
  //                 } else {
  //                   var emptyGroup: GroupDescriptor[] = [];
  //                   this.groups = emptyGroup;
  //                   this.defaultLayout.listLayoutDetails.defaultGroupBy = "";
  //                 }

  //                 if (event.sort) {
  //                   this.sort = event.sort;
  //                 }
  //                 this.totalRecords = 0;
  //                 this.totalRecords = data.totalRow;
  //                 this.gridData = this.results;

  //               }
  //             },
  //             error => {
  //               console.log(error);
  //             });
  //       } else {
  //         //No layout found
  //       }
  //     } else {
  //       isvalid = false;
  //       this.toster.showWarning('No fields found !');
  //     }
  //   }
  // }

  public onGridChangeEvent(event) {

    //this.isDataReady = false;
    this.totalRecords = 0;

    let maxResult: number;
    let isvalid: boolean = true;
    //let selectedFields: string = '';

    this.pageindex = event.pageIndex;
    this.pageSize = event.pageSize;

    var targetDetails = null;

    if (this.layoutType == "List") {
      maxResult = this.defaultLayout.listLayoutDetails.maxResult;
      targetDetails = this.defaultLayout.listLayoutDetails;
    }
    else {
      targetDetails = this.defaultLayout.viewLayoutDetails;
    }

    if (targetDetails) {
      if (targetDetails.fields || targetDetails.fields.length > 0) {

        targetDetails.fields = targetDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });

        this.selectedFields = '';

        targetDetails.fields.forEach((item, index) => {
          if (!this.selectedFields) {
            this.selectedFields = item.name;
          }
          else {
            this.selectedFields += ',' + item.name;
          }
        });

        if (targetDetails.searchProperties && targetDetails.searchProperties.length > 0) {
          targetDetails.searchProperties.forEach(element => {
            element.properties.forEach(prop => {
              if (prop.value != null) {
                this.filters += prop.name + ',' + prop.value + '|';
              }
            });
          });

          if (this.filters != "") {
            this.filters = this.filters.substring(0, this.filters.length - 1);
          }
        }

        if (isvalid) {
          this.loadData(this.entityName);
        }

      }
    }

    maxResult = this.defaultLayout.listLayoutDetails.maxResult;

    if (this.defaultLayout.listLayoutDetails) {
      if (this.defaultLayout.listLayoutDetails.fields || this.defaultLayout.listLayoutDetails.fields.length > 0) {

        this.defaultLayout.listLayoutDetails.fields = this.defaultLayout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });

        this.selectedFields = '';

        this.defaultLayout.listLayoutDetails.fields.forEach((item, index) => {
          if (!this.selectedFields) {
            this.selectedFields = item.name;
          }
          else {
            this.selectedFields += ',' + item.name;
          }
        });

        if (this.defaultLayout.listLayoutDetails.searchProperties && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
          this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
            element.properties.forEach(prop => {
              if (prop.value != null) {
                this.filters += prop.name + ',' + prop.value + '|';
              }
            });
          });

          if (this.filters != "") {
            this.filters = this.filters.substring(0, this.filters.length - 1);
          }
        }

        //this section is closed to prevent multiple call to database
        // if (isvalid) {
        //   let query: string = '';
        //   query = this.getQuery();

        //   this.entityValueService.getEntityValues(this.entityName, query)

        //     .pipe(first())
        //     .subscribe(
        //       data => {
        //         if (data && data) {

        //           this.results = [];
        //           this.results = data.result;
        //           this.skip = event.skip;
        //           this.pageindex = event.pageIndex;
        //           this.pageSize = event.pageSize;

        //           if (event.groupBy && event.groupBy.length > 0) {
        //             this.groups = event.groupBy;
        //           } else {
        //             var emptyGroup: GroupDescriptor[] = [];
        //             this.groups = emptyGroup;
        //             this.defaultLayout.listLayoutDetails.defaultGroupBy = "";
        //           }

        //           if (event.sort) {
        //             this.sort = event.sort;
        //           }
        //           this.totalRecords = 0;
        //           this.totalRecords = data.totalRow;
        //           this.gridData = this.results;

        //         }
        //       },
        //       error => {
        //         console.log(error);
        //       });
        // } else {
        //   //No layout found
        // }
      } else {
        isvalid = false;
        this.toster.showWarning('No fields found !');
      }
    }
    console.log('onGridChangeEvent');
  }

  //Methods section
  private getResource(): void {
            this.resource = this.globalResourceService.getGlobalResources();
            //this.entityName is a property binded in the selector of general-ui-list

            if (this.entityName == null || this.entityName == "") {
              this.route.parent.params.subscribe((urlPath) => {
                this.entityName = urlPath["name"];
              });
            }
            this.pageindex = 1;
            this.skip = 0;
            this.freetextsearch = '';
            this.getCurrentUserWorkflows();            
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
    this.workFlowService.getWorkFlows(this.entityName)
      .pipe(first()).subscribe(
        data => { 
          if (data) {
            this.workFlowInfo = data;
          }

          if (this.defaultLayoutData && this.defaultLayoutData.listLayoutDetails) {
            this.defaultLayout = this.defaultLayoutData;

            //Simple search or advance search
            if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
              this.defaultLayout.listLayoutDetails.searchProperties.forEach((result, index) => {
                if (result.name === 'SimpleSearch' || result.name === 'AdvanceSearch') {
                  result.properties.forEach((prop, index) => {
                    if (prop.values) {
                      prop.values = prop.values.sort(function (a, b) {
                        if (a.value < b.value) { return -1; }
                        if (a.value > b.value) { return 1; }
                        return 0;
                      })

                      prop.value = prop.defaultValue;
                    }
                  });
                }
              });
            }

            //generate the default orderby 
            if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.defaultSortOrder) {
              this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
              if (!this.sort)
                this.sort = [];

              this.sort.length = 0;
              this.sort.push({ dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
            }

            //this.renderToolbar();

            this.generateListlayout(this.defaultLayout, this.entityName);
          }
          else {
            this.getDefaultLayout(this.entityName, this.layoutType, '', '');
          }


        },
        error => {
          console.log(error);
        });
  }


  private getDefaultLayout(entityName: string, type: string, subtype: string, context: string): void {
    this.layoutService.getDefaultLayout(entityName, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.defaultLayout = data;

            //Simple search or advance search
            if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
              this.defaultLayout.listLayoutDetails.searchProperties.forEach((result, index) => {
                if (result.name === 'SimpleSearch' || result.name === 'AdvanceSearch') {
                  result.properties.forEach((prop, index) => {
                    if (prop.values) {
                      prop.values = prop.values.sort(function (a, b) {
                        if (a.value < b.value) { return -1; }
                        if (a.value > b.value) { return 1; }
                        return 0;
                      })

                      prop.value = prop.defaultValue;
                    }
                  });
                }
              });
            }

            //generate the default orderby 
            if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.defaultSortOrder) {
              this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
              if (!this.sort)
                this.sort = [];

              this.sort.length = 0;
              this.sort.push({ dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
            }

            //console.log('getDefaultLayout');
            //this.renderToolbar();
            this.generateListlayout(this.defaultLayout, entityName);
          }
        },
        error => {
          console.log(error);
        });
  }


  private generateListlayout(layout: LayoutModel, entityName: string): void {
    //console.log('generateListlayout GUILC', this.displaysearchQueryString);
    let isvalid: boolean = true;
    let filters: string = '';
    let maxResult: number;

    var targetDetails = null;
    if (this.layoutType == "List") {
      targetDetails = layout.listLayoutDetails;
    } else {
      targetDetails = layout.viewLayoutDetails;
    }

    if (targetDetails) {
      maxResult = targetDetails.maxResult;

      if (targetDetails.fields || targetDetails.fields.length > 0) {
        targetDetails.fields = targetDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
        this.selectedFields = '';
        targetDetails.fields.forEach((item, index) => {
          if (item.name !== 'InternalId') {
            if (!this.selectedFields) {
              this.selectedFields = item.name;
            }
            else {
              this.selectedFields += ',' + item.name;
            }
          }
        });

      } else {
        isvalid = false;
        this.toster.showWarning('No fields found !');
      }

      if (targetDetails && targetDetails.defaultGroupBy) {
        if (targetDetails.defaultGroupBy) {
          if (!this.groups) {
            var emptyGroup: GroupDescriptor[] = [];
            this.groups = emptyGroup;
          }
          //this.groups.push(defaultGroup);
          this.groups = [{ field: this.commonService.camelize(targetDetails.defaultGroupBy) }];
        }
      } else {
        if (this.groups) {
          var defaultGroup: GroupDescriptor[] = [];
          this.groups = defaultGroup;
        }
      }


      if (isvalid) {
        // let query: string = '';
        // query = this.getQuery();

        // this.entityValueService.getEntityValues(entityName, query)
        //   .pipe(first())
        //   .subscribe(
        //     data => {
        //       if (data) {
        //         this.results = [];
        //         this.results = data.result;

        //         //below values are requred for kendo grid dynamic paging
        //         this.totalRecords = data.totalRow;
        //         //this.gridDataResult = { data: this.results, total: this.totalRecords }
        //         this.gridData = this.results;
        //       }
        //     },
        //     error => {
        //       console.log(error);
        //     });

        this.loadData(entityName);
      }

    }








    ///need to comments...



    if (layout.listLayoutDetails) {

      // maxResult = layout.listLayoutDetails.maxResult;

      /*
      if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
        layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
        this.selectedFields = '';
        layout.listLayoutDetails.fields.forEach((item, index) => {
          if(item.name !== 'InternalId')
          {
            if (!this.selectedFields) {
              this.selectedFields = item.name;
            }
            else {
              this.selectedFields += ',' + item.name;
            }
          }         
        });

      } else {
        isvalid = false;
        this.toster.showWarning('No fields found !');
      }
      */



      // if (layout.listLayoutDetails.searchProperties && layout.listLayoutDetails.searchProperties.length > 0) {
      //   layout.listLayoutDetails.searchProperties.forEach(element => {
      //     element.properties.forEach(prop => {
      //       if (prop.value != null) {
      //         filters += prop.name + ',' + prop.value + '|';
      //       }
      //     });
      //   });

      //   if (filters != "") {
      //     filters = filters.substring(0, filters.length - 1);
      //   }
      // }






      //default group by set from layout = [{ field: 'Category.CategoryName' }];


      /* comment by tapash
      if (layout.listLayoutDetails && layout.listLayoutDetails.defaultGroupBy) {
        if (layout.listLayoutDetails.defaultGroupBy) {
          if (!this.groups) {
            var emptyGroup: GroupDescriptor[] = [];
            this.groups = emptyGroup;
          }
          //this.groups.push(defaultGroup);
          this.groups = [{ field: this.commonService.camelize(layout.listLayoutDetails.defaultGroupBy) }];
        }
      } else {
        if (this.groups) {
          var defaultGroup: GroupDescriptor[] = [];
          this.groups = defaultGroup;
        }
      }
      */

      // if (isvalid) {

      //   let query: string = '';
      //   query = this.getQuery();

      //   this.entityValueService.getEntityValues(entityName, query)
      //     .pipe(first())
      //     .subscribe(
      //       data => {
      //         if (data) {
      //           this.results = [];
      //           this.results = data.result;

      //           //below values are requred for kendo grid dynamic paging
      //           this.totalRecords = data.totalRow;
      //           //this.gridDataResult = { data: this.results, total: this.totalRecords }
      //           this.gridData = this.results;
      //         }
      //       },
      //       error => {
      //         console.log(error);
      //       });
      // }
    } else {
      //No layout found
      this.results = [];
      this.results.length = 0;
      this.totalRecords = 0;
      this.gridDataResult = { data: this.results, total: this.totalRecords }
      this.gridData = this.gridDataResult;
    }
  }

  private loadData(entityName: string) {
    let query: string = '';
    query = this.getQuery();
    if (this.layoutType == "List") {
      this.entityValueService.getEntityValues(entityName, query).pipe(first()).subscribe(data => {
        if (data) {
          this.results = [];
          this.results = [...data.result];
          this.results.forEach(info => {
            this.workFlowInfo.forEach(workFlow => {
              if (workFlow.subTypeCode === info.subType) {
                  workFlow.steps.forEach(step => {                  
                    if (step.transitionType.name === info.currentWorkFlowStep) {
                      info.innerSteps=[];

                      step.innerSteps.forEach(innerStep=>{
                      //Check valid transition assigned to user
                      var curentWorkFlow= this.currentUserWorkFlowInfo.filter(currentworkflow => currentworkflow.workFlowId ===workFlow.workFlowId);
                      if(curentWorkFlow !=null && curentWorkFlow.length>0)
                      {
                        var curentWorkFlowSteps= curentWorkFlow[0].steps.filter(allValidStep => allValidStep.transitionType.id ===innerStep.transitionType.id);
                        if(curentWorkFlowSteps!=null && curentWorkFlowSteps.length>0)
                        {
                          //Assign is mandatory or not
                          innerStep.isAssigmentMandatory=curentWorkFlowSteps[0].isAssigmentMandatory;
                          
                          //Assign To user list according to role
                          var assignToThisRoles= curentWorkFlowSteps[0].roles.filter(assignToThisRole => assignToThisRole.assignmentOperationType ===3);
                            if(assignToThisRoles.length>0)
                            {
                              innerStep.roles=assignToThisRoles;
                            }
                          //-------------------
                          info.innerSteps.push(innerStep);
                        }
                      }
                      });
                    }                   
                  });                        
              }
            });
          });



          //below values are requred for kendo grid dynamic paging
          this.totalRecords = data.totalRow;
          //this.gridDataResult = { data: this.results, total: this.totalRecords }
          this.gridData = [...this.results];
        }
      },
        error => {
          console.log(error);
        });
    }
    else {
      query = "?subType=" + this.subType + "&pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;

      this.route.params.subscribe((params: Params) => {
        this.parentId = params['id'];
        //this.parentEntity = params['name'];
      });

      this.route.parent.params.subscribe((urlPath) => {
        this.parentEntity = urlPath["name"];      
      });

      if (this.parentId !== undefined && this.parentId !== "" && this.subType !== undefined) {
        this.entityValueService.getDetailEntities(this.parentEntity, this.parentId, this.entityName, query)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.results = [];
                this.results = data.result;

                //below values are requred for kendo grid dynamic paging
                this.totalRecords = data.totalRow;
                //this.gridDataResult = { data: this.results, total: this.totalRecords }
                this.gridData = [...this.results];
              }
            },
            error => {
              console.log(error);
            });
      }

    }
  }

  private getQuery(): string {
    let queryString = "";


    var targetDetails = null;
    if (this.layoutType == "List") {
      targetDetails = this.defaultLayout.listLayoutDetails;
    } else {
      targetDetails = this.defaultLayout.viewLayoutDetails;
    }

    if (this.defaultLayout && targetDetails && targetDetails.fields) {
      for (let k = 0; k < targetDetails.fields.length; k++) {
        if (targetDetails.fields[k].hidden === false) {
          queryString += targetDetails.fields[k].name + ",";
        }
      }
      queryString = queryString.substring(0, queryString.length - 1);
    }

    // if (this.freetextsearch) {
    //   queryString += "&searchText=" + this.freetextsearch;
    // }
    if (this.displaysearchQueryString) {
      queryString += this.displaysearchQueryString;
    }
    queryString += "&pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;

    if (this.layoutType == "List") {
      if (this.defaultLayout.listLayoutDetails.defaultSortOrder !== null && this.defaultLayout.listLayoutDetails.defaultSortOrder.name !== null && this.defaultLayout.listLayoutDetails.defaultSortOrder.value !== null) {
        queryString += '&orderBy=' + this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value;
      }
    }

    // let str = "";
    // this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
    //   if (element.name !== 'AdvanceSearch') {
    //     element.properties.forEach(prop => {
    //       if (prop.value != null) {
    //         str += prop.name + "," + prop.value + "|";
    //       }
    //     });
    //   }
    // });

    // if (str != "") {
    //   str = str.substring(0, str.length - 1);
    //   queryString += "&filters=" + str;
    // }
    return queryString;
  }

  private onFreeSearchEvent($event) {
    //console.log('onFreeSearchEvent GUIL :: ' + $event);
  }
  private onSimpleSearchEvent($event) {
    //console.log('onSimpleSearchEvent GUIL :: ' + $event);
  }

  // public generateResourceName(word): string {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }

  private openDetailsEntityCreatePopup(id): void {
    this.route.params.subscribe((params: Params) => {
      this.parentId = params['id'];
      this.parentEntity = params['name'];
    });
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };
    var modalName = "custom_detailEntity";
    const modalRef = this.modalService.open(MODALS[modalName], ngbModalOptions);
    // let nodeObj = JSON.parse(JSON.stringify(node))
    //modalRef.componentInstance.node = nodeObj;
    modalRef.componentInstance.entityName = this.parentEntity
    modalRef.componentInstance.userid = this.parentId
    modalRef.componentInstance.detailEntityName = this.entityName;
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.subType = this.subType;

    modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
      //console.log("saveEvent.subscribe", receivedEntry);
      modalRef.close();

      // if (this.mode == 1) {
      //   this.loadLayout();
      // }
      //this.getResource();
      this.results = [];
      this.totalRecords = 0;
      this.loadData(this.entityName);
    });
  }

  private renderToolbar(): void {
    //console.log('this.sort ', this.sort);
    //Toolbar buttons
    if (this.defaultLayout.listLayoutDetails) {
      this.toolbarButtons.length = 0;
      if (this.defaultLayout.listLayoutDetails.toolbar) {
        //normal toolbar buttons 
        this.defaultLayout.listLayoutDetails.toolbar.forEach((item, index) => {
          this.toolbarButtons.push(item);
        });

        this.sendToButtons.length = 0;
        this.printButtons.length = 0;

        if (this.toolbarButtons.filter(x => x.group).length > 0) {
          var groupButtons = this.toolbarButtons.filter(x => x.group);
          groupButtons.forEach((item, index) => {
            if (item.group.toLocaleLowerCase() === 'communication'.toLocaleLowerCase()) {
              this.sendToButtons.push(item);
            } else if (item.group.toLocaleLowerCase() === 'Print'.toLocaleLowerCase()) {
              this.printButtons.push(item);
            }
          });
        }
      }
    }
  }

}



