import { Component, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';

import { LayoutModel } from '../model/layoutmodel';
import { LayoutService } from '../meta-data/layout/layout.service';
import { NgbModal, NgbModalOptions, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CommonService } from '../services/common.service';
import { ResourceService } from '../services/resource.service';
import { TosterService } from '../services/toster.service';
// import { LayoutModel } from '../../../model/layoutmodel';
// import { LayoutService } from '../layout.service';
import { Subject } from "rxjs";
import { debounceTime, distinctUntilChanged } from "rxjs/internal/operators";
import swal from 'sweetalert2';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { MenuService } from '../services/menu.service';


@Component({
  selector: 'user',
  templateUrl: './user.component.html',
})
export class UserComponent implements OnInit {
  isConfigToggle: boolean = false;

  public layoutInfo: LayoutModel = new LayoutModel();
  
  public result: any;
  public freetextsearch: string;
  public displaySearch: boolean = false;
  public modalReference: any;
  public layoutForm: FormGroup;
  public resource: any;
  public columns = [];
  public sort: SortDescriptor[];
  public sendtobuttons = [];
  public printbuttons = [];
  public freetextsearchChanged: Subject<string> = new Subject<string>();
  public actions: Array<any>;
  public gridData: any;
  public gridDataResult: GridDataResult;
  public skip = 0;
  public groups: GroupDescriptor[];

  private id: string;
  private entityname: string;
  //private currentPage: number = 1;
  public pageindex: number = 1;
  private pageSize: number = 10;
  public totalRecords: number = 0;
  private orderBy: string = '';

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private resourceService: ResourceService,
    private commonService: CommonService,
    private toster: TosterService,
    private globalResourceService: GlobalResourceService,
    public menuService: MenuService
  ) {
  }


  ngOnInit() {
    this.entityname = "User";  
    this.getResource();      

    this.freetextsearchChanged
      .pipe(debounceTime(2000), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;

        // api call
        this.displayPreview();
      });
    // this.activatedRoute.params.subscribe((params: Params) => {
    //   this.id = 'C03C407D-F0AB-4F1E-81B0-E23EF1C10875'
    //   this.entityname = "User";
    //   if (this.id) {
    //     this.getLayoutById(this.entityname, this.id);
    //   }
    // });  

    // {entityName}/layouts/
    //this.buttons = [{name:"Send email"}, {name:"Send sms"}];
    
  }

  onFieldChange(query: string) {
    this.freetextsearchChanged.next(query);
  }

  private getDefaultLayout(name:string, type:string, subtype:string, context:string) {
    this.layoutService.getDefaultLayout(name, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutInfo = data;

            if(this.layoutInfo.listLayoutDetails.searchProperties.length > 0)
            {
              this.layoutInfo.listLayoutDetails.searchProperties.forEach((result, index) => {  
                if(result.name === 'SimpleSearch' || result.name === 'AdvanceSearch') 
                {
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

            //default group by set from layout = [{ field: 'Category.CategoryName' }];
            if (this.layoutInfo.listLayoutDetails && this.layoutInfo.listLayoutDetails.defaultGroupBy){ 
              if(this.layoutInfo.listLayoutDetails.defaultGroupBy){
                let defaultGroup={field:this.commonService.camelize(this.layoutInfo.listLayoutDetails.defaultGroupBy) };
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
            
            if(this.layoutInfo && this.layoutInfo.listLayoutDetails.fields.length > 0)
            {
              var queryString = "";
              for (var k = 0; k < this.layoutInfo.listLayoutDetails.fields.length; k++) {
                if (this.layoutInfo.listLayoutDetails.fields[k].hidden === false) {
                  queryString += this.layoutInfo.listLayoutDetails.fields[k].name + ",";
                }
              }
              queryString = queryString.substring(0, queryString.length - 1);

              if(queryString != "" && queryString.length > 0)
              {
                this.displayPreview();
              }              
            }
            
            this.displaySearch = true;

            if(this.layoutInfo.listLayoutDetails != null && this.layoutInfo.listLayoutDetails.toolbar.length > 0)
            {
              this.layoutInfo.listLayoutDetails.toolbar.forEach((button, index) => {
                if(button.group === 'communication')
                {
                  this.sendtobuttons.push(button);
                }
                else if(button.group === 'print')
                {
                  this.printbuttons.push(button);
                }
              });
            }
          }
        },
        error => {
          console.log(error);
        });   
  }

  private displayPreview() {
    var query = this.getQuery();
    this.layoutService.displayPreview(this.entityname, query)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.result = data.result;

            if(data.result && data.result.length > 0)
            {
              //this.result.forEach(function (elements) { delete elements.internalId });

              if (this.layoutInfo.listLayoutDetails.fields.length > 0) {
                this.layoutInfo.listLayoutDetails.fields.forEach((field, index) => {
                  if (field.dataType == 'PickList') {
                    this.result.forEach((result, index) => {
                      if (field.name.toLocaleLowerCase() in result) {
                        if (result.active) {
                          result.active = 'Enabled';
                          if (field.values && field.values.length > 0) {
                            result.css = field.values.filter(x => x.id == '1')[0].value;
                          }
                        } else {
                          result.active = 'Disabled';
                          if (field.values && field.values.length > 0) {
                            result.css = field.values.filter(x => x.id == '0')[0].value;
                          }
                        }
                      }

                    });
                  }

                   //clickable field
                   if(field.clickable)
                   {
                     //var fieldname = field.name.toLocaleLowerCase();
                     var fieldname = '';
                     if(field.name)
                     {
                       fieldname = this.camelize(field.name);
   
                       this.result.forEach((result, index) => {
                         // if (field.name.toLocaleLowerCase() in result) {
                         //   result.clickable = result[index];
                         // }
                         if (fieldname in result) {
                           result.clickable = result[fieldname];
                         }                      
                       });
                     }                  
                   }           

              });

            }

            this.generateColumns(data.result, this.layoutInfo.listLayoutDetails.fields, this.layoutInfo.listLayoutDetails.actions);

            //below values are requred for kendo grid dynamic paging
            this.totalRecords = data.totalRow;
            //this.gridDataResult = { data: this.results, total: this.totalRecords }
            this.gridData = this.result;

            //this.generateColumns();
            }
            else {
              //No layout found
              this.result = [];
              this.result.length = 0;
              this.totalRecords = 0;
              this.gridDataResult = { data: this.result, total: this.totalRecords }
              this.gridData = this.gridDataResult;
            }
            
          }
        },
        error => {
          console.log(error);
        });

  }


  
  //private getQuery() {
  //  var queryString = "";
  //  if (this.layoutInfo && this.layoutInfo.listLayoutDetails.fields) {
  //      for (var k = 0; k < this.layoutInfo.listLayoutDetails.fields.length; k++) {
  //          queryString += this.layoutInfo.listLayoutDetails.fields[k].name+",";      
  //      }
  //      queryString = queryString.substring(0, queryString.length-1);
  //  }
  //  queryString += "&pageIndex=" + this.currentPage + "&pageSize=" + this.pageSize;
  //  return queryString;
  //}
  
  private getQuery() {
    var queryString = "";
    
    if (this.layoutInfo && this.layoutInfo.listLayoutDetails.fields) {
      for (var k = 0; k < this.layoutInfo.listLayoutDetails.fields.length; k++) {
        if (this.layoutInfo.listLayoutDetails.fields[k].hidden === false) {
          queryString += this.layoutInfo.listLayoutDetails.fields[k].name + ",";
        }
      }
      queryString = queryString.substring(0, queryString.length - 1);
    }
    if(this.freetextsearch){
      queryString+="&searchText="+this.freetextsearch;
    }
    queryString += "&pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;
    var str = "";
    this.layoutInfo.listLayoutDetails.searchProperties.forEach(element => {
      if(element.name !== 'AdvanceSearch') 
      {
        element.properties.forEach(prop => {
          if (prop.value != null) {
            str += prop.name + "," + prop.value + "|";
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

  public onGridChangeEvent(event) {
    this.totalRecords=0;
    // let maxResult: number;
    // let isvalid: boolean = true;
    // let selectedFields: string = '';
    // let filters: string = '';

    this.pageindex = event.pageIndex;
    this.pageSize = event.pageSize;

    var query = this.getQuery();
    this.layoutService.displayPreview(this.entityname, query)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.result = data.result;
            this.skip = event.skip;            

             if (event.groupBy && event.groupBy.length > 0){
                this.groups = event.groupBy;
             }
                   
            if (event.sort) {
                this.sort = event.sort;
            }
            this.totalRecords=0;
            this.totalRecords = data.totalRow;
            this.gridData = this.result;

            if(data.result && data.result.length > 0)
            {
              //this.result.forEach(function (elements) { delete elements.internalId });

              if (this.layoutInfo.listLayoutDetails.fields.length > 0) {
                this.layoutInfo.listLayoutDetails.fields.forEach((field, index) => {
                  if (field.dataType == 'PickList') {
                    this.result.forEach((result, index) => {
                      if (field.name.toLocaleLowerCase() in result) {
                        if (result.active) {
                          result.active = 'Enabled';
                          if (field.values && field.values.length > 0) {
                            result.css = field.values.filter(x => x.id == '1')[0].value;
                          }
                        } else {
                          result.active = 'Disabled';
                          if (field.values && field.values.length > 0) {
                            result.css = field.values.filter(x => x.id == '0')[0].value;
                          }
                        }
                      }

                    });
                  }

                   //clickable field
                   if(field.clickable)
                   {
                     //var fieldname = field.name.toLocaleLowerCase();
                     var fieldname = '';
                     if(field.name)
                     {
                       fieldname = this.camelize(field.name);
   
                       this.result.forEach((result, index) => {
                         // if (field.name.toLocaleLowerCase() in result) {
                         //   result.clickable = result[index];
                         // }
                         if (fieldname in result) {
                           result.clickable = result[fieldname];
                         }                      
                       });
                     }                  
                   }           

              });

            }

            this.generateColumns(data.result, this.layoutInfo.listLayoutDetails.fields, this.layoutInfo.listLayoutDetails.actions);

            //this.generateColumns();
            }
            else {
              //No layout found
              this.result = [];
              this.result.length = 0;
              this.totalRecords = 0;
              this.gridDataResult = { data: this.result, total: this.totalRecords }
              this.gridData = this.gridDataResult;
            }
            
          }
        },
        error => {
          console.log(error);
        });

  }
  
  private createForm() {
    this.layoutForm = this.formBuilder.group({
      //layoutName: '',
      //drpType: '',
      //drpSubType: '',
      //drpContext: ''
    });
  }

  openAdvanceSearch(content) {
    //alert('Hello');
    this.createForm();
    this.modalReference = this.modalService.open(content);
  }

  AdvanceSearch() {
    this.modalReference.close();
    this.displayPreview();
  }  

  private onChange() {
    this.displayPreview();

  }

  edit(id)
  {
    //this.router.navigate(["users/edit" + "/" + id.internalId + "?subType=Employee"]);
    this.router.navigate(["users/edit" + "/" + id.internalId]);
  }

  private getResource() {
    this.resource=this.resourceService.getResources()

             //Get the picklist entity name from URL route 
             this.activatedRoute.url.subscribe((urlPath) => {
              //this.entityname = urlPath[urlPath.length - 1].path;
              if (this.entityname) {
                this.pageindex = 1;
                this.skip = 0;
                this.freetextsearch = '';
                //this.getDefaultLayout(this.entityName);
                this.getDefaultLayout(this.entityname, "List", "", "");
              } else {
                this.toster.showWarning(this.getResourceValue("metadata_operation_alert_warning_message"));
              }
            });
            
          
  }

  generateColumns(results: Array<any>, fields: any, actions: Array<any>) {
    var that = this;
    that.columns = [];
    let isActionsAvailable: boolean = false;

    if (actions != null && actions.length > 0) {
      isActionsAvailable = true;
      this.actions = actions;
      //console.log(this.actions);
    }

    if (results.length > 0) {
      let i = 0;
      Object.keys(this.result[0]).forEach(function (key, index) {
        i++;

        let isFieldAvailable: boolean = false;
        let isStatusField: boolean = false;
        let isClickableField: boolean = false;

        isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
        isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden).length > 0 ? true : false;
        isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
        let widthValue:number=40;
        if(isStatusField){
          widthValue=20;
        }

        let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, isClickable: isClickableField, isAction: false,position: 0 };
        
        let columnTitle:string='';
        if(isFieldAvailable){
          columnTitle=that.resource[that.generateResourceName(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden)[0].name)]
        }

        columnObj.field = key.replace('.', '_');
        columnObj.title = columnTitle;
        columnObj.position = i;
        that.columns.push(columnObj);        
      });
    }

    if (isActionsAvailable) {
      let actionColumnObj = { field: actions.length > 1 ? 'Actions' : 'Action', title: actions.length > 1 ? 'Actions' : 'Action', width: 20, isVisible: true, isStatus: false, position: 0, isAction: true };
      that.columns.push(actionColumnObj);
    }

    that.result.forEach((result, index) => {

      Object.keys(result).forEach(function (key, index) {
        if (key.split(".").length > 1) {
          let value: string = '';
          value = key.replace('.', '_');
          result[value] = result[key];
          //delete result[key];
        }
      });

    });
  }

  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1); 
  }

  camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function(match, index) {
      if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
      return index == 0 ? match.toLowerCase() : match.toUpperCase();
    });
  }

  buttonOperation(operationName:string):void {
    //alert(buttonname);
    if(operationName.toLocaleLowerCase() === 'create')
      this.router.navigate(["/users/new"]);
    else
      this.toster.showWarning(this.getResourceValue("metadata_method_notimplement_message"));
  }

  configToggle() {
    this.isConfigToggle = !this.isConfigToggle;    
  }

  onActionClick(event) {
    if (event.actionName.toLowerCase() == 'delete'){
    this.globalResourceService.openDeleteModal.emit()
    this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
      this.layoutService.deleteUserValues(this.entityname, event.id).subscribe(result => {
        if (result) {
          this.displayPreview();
        }
      });

    });

  }



    // if (event.actionName.toLowerCase() == 'delete') {
    //   swal({
    //     title: this.getResourceValue("common_message_areyousure"),
    //     text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
    //     type: 'warning',
    //     showCancelButton: true,
    //     confirmButtonColor: '#3085d6',
    //     cancelButtonColor: '#d33',
    //     confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
    //     showLoaderOnConfirm: true,
    //   })
    //     .then((willDelete) => {
    //       if (willDelete.value) {
    //         this.layoutService.deleteUserValues(this.entityname, event.id).subscribe(result => {
    //           if (result) {
    //             this.displayPreview();
    //           }
    //         });

    //       } else {
    //         //write the code for cancel click
    //       }

    //     });
    // }

    if (event.actionName.toLowerCase() == 'UpdateStatus'.toLowerCase()) {
      this.toster.showWarning(this.getResourceValue("metadata_method_notimplement_message"));
    }

  }

  // onActionClick(actionName: string, id: string) {
  //   console.log(actionName);
  //   console.log(id);


  //   if (actionName.toLowerCase() == 'Delete'.toLowerCase()) {
  //     swal({
  //       title: 'Are you sure?',
  //       text: "You won't be able to revert this!",
  //       type: 'warning',
  //       showCancelButton: true,
  //       confirmButtonColor: '#3085d6',
  //       cancelButtonColor: '#d33',
  //       confirmButtonText: 'Yes, delete it!',
  //       showLoaderOnConfirm: true,
  //     })
  //       .then((willDelete) => {
  //         if (willDelete.value) {
  //           this.layoutService.deleteUserValues(this.entityname, id).subscribe(result => {
  //             if (result) {
  //               this.displayPreview();
  //             }
  //           });

  //         } else {
  //           //write the code for cancel click
  //         }

  //       });
  //   }

  //   if (actionName.toLowerCase() == 'UpdateStatus'.toLowerCase()) {
  //     this.toster.showWarning('Method not implemented !');
  //   }

  // }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
