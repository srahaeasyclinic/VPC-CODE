import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JsonPipe } from '@angular/common';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
  selector: 'richtextbox-setting',
  template: `
<div class="modal-header">
  <label class="text-important" id="modal-title">{{getResourceValue('RichTextboxSettings')}}</label>
  <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
    <span aria-hidden="true">&times;</span>
  </button>
</div>
<div class="modal-body">
  
    <div *ngFor="let validatorItem of node.validators" class="margin-bottom-15">
      <label class="text-important">{{validatorItem.name}}</label>
      <div [ngSwitch]="validatorItem.name | lowercase">

        <required-validator *ngSwitchCase="'requiredvalidator'" [node]="node" [validator]="validatorItem"></required-validator>
        <length-validator *ngSwitchCase="'lengthvalidator'" [validator]="validatorItem"></length-validator>


      </div>
    </div>
    <div class="modal-footer">
    <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue('Cancel')}}</button>
    <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue('Submit')}}</button>
  </div>
</div>

`
})
export class RichtextboxSettingComponent implements OnInit {

  @Input() node: any;
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  private columns = [];
  public resource: Resource;
  constructor(public modal: NgbActiveModal, private globalResourceService: GlobalResourceService, ) {

  }

  ngOnInit(): void {
    this.resource = this.globalResourceService.getGlobalResources();
    this.initData();
  }

  private initData() {
    //console.log("node", this.node);
  }
  //save called....
  public saveSattings() {
    // console.log("test lenght"+JSON.stringify(this.node));
    this.saveEvent.emit(this.node);
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }}
