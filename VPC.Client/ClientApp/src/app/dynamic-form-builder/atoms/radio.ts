import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'radio',
    template: `
    <div class="form-check">
        <div *ngIf="mode!==2">
            <label class="form-check-label">
                <input type="radio" class="form-check-input" name="optradio" (change)="onChange($event)">Option 1
            </label>
        </div>
        <div *ngIf="mode===2"> 
            <label class="form-check-label">
                <input type="radio" class="form-check-input" name="optradio" ng-disabled="true">Option 1
            </label>
        </div>
    </div>
    `
})
export class RadioComponent {
    @Input() field:any = {};
    @Input() form:FormGroup;
    @Input() mode: Number;

    @Output() changeEmitter: EventEmitter<any>

    onChange(value) {
        //for rule changeEmitter is emitting the value to tree-node
        this.changeEmitter.emit(value);
      }
}

      // <div [formGroup]="form">
      //   <div class="form-check" *ngFor="let opt of field.options">
      //     <input class="form-check-input" type="radio" [value]="opt.key" >
      //     <label class="form-check-label">
      //       {{opt.label}}
      //     </label>
      //   </div>
      // </div> 