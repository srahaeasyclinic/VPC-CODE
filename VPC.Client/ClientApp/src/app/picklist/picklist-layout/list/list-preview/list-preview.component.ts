import { Component, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PicklistService } from '../../../picklist.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../../../model/layoutmodel';
import { TosterService } from '../../../../services/toster.service';
import { CommonService } from '../../../../services/common.service';
import { ResourceService } from '../../../../services/resource.service';
import { CountryService } from "../../../../country/country.service";
import { Subject } from "rxjs";
import { debounceTime, distinctUntilChanged } from "rxjs/internal/operators";
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';


@Component({
  selector: 'app-list-preview',
  templateUrl: './list-preview.component.html',
  styleUrls: ['./list-preview.component.css']
})
export class ListPreviewComponent implements OnInit {

  public layoutInfo: LayoutModel = new LayoutModel();

  public results: any;
  public columns = [];
  public displaySearch: boolean = false;
  public freetextsearch: string;
  public freetextsearchChanged: Subject<string> = new Subject<string>();
  public isExpanded: boolean = false;
  public gridData: any;
  public gridDataResult: GridDataResult;
  public sort: SortDescriptor[];
  public skip = 0;
  public groups: GroupDescriptor[];

  public resource: any;
  private id: string;
  private entityname: string;
  private currentPage: number = 1;
  public pageindex: number = 1;
  private pageSize: number = 10;
  public totalRecords: number = 0;
  private orderBy: string = '';

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistService,
    private resourceService: ResourceService,
    private toster: TosterService,
    private countryService: CountryService,
    private commonService: CommonService,
    private globalResourceService: GlobalResourceService,

  ) {
  }

  ngOnInit() {
    //this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];

    this.getResource();

    // this.activatedRoute.params.subscribe((params: Params) => {
    //   this.id = params['id'];
    //   this.entityname = params['name'];

    //   if (this.id) {
    //     this.getLayoutById(this.id);
    //   }
    // });

    this.freetextsearchChanged
      .pipe(debounceTime(2000), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;

        // api call
        this.generateListlayout(this.layoutInfo);
      });
  }

  onFieldChange(query: string) {
    this.freetextsearchChanged.next(query);
  }

  private getResource() {
    // this.resourceService.getResources()
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data) {
            this.resource = this.globalResourceService.getGlobalResources();

            this.activatedRoute.params.subscribe((params: Params) => {
              this.id = params['id'];
              this.entityname = params['name'];
        
              if (this.id) {
                this.pageindex = 1;
                this.skip = 0;
                this.freetextsearch = '';
                this.getLayoutById(this.id);
              }
            });
        //   }
        // },
        // error => {
        //   console.log(error);
        // });
  }

  private getLayoutById(layoutId) {
    this.picklistService.getLayoutById(layoutId)
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data && data) {
            console.table(data);
            this.layoutInfo = data;

            if(this.layoutInfo.listLayoutDetails.searchProperties.length > 0)
            {
              this.layoutInfo.listLayoutDetails.searchProperties.forEach((result, index) => {  
                if(result.name === 'SimpleSearch') 
                {
                  // result.properties.forEach((prop, index) => {                
                  //   prop.value = prop.defaultValue;
                  // });

                  result.properties.forEach((prop, index) => {
                    prop.values = prop.values.sort(function (a, b) {
                      if (a.value < b.value) { return -1; }
                      if (a.value > b.value) { return 1; }
                      return 0;
                    })

                    prop.value = prop.defaultValue;
                  });
                }             
              });
            }

            //generate the default orderby 
            if (this.layoutInfo.listLayoutDetails) {
              if (this.layoutInfo.listLayoutDetails.defaultSortOrder) {

                this.orderBy = this.layoutInfo.listLayoutDetails.defaultSortOrder.name + ',' + this.layoutInfo.listLayoutDetails.defaultSortOrder.value.toUpperCase();
                //{dir: "asc", field: "text"}
                //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
                if (!this.sort)
                  this.sort = [];

                this.sort.length = 0;
                this.sort.push({ dir: this.layoutInfo.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.layoutInfo.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
              }
            }

            //this.displayPreview();
            this.generateListlayout(this.layoutInfo)
            this.displaySearch = true;
          }

        },
        error => {
          console.log(error);
        });
  }

  private generateListlayout(layout: LayoutModel) {

    let isvalid: boolean = true;
    let selectedFields: string = '';
    let filters: string = '';
    let orderBy: string = '';
    let maxResult: number;

    if (layout.listLayoutDetails) {

      maxResult = layout.listLayoutDetails.maxResult;

      if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
        layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });

        layout.listLayoutDetails.fields.forEach((item, index) => {
          if (!selectedFields) {
            selectedFields = item.name;
          }
          else {
            selectedFields += ',' + item.name;
          }
        });

      } else {
        isvalid = false;
        this.toster.showWarning(this.getResourceValue("metadata_operation_warning_notfoundmessage"));
      }



      if (layout.listLayoutDetails.searchProperties || layout.listLayoutDetails.searchProperties.length > 0) {
        layout.listLayoutDetails.searchProperties.forEach(element => {
          element.properties.forEach(prop => {
            if (prop.value != null) {
              filters += prop.name + "," + prop.value + "|";
            }
          });
        });

        if (filters != "") {
          filters = filters.substring(0, filters.length - 1);
        }

      }

      //default group by set from layout = [{ field: 'Category.CategoryName' }];
      if (layout.listLayoutDetails && layout.listLayoutDetails.defaultGroupBy){ 
        if(layout.listLayoutDetails.defaultGroupBy){
          let defaultGroup={field:this.commonService.camelize(layout.listLayoutDetails.defaultGroupBy) };
          if(!this.groups){
            this.groups=[];
          }
          this.groups.push(defaultGroup);
        } 
      }else{
        if(this.groups){
          this.groups.length=0;
        }
      }


      if (isvalid) {
        this.picklistService.getPicklistValues(this.entityname, selectedFields, filters, this.pageindex, this.pageSize, maxResult, orderBy, this.freetextsearch)
          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
                if (data.result.length > 0) {
                  this.results = data.result
                  if (layout.listLayoutDetails.fields.length > 0) {
                    layout.listLayoutDetails.fields.forEach((field, index) => {
                      if (field.dataType == 'PickList') {
                        this.results.forEach((result, index) => {
                          if (field.name.toLocaleLowerCase() in result) {
                            if (result.active) {
                              result.active = 'Active';
                              if (field.values || field.values.length > 0) {
                                result.css = field.values.filter(x => x.id == '1')[0].value;
                              }
                            } else {
                              result.active = 'Inactive';
                              if (field.values || field.values.length > 0) {
                                result.css = field.values.filter(x => x.id == '0')[0].value;
                              }
                            }
                          }

                        });
                      }
                    });

                  }
                  this.generateColumns(data.result, layout.listLayoutDetails.fields);

                  //below values are requred for kendo grid dynamic paging
                this.totalRecords = data.totalRow;
                //this.gridDataResult = { data: this.results, total: this.totalRecords }
                this.gridData = this.results;
                }
              }
            },
            error => {
              console.log(error);
            });
      }
    }
    else {
      //No layout found
      this.results = [];
      this.results.length = 0;
      this.totalRecords = 0;
      this.gridDataResult = { data: this.results, total: this.totalRecords }
      this.gridData = this.gridDataResult;
    }
  }

   

  private onChange() {
    this.generateListlayout(this.layoutInfo);
  }

  private getQuery() {
    var queryString = "";
    queryString = "&PageIndex=" + this.currentPage + "&PageSize=" + this.pageSize;

    return queryString;
  }

  private generateColumns(results: Array<any>, fields: any) {
    var that = this;
    that.columns = [];


    let i = 0;
    Object.keys(results[0]).forEach(function (key, index) {
      i++;
      let isFieldAvailable: boolean = false;
      let isStatusField: boolean = false;

      //isFieldAvailable = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
      isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
      isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden).length > 0 ? true : false;

      let columnObj = { field: '', title: '', width: 40, isVisible: isFieldAvailable, isStatus: isStatusField, position: 0 };
      
      let columnTitle:string='';
      if(isFieldAvailable){
        columnTitle=that.resource[that.generateResourceName(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden)[0].name)]
      }

      columnObj.field =key.replace('.','_'); 
      columnObj.title = columnTitle;
      columnObj.position = i;
      that.columns.push(columnObj);

    });

    that.results.forEach((result, index) => {
      
      Object.keys(result).forEach(function (key, index) { 
        if(key.split(".").length > 1){ 
          let value:string ='';
          value=key.replace('.','_');
           result[value]=result[key]; 
           //delete result[key];
        } 
      });

    });

  }

  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
    // let hierarchyPresent = word.split(".");
    // if (hierarchyPresent.length == 1) {
    //   return word[0].toLowerCase() + word.substr(1);
    // }
    // else if (hierarchyPresent.length > 1) {
    //   let lastItem = hierarchyPresent[hierarchyPresent.length - 1];
    //   if (lastItem)
    //     return lastItem[0].toLowerCase() + lastItem.substr(1);
    // }
  }

  public onGridChangeEvent(event) {
    //this.isDataReady = false;
    this.totalRecords=0;
    let maxResult: number;
    let isvalid: boolean = true;
    let selectedFields: string = '';
    let filters: string = '';


    maxResult = this.layoutInfo.listLayoutDetails.maxResult;

    if (this.layoutInfo.listLayoutDetails) {
      if (this.layoutInfo.listLayoutDetails.fields || this.layoutInfo.listLayoutDetails.fields.length > 0) {

        this.layoutInfo.listLayoutDetails.fields = this.layoutInfo.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });


        this.layoutInfo.listLayoutDetails.fields.forEach((item, index) => {
          if (!selectedFields) {
            selectedFields = item.name;
          }
          else {
            selectedFields += ',' + item.name;
          }
        });

        if (this.layoutInfo.listLayoutDetails.searchProperties && this.layoutInfo.listLayoutDetails.searchProperties.length > 0) {
          this.layoutInfo.listLayoutDetails.searchProperties.forEach(element => {
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


        if (isvalid) { 
          this.picklistService.getPicklistValues(this.entityname, selectedFields, filters, event.pageIndex, event.pageSize, maxResult, event.orderBy, this.freetextsearch)

            .pipe(first())
            .subscribe(
              data => {
                if (data && data) {

                  this.results = [];
                  this.results = data.result;
                  this.skip = event.skip;
                  this.pageindex = event.pageIndex;
                  this.pageSize = event.pageSize;

                  if (event.groupBy && event.groupBy.length > 0){
                     this.groups = event.groupBy;
                  }
                   
                  if (event.sort) {
                    this.sort = event.sort;
                  }
                  this.totalRecords=0;
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
        this.toster.showWarning(this.getResourceValue("metadata_operation_warning_notfoundmessage"));
      }
    }
  }

  public configToggle(): void {
    this.isExpanded = !this.isExpanded;
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
