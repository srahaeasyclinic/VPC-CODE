import { Component, OnInit, Input} from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
    selector: 'emailformat-validator',
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
export class EmailFormatValidatorComponent implements OnInit {
    @Input() validator: any;
    public resource = Resource;
    constructor(public modal: NgbActiveModal,public globalResourceService: GlobalResourceService)
    {

    }

    ngOnInit(): void {
        //this.resource = this.globalResourceService.getGlobalResources();
       // console.log("test"+JSON.stringify(this.validator.options));
        //this.validator.EmailFromat;
    }
   
getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}


