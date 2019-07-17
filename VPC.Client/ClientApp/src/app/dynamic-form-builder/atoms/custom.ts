import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TreeService } from '../service/tree.service';
import { first } from 'rxjs/operators';
import * as moment from 'moment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor, GroupDescriptor, DataResult, process } from '@progress/kendo-data-query';
import { LayoutService } from '../../meta-data/layout/layout.service';
import { NgbModal,NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from '../../services/toster.service';
import { MODALS } from '../tree.config';
import { Router, ActivatedRoute, Params } from '@angular/router';
import swal from 'sweetalert2';

// text,email,tel,textarea,password, 
@Component({
  selector: 'custom',
  template: `
    <div> 
    <utility-top-bar></utility-top-bar>
      <a *ngIf="mode!==2" class="btn btn-primary margin-top-20 margin-bottom-10" (click)="addDetailEntity()">Add</a>
      <app-general-ui-list entityName="{{field.name}}" layoutType="View" [mode]="mode"></app-general-ui-list>
    </div> 
    `
})

export class CustomComponent implements OnInit {

  @Input() field: any = {};
  @Input() mode: Number;
  @Input() form: FormGroup;


  //results: [any];
  private nodeName: string = "";
  private pageindex: Number = 1;
  private gridDataResult: GridDataResult;
  public actions: any | null;
  public columns = [];
  private userid: string = "";
  private entityName: string = "";
  private subType: string = "";
  
  // (columnClick)="edit($event)" 
  // (onActionClick)="onActionClick($event)"
  // (onGridChangeEvent)="onGridChangeEvent($event)"

  //layoutId: string |null;
  layoutDetails: any | null;
  public gridData: any | null;
  constructor(
    private treeService: TreeService, 
    private layoutService: LayoutService,
    private modalService: NgbModal,
    private activatedRoute: ActivatedRoute,
    private toster: TosterService
    ) {

  }
  public onSearchQueryEvent(event:any){

  }
  ngOnInit(): void {
    //console.log('this.ActivatedRoute', this.activatedRoute);
    this.activatedRoute.params.subscribe((params: Params) => {
      //console.log('ddd', params);
      this.userid = params['id'];
      //this.userid = this.userid.substring(0, 36);
      //this.entityName = params['name']
    }); 

    //Get the entity name from URL route 
    this.activatedRoute.parent.params.subscribe((urlPath) => {
      this.entityName = urlPath["name"];      
    });

    this.subType = this.activatedRoute.snapshot.queryParams["subType"];

    if(this.mode==1){
     
     // this.loadLayout();
    }
    
  }

  loadLayout(): void {
    console.log("get data form custome...", this.field);
    this.treeService.getViewLayout(this.field.name, this.field.selectedView)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutDetails = data;
            //this.gridDataResult = { data: this.layoutDetails.viewLayoutDetails.fields, total: this.layoutDetails.viewLayoutDetails.fields.length }

            // if(this.layoutDetails.viewLayoutDetails.fields.length > 0)
            // {
            //     this.layoutDetails.viewLayoutDetails.fields.forEach((field, index) => {
            //       //clickable field
            //       if(field.clickable)
            //       {
            //         //var fieldname = field.name.toLocaleLowerCase();
            //         var fieldname = '';
            //         if(field.name)
            //         {
            //           fieldname = this.camelize(field.name);

            //           this.layoutDetails.forEach((result, index) => {
            //             if (fieldname in result) {
            //               result.clickable = result[fieldname];
            //             }                      
            //           });
            //         }                  
            //       }         
            //   });
            // }
//action checking required.
            if(this.layoutDetails && this.layoutDetails.viewLayoutDetails && this.layoutDetails.viewLayoutDetails.fields){
              this.generateColumns(this.field.value, this.layoutDetails.viewLayoutDetails.fields, this.layoutDetails.viewLayoutDetails.actions, 10);
            }
            
          }
        },
        error => {
          console.log(error);
        });
  }

  camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
      if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
      return index == 0 ? match.toLowerCase() : match.toUpperCase();
    });
  }
  private generateColumns(results: Array<any>, fields: any, actions: Array<any>, totalRecord: number): void {
    //console.log(results);
    //console.log(fields);
    let isActionsAvailable: boolean = false;

    fields.forEach((field, index) => {

      if (field.dataType == 'PickList') {
        results.forEach((result, index) => {
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

      //  check whether the data contains any date objects and check the datetime values;
      if (field.dataType == 'DateTime') {
        results.forEach((result, index) => {
          if (this.camelize(field.name) in result) {
            var datefield = this.camelize(field.name);

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
          //fieldname = this.camelize(field.name);
          fieldname = field.name;
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

    Object.keys(results[0]).forEach(function (key, index) {
      i++;
      let isFieldAvailable: boolean = false;
      let isStatusField: boolean = false;
      let isClickableField: boolean = false;
      let isDateField: boolean = false;

      isFieldAvailable = fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.values && !x.hidden).length > 0 ? true : false;
      isStatusField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'PickList' && !x.hidden && x.values && x.values.length > 0).length > 0 ? true : false;
      isClickableField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.clickable == true && !x.hidden).length > 0 ? true : false;
      isDateField = fields.filter(x => x.name.toLowerCase() === key.toLowerCase() && x.dataType == 'DateTime' && !x.hidden).length > 0 ? true : false;

      let widthValue: number = 40;
      if (isStatusField) {
        widthValue = 20;
      }

      let columnObj = { field: '', title: '', width: widthValue, isVisible: isFieldAvailable, isStatus: isStatusField, position: 0, isClickable: isClickableField, isAction: false, isDateField: isDateField };

      let columnTitle: string = '';
      if (isFieldAvailable) {
        //columnTitle = that.resources[that.commonService.generateResourceName(fields.filter(x => x.name.toLowerCase().includes(key.toLowerCase()) && !x.values && !x.hidden)[0].name)]
      }
      columnObj.field = key.replace('.', '_');
      columnObj.title = columnTitle;
      columnObj.position = i;

      if (columnObj.isVisible || columnObj.isStatus) {
        that.columns.push(columnObj);
      }
    });

    console.table(that.columns);

    if (isActionsAvailable) {
      let actionColumnObj = { field: actions.length > 1 ? 'Actions' : 'Action', title: actions.length > 1 ? 'Actions' : 'Action', width: 20, isVisible: true, isStatus: false, position: 0, isAction: true, isDateField: false };
      that.columns.push(actionColumnObj);
    }

    results.forEach((result, index) => {
      Object.keys(result).forEach(function (key, index) {
        if (key.split('.').length > 1) {
          let value: string;
          value = key.replace('.', '_');
          result[value] = result[key];
          // delete result[key];
        }
      });

    });


    //final grouping
    // let groupdata: any;
    // if (this.groups && this.groups.length > 0) {
    //   groupdata = process(results, { group: this.groups });
    //   //console.log('groupdata', groupdata);
    // } else {
    //   groupdata = results;
    // }
    let groupdata = results;
    this.gridDataResult = { data: groupdata, total: totalRecord }
    // this.data = this.gridDataResult;
    //console.log('data', this.data);


  }

  public addDetailEntity(){
    var id = '';
    this.openDetailsEntityCreatePopup(id);
    //this.getDefaultLayout(this.field.name, "Form", "EN10003-ST01", "New");
  }

  // private getDefaultLayout(name:string, type:string, subtype:string, context:string) {
  //   this.layoutService.getDefaultLayout(name, type, subtype, context)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.openDetailsEntityCreatePopup();
  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });   
  // }
  openDetailsEntityCreatePopup(id) {
    var modalName = this.field.controlType+"_detailEntity";
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };
    const modalRef = this.modalService.open(MODALS[modalName],ngbModalOptions);
		// let nodeObj = JSON.parse(JSON.stringify(node))
    //modalRef.componentInstance.node = nodeObj;
    modalRef.componentInstance.entityName = this.entityName;
    modalRef.componentInstance.userid = this.userid;
    modalRef.componentInstance.detailEntityName = this.field.name;
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.subType = this.subType;

		modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
      console.log("saveEvent.subscribe", receivedEntry);  
      modalRef.close();
      
      if(this.mode==1){     
        this.loadLayout();
      }
		});
  }

  onActionClick(id, event) {

    if (event.toLowerCase() == 'delete') {
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
            this.layoutService.deleteUserValues(this.field.name, id).subscribe(result => {
              if (result) {
                if(this.mode==1){     
                  this.loadLayout();
                }
              }
            });

          } else {
            //write the code for cancel click
          }

        });
    }

    if (event.toLowerCase() == 'UpdateStatus'.toLowerCase()) {
      this.toster.showWarning('Method not implemented !');
    }

  }

  public edit(id): void {
    this.openDetailsEntityCreatePopup(id);
  }
}
