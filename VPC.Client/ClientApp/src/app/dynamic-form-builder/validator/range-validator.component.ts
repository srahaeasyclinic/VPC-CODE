import { Component, OnInit, Input} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
    selector: 'range-validator',
    template: `
    <div>
        <div *ngFor="let option of validator.options">
            <label>{{getResourceValue(option.name)}}</label>
            <div [ngSwitch]="option.controlType | lowercase">
                <div *ngSwitchCase="'textbox'"><input type="text" class="input-control" [(ngModel)]="option.value"></div>
                <div *ngSwitchCase="'checkbox'"><input type="checkbox" class="input-control" [(ngModel)]="option.value"></div>
            </div>
        </div>
    </div>
  `
})
export class RangeValidatorComponent implements OnInit {
    @Input() validator: any;
    public resource = Resource;
    constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService) {

    }

    ngOnInit(): void {
        //this.resource = this.globalResourceService.getGlobalResources();
    }
    
     getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}


