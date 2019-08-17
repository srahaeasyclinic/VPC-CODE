import { Component, OnInit, EventEmitter, ChangeDetectionStrategy, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { PageChangeEvent, GridComponent, GridDataResult, DataStateChangeEvent, SelectableSettings } from '@progress/kendo-angular-grid';
import { Router, ActivatedRoute, NavigationExtras, Params, NavigationEnd } from '@angular/router';
import { State, SortDescriptor, GroupDescriptor, process } from '@progress/kendo-data-query';
import { Observable, Subject } from 'rxjs';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { GlobalResourceService } from '../global-resource/global-resource.service';

import { LayoutModel } from '../model/layoutmodel';
import { CommonService } from '../services/common.service';
import { first } from 'rxjs/operators';
import { LayoutService } from '../meta-data/layout/layout.service';
import { NgOnChangesFeature } from '@angular/core/src/render3';
import { MenuService, editableColumnname } from '../services/menu.service';
import { BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';


@Component({
  selector: 'common-grid-data',
  templateUrl: './common-grid-data.component.html',
  styleUrls: ['./common-grid-data.component.css'],
  inputs: ["gridData", "totalRecords", "resources", "defaultLayout", "defaultSortOrder", "currentPage", "dataSkip", "groupBy", "abc", "mode", "pageSize"],
  outputs: ["rowColumnClickEvent:columnClick", "actionClickEvent:onActionClick", "gridChangeEvent:onGridChangeEvent", "actionWorkFlowClick:onActionWorkFlowClick"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CommonGridDataComponent implements OnInit, OnChanges {

  @Input() mode: Number;

  public resources: any | null;
  public gridData: any | null;
  public totalRecords: number;
  public abc: number;
  public defaultLayout: LayoutModel = new LayoutModel();
  public defaultSortOrder: SortDescriptor[];
  public currentPage: number;
  public rowColumnClickEvent: EventEmitter<any>;
  public actionClickEvent: EventEmitter<any>;
  public gridChangeEvent: EventEmitter<any>;
  public actionWorkFlowClick: EventEmitter<any>;
  public dataSkip: number = 0;
  public groupBy: string;
  private view: Observable<GridDataResult>;
  private gridDataResult: GridDataResult;
  public groups: GroupDescriptor[]; // = [{ field: 'Category.CategoryName' }];
  public sortingOrderRightSearch: SortDescriptor[];
  public data: any;
  public columns = [];
  private actions: Array<any>;
  private dateFormat = this.commonService.defaultDateformat();
  public skip = 0;
  private pageindex: number = 1;
  public pageSize = this.commonService.defaultPageSize();
  public selectableSettings: SelectableSettings;


  constructor(private menuService: MenuService,private breadcrumsService: BreadcrumbsService,private commonService: CommonService, private router: ActivatedRoute, private globalResourceService: GlobalResourceService) {
    this.gridData = null;
    this.resources = null;
    this.rowColumnClickEvent = new EventEmitter();
    this.actionClickEvent = new EventEmitter();
    this.gridChangeEvent = new EventEmitter();
    this.actionWorkFlowClick = new EventEmitter();
    this.selectableSettings = {
      mode: 'single'
    };
  }


  public results: any;
  public freetextsearchChanged: Subject<string> = new Subject<string>();

  public multiple = false;
  public allowUnsort = true;
  public freetextsearch: string;

  public sort: SortDescriptor[];

  public isExpanded: boolean = false;
  public toolbarButtons = [];
  public sendToButtons = [];
  public printButtons = [];

  public subTypes = [];
  public modalReference: any;
  public selectLayoutForm: FormGroup;
  public selectedSubType: string;

  private entityName: string = '';
  private layoutType: string = 'List'; // List page
  // public pageindex: number = 1;
  // private pageSize: number = 10;
  // public totalRecords: number = 0;
  public resource: any;
  private orderBy: string = '';
  public storage: any;
  public filters: string = '';
  public selectedFields: string = '';
  private totalNumber: number;

  private kendoSearch = [];
  private grid: GridComponent;

  ngOnChanges(changes: SimpleChanges) {
    //console.log('\n\n\n\n changes ', JSON.stringify(changes))
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
      //console.log('changes.gridData ', changes.gridData);
      totalData = changes.gridData.currentValue;

    }

    if (changes.dataSkip && changes.dataSkip.currentValue > -1) {
      this.skip = changes.dataSkip.currentValue;
    }

    if (changes.defaultSortOrder && changes.defaultSortOrder.currentValue) {
      this.sortingOrderRightSearch = changes.defaultSortOrder.currentValue
      //console.log('CGD this.sortingOrderRightSearch  ::  '+this.sortingOrderRightSearch);
    }

    if (changes.currentPage && changes.currentPage.currentValue) {
      this.pageindex = this.currentPage;
    }

    if (changes.groupBy && changes.groupBy.currentValue) {
      this.groups = changes.groupBy.currentValue;// [{ field: changes.groupBy.currentValue }];
      //console.log('SimpleChanges',this.groups)
    }

    if (total) {
      this.totalNumber = total;
    }
    if (this.defaultLayout && this.defaultLayout.listLayoutDetails && totalData && this.totalNumber > -1) {
      //console.log('if ', JSON.stringify(totalData));
      this.generateColumns(totalData, this.defaultLayout.listLayoutDetails.fields, this.defaultLayout.listLayoutDetails.actions, this.groups, this.totalNumber);
    }
    else if (this.defaultLayout && this.defaultLayout.viewLayoutDetails && totalData && this.totalNumber > -1) {
      //console.log('else ', JSON.stringify(totalData));
      this.generateColumns(totalData, this.defaultLayout.viewLayoutDetails.fields, this.defaultLayout.viewLayoutDetails.actions, this.groups, this.totalNumber);
    }

  }

  ngOnInit() {
    this.getMenuparameterName();
  }

  private generateColumns(results: Array<any>, fields: any, actions: Array<any>, group: GroupDescriptor[], totalRecord: number): void {
    //console.log('results '+JSON.stringify(results));
    // console.log('fields ' + JSON.stringify(fields));

    let isActionsAvailable: boolean = false;
    fields.forEach((field, index) => {

      //Boolean type fields displayed as disabled checkbox
      if (field.dataType.toLowerCase() == 'bool') {
        results.forEach((result, index) => {
          if (this.commonService.camelize(field.name) in result) {
            let selectedField = this.commonService.camelize(field.name);
            //console.log('GC',result );
            if (!field.hidden && result[selectedField]) {
              result.boolFieldValue = result[selectedField];
            }

          }
        });
      }


      // //for css colur
      // if (field && field.dataType == 'PickList') {

      //   console.log("css values", field);
      // }

      //Status type picklist
      //Status type picklist
      //&& field.name.toLocaleLowerCase() !== 'active'


      //------------------ CLINT SIDE HACK ------------------------------------------------- NEED TO CHANGE THIS PORTION ---------------------------------
      if (field && field.dataType == 'PickList' && field.name.toLocaleLowerCase() == 'active') {
        //console.log('picklist'+JSON.stringify(field));
        results.forEach((result, index) => {
          if (field.name.toLocaleLowerCase() in result) {
            if (result.active != null) {
              var i = 0;
              if (result.active.toString().toLowerCase() == "true") {
                result.active = "Enable";
                i = 1;
              } else {
                result.active = "Disable";
              }
              // if (field.values && field.values.length > 0) {
              //   console.log("css values", field.values);
              //   //result.PickListcss = field.values.filter(x => x.id == i.toString())[0].value;
              // }
            }

          }
        });
      }
      //------------------ CLINT SIDE HACK ------------------------------------------------- NEED TO CHANGE THIS PORTION ---------------------------------



      
      // if (field && field.dataType == 'PickList' && field.name.toLocaleLowerCase() != 'active') {
      //   //console.log('PickList && field.name.toLocaleLowerCase() !== active '+JSON.stringify(results));
      //   //console.log('picklist'+JSON.stringify(field));

      //   // results.forEach((r, index) => {
      //   //   let fieldname = this.commonService.camelize(field.name);
      //   //   if (fieldname in r) {

      //   //     if (field.values && field.values.length > 0) {
      //   //       r.PickListvalue = r[fieldname];
      //   //       let dd = field.values.filter(x => x.id == r.PickListvalue);
      //   //       r.PickListcss = field.values.filter(x => x.id == r.PickListvalue).value;
      //   //     }

      //   //   }

      //   // });

      // }

      //check whether the data contains any date objects and check the datetime values;
      if (field.dataType == 'DateTime') {
        results.forEach((result, index) => {
          if (this.commonService.camelize(field.name) in result) {
            var datefield = this.commonService.camelize(field.name);

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
    else {
      results.forEach((result, index) => {
        if (result.currentWorkFlowStep !== null && result.currentWorkFlowStep !== undefined && result.innerSteps !== null && result.innerSteps !== undefined && result.innerSteps.length > 0) {
          isActionsAvailable = true;
        }
      });
    }


    var that = this;
    that.columns.length = 0;
    let i = 0;

    if (results && results.length > 0) {
      Object.keys(results[0]).forEach(function (key, index) {
        i++;
        let isFieldAvailable: boolean = false;
        let isStatusField: boolean = false;
        let isClickableField: boolean = false;
        let isDateField: boolean = false;
        let isBooleanField: boolean = false;
        let isPicklisttype: boolean = false;

        //isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.values && !x.hidden).length > 0 ? true : false;

        // isFieldAvailable = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && !x.values && !x.hidden).length > 0 ? true : false;

        isFieldAvailable = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && !x.hidden).length > 0 ? true : false;
        isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'PickList'.toLocaleLowerCase() && !x.hidden && x.values && x.values.length > 0 && x.name.toLowerCase() == 'active').length > 0 ? true : false;
        isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
        isDateField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'DateTime'.toLocaleLowerCase() && !x.hidden).length > 0 ? true : false;
        isBooleanField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'Bool'.toLocaleLowerCase() && !x.hidden).length > 0 ? true : false;
        isPicklisttype = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType.toLocaleLowerCase() == 'PickList'.toLocaleLowerCase() && !x.hidden && x.values && x.values.length > 0 && x.name.toLowerCase() != 'active').length > 0 ? true : false;
        let widthValue: number = 40;
        if (isStatusField) {
          widthValue = 20;
        }

        let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, position: 0, isClickable: isClickableField, isAction: false, isDateField: isDateField, isBooleanField: isBooleanField, isPicklisttype: isPicklisttype };

        let columnTitle: string = '';

        if (isFieldAvailable) {
          columnTitle = that.getResourceValue(that.entityName.toLowerCase() + '_field_' + (fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.hidden)[0].name).replace('.', '_').toLowerCase());
        }
        if (isPicklisttype) {

          columnTitle = that.getResourceValue(that.entityName.toLowerCase() + '_field_' + key.replace('.', '_').toLowerCase());
        }

        columnObj.field = key.replace('.', '_');
        columnObj.title = columnTitle;
        columnObj.position = i;

        if (columnObj.isVisible || columnObj.isStatus || isPicklisttype) {
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
        this.groups = [{ field: this.commonService.camelize(this.defaultLayout.listLayoutDetails.defaultGroupBy) }];
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

  //Generate the resource names
  // private generateResourceName(word: string) {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }


  //Events
  //First editable column click in grid
  private rowEditableColumnClick(id: string, fieldvalue: string, coulmnname: string): void {
    
    this.setbreadcrums(fieldvalue, coulmnname);
    
    this.rowColumnClickEvent.emit({ internalId: id });
  }

  //Action click
  private onActionClick(id, action): void {
    this.actionClickEvent.emit({ internalId: id, action: action })
    //console.log(id +"  name  ::  "+name);
  }

  private onActionWorkFlowClick(internalId, entityName, entitySubTypeName, currentWorkFlowTransitionId, nextTransitioinStepId, innerStep): void {
    this.actionWorkFlowClick.emit({ refId: internalId, entityName: entityName, subTypeName: entitySubTypeName, currentTransitionType: currentWorkFlowTransitionId, nextTransitionType: nextTransitioinStepId, innerStep: innerStep });
  }

  public checkActiveVesion(info)
  {
    if(info.activeVersion !=undefined)
    {
      return info.activeVersion;
    }
   return info.internalId;
  }

  

  //Sorting
  public sortChange(sort: SortDescriptor[]): void {
    this.sortingOrderRightSearch = sort;
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
      var targetFields = null;
      if (this.defaultLayout && this.defaultLayout.listLayoutDetails && this.defaultLayout.listLayoutDetails.fields) {
        targetFields = this.defaultLayout.listLayoutDetails.fields;
      } else if (this.defaultLayout && this.defaultLayout.viewLayoutDetails && this.defaultLayout.viewLayoutDetails.fields) {
        targetFields = this.defaultLayout.viewLayoutDetails.fields;
      }
      if (state.sort[0].field.includes("_")) {
        sortColumn = state.sort[0].field.replace('_', '.');
        var OrgColumn = targetFields.filter(x => x.name.toLowerCase().includes(sortColumn.toLowerCase()))[0].name;
        sortColumn = OrgColumn;
      } else {
        sortColumn = state.sort[0].field;
      }
      if (state.sort[0].dir) {
        sortOrder = state.sort[0].dir.toUpperCase();
      }
      orderBy = sortColumn + ',' + sortOrder;
    }

    //this.generateListlayout(this.defaultLayout, this.entityName)

    this.gridChangeEvent.emit({ pageIndex: this.pageindex, pageSize: this.pageSize, orderBy: orderBy, skip: this.skip, sort: state.sort, groupBy: this.groups })
    //this.gridChangeEvent.emit(state);
    console.log('this.gridChangeEvent');
  }

  private dataStateChange(state: DataStateChangeEvent) {
    console.log('state ', state.filter.filters);
    //console.log('this.kendoSearch 1 ', this.kendoSearch);
    let validation = this.validateExist(state);
    let index = validation["position"];
    let reset = validation["isExist"];
    if (reset) {
      this.kendoSearch[index] = state.filter.filters[0];
    }
    else {
      this.kendoSearch.push(state.filter.filters[0]);
    }
    console.log('this.kendoSearch 2 ', this.kendoSearch);
  }

  private validateExist(argState) {
    let isExist = false;
    if (this.kendoSearch.length > 0) {
      for (let i = 0; i < this.kendoSearch.length; i++) {
        //console.log('this.kendoSearch[i]["field"] ', this.kendoSearch[i]["field"]);
        //console.log('state.filter.filters[0]["field"] ', argState.filter.filters[0]["field"]);
        if (this.kendoSearch[i]["field"] === argState.filter.filters[0]["field"]) {
          isExist = true;
          return { "isExist": isExist, "position": i };
        }
      }
    }
    return { "isExist": isExist, "position": null };
  }


  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }



  //Added from Parent


  // for common data list
  // public edit(obj: any): void {

  //   //this.gridData
  //   let currentIndex: number = 0;
  //   let subType: string = "";
  //   this.gridData.forEach((item, index) => {
  //     if (item.internalId.toLowerCase() == obj.internalId.toLowerCase()) {
  //       currentIndex = index;
  //       subType = item.subType;
  //     }
  //   });


  //   let transitObject = {
  //     name: this.entityName,
  //     fields: this.selectedFields,
  //     searchText: this.freetextsearch,
  //     orderBy: this.orderBy,
  //     filters: this.filters,
  //     pageIndex: this.pageindex,
  //     pageSize: this.pageSize,
  //     itemIndex: currentIndex,
  //     totalRecords: this.totalRecords
  //   };

  //   console.log("transitObject.totalRecords :: " + transitObject.totalRecords);

  //   this.data.storage = transitObject;
  //   this.router.navigate(["ui/" + this.entityName + "/edit/" + obj.internalId], { queryParams: { subType: subType } });
  // }

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
