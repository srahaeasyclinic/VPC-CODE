import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
    selector: 'length-validator',
    template: `
    <div class="row">
        <div *ngFor="let option of validator.options" class="col-md-6">
            <label>  {{getResourceValue('metadata_label_minlength')}} <!-- +option.name.toLowerCase()-->
           </label>
            <div [ngSwitch]="option.controlType | lowercase">
                <div *ngSwitchCase="'textbox'">
                  <input type="number" class="input-control" [(ngModel)]="validator.userSetMinlength" (change)="Lenghtvaluechange()" (blur)="Lenghtvaluechange()" [class.has-error]="!Isvalidlenght">
                     <span class="help-block" *ngIf="!Isvalidlenght">
                        {{this.LengthErrormsg}}
                     </span>
                </div>
                <div *ngSwitchCase="'checkbox'"><input type="checkbox" class="input-control" [(ngModel)]="option.value"></div>
            </div>
        </div>
        <div *ngFor="let option of validator.options" class="col-md-6">
            <label>  {{getResourceValue('metadata_label_maxlength')}}
           </label>
            <div [ngSwitch]="option.controlType | lowercase">
                <div *ngSwitchCase="'textbox'">
                  <input type="number" class="input-control" [(ngModel)]="validator.userSetlength" (change)="Lenghtvaluechange(option)" (blur)="Lenghtvaluechange(option)" [class.has-error]="!Isvalidlenght">
                     <span class="help-block" *ngIf="!Isvalidlenght">
                        {{this.LengthErrormsg}}
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
        this.validator.userSetMinlength = (this.validator.userSetMinlength===null||this.validator.userSetMinlength==undefined)? this.validator.dbminlength:this.validator.userSetMinlength;
    // console.log('user set' + this.validator.userSetlength);
    }

    Lenghtvaluechange( obj:any)
    {
      this.Isvalidlenght = (this.validator.userSetlength==null) ? false : true;
        if (!this.Isvalidlenght)
        {
            this.LengthErrormsg =this.getResourceByStringReplacer('validation_requiredvalidator',[this.getResourceValue('metadata_label_'+obj.name.toLowerCase())]);
            //this.LengthErrormsg = this.globalResourceService.getResourceByKey("metadata_validator_length_empty");
            return;
        }

        this.Isvalidlenght = (this.validator.userSetlength > this.validator.dblength) ? false : true;
        
        if (!this.Isvalidlenght)
        {
            //
            this.LengthErrormsg = this.getResourceByStringReplacer('validation_rangevalidator',[this.getResourceValue('metadata_label_'+obj.name.toLowerCase()),0,this.validator.dblength]);
            //this.LengthErrormsg = this.globalResourceService.getResourceByKey("metadata_validator_length_maxlength") + this.validator.dblength;
        }

        //Added by Soma
        this.Isvalidlenght = (this.validator.userSetMinlength < this.validator.dbminlength) ? false : true;
        
        if (!this.Isvalidlenght)
        {
            this.LengthErrormsg = this.globalResourceService.getResourceByKey("metadata_validator_length_minlength") + this.validator.dbminlength;
        }
       
    }
    
     getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  getResourceByStringReplacer(validationName:string,object:any[]) {
    return this.globalResourceService.getResourceByStringReplacer(validationName,object);
  }
}


