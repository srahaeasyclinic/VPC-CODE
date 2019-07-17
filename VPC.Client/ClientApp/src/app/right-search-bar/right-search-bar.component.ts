import { Component, OnInit, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, NavigationExtras, Params, NavigationEnd } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';
import swal from 'sweetalert2';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import{GlobalResourceService} from '../global-resource/global-resource.service';
import { EntityValueService } from '../general/entityValue.service';
import { ResourceService } from '../services/resource.service';
import { Data } from '../services/storage.data';
import { TosterService } from '../services/toster.service';
import { LayoutModel } from '../model/layoutmodel';
import { CommonService } from '../services/common.service';
import { LayoutService } from '../meta-data/layout/layout.service';
import { element } from '@angular/core/src/render3/instructions';


@Component({
  selector: 'right-search-bar',
  templateUrl: './right-search-bar.component.html',
  styleUrls: ['./right-search-bar.component.css'],
  inputs: ['resourceData', 'defaultLayoutData']
})

export class RightSearchBarComponent implements OnInit {

  @ViewChild('contentExchange') modalRef: ElementRef;

  @Output() freetextsearchEvent = new EventEmitter();
  @Output() simplesearchEvent = new EventEmitter();
  @Output() searchQueryEvent = new EventEmitter();



  constructor(private entityValueService: EntityValueService,
    private data: Data,
    private modalService: NgbModal,
    private router: Router,
    private route: ActivatedRoute,
    private toster: TosterService,
    private commonService: CommonService,
    private resourceService: ResourceService,
    private layoutService: LayoutService,
    private globalResourceService:GlobalResourceService,
    ) { }

  public toolbarButtons = [];
  public results: any;
  public freetextsearchChanged: Subject<string> = new Subject<string>();

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
  public isExpanded: boolean;
  public resourceData: any;
  public defaultLayoutData: LayoutModel = new LayoutModel();

  //private _searchRequired: boolean = true;
  // private entityname: string;

  // public columns = [];
  // public actions: Array<any>;

  ngOnChanege() {

  }

  ngOnInit() {
    this.selectedSubType = '';

    //Get the entity name from URL route 
    this.route.parent.params.subscribe((urlPath) => {
      this.entityName = urlPath["name"];
    });

    if (this.resourceData) {
      this.resource = this.resourceData;
      this.getLayout();
    }
    else {
      this.getResource();
    }

    this.freetextsearchChanged
      .pipe(debounceTime(500), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;

        // this.displayPreview();
        //console.log(this.freetextsearch);
        //this.generateListlayout(this.defaultLayout, this.entityName);
      });
  }

  public checkSearchRequired() {
    var searchCounter = 0;
    let _searchRequired = true;

    if (this.defaultLayout && this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.searchProperties) {
      this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
        if (element.properties.length !== 0) {
          searchCounter++;
        }
      });
      if (searchCounter < 1) {
        _searchRequired = false;
      }
      return _searchRequired;
    }
  }

  public toolbarButtonOperation(operationName: string): void {
    //console.log(" operationName:: " + operationName);
    switch (operationName.toLocaleLowerCase()) {
      case "create": {
        this.entityValueService.getEntitySubTypes(this.entityName)
          .pipe(first())
          .subscribe(
            data => {
              if (data && data.length > 1) {
                //console.log(" this.entityName:: " + this.entityName);
                this.subTypes.length = 0;
                this.subTypes = data;
                this.selectedSubType = this.subTypes[0].name;
                let ngbModalOptions: NgbModalOptions = {
                  backdrop: 'static',
                  keyboard: false
                };
                this.modalReference = this.modalService.open(this.modalRef, ngbModalOptions);
                //console.log(" this.modalRef:: " + this.modalRef);
              } else {
                this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: data[0].name } });
              }
            },
            error => {
              console.log(error);
            });


        break;
      }
      case "send email": {
        this.toster.showWarning("Not yet implemented");
        break;
      }
      case "print": {
        this.toster.showWarning("Not yet implemented");
        break;
      }
      default: {
        this.toster.showWarning("Not yet implemented");
        break;
      }
    }

  }



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
        this.toster.showWarning('No fields found !');
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
        query = this.getQuery();

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
  }

  private getQuery(): string {
    let queryString = "";

    if (this.freetextsearch) {
      queryString += "&searchText=" + this.freetextsearch;
    }

    let str = "";

    this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
      if (element.name !== 'AdvanceSearch') {
        element.properties.forEach(prop => {
          if (prop.value != null) {
            if (prop.name == 'Active' && prop.value == '2') { }
            else {
              str += prop.name + "," + prop.value + "|";
            }
          }
        });
      }

    });

    if (str != "") {
      str = str.substring(0, str.length - 1);
      queryString += "&filters=" + str;
    }
    return queryString;
  }

  private getAdvanceQuery(): string {
    let queryString = "";

    let str = "";
    this.defaultLayout.listLayoutDetails.searchProperties.forEach(element => {
      if (element.name === 'AdvanceSearch') {
        element.properties.forEach(prop => {
          if (prop.value != null) {
            if (prop.name == 'Active' && prop.value == '2') { }
            else {
              str += prop.name + "," + prop.value + "|";
            }
          }
        });
      }

    });

    if (str != "") {
      str = str.substring(0, str.length - 1);
      queryString += "&filters=" + str;
    }
    return queryString;
  }

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

  // for generate list
  public onFreeTextSearch(query: string): void {
    this.freetextsearchChanged.next(query);

    this.searchQueryEvent.emit(this.getQuery());
    //this.freetextsearchEvent.emit(this.getQuery());
    //this.displayqueryEvent.emit(this.getQuery());
    //console.log("query");
    //
  }
  // for generate list
  public onSimpleSearch(): void {
    //this.generateListlayout(this.defaultLayout, this.entityName);
    // this.simplesearchEvent.emit(this.getQuery());
    //this.displayqueryEvent.emit(this.getQuery());
    this.searchQueryEvent.emit(this.getQuery());
  }

  openAdvanceSearch(content) {
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(content, ngbModalOptions);
  }
  AdvanceSearch() {
    this.modalReference.close();
    //this.displayPreview();
    // console.log("this.getQuery() ", this.getQuery());
    //this.searchQueryEvent.emit(this.getQuery());
    this.searchQueryEvent.emit(this.getAdvanceQuery());
  }
  private onChange() {
    // this.displayPreview();

  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }



  // private displayPreview() {
  //   var query = this.getQuery();
  //   this.layoutService.displayPreview(this.entityname, query)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data && data) {
  //           this.results = data.result;

  //           if (data.result && data.result.length > 0) {
  //             //this.result.forEach(function (elements) { delete elements.internalId });

  //             if (this.defaultLayout.listLayoutDetails.fields.length > 0) {
  //               this.defaultLayout.listLayoutDetails.fields.forEach((field, index) => {
  //                 if (field.dataType == 'PickList') {
  //                   this.results.forEach((result, index) => {
  //                     if (field.name.toLocaleLowerCase() in result) {
  //                       if (result.active) {
  //                         result.active = 'Enabled';
  //                         if (field.values && field.values.length > 0) {
  //                           result.css = field.values.filter(x => x.id == '1')[0].value;
  //                         }
  //                       } else {
  //                         result.active = 'Disabled';
  //                         if (field.values && field.values.length > 0) {
  //                           result.css = field.values.filter(x => x.id == '0')[0].value;
  //                         }
  //                       }
  //                     }

  //                   });
  //                 }

  //                 //clickable field
  //                 if (field.clickable) {
  //                   //var fieldname = field.name.toLocaleLowerCase();
  //                   var fieldname = '';
  //                   if (field.name) {
  //                     fieldname = this.camelize(field.name);

  //                     this.results.forEach((result, index) => {
  //                       // if (field.name.toLocaleLowerCase() in result) {
  //                       //   result.clickable = result[index];
  //                       // }
  //                       if (fieldname in result) {
  //                         result.clickable = result[fieldname];
  //                       }
  //                     });
  //                   }
  //                 }

  //               });

  //             }

  //             this.generateColumns(data.result, this.defaultLayout.listLayoutDetails.fields, this.defaultLayout.listLayoutDetails.actions);

  //             //below values are requred for kendo grid dynamic paging
  //             this.totalRecords = data.totalRow;
  //             //this.gridDataResult = { data: this.results, total: this.totalRecords }
  //             this.gridData = this.results;

  //             //this.generateColumns();
  //           }
  //           else {
  //             //No layout found
  //             this.results = [];
  //             this.results.length = 0;
  //             this.totalRecords = 0;
  //             this.gridDataResult = { data: this.results, total: this.totalRecords }
  //             this.gridData = this.gridDataResult;
  //           }

  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }

  // camelize(str) {
  //   return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
  //     if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
  //     return index == 0 ? match.toLowerCase() : match.toUpperCase();
  //   });
  // }

  // generateColumns(results: Array<any>, fields: any, actions: Array<any>) {
  //   var that = this;
  //   that.columns = [];
  //   let isActionsAvailable: boolean = false;

  //   if (actions != null && actions.length > 0) {
  //     isActionsAvailable = true;
  //     this.actions = actions;
  //     //console.log(this.actions);
  //   }

  //   if (results.length > 0) {
  //     let i = 0;
  //     Object.keys(this.results[0]).forEach(function (key, index) {
  //       i++;

  //       let isFieldAvailable: boolean = false;
  //       let isStatusField: boolean = false;
  //       let isClickableField: boolean = false;

  //       isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
  //       isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden).length > 0 ? true : false;
  //       isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
  //       let widthValue: number = 40;
  //       if (isStatusField) {
  //         widthValue = 20;
  //       }

  //       let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, isClickable: isClickableField, isAction: false, position: 0 };

  //       let columnTitle: string = '';
  //       if (isFieldAvailable) {
  //         columnTitle = that.resource[that.generateResourceName(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden)[0].name)]
  //       }

  //       columnObj.field = key.replace('.', '_');
  //       columnObj.title = columnTitle;
  //       columnObj.position = i;
  //       that.columns.push(columnObj);
  //     });
  //   }

  //   if (isActionsAvailable) {
  //     let actionColumnObj = { field: actions.length > 1 ? 'Actions' : 'Action', title: actions.length > 1 ? 'Actions' : 'Action', width: 20, isVisible: true, isStatus: false, position: 0, isAction: true };
  //     that.columns.push(actionColumnObj);
  //   }

  //   that.results.forEach((result, index) => {

  //     Object.keys(result).forEach(function (key, index) {
  //       if (key.split(".").length > 1) {
  //         let value: string = '';
  //         value = key.replace('.', '_');
  //         result[value] = result[key];
  //         //delete result[key];
  //       }
  //     });

  //   });
  // }


  public onSubTypeChange(value) {
    if (value) {
      this.selectedSubType = value;
    }
  }


  public navigate(): void {
    this.modalReference.close();
    this.router.navigate(["ui/" + this.entityName + "/new"], { queryParams: { subType: this.selectedSubType } });
    //console.log('this.selectedSubType :: ' + this.selectedSubType);
  }

  private getLayout(): void {
    if (this.entityName) {
      this.pageindex = 1;
      this.skip = 0;
      this.freetextsearch = '';

      if (this.defaultLayoutData) {
        this.defaultLayout = this.defaultLayoutData;
        this.renderToolbar();
      }
      else
      {
        this.getDefaultLayout(this.entityName, this.layoutType, '', '');
      }
      
    } else {
      this.toster.showWarning('Url tempered! or no entity name found! or entity not yet decoreted!');
    }
  }

  private renderToolbar(): void {
    //Simple search or advance search
    if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
      this.defaultLayout.listLayoutDetails.searchProperties.forEach((result, index) => {
        //console.log("result.name ", result.name);
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

    //Toolbar buttons
    if (this.defaultLayout.listLayoutDetails) {
      this.toolbarButtons.length = 0;
      if (this.defaultLayout.listLayoutDetails.toolbar) {
        //normal toolbar buttons 
        this.defaultLayout.listLayoutDetails.toolbar.forEach((item, index) => {
          this.toolbarButtons.push(item);
          //console.log('this.toolbarButtons DM :: ' + this.toolbarButtons[index].name);
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

    

    //  this.generateListlayout(this.defaultLayout, entityName);
  }
}