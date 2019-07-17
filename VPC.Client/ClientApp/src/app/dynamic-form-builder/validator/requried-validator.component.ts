import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
    selector: 'required-validator',
    template: `
    <div>
   
        <div *ngFor="let option of validator.options">
           
            <div [ngSwitch]="option.controlType | lowercase">
                <div *ngSwitchCase="'textbox'"><input type="text" class="input-control" [(ngModel)]="option.value"></div>
                <div *ngSwitchCase="'checkbox'">
                     <label class="control control--checkbox">
                     {{getResourceValue(option.name)}}
                        <input type="checkbox" [(ngModel)]="option.value">
                        <span class="control__indicator"></span>
                    </label>


                </div>
            </div>
        </div>
    </div>
  `
})
export class RequiredValidatorComponent implements OnInit {
    @Input() validator: any;
    @Input() node: any;
    public resource = Resource;
    constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService) {

    }

    ngOnInit(): void {
        //this.resource = this.globalResourceService.getGlobalResources();

        if (this.node&&this.node.required)
        {
            this.validator.options.forEach(element => {

                if (element&&element.controlType=='Checkbox')
                {
                    element.value = true;
                }
                
            });
    }
    }
    getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}


