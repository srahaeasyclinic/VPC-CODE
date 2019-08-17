import { Component, OnInit, EventEmitter, ChangeDetectionStrategy, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import{ActivatedRoute} from '@angular/router';
import { GridDataResult, DataStateChangeEvent, SelectableSettings } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor, process } from '@progress/kendo-data-query';
import { Observable, from } from 'rxjs';
import * as moment from 'moment';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { LayoutModel } from '../model/layoutmodel';
import { CommonService } from '../services/common.service';
import { MenuService, editableColumnname } from '../services/menu.service';
import { BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';


@Component({
  selector: 'dynamic-grid',
  templateUrl: './dynamic-grid.component.html',
  styleUrls: ['./dynamic-grid.component.css'],
  inputs: ["gridData", "totalRecords", "resources", "defaultLayout", "defaultSortOrder", "currentPage", "dataSkip", "groupBy", "pageSize", "pageable"],
  outputs: ["rowColumnClickEvent:columnClick", "actionClickEvent:onActionClick", "gridChangeEvent:onGridChangeEvent"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicGridComponent implements OnChanges, OnInit {
  public resources: any | null;
  public entityName:string;
  public gridData: any | null;
  public totalRecords: number;
  public defaultLayout: LayoutModel = new LayoutModel();
  public defaultSortOrder: SortDescriptor[];
  public currentPage: number;
  public rowColumnClickEvent: EventEmitter<any>;
  public actionClickEvent: EventEmitter<any>;
  public gridChangeEvent: EventEmitter<any>;
  public dataSkip: number = 0;
  public groupBy: string;
  private view: Observable<GridDataResult>;
  private gridDataResult: GridDataResult;
  public groups: GroupDescriptor[]; // = [{ field: 'Category.CategoryName' }];
  public sort: SortDescriptor[];
  public data: any;
  public columns = [];
  private actions: Array<any>;
  private dateFormat = this.commonService.defaultDateformat();
  public skip = 0;
  private pageindex: number = 1;
  public pageSize: number = this.commonService.defaultPageSize();
  public selectableSettings: SelectableSettings;

  constructor(private breadcrumsService: BreadcrumbsService,private commonService: CommonService,public globalResourceService:GlobalResourceService,private route:ActivatedRoute,private menuService: MenuService) {
    this.gridData = null;
    this.resources = null;
    this.rowColumnClickEvent = new EventEmitter();
    this.actionClickEvent = new EventEmitter();
    this.gridChangeEvent = new EventEmitter();

    this.selectableSettings = {
      mode: 'single'
    };
  }

  ngOnChanges(changes: SimpleChanges) {

    let totalData: any;
    let total: number;

    // if (changes.resources && changes.resources.currentValue) {
    //   this.resources = changes.resources.currentValue;
    // }

    if (changes.defaultLayout && changes.defaultLayout.currentValue) {
      this.defaultLayout = changes.defaultLayout.currentValue;
    }

    if (changes.totalRecords && changes.totalRecords.currentValue > -1) {
      total = changes.totalRecords.currentValue;
    }

    if (changes.gridData && changes.gridData.currentValue) {
      totalData = changes.gridData.currentValue;

    }

    if (changes.dataSkip && changes.dataSkip.currentValue > -1) {
      this.skip = changes.dataSkip.currentValue;
    }

    if (changes.defaultSortOrder && changes.defaultSortOrder.currentValue) {
      this.sort = changes.defaultSortOrder.currentValue
    }

    if (changes.currentPage && changes.currentPage.currentValue) {
      this.pageindex = this.currentPage;
    }

    if (changes.groupBy && changes.groupBy.currentValue) {
      this.groups = changes.groupBy.currentValue;// [{ field: changes.groupBy.currentValue }];
      //console.log('SimpleChanges',this.groups)
    }

    if (this.defaultLayout && this.defaultLayout.listLayoutDetails && totalData && total > -1) {

      this.generateColumns(totalData, this.defaultLayout.listLayoutDetails.fields, this.defaultLayout.listLayoutDetails.actions, this.groups, total)

    }

  }

  ngOnInit() {
    // this.route.parent.params.subscribe((urlPath) => {
    //   this.entityName = urlPath["name"];
    // });
    this.getMenuparameterName();
  }


  private generateColumns(results: Array<any>, fields: any, actions: Array<any>, group: GroupDescriptor[], totalRecord: number): void {
    //console.log(results);
    //console.log(fields);
    let isActionsAvailable: boolean = false;

    fields.forEach((field, index) => {

      //Boolean type fields displayed as disabled checkbox
      if (field.dataType.toLowerCase() == 'bool') {
        results.forEach((result, index) => {
          if (field.name in result) {
            let selectedField = field.name;
            //console.log('GC',result );
            if (!field.hidden && result[selectedField]) {
              result.boolFieldValue = result[selectedField];
            }

          }
        });
      }

      //Status type picklist
      // if (field.dataType == 'PickList') {
      //   results.forEach((result, index) => {
      //     if (field.name.toLocaleLowerCase() in result) {
      //       if (result.active) {
      //         result.active = 'Enabled';
      //         if (field.values && field.values.length > 0) {
      //           result.css = field.values.filter(x => x.id == '1')[0].value;
      //         }
      //       } else {
      //         result.active = 'Disabled';
      //         if (field.values && field.values.length > 0) {
      //           result.css = field.values.filter(x => x.id == '0')[0].value;
      //         }
      //       }
      //     }

      //   });
      // }

      if (field && field.dataType == 'PickList' && field.name.toLocaleLowerCase() == 'active') {
        //console.log('picklist'+JSON.stringify(field));
        results.forEach((result, index) => {
          if (field.name.toLocaleLowerCase() in result) {
            if (result.active === true) {
              result.active = 'Enable';
              if (field.values && field.values.length > 0) {
                result.css = field.values.filter(x => x.id == '1')[0].value;
              }
            } else if (result.active === false) {
              result.active = 'Disable';
              if (field.values && field.values.length > 0) {
                result.css = field.values.filter(x => x.id == '0')[0].value;
              }
            }
          }

        });
      }
      if (field && field.dataType == 'PickList' && field.name.toLocaleLowerCase() != 'active') {
        //console.log('PickList && field.name.toLocaleLowerCase() !== active '+JSON.stringify(results));
        //console.log('picklist'+JSON.stringify(field));
        results.forEach((r, index) => {
          let fieldname = this.commonService.camelize(field.name);
          if (fieldname in r) {
            if (field.values && field.values.length > 0) {
              r.PickListvalue = r[fieldname];
              let dd = field.values.filter(x => x.id == r.PickListvalue);
              r.PickListcss = field.values.filter(x => x.id == r.PickListvalue).value;
            }
          }
        });
      }

      //check whether the data contains any date objects and check the datetime values;
      if (field.dataType == 'DateTime') {
        results.forEach((result, index) => {
          if (field.name in result) {
            var datefield = field.name;

            if (!field.hidden && result[datefield]) {
              //console.log(result[datefield]);
              if (moment(result[datefield], moment.ISO_8601, true).isValid()) {
                result[datefield] = new Date(result[datefield]);
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
          results.forEach((result, index) => {
            if (fieldname in result) {
              result.clickable = result[fieldname];
            }
          });
        }
      }
    });

    if (actions != null && actions.length > 0) {
      isActionsAvailable = true;
      this.actions = actions;
      //console.log(this.actions);
    }


    var that = this;
    that.columns.length = 0;
    let i = 0;
    var that = this;
    if (results && results.length > 0) {
      Object.keys(results[0]).forEach(function (key, index) {
        i++;
        let isFieldAvailable: boolean = false;
        let isStatusField: boolean = false;
        let isClickableField: boolean = false;
        let isDateField: boolean = false;
        let isBooleanField: boolean = false;
        let isPicklisttype: boolean = false;

        isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.values && !x.hidden).length > 0 ? true : false;
        //isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'PickList'.toLocaleLowerCase() && !x.hidden && x.values && x.values.length > 0).length > 0 ? true : false;
        isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'PickList'.toLocaleLowerCase() && !x.hidden && x.values && x.values.length > 0 && x.name.toLowerCase()=='active').length > 0 ? true : false;
        isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
        isDateField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'DateTime'.toLocaleLowerCase() && !x.hidden).length > 0 ? true : false;
        isBooleanField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'Bool'.toLocaleLowerCase() && !x.hidden).length > 0 ? true : false;
        isPicklisttype=fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'PickList'.toLocaleLowerCase() && !x.hidden && x.values && x.values.length > 0 && x.name.toLowerCase()!='active').length > 0 ? true : false;

        let widthValue: number = 40;
        if (isStatusField) {
          widthValue = 20;
        }

        let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, position: 0, isClickable: isClickableField, isAction: false, isDateField: isDateField, isBooleanField: isBooleanField, isPicklisttype:isPicklisttype };

        let columnTitle: string = '';
        if (isFieldAvailable) {
          columnTitle = that.getResourceValue(that.entityName.toLowerCase()+'_field_'+(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.values && !x.hidden)[0].name).replace('.','_').toLowerCase());
        }
          if (isPicklisttype) {
              columnTitle = that.getResourceValue(key.replace('.', '_').toLowerCase());
        }

        columnObj.field = key.replace('.', '_');
        columnObj.title = columnTitle;
        columnObj.position = i;

        if (columnObj.isVisible || columnObj.isStatus||isPicklisttype) {
          that.columns.push(columnObj);
        }
      });
    }


    //console.table(that.columns);

    if (isActionsAvailable) {
      let actionColumnObj = { field: actions.length > 1 ? 'Actions' : 'Action', title: actions.length > 1 ? that.getResourceValue('metadata_actions') : that.getResourceValue('metadata_action'), width: 20, isVisible: true, isStatus: false, position: 0, isAction: true, isDateField: false };
      that.columns.push(actionColumnObj);
    }

    results.forEach((result, index) => {
      Object.keys(result).forEach(function (key, index) {
        if (key.split('.').length > 1) {
          let value: string;
          value = key.replace('.', '_');
          result[value] = result[key];
        }
      });
    });


    //final grouping
    let groupdata: any;

    if (this.groups && this.groups.length == 0) {
      if (this.defaultLayout && this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.defaultGroupBy) {
        this.groups = [{ field: this.defaultLayout.listLayoutDetails.defaultGroupBy }];
      }
    }


    if (this.groups && this.groups.length > 0) {
      groupdata = process(results, { group: this.groups });
    } else {
      groupdata = results;
      var emptyGroup: GroupDescriptor[] = [];
      this.groups = emptyGroup;
    }

    this.gridDataResult = { data: groupdata.data ? groupdata.data : groupdata, total: totalRecord }
    this.data = this.gridDataResult;

    //Create the group with resource name
    // if(this.defaultLayout.listLayoutDetails.defaultGroupBy){
    //   let groupName=this.commonService.camelize(this.defaultLayout.listLayoutDetails.defaultGroupBy);
    //   groupName=this.resources[this.commonService.generateResourceName(groupName)] ;
    //   this.groups=[{ field: groupName }];
    // }

  }

  //Events
  //First editable column click in grid
  private rowEditableColumnClick(id: string, fieldvalue: string,coulmnname:string): void {

    this.setbreadcrums(fieldvalue,coulmnname);

    this.rowColumnClickEvent.emit({ internalId: id });
  }

  //Action click
  private onActionClick(id: string, name: string): void {
    this.actionClickEvent.emit({ internalId: id, actionName: name })
  }

  //Sorting
  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
  }

  //Page change
  public pageChange(state: DataStateChangeEvent): void {
    this.skip = state.skip;

    if (state.group && state.group.length > 0) {
      this.groups = state.group;
    }

    if (state.group && state.group.length == 0) {
      var emptyGroup: GroupDescriptor[] = [];
      this.groups = emptyGroup;
    }


    //Calculating the paging requirements
    if (state.skip == 0) {
      this.pageindex = 1;
    } else {
      this.pageindex = (state.skip / this.pageSize) + 1;
    }

    let sortColumn: string = '';
    let sortOrder: string = '';
    let orderBy: string = '';

    //If sorting done then
    if (state.sort && state.sort.length > 0) {
      if (this.defaultLayout && this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.fields) {

        if (state.sort[0].field.includes("_")) {
          sortColumn = state.sort[0].field.replace('_', '.');
          var OrgColumn = this.defaultLayout.listLayoutDetails.fields.filter(x => x.name.toLowerCase().includes(sortColumn.toLowerCase()))[0].name;
          sortColumn = OrgColumn;
        } else {
          sortColumn = state.sort[0].field;
        }
        if (state.sort[0].dir) {
          sortOrder = state.sort[0].dir.toUpperCase();
        }
      }
      orderBy = sortColumn + ',' + sortOrder;
    }

    //this.generateListlayout(this.defaultLayout, this.entityName)

    this.gridChangeEvent.emit({ pageIndex: this.pageindex, pageSize: this.pageSize, orderBy: orderBy, skip: this.skip, sort: state.sort, groupBy: this.groups })

  }


  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  // BreadCrums
  setbreadcrums(fieldvalue:string,coulmnname:string)
  {
     let objeditableColumnname=new editableColumnname();
    objeditableColumnname.name = fieldvalue;
    objeditableColumnname.columnname = coulmnname;
    localStorage.setItem("editableColumnname", JSON.stringify(objeditableColumnname));
    this.breadcrumsService.setchildMenuBreadcums([{ elementName: "preview", 'elementURL': "" ,'isGroup':false}, { elementName: fieldvalue, 'elementURL': "",'isGroup':false }]);
  }
  //MenuContext
  getMenuparameterName()
  {
      let result=this.menuService.getMenuconext();
      this.entityName = result.param_name;
  }
}
