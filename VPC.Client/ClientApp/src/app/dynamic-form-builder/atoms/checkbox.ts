import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'checkbox',
    template: `
    <div class="">
    
      <div *ngIf="mode!==2">
        <label class="control control--checkbox" style="padding-left:15px;">
          <input type="checkbox" [id]="field.name" [(ngModel)]="field.value" (change)="onChange($event)">
          <span class="control__indicator" style="top:3px;"></span>
        </label>
      </div>
      <div *ngIf="mode===2"> 
        <label class="text-label-preview margin-right-10">{{checkboxValue+' '}}</label>
      </div>
    </div>
    `
})
export class CheckBoxComponent implements OnInit{
    @Input() field:any = {};
    @Input() form:FormGroup;
    @Input() mode: Number;
    public checkboxValue: string = "";
    @Output() changeEmitter: EventEmitter<any> = new EventEmitter();

  constructor()
  {
    this.changeEmitter=new EventEmitter();
  }
  ngOnInit(): void {    
    //debugger;
      // let data : any = this.field;
      if(this.field.value === true)
      {
        this.checkboxValue = "Yes";        
      }
      else if(this.field.value === false)
      {
        this.checkboxValue = "No";   
      }
      else
      {
        this.field.value =false; // in case null value, the validation will be faild thereby set as false default.
        this.checkboxValue = "";    
      }
   // console.log(this.field.name+'--'+ this.field.value);
    }

  onChange(value) {
  
      this.changeEmitter.emit(value);
    }
    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }
}