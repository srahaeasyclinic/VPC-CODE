import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ValidationService } from 'src/app/services/validation.service';
import { Broadcaster } from '../messaging/broadcaster';
import { MessageEvent } from '../messaging/message.event';

// import * as s from "../messaging/result/methods";

import { ResultInitialiser } from '../messaging/result/resultintialiser';
import { IMethod } from '../messaging/result/resultintialiser';
import { AgeCalculation } from '../messaging/result/methods/agecalculation';
import { MenuService } from 'src/app/services/menu.service';
// text,email,tel,textarea,password, 
@Component({
    selector: 'textbox',
    template: `   
    <div *ngIf="mode!==2" >
        <div *ngIf="field.controlType=='Label'"> 
            <input type="text" [id]="field.name" class="input-control" [(ngModel)]="field.value">
        </div>
        <div *ngIf="field.controlType!=='Label'"> 
            <input type="text" [id]="field.name" class="input-control" [(ngModel)]="field.value" (input)="Validationcheck($event)" (blur)="Validationcheck($event)" [class.has-error]="!Isvalid" >
            <span class="help-block" *ngIf="!Isvalid">{{this.validateMessages}}</span>
        </div> 
    </div>
    <div *ngIf="mode===2"> 
    <label class="text-label-preview">{{field.value}}</label>
    </div>
    `
})
export class TextBoxComponent implements OnInit {
    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() mode: number;

    @Output() changeEmitter = new EventEmitter();

    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }
    private Isvalid: boolean;
    public entityName:string;
    private validateMessages = '';
    messages: any;
    constructor(private menuService: MenuService,private validateService: ValidationService, private broadcaster: Broadcaster, private messageEvent: MessageEvent,private route:ActivatedRoute,) {

    }
    ngOnInit(): void {
        // console.log(JSON.stringify(this.field));
        this.Isvalid = true;

        // if(this.route.parent!=null){
        //     this.route.parent.params.subscribe((urlPath) => {
        //         this.entityName = urlPath["name"];
        //     });
        // }
        let result=this.menuService.getMenuconext();
        this.entityName = result.param_name;
        
        // if (this.field && this.field.typeOf && this.field.typeOf.toLowerCase() == "computedtype" && this.field.receivingType) {
        //     this.registerTypeBroadcast();

    }

    registerTypeBroadcast() {
        this.messageEvent.on()
            .subscribe(message => {
                if (message && this.field.receivingType.toLowerCase() == message.method.toLowerCase()) {
                    var ageCalculation = new AgeCalculation();
                    this.field.value = ageCalculation.GetResult(new Date(message.data));
                }

                //------------------------------------------------------------------------
                this.broadcaster.dependencyRules$.subscribe(
                    t => {
                        this.messages = t;
                        this.prepareDependedData();
                    });
            })
    }
    // registerTypeBroadcast() {
    //     this.messageEvent.on()
    //         .subscribe(message => {
    //             if (message && this.field.receivingType.toLowerCase() == message.method.toLowerCase()) {
    //                 var ageCalculation = new AgeCalculation();
    //                 this.field.value = ageCalculation.GetResult(new Date(message.data));
    //             }
    //         });
    // }

    prepareDependedData() {
        if ((this.messages.method != null && this.field.receivingTypes)) {
            this.field.receivingTypes.forEach(element => {
                if (this.messages.method.toLowerCase() == element.toLowerCase()) {
                    //console.log("this.messages", this.messages);
                    if (this.messages.method.toLowerCase() == "agecalculation") {
                        var ageCalculation = new AgeCalculation();
                        this.field.value = ageCalculation.GetResult(new Date(this.messages.data));
                    } else {
                        this.field.value = this.messages.data;
                    }
                }
            });
        }
      }

    Validationcheck(value) {
        //console.log("Validationcheck"+this.mode);
        // if (this.field != null && this.field.value.length <= 0)
        // {
        //     this.Isvalid = true;
        // }
        if (this.mode === 1) {

            if (this.field.value) {
                this.validateMessages = this.validateService.Validatefield(this.field, this.field.value,this.entityName);
            }
            else {
                // if (this.field.required) {
                //     this.validateMessages = this.validateService.requiredFieldMsg(this.field);
                // } else {
                    this.validateMessages = "";
                //}
            }
            this.Isvalid = this.validateMessages.length > 0 ? false : true;
            // if (this.validateMessages.length > 0) {
            //     this.Isvalid = false;
            // } else {
            //     this.Isvalid = true;
            // }
        }
        //for rule changeEmitter is emitting the $event to tree-node
        this.changeEmitter.emit(value);
    }
    onChange() { }
}