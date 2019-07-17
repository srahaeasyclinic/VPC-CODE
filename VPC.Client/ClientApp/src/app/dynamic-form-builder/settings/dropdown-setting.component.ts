import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'src/environments/environment';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
  selector: 'dropdown-setting',
  template: `
  <div class="modal-header">
    <label id="modal-title">{{getResourceValue('DropdownSettings')}}</label>
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
              <range-validator *ngSwitchCase="'rangevalidator'" [validator]="validatorItem"></range-validator>     
            </div>          
        </div>
        <div *ngIf="(node.dataType.toLowerCase()=='metadatapicklist')">
            <label>API</label>
            <input type="text" class="input-control" [(ngModel)]="apiUrl + node.defaultValue "/>
        </div>
        <div class="modal-footer">
        <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue('Cancel')}}</button>
        <button type="button" class="btn btn-primary" (click)="modal.close('Ok click')">{{getResourceValue('Submit')}}</button>
      </div>
  </div>
  
  `
})
export class DropdownSettingComponent {

  @Input() node: any;
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  
  private apiUrl:any;
  public resource: Resource;

  constructor(public modal: NgbActiveModal,private globalResourceService: GlobalResourceService,) { }

  private columns = [];
  ngOnInit(): void {
    this.resource = this.globalResourceService.getGlobalResources();
   // console.log('dropdown setting check',JSON.stringify(this.node.validators));
    this.apiUrl=environment.apiUrl;
  }



  private saveSattings() {
    this.saveEvent.emit(this.node);
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
