
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NativeDateAdapter, DateAdapter, MAT_DATE_FORMATS, MatDatepickerInputEvent, MatDatepickerModule } from "@angular/material";
import { AppDateAdapter, APP_DATE_FORMATS } from '../filter/date.adapter';
import { Broadcaster } from '../messaging/broadcaster';
import { MessageEvent } from '../messaging/message.event';
import { Payload } from '../messaging/payload';
@Component({
    selector: 'calander',
    template: `
    <div *ngIf="mode!==2">
    <mat-form-field>
        <input matInput [min]="minDate"  [matDatepicker]="picker" [(ngModel)]="field.value" (dateChange)="changeEvent('change', $event)">
        <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
        <mat-datepicker #picker></mat-datepicker>
    </mat-form-field>
    
    </div>
    <div *ngIf="mode===2"> 
        <label class="text-important text-bold">{{field.value | date:'dd/MM/yyyy'}}</label>
    </div>
    `,
    providers: [
        {
            provide: DateAdapter, useClass: AppDateAdapter
        },
        {
            provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
        },MatDatepickerModule
    ]
})

// <input type="date" name="startDate" id="startDate"  [(ngModel)]="field.value | date:'yyyy-MM-dd'">  [max]="maxDate"
export class CalanderComponent implements OnInit {
    public minDate = new Date(1800, 0, 1);
    public  maxDate = new Date();
    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() mode: number;

    @Output() changeEmitter: EventEmitter<any>
    
    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }

    constructor(
        private broadcaster: Broadcaster,
        private messageEvent: MessageEvent
    ) 
    {
        
        // this.registerTypeBroadcast();
    }
    ngOnInit(): void {
        // if(this.field && this.field.value && this.field.broadcastingType){
        //     this.brodcastData(this.field.value);
        // }
    }
    // registerTypeBroadcast() {
    //     this.messageEvent.on()
    //         .subscribe(message => {
    //             console.log("message checking", message);
    //         });
    // }
    // emitTypeBroadcast() {
    //    // this.messageEvent.fire(`Message from ${this.childID}`);
    //    this.messageEvent.fire("test");
    // }
    changeEvent(type: string, event: MatDatepickerInputEvent<Date>) {
        // this.events.push(`${type}: ${event.value}`);
        if (this.field.broadcastingTypes) {
            for (let i = 0; i < this.field.broadcastingTypes.length; i++) {
              this.brodcastData(this.field.broadcastingTypes[i]);
            }
          }
    }
    brodcastData(methodName: string) {
        var payload = new Payload();
        payload.name = this.getLastValue(this.field.name);
        payload.data = this.field.value;
        payload.method = methodName;
        this.broadcaster.setDependency(payload);
      }
      getLastValue(name: string): string {
        var arr = name.split(".");
        return arr[arr.length - 1];
      }
    // brodcastData(value: any) {
    //     console.log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
    //     var payload = new Payload();
    //     payload.data = value;
    //     payload.method = this.field.broadcastingType;
    //     this.messageEvent.fire(payload);
    // }
}