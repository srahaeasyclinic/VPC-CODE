import { Component, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../../model/layoutmodel';
import { LayoutService } from '../layout.service';
import { NgbModal, NgbModalOptions, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CommonService } from '../../../services/common.service';
import { TosterService } from '../../../services/toster.service';
import { Subject } from "rxjs";
import { debounceTime, distinctUntilChanged } from "rxjs/internal/operators";
import swal from 'sweetalert2';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'layout-preview',
  templateUrl: './layoutpreview.component.html',
  styleUrls: ['./layoutpreview.component.css']
})

export class LayoutPreviewComponent implements OnInit {
  public isExpanded: boolean = false;
  public layoutInfo: LayoutModel = new LayoutModel();
  public result: any;
  public freetextsearch: string;
  public displaySearch: boolean = false;
  public modalReference: any;
  public layoutForm: FormGroup;
  public resource: Resource;
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
    private commonService: CommonService,
    private toster: TosterService,
    public globalResourceService: GlobalResourceService,

  ) {
  }

  ngOnInit() {
    this.getResource();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.entityname = params['name'];
    });
    this.freetextsearchChanged
      .pipe(debounceTime(2000), distinctUntilChanged())
      .subscribe(model => {
        this.freetextsearch = model;
        this.displayPreview();
      });
  }

  public configToggle(): void {
    this.isExpanded = !this.isExpanded;
  }

  onFieldChange(query: string) {
    this.freetextsearchChanged.next(query);
  }

  private getResource() {
    //this.resource = this.globalResourceService.getGlobalResources();
    this.activatedRoute.url.subscribe((urlPath) => {
      if (this.entityname, this.id) {
        this.pageindex = 1;
        this.skip = 0;
        this.freetextsearch = '';
        this.getLayoutById(this.entityname, this.id);
      } else {
        this.toster.showWarning(this.globalResourceService.getResourceByKey("UrlTemperedorNoEntityNameoridFoundorEntityNotYetDecorated"));
      }
    });

  }

  private getLayoutById(name, layoutId) {
    this.layoutService.getLayoutById(name, layoutId)
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data && data) {
            //console.table(data);
            this.layoutInfo = data;
            if (this.layoutInfo.listLayoutDetails.searchProperties.length > 0) {
              this.layoutInfo.listLayoutDetails.searchProperties.forEach((result, index) => {
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
            if (this.layoutInfo.listLayoutDetails && this.layoutInfo.listLayoutDetails.defaultGroupBy) {
              if (this.layoutInfo.listLayoutDetails.defaultGroupBy) {
                let defaultGroup = { field: this.commonService.camelize(this.layoutInfo.listLayoutDetails.defaultGroupBy) };
                if (!this.groups) {
                  this.groups = [];
                }
                this.groups.push(defaultGroup);
              }
            } else {
              if (this.groups) {
                this.groups.length = 0;
              }
            }

            if (this.layoutInfo && this.layoutInfo.listLayoutDetails.fields.length > 0) {
              var queryString = "";
              for (var k = 0; k < this.layoutInfo.listLayoutDetails.fields.length; k++) {
                if (this.layoutInfo.listLayoutDetails.fields[k].hidden === false) {
                  queryString += this.layoutInfo.listLayoutDetails.fields[k].name + ",";
                }
              }
              queryString = queryString.substring(0, queryString.length - 1);

              if (queryString != "" && queryString.length > 0) {
                this.displayPreview();
              }
            }

            this.displaySearch = true;

            if (this.layoutInfo.listLayoutDetails != null && this.layoutInfo.listLayoutDetails.toolbar.length > 0) {
              this.layoutInfo.listLayoutDetails.toolbar.forEach((button, index) => {
                if (button.group === 'communication') {
                  this.sendtobuttons.push(button);
                }
                else if (button.group === 'print') {
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
            if (data.result && data.result.length > 0) {
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
                  if (field.clickable) {
                    var fieldname = '';
                    if (field.name) {
                      fieldname = this.camelize(field.name);
                      this.result.forEach((result, index) => {
                        if (fieldname in result) {
                          result.clickable = result[fieldname];
                        }
                      });
                    }
                  }
                });
              }
              //this.generateColumns(data.result, this.layoutInfo.listLayoutDetails.fields, this.layoutInfo.listLayoutDetails.actions);
              this.generateColumns(data.result, this.layoutInfo.listLayoutDetails.fields, this.layoutInfo.listLayoutDetails.actions);
              this.totalRecords = data.totalRow;
              this.gridData = this.result;
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

  // generateColumns(results: Array<any>, fields: any, actions: Array<any>) {
  //   var that = this;
  //   that.columns = [];
  //   let isActionsAvailable: boolean = false;
  //   if (actions != null && actions.length > 0) {
  //     isActionsAvailable = true;
  //     this.actions = actions;
  //   }
  //   if (results.length > 0) {
  //     let i = 0;
  //     Object.keys(results[0]).forEach(function (key, index) {
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

  //       let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, isClickable: isClickableField, position: 0 };

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
  //   that.result.forEach((result, index) => {
  //     Object.keys(result).forEach(function (key, index) {
  //       if (key.split(".").length > 1) {
  //         let value: string = '';
  //         value = key.replace('.', '_');
  //         result[value] = result[key];
  //       }
  //     });
  //   });
  // }

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
      Object.keys(results[0]).forEach(function (key, index) {
        i++;

        let isFieldAvailable: boolean = false;
        let isStatusField: boolean = false;
        let isClickableField: boolean = false;

        isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden).length > 0 ? true : false;
        isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden).length > 0 ? true : false;
        isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;

        let widthValue: number = 40;
        if (isStatusField) {
          widthValue = 20;
        }

        let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, isClickable: isClickableField, isAction: false, position: 0 };

        let columnTitle: string = '';
        if (isFieldAvailable) {
          columnTitle = that.globalResourceService.getResourceByKey(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && x.dataType !== 'PickList' && !x.hidden)[0].name);
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
        }
      });

    });

    //console.table(that.columns);
  }

  // generateResourceName(word) {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }

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
    if (this.freetextsearch) {
      queryString += "&searchText=" + this.freetextsearch;
    }
    queryString += "&pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;
    var str = "";
    this.layoutInfo.listLayoutDetails.searchProperties.forEach(element => {
      if (element.name !== 'AdvanceSearch') {
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

  private createForm() {
    this.layoutForm = this.formBuilder.group({
    });
  }



  openAdvanceSearch(content) {
    this.createForm();
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(content, ngbModalOptions);
  }

  AdvanceSearch() {
    this.modalReference.close();
    this.displayPreview();
  }

  edit(id) {
  }

  private onChange() {
    this.displayPreview();

  }

  onTextChange(property) {
    alert(this.freetextsearch);
  }


  onRadioChange(option) {
    alert(option);
  }

  onChangeEvent(ev) {
    alert(ev.target.value);
  }

  camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
      if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
      return index == 0 ? match.toLowerCase() : match.toUpperCase();
    });
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  public onGridChangeEvent(event) {
    this.totalRecords = 0;
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
            if (event.groupBy && event.groupBy.length > 0) {
              this.groups = event.groupBy;
            }
            if (event.sort) {
              this.sort = event.sort;
            }
            this.totalRecords = 0;
            this.totalRecords = data.totalRow;
            this.gridData = this.result;
            if (data.result && data.result.length > 0) {
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
                  if (field.clickable) {
                    var fieldname = '';
                    if (field.name) {
                      fieldname = this.camelize(field.name);
                      this.result.forEach((result, index) => {
                        if (fieldname in result) {
                          result.clickable = result[fieldname];
                        }
                      });
                    }
                  }
                });
              }
              this.generateColumns(data.result, this.layoutInfo.listLayoutDetails.fields, this.layoutInfo.listLayoutDetails.actions);
            }
            else {
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


  onActionClick(event) {
    if (event.actionName.toLowerCase() == 'Delete'.toLowerCase()) {
      swal({
        title: this.globalResourceService.getResourceByKey("Areyousure"),
        text: this.globalResourceService.getResourceByKey("Youwntbeabletorevertthis"),
        type: this.globalResourceService.getResourceByKey('warning'),
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: this.globalResourceService.getResourceByKey('Yesdeleteit'),
        showLoaderOnConfirm: true,
      })
        .then((willDelete) => {
          if (willDelete.value) {
            this.layoutService.deleteUserValues(this.entityname, event.internalId).subscribe(result => {
              if (result) {
                this.displayPreview();
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





}
