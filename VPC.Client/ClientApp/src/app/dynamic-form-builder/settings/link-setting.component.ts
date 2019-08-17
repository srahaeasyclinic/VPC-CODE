import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JsonPipe } from '@angular/common';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';
import { LinkSetting } from 'src/app/model/treeNode';

@Component({
  selector: 'link-setting',
  template: `
  <div class="modal-header">
    <label class="text-important" id="modal-title">{{getResourceValue('metadata_label_textboxsettings')}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
        <div class="row">
          <div class="col-md-12">
            <div class="form-group">
              <label for="sel1">{{getResourceValue("metadata_label_width")}}</label>
              <select [(ngModel)]="node.setting.columnWidth" class="input-control">
                <option *ngFor="let w of widths" [value]="w.id">{{w.name}}</option>
              </select>
            </div>
          </div>
          <div class="col-md-12">
            <div class="form-group">
              <label for="sel1">{{getResourceValue("metadata_label_linktype")}}</label>
              <select [(ngModel)]="node.setting.linkSetting.type" class="input-control">
                <option *ngFor="let t of linkTypes" [value]="t.id">{{t.name}}</option>
              </select>
            </div>
            <div class="form-group">
              <label for="sel1">{{getResourceValue("metadata_label_Url")}}</label>
              <input type="text" class="input-control" id="name" required [(ngModel)]="node.setting.linkSetting.url" name="name">
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
export class LinkSettingComponent implements OnInit {

  @Input() node: any;
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  private columns = [];
  public resource = Resource;
  public widths = [
    { id: 12, name: "100%" },
    { id: 9, name: "75%" },
    { id: 6, name: "50%" },
    { id: 3, name: "25%" }
  ];
  public linkTypes = [
    { id: 1, name: "Internal route" },
    { id: 2, name: "External link" },
  ]
  constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService) {

  }

  ngOnInit(): void {
    if(!this.node.setting.linkSetting){
      this.node.setting.linkSetting = new LinkSetting();
      this.node.setting.linkSetting.type = 1;
      this.node.setting.linkSetting.url = "";
    }
    // this.resource = this.globalResourceService.getGlobalResources();
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
  }
}

