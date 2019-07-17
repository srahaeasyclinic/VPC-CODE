import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
    selector: 'length-validator',
    template: `
    <div>
        <div *ngFor="let option of validator.options">
            <label> {{getResourceValue(option.name)}}
           </label>
            <div [ngSwitch]="option.controlType | lowercase">
                <div *ngSwitchCase="'textbox'">
                  <input type="number" class="input-control" [(ngModel)]="validator.userSetlength" (change)="Lenghtvaluechange()" (blur)="Lenghtvaluechange()" [class.has-error]="!Isvalidlenght">
                     <span class="help-block" *ngIf="!Isvalidlenght">
                        {{this.LengthErrormsg}}!
                     </span>
                </div>
                <div *ngSwitchCase="'checkbox'"><input type="checkbox" class="input-control" [(ngModel)]="option.value"></div>
            </div>
        </div>
    </div>
  `
})
export class LengthValidatorComponent implements OnInit {
    @Input() validator: any;

    private Isvalidlenght: boolean = true;
    private LengthErrormsg: string = "";
    public resource = Resource;
    constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService)
    {

    }

    ngOnInit(): void {
        //this.resource = this.globalResourceService.getGlobalResources();

       // console.log(JSON.stringify(this.validator));
        ///console.log('user set' + this.validator.userSetlength);
        //console.log('db set'+this.validator.dblength);
        this.validator.userSetlength = (this.validator.userSetlength===null||this.validator.userSetlength==undefined)? this.validator.dblength:this.validator.userSetlength;
    // console.log('user set' + this.validator.userSetlength);
    }

    Lenghtvaluechange()
    {
      this.Isvalidlenght = (this.validator.userSetlength==null) ? false : true;
        if (!this.Isvalidlenght)
        {
          
            this.LengthErrormsg = this.globalResourceService.getResourceByKey("TheEmptyLengthIsNotAllowed");
            return;
        }

        this.Isvalidlenght = (this.validator.userSetlength > this.validator.dblength) ? false : true;
        
        if (!this.Isvalidlenght)
        {
            this.LengthErrormsg = this.globalResourceService.getResourceByKey("TheLengthShouldBeLessThan") + this.validator.dblength;
        }
       
    }
    
     getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}


