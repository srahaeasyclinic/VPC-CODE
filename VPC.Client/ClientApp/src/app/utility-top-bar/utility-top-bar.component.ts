import { Component, OnInit, ViewChild, ElementRef, Output, EventEmitter, Compiler, Input } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, NavigationExtras, Params, NavigationEnd } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';
import swal from 'sweetalert2';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EntityValueService } from '../general/entityValue.service';
import { Data } from '../services/storage.data';
import { TosterService } from '../services/toster.service';
import { LayoutModel } from '../model/layoutmodel';
import { CommonService } from '../services/common.service';
import { LayoutService } from '../meta-data/layout/layout.service';
import { Operation } from '../model/operation';
import { ValidationService } from '../services/validation.service';
import { ITreeNode } from '../dynamic-form-builder/tree.module';
import { MetadataService } from '../meta-data/metadata.service';
import { PicklistUiService } from "../picklist-ui/picklist-ui.service";
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { MenuService } from '../services/menu.service';
import { BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';


@Component({
  selector: 'utility-top-bar',
  templateUrl: './utility-top-bar.component.html',
  styleUrls: ['./utility-top-bar.component.css'],
  inputs: ['layoutType', 'tree', 'resourceData', 'defaultLayoutData'],
  outputs: ["toolBarWorkFlowClick:onToolBarWorkFlowClick"],
})

export class UtilityTopBarComponent implements OnInit {

  @ViewChild('contentExchange') modalRef: ElementRef;
  @Input() public possiblesteps;
  @Input() public entityInfo;
  public toolBarWorkFlowClick: EventEmitter<any>;

  // @Output() freetextsearchEvent = new EventEmitter();
  // @Output() simplesearchEvent = new EventEmitter();
  // @Output() searchQueryEvent = new EventEmitter();

  constructor(private entityValueService: EntityValueService,
    private data: Data,
    private modalService: NgbModal,
    private router: Router,
    private route: ActivatedRoute,
    private toster: TosterService,
    private commonService: CommonService,
    private _compiler: Compiler,
    private layoutService: LayoutService,
    private validateService: ValidationService,
    private metadataService: MetadataService,
    private globalResourceService: GlobalResourceService,
    private picklistService: PicklistUiService,
    private menuService: MenuService,
    private breadcrumsService: BreadcrumbsService,
  ) {
    this.toolBarWorkFlowClick = new EventEmitter();

  }

  public toolbarButtons = [];
  public results: any;
  // public freetextsearchChanged: Subject<string> = new Subject<string>();

  public view: Observable<GridDataResult>;
  public defaultLayout: LayoutModel = new LayoutModel();

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

  public gridData: any;
  public gridDataResult: GridDataResult;
  public freetextsearch: string;

  public skip = 0;

  public subTypes = [];
  public modalReference: any;
  public selectLayoutForm: FormGroup;
  public selectedSubType: string;

  public dateFormat = this.commonService.defaultDateformat();
  public groups: GroupDescriptor[];

  public sort: SortDescriptor[];
  public sendToButtons = [];
  public printButtons = [];

  private subType: string;
  public transitObject: any;
  private pageType: string = "";
  private toolbarLength: number = 0;
  public tree: ITreeNode;
  public validateMessages: Array<string> = [];
  public resourceData: any;
  public defaultLayoutData: LayoutModel = new LayoutModel();

  public taskProductVersionButton: boolean;

  ngOnInit() {

    //Get the entity name from URL route 
    // this.route.parent.params.subscribe((urlPath) => {
    //   this.entityName = urlPath["name"];
    // });
    let result = this.menuService.getMenuconext();
    this.entityName = result.param_name;

    if (this.route.snapshot.url !== null && this.route.snapshot.url.length > 0)
      this.pageType = this.route.snapshot.url[0].path

    this.transitObject = this.data.storage;
    this.selectedSubType = '';
    this.subType = this.route.snapshot.queryParams["subType"];

    if (this.entityInfo) {
      if (this.entityInfo.product.draftVersion == null) {
        this.taskProductVersionButton = true;
      }
      else {
        this.taskProductVersionButton = false;
      }
      //console.log('this.entityInfo.product.draftVersion ', this.entityInfo.product.draftVersion);
    }
    if (this.resourceData) {
      this.resource = this.resourceData;
      //console.log('Resource -' + JSON.stringify(this.resource));
      this.getLayout();
    }
    else {
      this.getResource();
    }

  }

  public toolbarButtonOperation(taskAttribute, content: string): void {
    //console.log('taskAttribute ', taskAttribute);
    if (taskAttribute.name === "Create") {
      this.entityValueService.getEntitySubTypes(this.entityName).pipe(first()).subscribe(
        data => {
          if (data && data.length != 0) {
            this.subTypes = [];
            this.subTypes = data;
            this.selectedSubType = this.subTypes[0].name;
            if (data.length === 1) {
              this.breadcrumsService.setchildMenuBreadcums([{ elementName: "new", 'elementURL': "", 'isGroup': false }]);
              this.router.navigate(["./new"], { queryParams: { subType: this.selectedSubType }, relativeTo: this.route });
            } else {
              let ngbModalOptions: NgbModalOptions = {
                backdrop: 'static',
                keyboard: false
              };
              this.modalReference = this.modalService.open(this.modalRef, ngbModalOptions);
            }
          }
          else if (this.selectedSubType === "") {//picklist
            var currentUrl = this.router.url;
            this.breadcrumsService.setchildMenuBreadcums([{ elementName: "new", 'elementURL': "", 'isGroup': false }]);
            this.router.navigate([currentUrl + "/new"]);
          }
          else {
            //this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: data[0].name } });

            this.router.navigate(["./new"], { queryParams: { subType: data[0].name }, relativeTo: this.route });
          }
        },
        error => {
          console.log(error);
        });
    }
    else if (taskAttribute.name === "Edit") {
      let transitObject = {
        name: this.entityName,
        fields: this.transitObject.fields,
        searchText: this.transitObject.searchText,
        orderBy: this.transitObject.orderBy,
        filters: this.transitObject.filters,
        pageIndex: this.transitObject.pageIndex,
        pageSize: this.transitObject.pageSize,
        itemIndex: this.transitObject.itemIndex,
        totalRecords: this.transitObject.totalRecords
      };

      this.data.storage = transitObject;

      this.route.params.subscribe((params: Params) => {
        let entityValueId = params['id'];
        if (entityValueId) {
          // this.router.navigate(["ui/" + this.entityName + "/edit/" + entityValueId], { queryParams: { subType: this.subType } });
          if (this.subType === undefined) { //picklist
            this.router.navigate(["../../edit", entityValueId], { relativeTo: this.route });
          }
          else {
            this.router.navigate(["../../edit", entityValueId], { queryParams: { subType: this.subType }, relativeTo: this.route });
          }
        }
      });

    }
    else if (taskAttribute.name === "Update") {
      this.validateMessages = [];
      this.validateMessages = this.validateService.validate(this.tree.fields, this.entityName.toLowerCase());
      if (this.validateMessages.length > 0) {
        this.modalService.open(content);
        return
      }
      let value: any = {};


      //value = this.commonService.createKeyValue(this.tree.fields, value);
      // value = this.commonService.buildkey(this.tree.fields, value);

      //let id = this.getEntityValueId();
      this.route.params.subscribe((params: Params) => {
        let entityValueId = params['id'];
        if (entityValueId) {
          //console.log('this.subType ', this.subType);
          if (this.subType === undefined) { //picklist


            value = this.commonService.createKeyValue(this.tree.fields, value);
            this.picklistService.updatePicklistValues(this.entityName, value, entityValueId)
              .pipe(first())
              .subscribe(
                data => {
                  //debugger;
                  // this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("UpdatedSuccessfully")]);

                  this.router.navigate(["../../"], { relativeTo: this.route }).then(() => {
                    this.toster.showSuccess(this.globalResourceService.updateSuccessMessage(this.entityName.toLowerCase() + '_displayname'));
                    // this.toster.showSuccess(this.getResourceValue(this.entityName.toLowerCase() + "_operation_update_success_message")); //by debdut
                    this.breadcrumsService.splicebreadcrums({ elementName: "edit", 'elementURL': "", 'isGroup': false }, true);
                  });
                  //this.router.navigate(["../../"], { relativeTo: this.route });
                  //this.router.navigate(["../../"], { relativeTo: this.route });
                },
                error => {
                  console.log(error);
                });
          } else {
            value = this.commonService.buildkey(this.tree.fields, value, this.entityName);
            if (this.entityInfo != null) {
              Object.keys(this.entityInfo).forEach(element => {
                if (value.hasOwnProperty(element.toLowerCase()) && this.entityInfo[element]['internalId']) {
                  value[element.toLowerCase()]['internalId'] = this.entityInfo[element]['internalId']
                }
              });
            }
            this.entityValueService.updateEntityValues(this.entityName, value, entityValueId, this.subType)
              .pipe(first())
              .subscribe(
                data => {
                  this.toster.showSuccess(this.globalResourceService.updateSuccessMessage(this.entityName.toLowerCase() + '_displayname'));
                  //this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("UpdatedSuccessfully")]);
                  this.redirectToListPage();
                },
                error => {
                  if (error.status === 501) {
                    this.toster.showError(error.message);
                  }
                });
          }
        }
      });

    }
    else if (taskAttribute.name === "Save") {
      this.validateMessages = [];
      this.validateMessages = this.validateService.validate(this.tree.fields, this.entityName.toLowerCase());
      if (this.validateMessages.length > 0) {
        this.modalService.open(content);
        return
      }
      let newValue: any = {};
      // newValue = this.commonService.createKeyValue(this.tree.fields, newValue);
      //newValue = this.commonService.buildkey(this.tree.fields, newValue);

      if (this.subType !== undefined) {
        newValue = this.commonService.buildkey(this.tree.fields, newValue, this.entityName);
        this.entityValueService.saveEntityValue(this.entityName, this.subType, newValue)
          .pipe(first())
          .subscribe(
            data => {
              this.router.navigate(["../"], { relativeTo: this.route }).then(() => {
                this.toster.showSuccess(this.globalResourceService.saveSuccessMessage(this.entityName.toLowerCase() + '_displayname'));
                //this.toster.showSuccess(this.getResourceValue(this.entityName.toLowerCase() + "_operation_save_success_message")); // by debdut
                this.breadcrumsService.splicebreadcrums({ elementName: "new", 'elementURL': "", 'isGroup': false }, true);
              });
              //this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("SavedSuccessfully")]);
              // this.router.navigate(["../"], { relativeTo: this.route });
            },
            error => {
              if (error.status === 501) {
                this.toster.showError(error.message);
              }
            });
      }
      else { //picklist
        newValue = this.commonService.createKeyValue(this.tree.fields, newValue);
        this.picklistService.savePicklistValues(this.entityName, newValue)
          .pipe(first())
          .subscribe(
            data => {

              // this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("SavedSuccessfully")]);
              //this.router.navigate(["../"], { relativeTo: this.route });
              this.router.navigate(["../"], { relativeTo: this.route }).then(() => {
                this.toster.showSuccess(this.globalResourceService.saveSuccessMessage(this.entityName.toLowerCase() + '_displayname'));
                //this.toster.showSuccess(this.getResourceValue(this.entityName.toLowerCase() + "_operation_save_success_message"));// by debdut
                this.breadcrumsService.splicebreadcrums({ elementName: "new", 'elementURL': "", 'isGroup': false }, true);
              });

              // this.router.navigate(['picklist/ui/' + this.entityName.toLowerCase()]);
              //this.router.navigate(["../"], { relativeTo: this.activatedRoute });
            },
            error => {

              console.log(error);
            });
      }

    }

    else if (taskAttribute.taskType === "FrontTask") {
      if (taskAttribute.taskDisplay === "PopUp") {

        this.router.config.forEach((item) => {
          if (item.component.name === taskAttribute.name + "Component") {
            this.modalService.open(item.component);
            return;
          } else {
            if (item.children) {
              item.children.forEach(element => {
                if (element.component) {
                  if (element.component.name === taskAttribute.name + "Component") {
                    this.modalService.open(element.component);
                    return;
                  }
                }
              });
            }
          }
        });
      } else {
        this.router.config.forEach((item) => {
          if (item.component.name === taskAttribute.name + "Component") {
            this.router.navigate([item.path]);
            return;
          }
        });
      }
    } else if (taskAttribute.taskType === "BackTask") {
      if (taskAttribute.taskVerb === "") {
        this.toster.showWarning(this.getResourceValue("metadata_verb_undefined"));
        return;
      }

      this.route.params.subscribe((params: Params) => {
        let entityValueId = params['id'];
        if (entityValueId) {
          var obj = {
            id: entityValueId
          };
          this.commonService.executeTask(this.entityName, taskAttribute, obj).pipe(first()).subscribe(
            data => {
              if (data && data.result) {
                if (data.result.message) {
                  if (data.result.message == "308") {
                    this.router.navigate(['.'], { relativeTo: this.route, queryParams: { versionId: data.result.id }, queryParamsHandling: 'merge' });
                  } else if (data.result.message == "409") {
                    this.toster.showWarning(this.getResourceValue("metadata_task_warning_message"));
                  }
                }else{
                  this.toster.showSuccess(this.getResourceValue("metadata_task_success_message"));
                }
              }
            },
            error => {
              console.log(error);
            });
        }
      });

    } else {
      this.toster.showWarning(this.getResourceValue("metadata_task_notdecorate_message"));
      return;
    }



  }


  // public toolbarButtonOperation(operationName: string): void {
  //   console.log(" operationName:: " + operationName);
  //   switch (operationName.toLocaleLowerCase()) {
  //     case "create": {
  //       this.entityValueService.getEntitySubTypes(this.entityName)
  //         .pipe(first())
  //         .subscribe(
  //           data => {
  //             if (data && data.length > 1) {
  //               console.log(" this.entityName:: " + this.entityName);
  //               this.subTypes.length = 0;
  //               this.subTypes = data;
  //               this.selectedSubType = this.subTypes[0].name;
  //               let ngbModalOptions: NgbModalOptions = {
  //                 backdrop : 'static',
  //                 keyboard : false
  //               };
  //               this.modalReference = this.modalService.open(this.modalRef, ngbModalOptions);
  //               console.log(" this.modalRef:: " + this.modalRef);
  //             } else {
  //               this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: data[0].name } });
  //             }
  //           },
  //           error => {
  //             console.log(error);
  //           });


  //       break;
  //     }
  //     case "send email": {
  //       this.toster.showWarning("Not yet implemented");
  //       break;
  //     }
  //     case "print": {
  //       this.toster.showWarning("Not yet implemented");
  //       break;
  //     }
  //     default: {
  //       this.toster.showWarning("Not yet implemented");
  //       break;
  //     }
  //   }

  // }

  private generateListlayout(layout: LayoutModel, entityName: string): void {

    let isvalid: boolean = true;

    let filters: string = '';

    let maxResult: number;

    if (layout.listLayoutDetails) {

      maxResult = layout.listLayoutDetails.maxResult;

      if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
        layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
        this.selectedFields = '';
        layout.listLayoutDetails.fields.forEach((item, index) => {
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
        this.toster.showWarning(this.getResourceValue("metadata_operation_warning_notfoundmessage"));
      }

      if (layout.listLayoutDetails.searchProperties && layout.listLayoutDetails.searchProperties.length > 0) {
        layout.listLayoutDetails.searchProperties.forEach(element => {
          element.properties.forEach(prop => {
            if (prop.value != null) {
              filters += prop.name + ',' + prop.value + '|';
            }
          });
        });

        if (filters != "") {
          filters = filters.substring(0, filters.length - 1);
        }
      }

      //default group by set from layout = [{ field: 'Category.CategoryName' }];
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


      if (isvalid) {

        let query: string = '';
        //query = this.getQuery();

        this.entityValueService.getEntityValues(entityName, query)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.results = [];
                this.results = data.result;

                //below values are requred for kendo grid dynamic paging
                this.totalRecords = data.totalRow;
                //this.gridDataResult = { data: this.results, total: this.totalRecords }
                this.gridData = this.results;

              }
            },
            error => {
              console.log(error);
            });
      }
    } else {
      //No layout found
      this.results = [];
      this.results.length = 0;
      this.totalRecords = 0;
      this.gridDataResult = { data: this.results, total: this.totalRecords }
      this.gridData = this.gridDataResult;
    }
  }

  private getResource(): void {
    // this.resource = this.globalResourceService.getGlobalResources();;
    this.getLayout();

    // //Get the entity name from URL route 
    // this.route.parent.params.subscribe((urlPath) => {
    //   this.entityName = urlPath["name"];

    //   if (this.route.snapshot.url !== null && this.route.snapshot.url.length > 0)
    //     this.pageType = this.route.snapshot.url[0].path

    //   // if (urlPath != null && urlPath.length > 2) {
    //   //   this.pageType = urlPath[2].path;
    //   // }

    //   if (this.entityName) {
    //     // this.pageindex = 1;
    //     // this.skip = 0;
    //     // this.freetextsearch = '';
    //     if (this.pageType !== null && this.pageType !== "" && this.pageType === "new") {
    //       this.getDefaultLayout(this.entityName, 'Form', this.subType, 'New');
    //     }
    //     else if (this.pageType !== null && this.pageType !== "" && this.pageType === "edit") {
    //       this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
    //     }
    //     else if (this.pageType !== null && this.pageType !== "" && this.pageType === "preview") {
    //       this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
    //     }
    //     else {
    //       this.getDefaultLayout(this.entityName, this.layoutType, '', '');
    //     }
    //   } else {
    //     this.toster.showWarning('Url tempered! or no entity name found! or entity not yet decoreted!');
    //   }
    // });
  }

  // private getQuery(): string {
  //   let queryString = "";

  //   // if (this.defaultLayout && this.defaultLayout.listLayoutDetails.fields) {
  //   //   for (let k = 0; k < this.defaultLayout.listLayoutDetails.fields.length; k++) {
  //   //     if (this.defaultLayout.listLayoutDetails.fields[k].hidden === false) {
  //   //       queryString += this.defaultLayout.listLayoutDetails.fields[k].name + ",";
  //   //     }
  //   //   }
  //   //   queryString = queryString.substring(0, queryString.length - 1);
  //   // }

  //   if (this.freetextsearch) {
  //     queryString += "&searchText=" + this.freetextsearch;
  //   }

  // //  queryString += "&pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;

  //   let str = "";
  //   this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
  //     if (element.name !== 'AdvanceSearch') {
  //       element.properties.forEach(prop => {
  //         if (prop.value != null) {
  //           str += prop.name + "," + prop.value + "|";
  //         }
  //       });
  //     }
  //   });

  //   if (str != "") {
  //     str = str.substring(0, str.length - 1);
  //     queryString += "&filters=" + str;
  //   }
  //   return queryString;
  // }



  private getDefaultLayout(entityName: string, type: string, subtype: string, context: string): void {
    this.layoutService.getDefaultLayout(entityName, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.defaultLayout = data;
            this.renderToolbar();
          }

        },
        error => {
          console.log(error);
        });
  }

  public generateResourceName(word): string {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  // // for generate list
  // public onFreeTextSearch(query: string): void {
  //   this.freetextsearchChanged.next(query);

  //   this.searchQueryEvent.emit(this.getQuery());
  //   //
  // }
  // // for generate list
  // public onSimpleSearch(): void {
  //   //this.generateListlayout(this.defaultLayout, this.entityName);
  //  // this.simplesearchEvent.emit(this.getQuery());
  //   //this.displayqueryEvent.emit(this.getQuery());
  //   this.searchQueryEvent.emit(this.getQuery());
  // }

  public onSubTypeChange(value) {
    if (value) {
      this.selectedSubType = value;
    }
  }

  public navigate(): void {
    this.modalReference.close();
    //this.router.navigate(["../new"], { queryParams: { subType: this.selectedSubType }, relativeTo: this.route });
    this.router.navigate(["./new"], { queryParams: { subType: this.selectedSubType }, relativeTo: this.route });
    // this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: this.selectedSubType } });

  }

  private redirectToListPage() {

    this.router.navigate(["../../"], { relativeTo: this.route }).then(() => {
      this.toster.showSuccess(this.globalResourceService.updateSuccessMessage(this.entityName.toLowerCase() + '_displayname'));
      //this.toster.showSuccess(this.getResourceValue(this.entityName.toLowerCase() + "_operation_update_success_message")); // by debdut
      this.breadcrumsService.splicebreadcrums({ elementName: "edit", 'elementURL': "", 'isGroup': false }, true);
    });
    //this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("UpdatedSuccessfully")]);
    //this.router.navigate(['ui/' + this.entityName]);
    //console.log('this.route ', this.route);
    //this.router.navigate(['../../'], { relativeTo: this.route });
  }

  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.systemOperations) {
            data.systemOperations.forEach((item, index) => {
              this.toolbarButtons.push(item);
            });
          }
        },
        error => {
          console.log(error);
        });
  }


  private onToolBarWorkFlowClick(transitionWapper): void {
    this.toolBarWorkFlowClick.emit(transitionWapper);
  }

  private getLayout(): void {
    if (this.entityName) {
      if (this.defaultLayoutData) {
        //console.log('this.defaultLayout ', this.defaultLayout);
        this.defaultLayout = this.defaultLayoutData;
        this.renderToolbar();
      }
      else {
        if (this.pageType !== null && this.pageType !== "" && this.pageType === "new") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Add');
        }
        else if (this.pageType !== null && this.pageType !== "" && this.pageType === "edit") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
        }
        else if (this.pageType !== null && this.pageType !== "" && this.pageType === "preview") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
        }
        else {
          this.getDefaultLayout(this.entityName, this.layoutType, '', '');
        }
      }

    } else {
      this.toster.showWarning(this.getResourceValue("metadata_operation_alert_warning_message"));
    }
  }

  private renderToolbar(): void {
    var toolbar = [];
    if (this.defaultLayout.formLayoutDetails) {
      if (this.defaultLayout.formLayoutDetails.toolbar) {
        toolbar = this.defaultLayout.formLayoutDetails.toolbar;
      }
    } else if (this.defaultLayout.listLayoutDetails) {
      if (this.defaultLayout.listLayoutDetails.toolbar) {
        toolbar = this.defaultLayout.listLayoutDetails.toolbar;
      }
    }

    if (this.pageType === 'new') {
      //this.getMetadataFieldsByName(this.entityName);
      this.toolbarLength = this.toolbarLength + 1;
      var saveButton: Operation = { name: "Save", type: '', sequence: this.toolbarLength, group: '', properties: '' };
      this.toolbarButtons.push(saveButton);
    }

    toolbar.forEach((item, index) => {
      this.toolbarButtons.push(item);
    });
    if (this.toolbarButtons.length > 0) {
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

    if (this.pageType === 'preview') {
      this.toolbarButtons = [];
      this.sendToButtons.length = 0;
      this.printButtons.length = 0;
      this.toolbarLength = this.toolbarLength + 1;
      var editButton: Operation = { name: "Edit", type: '', sequence: this.toolbarLength, group: '', properties: '' };
      this.toolbarButtons.push(editButton);
    }

  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  getResourceForMessage(messagekey, object: any[]) {
    return this.globalResourceService.getResourceByStringReplacer(messagekey, object);
  }

}
