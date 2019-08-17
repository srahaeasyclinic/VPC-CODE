import { Component, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { CountryService } from '../country/country.service';
import { TosterService } from '../services/toster.service';
import { CommonService } from '../services/common.service';
import { ResourceService } from '../services/resource.service';
import { debounceTime, distinctUntilChanged } from 'rxjs/internal/operators';
import swal from 'sweetalert2';
import {GlobalResourceService} from '../global-resource/global-resource.service';
@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  isConfigToggle: boolean = false;

  constructor(private router: Router, private countryService: CountryService, 
              private toster: TosterService, private globalResourceService: GlobalResourceService, 
              private commonService: CommonService) { }

  public view: Observable<GridDataResult>;


  public defaultLayout: LayoutModel = new LayoutModel();
  private layoutType: number = 3; // List page
  private pageindex: number = 1;
  public pageSize: number = this.commonService.defaultPageSize();
  private totalRecords: number = 0;
  public columns = [];
  public resource: any;
  public results: any;
  public freetextsearchChanged: Subject<string> = new Subject<string>();
  public actions: Array<any>;
  public gridData: any;
  public gridDataResult: GridDataResult;
  public multiple = false;
  public allowUnsort = true;
  public freetextsearch: string;
  private orderBy: string = '';
  public sort: SortDescriptor[];
  public skip = 0;

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getDefaultLayout();

    this.freetextsearchChanged
      .pipe(debounceTime(500), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;
        // api call
        this.generateListlayout(this.defaultLayout);
      });
  }

  //Events sections
  public onFreeTextSearch(query: string): void {
    this.freetextsearchChanged.next(query);
  }

  public onSimpleSearch(): void {
    this.generateListlayout(this.defaultLayout);
  }

  public onActionClick(actionName: string, id: string):void {
    //console.log(actionName);
    //console.log(id);


    if (actionName.toLowerCase() == 'Delete'.toLowerCase()) {

      this.globalResourceService.openDeleteModal.emit()
      this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
        this.countryService.deleteCountry('country', id).subscribe(result => {
          if (result) {
            this.generateListlayout(this.defaultLayout)
          }
        });
         
        })






      // swal({
      //   title: this.getResourceValue("common_message_areyousure"),
      //   text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
      //   type: 'warning',
      //   showCancelButton: true,
      //   confirmButtonColor: '#3085d6',
      //   cancelButtonColor: '#d33',
      //   confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
      //   showLoaderOnConfirm: true,
      // })
      //   .then((willDelete) => {
      //     if (willDelete.value) {
      //       this.countryService.deleteCountry('country', id).subscribe(result => {
      //         if (result) {
      //           this.generateListlayout(this.defaultLayout)
      //         }
      //       });

      //     } else {
      //       //write the code for cancel click
      //     }

      //   });
    }

    if (actionName.toLowerCase() == 'UpdateStatus'.toLowerCase()) {
      this.toster.showWarning(this.getResourceValue("metadata_method_notimplement_message"));
    }

  }


  public edit(id): void {
    this.router.navigate(["country/edit/" + id]);
  }

  public pageChange(state: DataStateChangeEvent): void {
    this.skip = state.skip;
    if (state.skip == 0) {
      this.pageindex = 1;
    } else {
      this.pageindex = (state.skip / this.pageSize) + 1;
    }

    let sortColumn: string = '';
    let sortOrder: string = '';


    if (state.sort && state.sort.length > 0) {
      if (this.defaultLayout && this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.fields) {

        if (state.sort[0].field.includes("_")) {
          sortColumn = state.sort[0].field.replace('_', '.');
          var OrgColumn = this.defaultLayout.listLayoutDetails.fields.filter(x => x.name.toLowerCase().includes(sortColumn.toLowerCase()))[0].name;
          sortColumn = OrgColumn;
        } else {
          sortColumn = state.sort[0].field;
        }
 
        sortOrder = state.sort[0].dir.toUpperCase();
      }

      //et sortOrder:string=state.sort[0].dir.toUpperCase();
      this.orderBy = sortColumn + ',' + sortOrder;
    }

    this.generateListlayout(this.defaultLayout)
  }

  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    //this.generateListlayout(this.defaultLayout)
  }
 

  //Methods section
  // private getResource(): void {
  //   this.resourceService.getResources()
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.resource = data;
  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }

  private getDefaultLayout(): void {
    this.countryService.getDefaultLayout('country', this.layoutType, 0)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.defaultLayout = data;
            if (this.defaultLayout) {

              //generate the default orderby 
              if(this.defaultLayout.listLayoutDetails){
                if(this.defaultLayout.listLayoutDetails.defaultSortOrder){
                  //Currently throwing error that is why commented 
                  //this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
                  //{dir: "asc", field: "text"}
                  //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
                 if(!this.sort)
                  this.sort=[];
                  this.sort.push({dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase()=='ASC'?'asc':'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()});
                }
              }
              this.generateListlayout(this.defaultLayout) 
            } 
          }
        },
        error => {
          console.log(error);
        });
  }

  private generateListlayout(layout: LayoutModel): void {

    let isvalid: boolean = true;
    let selectedFields: string = '';
    let filters: string = '';

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
        this.toster.showWarning(this.getResourceValue('metadata_operation_warning_notfoundmessage'));
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

      if (isvalid) {
        this.countryService.getCountries('country', selectedFields, filters, this.pageindex, this.pageSize, maxResult, this.orderBy, this.freetextsearch)

          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
                this.results = [];
                this.results = data.result;
                //below values are requred for kendo grid dynamic paging
                this.totalRecords = data.totalRow;
                this.gridDataResult = { data: this.results, total: this.totalRecords }
                this.gridData = this.gridDataResult;
                if (data.result.length > 0) { 
                  if (layout.listLayoutDetails.fields.length > 0) {

                    layout.listLayoutDetails.fields.forEach((field, index) => {

                      if (field.dataType == 'PickList') {
                        this.results.forEach((result, index) => {
                          if (field.name.toLocaleLowerCase() in result) {
                            if (result.active) {
                              result.active = 'Enabled';
                              if (field.values || field.values.length > 0) {
                                result.css = field.values.filter(x => x.id == '1')[0].value;
                              }
                            } else {
                              result.active = 'Disabled';
                              if (field.values || field.values.length > 0) {
                                result.css = field.values.filter(x => x.id == '0')[0].value;
                              }
                            }
                          }

                        });
                      }
                      // Clickable fields implementation
                      if (field.clickable) {

                        var fieldname = '';
                        if (field.name) {
                          fieldname = this.commonService.camelize(field.name);

                          this.results.forEach((result, index) => {
                            if (fieldname in result) {
                              result.clickable = result[fieldname];
                            }
                          });
                        }
                      }
                    });

                  }
                  // console.log(this.results);
                  this.generateColumns(data.result, layout.listLayoutDetails.fields, layout.listLayoutDetails.actions);
                }
              }
            },
            error => {
              console.log(error);
            });
      }
    }
  }

  private generateColumns(results: Array<any>, fields: any, actions: Array<any>): void {

    var that = this;
    that.columns = [];
    // console.log('fields',fields);
    // console.log('results',results);
    let isActionsAvailable: boolean = false;

    if (actions != null || actions.length > 0) {
      isActionsAvailable = true;
      this.actions = actions;
      //console.log(this.actions);
    }


    let i = 0;
    Object.keys(results[0]).forEach(function (key, index) {
      i++;
      let isFieldAvailable: boolean = false;
      let isStatusField: boolean = false;
      let isClickableField: boolean = false;

      isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
      isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden).length > 0 ? true : false;
      isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
      let columnObj = { field: '', title: '', width: 40, isVisible: isFieldAvailable, isStatus: isStatusField, position: 0, isClickable: isClickableField, isAction: false };

      let columnTitle: string = '';
      if (isFieldAvailable) {
        columnTitle = that.resource[that.commonService.generateResourceName(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden)[0].name)]
      }
      columnObj.field = key.replace('.', '_');
      columnObj.title = columnTitle;
      columnObj.position = i;

      if (columnObj.isVisible || columnObj.isStatus) {
        that.columns.push(columnObj);
      }
    });

    if (isActionsAvailable) {
      let actionColumnObj = { field: actions.length > 1 ? 'Actions' : 'Action', title: actions.length > 1 ? 'Actions' : 'Action', width: 20, isVisible: true, isStatus: false, position: 0, isAction: true };
      that.columns.push(actionColumnObj);
    }
    //console.table(that.columns);
    that.results.forEach((result, index) => {

      Object.keys(result).forEach(function (key, index) {
        if (key.split('.').length > 1) {
          let value: string;
          value = key.replace('.', '_');
          result[value] = result[key];
          // delete result[key];
        }
      });

    });

    // console.log( that.results);

  }
  configToggle() {
    this.isConfigToggle = !this.isConfigToggle;    
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
