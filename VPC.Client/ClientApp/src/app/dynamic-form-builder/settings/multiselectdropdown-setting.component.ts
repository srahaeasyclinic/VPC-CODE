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
    <label id="modal-title">{{getResourceValue('CustomSettings')}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
    
        <select [(ngModel)]="node.selectedView" class="input-control">
          <option *ngFor="let view of listsource" value={{view.id}}>
            {{view.name}}
          </option>
        </select> 
        <div class="modal-footer">
        <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue('Cancel')}}</button>
        <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue('Submit')}}</button>
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

  constructor(public modal: NgbActiveModal, private layoutService: LayoutService,
    private globalResourceService: GlobalResourceService, ) {
    
  }

  ngOnInit(): void {
    this.resource = this.globalResourceService.getGlobalResources();
    this.initData();
  }
  
  private initData() {
   console.log("code:::", this.node);
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
    return this.globalResourceService.getResourceValueByKey(key);
  }
}

