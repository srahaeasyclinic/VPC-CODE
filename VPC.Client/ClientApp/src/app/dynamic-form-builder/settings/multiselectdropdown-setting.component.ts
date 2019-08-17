import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LayoutService } from "../../meta-data/layout/layout.service";
import { first } from "rxjs/operators";
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
  selector: 'multiselectdropdown-setting',
  template: `
  <div class="modal-header">
    <label id="modal-title">{{getResourceValue(node.name.toLowerCase()+'_displayname')}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
  <div *ngFor="let validatorItem of node.validators" class="margin-bottom-15">
          <label class="text-important">{{getResourceValue('metadata_label_'+validatorItem.name.toLowerCase())}}</label>      
            <div [ngSwitch]="validatorItem.name | lowercase">           
              <required-validator *ngSwitchCase="'requiredvalidator'" [node]="node" [validator]="validatorItem"></required-validator>
              <length-validator *ngSwitchCase="'lengthvalidator'" [validator]="validatorItem"></length-validator> 
              <range-validator *ngSwitchCase="'rangevalidator'" [validator]="validatorItem"></range-validator>
              <defaultvalue-validator *ngSwitchCase="'defaultvaluevalidator'" [validator]="validatorItem" [datatype]="node.dataType" [typeof]="node.typeOf" ></defaultvalue-validator>
            </div>          
        </div>
  <div class="form-group">
    <label for="sel1">{{getResourceValue('menuitem_field_layout')}}</label>
        <select [(ngModel)]="node.selectedView" class="input-control">
          <option *ngFor="let view of listsource" value={{view.id}}>
            {{view.name}}
          </option>
        </select> 
        </div>
        <div class="row">
        <div class="col-md-6">
          <div class="form-group">
            <label for="sel1">{{getResourceValue('metadata_label_width')}}</label>
            <select [(ngModel)]="node.setting.columnWidth" class="input-control">
              <option *ngFor="let w of widths" [value]="w.id">{{w.name}}</option>
            </select>
          </div>
        </div>
    </div>


        <div class="modal-footer">
        <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue('operation_submit')}}</button>
        <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue('task_cancel')}}</button>
      </div>
  </div>
  
  `
})

export class MultiSelectDropdownSettingComponent implements OnInit {

  @Input() node: any;
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  private columns = [];
  public listsource: Array<any> = [];
  public resource: Resource;
  public widths = [
    { id: 12, name: "100%" },
    { id: 9, name: "75%" },
    { id: 6, name: "50%" },
    { id: 3, name: "25%" }
  ];
  constructor(public modal: NgbActiveModal, private layoutService: LayoutService,
    private globalResourceService: GlobalResourceService, ) {
    
  }

  ngOnInit(): void {
    this.resource = this.globalResourceService.getGlobalResources();
    this.initData();
  }
  
  private initData() {
   //console.log("code:::", this.node);
    this.layoutService.getLayoutListViews(this.node.dataType)
      .pipe(first())
      .subscribe(
        data => {
          if (data) { 
            this.listsource = data;
          }

        },
        error => {
          console.log(error);
        });
  }
  //save called....
  public saveSattings() {
    this.saveEvent.emit(this.node);
  }
  getResourceValue(key) {
    key=key.replace('.', '_');
    return this.globalResourceService.getResourceValueByKey(key);
  }
}

