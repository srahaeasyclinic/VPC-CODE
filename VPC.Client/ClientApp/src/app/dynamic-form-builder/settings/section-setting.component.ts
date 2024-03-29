import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';


@Component({
  selector: 'section-setting',
  template: `
  <div class="modal-header">
    <label id="modal-title">
    {{getResourceValue('metadata_label_sectioncomponent')}}
    </label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
 
    

      <div class="row">
          <div class="col-md-12 form-group">
            <div class="checkbox">
                <label class="control control--checkbox">
                {{getResourceValue('metadata_showheader')}}
                <input type="checkbox" [(ngModel)]="node.setting.showHeader">
                <span class="control__indicator"></span>
              </label>
            </div>
          </div>
          <div class="col-md-12 form-group">
            <input type="text" class="input-control" id="name" required [(ngModel)]="node.name" name="name">
          </div>
      </div>

      <div class="row">
          <div class="col-md-12">
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
export class SectionSettingComponent implements OnInit {
 
    @Input() node: any;
    @Input() eventType: any;

    @Output() saveEvent: EventEmitter<any> = new EventEmitter();

    private columns = [];
    public resource: Resource;
    public widths = [
      {id:12, name:"100%"},
      {id:9, name:"75%"},
      {id:6, name:"50%"},
      {id:3, name:"25%"}
    ];

    constructor(public modal: NgbActiveModal, private globalResourceService: GlobalResourceService) {

    }

    ngOnInit(): void {
      //this.resource = this.globalResourceService.getGlobalResources();
      this.initData();
    }

    private getDefaultColumn():any{
      var column = {
        id:0, width:12, offset:0, push:0, pull:0
      }
      return column;
    }
    private initData(){
      //if previous data not saved ===> then should add default data in columns...
      this.addModal();
    }

    private addModal(){
      this.columns.push(this.getDefaultColumn());
    }


    //save called....
    public saveSattings(){
      this.saveEvent.emit(this.node);
    }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
