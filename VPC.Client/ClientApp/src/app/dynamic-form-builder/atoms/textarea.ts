import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MessageEvent } from '../messaging/message.event';
import { Broadcaster } from '../messaging/broadcaster';
@Component({
  selector: 'text-area',
  template: `
    <div *ngIf="mode!==2">
       <textarea rows="4" cols="50" type="text" [id]="field.name" class="input-control" [(ngModel)]="field.value" (change)="onChange($event)"> </textarea>
    </div> 
    <div *ngIf="mode===2"> 
        <label>{{field.value}}</label>
    </div>
    `
})
export class TextAreaComponent {
  @Input() field: any = {};
  @Input() form: FormGroup;
  @Input() mode: number;

  @Output() changeEmitter: EventEmitter<any>

  get isValid() { return this.form.controls[this.field.name].valid; }
  get isDirty() { return this.form.controls[this.field.name].dirty; }

  constructor(private broadcaster: Broadcaster, private messageEvent: MessageEvent) {
     this.changeEmitter = new EventEmitter();
    if (this.field && this.field.typeOf && this.field.typeOf.toLowerCase() == "computedtype" && this.field.receivingType) {
      this.registerTypeBroadcast();
    }
  }

  private onChange = ($event: any) => {
    //this.changeEmitter.emit(this.field);
    //for rule changeEmitter is emitting the $event to tree-node
    this.changeEmitter.emit($event);
  }

  registerTypeBroadcast() {
    this.messageEvent.on()
      .subscribe(message => {
        if (message && this.field.receivingType.toLowerCase() == message.method.toLowerCase()) {
          console.log("message get1", message.method);
        }
        // this.message = message;
        // if (this._timer) {
        //   clearTimeout(this._timer);
        // }
        // this._timer = setTimeout(() => {
        //   this.message = '';
        //   this._timer = null;
        // }, 3000);
      });

    //   this.broadcaster.on<string>('test')
    //   .subscribe(message => {
    //     console.log("message get2", message);
    //     // this.message = message;
    //     // if (this._timer) {
    //     //   clearTimeout(this._timer);
    //     // }
    //     // this._timer = setTimeout(() => {
    //     //   this.message = '';
    //     //   this._timer = null;
    //     // }, 3000);
    //   });
  }
}
      // <div [formGroup]="form">
      //   <input *ngIf="!field.multiline" [attr.type]="field.type" class="form-control"  [id]="field.name" [name]="field.name" [formControlName]="field.name">
      //   <textarea *ngIf="field.multiline" [class.is-invalid]="isDirty && !isValid" [formControlName]="field.name" [id]="field.name"
      //   rows="9" class="form-control" [placeholder]="field.placeholder"></textarea>
      // </div> 