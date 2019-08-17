import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JsonPipe } from '@angular/common';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
  selector: 'calendar-setting',
  template: `
  <div class="modal-header">
	<label id="modal-title">{{node.name}}</label>
	<button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
		<span aria-hidden="true">&times;</span>
	</button>
</div>
<div class="modal-body">
	<div class="row">

		<div class="col-md-12">
			<div class="form-group">

				<div *ngFor="let validatorItem of node.validators" class="margin-bottom-15">
					<label class="text-important">{{validatorItem.name}}</label>
					<div [ngSwitch]="validatorItem.name | lowercase">
						<required-validator *ngSwitchCase="'requiredvalidator'" [node]="node"
							[validator]="validatorItem"></required-validator>
						<length-validator *ngSwitchCase="'lengthvalidator'" [validator]="validatorItem">
						</length-validator>
						<range-validator *ngSwitchCase="'rangevalidator'" [validator]="validatorItem"></range-validator>
						<emailformat-validator *ngSwitchCase="'emailformatvalidator'" [validator]="validatorItem">
						</emailformat-validator>
						<defaultvalue-validator *ngSwitchCase="'defaultvaluevalidator'" [validator]="validatorItem"
							[datatype]="node.dataType" [typeof]="node.typeOf"></defaultvalue-validator>

					</div>
				</div>

			</div>
		</div>


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
		<button type="button" class="btn btn-primary"
			(click)="saveSattings()">{{getResourceValue('operation_submit')}}</button>
		<button type="button" class="btn btn-secondary"
			(click)="modal.dismiss('cancel click')">{{getResourceValue('task_cancel')}}</button>

	</div>
</div>
  
  `
})
export class CalendarSettingComponent implements OnInit {

  @Input() node: any;
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  private columns = [];
  public resource = Resource;
  public widths = [
    {id:12, name:"100%"},
    {id:9, name:"75%"},
    {id:6, name:"50%"},
    {id:3, name:"25%"}
  ];
  constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService) {

  }

  ngOnInit(): void {
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

