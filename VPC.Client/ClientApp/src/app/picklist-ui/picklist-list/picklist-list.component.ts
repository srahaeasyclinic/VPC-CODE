import { Component, Input, Output, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, NavigationExtras, Params, NavigationEnd } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';


import { LayoutModel } from '../../model/layoutmodel';
import { PicklistUiService } from '../picklist-ui.service';
import { TosterService } from '../../services/toster.service';
import { CommonService } from '../../services/common.service';
import { ResourceService } from '../../services/resource.service';
import { Data } from '../../services/storage.data';

import { MenuService } from '../../services/menu.service';

import swal from 'sweetalert2';
import { GlobalResourceService } from '../../global-resource/global-resource.service'

@Component({
  selector: 'app-picklist-list',
  templateUrl: './picklist-list.component.html',
  styleUrls: ['./picklist-list.component.css']
})
export class PicklistListComponent implements OnInit {

  constructor(private router: Router, private picklistService: PicklistUiService,
    private toster: TosterService, private resourceService: ResourceService,
    private commonService: CommonService, private route: ActivatedRoute, private data: Data,
    private globalResourceService: GlobalResourceService) {

  }

  public view: Observable<GridDataResult>;
  //public defaultLayout: LayoutModel = new LayoutModel();
  public defaultLayout: LayoutModel;
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

  private entityName: string = '';
  private layoutType: number = 3; // List page
  public pageindex: number = 1;
  private pageSize: number = 10;
  public totalRecords: number = 0;
  public resource: any;
  private orderBy: string = '';
  public storage: any;
  public filters: string = '';
  public selectedFields: string = '';
  ngOnInit() {
    //Get the resources for display rendering
    this.getResource();
    this.freetextsearchChanged
      .pipe(debounceTime(500), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;
        this.generateListlayout(this.defaultLayout, this.entityName);
      });
      this.resource = this.globalResourceService.getGlobalResources();
  }


  //Events sections
  public onFreeTextSearch(query: string): void {
    this.freetextsearchChanged.next(query);
    this.pageindex = 1;
  }

  public onSimpleSearch(): void {
    this.generateListlayout(this.defaultLayout, this.entityName);
    this.pageindex = 1;
  }

  public onActionClick(event): void {
    //console.log(actionName);
    //console.log(id); 
    if (event.actionName.toLowerCase() == 'Delete'.toLowerCase()) {
      swal({
        title: this.getResourceValue("Areyousure"),
        text: this.getResourceValue("Youwntbeabletorevertthis"),
        type: this.getResourceValue('warning'),
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: this.getResourceValue('Yesdeleteit'),
        showLoaderOnConfirm: true,
      })
        .then((willDelete) => {
          if (willDelete.value) {
            this.picklistService.deletePicklistValues(this.entityName, event.internalId).subscribe(result => {
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
      this.toster.showWarning(this.getResourceValue("MethodNotImplemented"));
    }

  }

  public edit(id): void {
    //this.gridData
    let currentIndex: number = 0;
    this.gridData.forEach((item, index) => {
      if (item.internalId.toLowerCase() == id.internalId.toLowerCase()) {
        currentIndex = index;
      }
    });
    var transitObject = {
      name: this.entityName,
      fields: this.selectedFields,
      searchText: this.freetextsearch,
      orderBy: this.orderBy,
      filters: this.filters,
      pageIndex: this.pageindex,
      pageSize: this.pageSize,
      itemIndex: currentIndex,
      totalRecords: this.totalRecords
    };
    this.data.storage = transitObject;
    this.router.navigate(["./preview", id.internalId], { relativeTo: this.route });
  }



  public onGridChangeEvent(event) {

    //this.isDataReady = false;
    this.totalRecords = 0;
    let maxResult: number;
    let isvalid: boolean = true;
    //let selectedFields: string = '';



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


        if (isvalid) {
          this.picklistService.getPicklistValues(this.entityName, this.selectedFields, this.filters, event.pageIndex, event.pageSize, maxResult, event.orderBy, this.freetextsearch)

            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  //console.log('data ', data);
                  this.results = [];
                  this.results = data.result;
                  this.skip = event.skip;
                  this.pageindex = event.pageIndex;
                  this.pageSize = event.pageSize;

                  if (event.groupBy && event.groupBy.length > 0) {
                    this.groups = event.groupBy;
                  } else {
                    var emptyGroup: GroupDescriptor[] = [];
                    this.groups = emptyGroup;
                    this.defaultLayout.listLayoutDetails.defaultGroupBy = "";
                  }

                  if (event.sort) {
                    this.sort = event.sort;
                  }
                  this.totalRecords = 0;
                  this.totalRecords = data.totalRow;
                  this.gridData = this.results;

                }
              },
              error => {
                console.log(error);
              });
        } else {
          //No layout found
        }




      } else {
        isvalid = false;
        this.toster.showWarning(this.getResourceValue("NoFieldsFound"));
      }
    }
  }

  public configToggle(): void {
    this.isExpanded = !this.isExpanded;
  }

  public toolbarButtonOperation(operationName: string): void {
    var currentUrl = this.router.url;
    switch (operationName.toLocaleLowerCase()) {
      case "create": {
        this.router.navigate([currentUrl + "/new"]);
        break;
      }
      case "send email": {
        this.toster.showWarning(this.getResourceValue("NotYetImplemented"));
        break;
      }
      case "print": {
        this.toster.showWarning(this.getResourceValue("NotYetImplemented"));
        break;
      }
      default: {
        this.toster.showWarning( this.getResourceValue("NotYetImplemented"));
        break;
      }
    }

  }


  //Methods section
  private getResource(): void {
    // this.resourceService.getResources()
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data) {
            this.resource = this.globalResourceService.getGlobalResources();

            this.route.parent.params.subscribe(params => {
              this.entityName = params["name"];
              if (this.entityName) {
                this.pageindex = 1;
                this.skip = 0;
                this.freetextsearch = '';
                this.getDefaultLayout(this.entityName);
              } else {
                this.toster.showWarning( this.getResourceValue("UrlTemperedorNoEntityNameoridFoundorEntityNotYetDecorated"));
              }
            });
            // //Get the picklist entity name from URL route 
            // this.route.url.subscribe((urlPath) => {
            //   console.log("urlPath", urlPath)
            //   // this.entityName = urlPath[urlPath.length - 1].path;
            //   // if (this.entityName) {
            //   //   this.pageindex = 1;
            //   //   this.skip = 0;
            //   //   this.freetextsearch = '';
            //   //   this.getDefaultLayout(this.entityName);                
            //   // } else {
            //   //   this.toster.showWarning('Url tempered! or no entity name found! or entity not yet decoreted!');
            //   // }
            // });

        //   }
        // },
        // error => {
        //   console.log(error);
        // });
  }

  private getDefaultLayout(entityName: string): void {
    this.picklistService.getDefaultLayout(entityName, this.layoutType, 0)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            //console.log('data ', data);
            this.defaultLayout = new LayoutModel();
            this.defaultLayout = data;
            //console.log('this.defaultLayout ', this.defaultLayout);
            if (this.defaultLayout) {

              if (this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.searchProperties && this.defaultLayout.listLayoutDetails.searchProperties.length > 0) {
                this.defaultLayout.listLayoutDetails.searchProperties.forEach((result, index) => {
                  if (result.name === 'SimpleSearch') {
                    if(result.properties){
                      result.properties.forEach((prop, index) => {
                        if(prop.values){
                          prop.values = prop.values.sort(function (a, b) {
                            if (a.value < b.value) { return -1; }
                            if (a.value > b.value) { return 1; }
                            return 0;
                          })
                          prop.value = prop.defaultValue;
                        }
                      });
                    }
                  }
                });
              }

              // if (this.defaultLayout.listLayoutDetails) {
              //   this.toolbarButtons.length = 0;
              //   if (this.defaultLayout.listLayoutDetails.toolbar) {
              //     //normal toolbar buttons 
              //     this.defaultLayout.listLayoutDetails.toolbar.forEach((item, index) => {
              //       this.toolbarButtons.push(item);
              //     });
              //     this.sendToButtons.length = 0;
              //     this.printButtons.length = 0;
              //     if (this.toolbarButtons.filter(x => x.group).length > 0) {
              //       var groupButtons = this.toolbarButtons.filter(x => x.group);
              //       groupButtons.forEach((item, index) => {
              //         if (item.group.toLocaleLowerCase() === 'communication'.toLocaleLowerCase()) {
              //           this.sendToButtons.push(item);
              //         } else if (item.group.toLocaleLowerCase() === 'Print'.toLocaleLowerCase()) {
              //           this.printButtons.push(item);
              //         }
              //       });
              //     }
              //   }
              // }

              //generate the default orderby
              if (this.defaultLayout.listLayoutDetails) {
                if (this.defaultLayout.listLayoutDetails.defaultSortOrder) {

                  this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
                  //{dir: "asc", field: "text"}
                  //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
                  if (!this.sort)
                    this.sort = [];

                  this.sort.length = 0;
                  this.sort.push({ dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
                }
              }
              this.generateListlayout(this.defaultLayout, entityName);
            }
          }
        },
        error => {
          console.log("error checking...", error);
        });
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
          if (!this.selectedFields) {
            this.selectedFields = item.name;
          }
          else {
            this.selectedFields += ',' + item.name;
          }
        });

      } else {
        isvalid = false;
        this.toster.showWarning( this.getResourceValue("NoFieldsFound"));
      }

      if (layout.listLayoutDetails.searchProperties && layout.listLayoutDetails.searchProperties.length > 0) {
        layout.listLayoutDetails.searchProperties.forEach(element => {
          element.properties.forEach(prop => {
            // if (prop.value != null) {
            //   filters += prop.name + ',' + prop.value + '|';
            // }

            if (prop.value != null) {
              if (prop.name == 'Active' && prop.value == '2') { }
              else {
                filters += prop.name + "," + prop.value + "|";
              }
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

          //let groupName=this.commonService.camelize(layout.listLayoutDetails.defaultGroupBy);
          //groupName=this.resource[this.commonService.generateResourceName(groupName)] ;

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
        this.results = [];
        this.totalRecords = 0;
        this.gridData = this.results;
        this.picklistService.getPicklistValues(entityName, this.selectedFields, filters, this.pageindex, this.pageSize, maxResult, this.orderBy, this.freetextsearch)
          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
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
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
