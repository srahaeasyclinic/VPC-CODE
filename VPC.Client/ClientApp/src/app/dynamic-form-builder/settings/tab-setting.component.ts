import { Component, EventEmitter, Output, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ResourceService } from '../../services/resource.service';
import { first } from 'rxjs/operators';
import { Resource } from '../../model/resource';
import { LayoutModel } from '../../model/layoutmodel';
import { ITreeNode } from "../../dynamic-form-builder/tree.module";
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import {getTreenodeinstanceWithObject } from '../../model/treeNode';

@Component({
  selector: 'Tab-setting',
  template: `
  <div class="modal-header">
    <label id="modal-title">{{getResourceValue('EditTab')}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
    



      <div class="row">
        <div class="col-md-12"><label>{{getResourceValue('Tabs')}}</label></div>        
      </div>

      <div class="row"  *ngFor="let item of node.tabs">
        <div class="col-md-11 form-group">
          <input type="text" [(ngModel)]="item.name" class="input-control">
        </div>   
        <div class="col-md-1 margin-top-5 btn-small-action">
        <i class="fa fa-times" ngbTooltip="{{getResourceValue('Delete')}}" container="body" placement="left" (click)="deleteTab(item.name)"></i>
        </div>     
      </div>

      <div class="row">
        <div class="col-md-12">
        <button type="button" class="btn btn-primary" (click)="addTab()">{{getResourceValue('AddTab')}}</button>
        </div>

      </div>

      <div class="modal-footer">
      <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue('Cancel')}}</button>
      <button type="button" class="btn btn-primary" (click)="saveTabs()">{{getResourceValue('Save')}}</button>
    </div>
    
  </div>
  
  `
})
export class TabSettingComponent {
  // public resource:Resource;
  // public layoutInfo: LayoutModel = new LayoutModel();
  // public tree: TreeNode;

  //   constructor(
  //     public modal: NgbActiveModal,
  //     private resourceService: ResourceService,
  //     private activatedRoute: ActivatedRoute,
  //     ) {}

  //   ngOnInit() {	
  //     //this.getResource();
  //     this.init();
  //   }

  //   private getResource(): void {
  //     this.resourceService.getResources()
  //       .pipe(first())
  //       .subscribe(
  //       data => {
  //         if (data) {
  //         this.resource = data;

  //         this.init();
  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });
  //     }

  //     private init(){
  //       this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];
  //       if (this.layoutInfo != null && this.layoutInfo.formLayoutDetails != null) {
  //         this.tree = this.layoutInfo.formLayoutDetails;
  //       } else {
  //         this.tree = {
  //           name: "defaultSection",
  //           value: "",
  //           required: true,
  //           dataType: "",
  //           fields: [],
  //           controlType: "section",
  //           decimalPrecision: null,
  //           defaultValue: "",
  //           properties: "",
  //           tabs:[],
  //           setting: {columnWidth:12, showHeader:true},
  //           validators:[]
  //         };
  //       }
  //       // this.activatedRoute.params.subscribe((params: Params) => {
  //       //   this.layoutId = params['id'];
  //       //   this.entityname = params['name'];
  //       // });
  //       //this.getMetadataFieldsByName(this.entityname);

  //     }






  @Input() node: any;
  @Input() eventType: any;

  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  public resource: Resource;
  private columns = [];  
  //private i: number = 0;

  constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService) {

  }

  ngOnInit(): void {
    // this.resource = this.globalResourceService.getGlobalResources();
    //console.log("node", this.node);
    //this.i = 2; 
  }





  //save called....
  private saveSattings() {
    //console.log('save settings called');
    // 
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  public addTab(): void {

    var count = this.node.tabs.length;
    count = count + 1;

    if(!this.node || !this.node.tabs){
      return;
    }
    var tab = getTreenodeinstanceWithObject({
      //name: "Tab " + Math.floor(Math.random() * 10),
      name: "Tab " + count,
      value: "",
      required: false, //need to change
      dataType: "tab",
      fields: [],
      controlType: "tab",
      decimalPrecision: null,
      defaultValue: "",
      properties: "",
      tabs: [],
      setting: { columnWidth: 12, showHeader: true },
    });


    // var tabSection = {
    //   name: "Section",
    //   value: "",
    //   required: true, //need to change
    //   dataType: "Section",
    //   fields: [],
    //   controlType: "Section",
    //   decimalPrecision: null,
    //   defaultValue: "",
    //   properties: "",
    //   tabs: [],
    //   setting: { columnWidth: 12, showHeader: true },
    // }
    //this.i = this.i + 1;    

    var tabSection = getTreenodeinstanceWithObject({
      required: true, //need to change
      controlType: "section",
      setting: { columnWidth: 12, showHeader: true },
     
    });

    tab.fields.push(tabSection);
    this.node.tabs.push(tab);
    //console.log("after save..", this.node);
    //this.saveEvent.emit(this.node);
    //this.selectedTreeNode = myObj;
  
  }
  deleteTab(name){
    this.node.tabs.forEach((item,index) =>{
      if(item.name==name){
        this.node.tabs.splice(index,1)
        }
    });
   
}

  public saveTabs(): void {
    this.saveEvent.emit(this.node);
  }
  // generateResourceName(word) {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }

}
