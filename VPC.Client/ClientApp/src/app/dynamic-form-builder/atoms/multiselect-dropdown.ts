import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { nodeName } from '../helper/utils';
import { TreeService } from '../service/tree.service';
import { first } from 'rxjs/operators';


@Component({
  selector: 'multiselect-dropdown',
  template: `
    <div class="form-group" *ngIf="mode!==2">    
    <ng-multiselect-dropdown 
      [placeholder]="'Select options'" 
      [data]="results" 
      [(ngModel)]="field.value" 
      [settings]="dropdownSettings" 
      class="custom-multi-select" (change)="onChange($event)"
     >
    </ng-multiselect-dropdown>
  </div>

  <div *ngIf="mode===2" >
  {{dropdownValue}}
  </div>
    `,
  styles: [`     
  div.dropdown-wrapper select {  
      background-color:transparent; 
      background-image:none; 
      -webkit-appearance: none; 
      border:none; 
      box-shadow:none; 
      padding:0.3em 0.5em; 
  }
  `]
})
export class MultiSelectDropDownComponent implements OnInit {

  @Input() field: any = {};
  @Input() form: FormGroup;
  @Input() mode: Number;
  @Input() disableProp: boolean;

  @Output() changeEmitter: EventEmitter<any>
  public results: [any];
  public dropdownValue: string = "";
  public dropdownSettings = {
    singleSelection: false,
    idField: 'internalId',
    textField: 'itemName',
    // selectAllText: 'Select All',
    // unSelectAllText: 'UnSelect All',
    enableCheckAll: false,
    itemsShowLimit: '10',
    allowSearchFilter: true
  };

  constructor(private treeService: TreeService) {

  }

  ngOnInit(): void {
    if (!this.mode) return;
    if (this.mode == 1 || this.mode == 2) {
      this.loadLayout();
      if (this.field.value && this.field.value.length > 0) {
        this.field.value.forEach(item => {
          if (item.itemName) {
            this.dropdownValue += item.itemName + ',';
          }
        });
        if(this.dropdownValue!=""){
          this.dropdownValue = this.dropdownValue.substring(0, this.dropdownValue.length - 1);
        }
      }
    }
  };

  loadLayout(): void {
    this.treeService.getViewLayout(this.field.dataType, this.field.selectedView)
      .pipe(first())
      .subscribe(
        data => {
          console.log("data for role test", data);
          if (data && data.viewLayoutDetails && data.viewLayoutDetails.fields) {
            this.dropdownSettings.idField = data.viewLayoutDetails.fields[0];
            this.dropdownSettings.textField = data.viewLayoutDetails.fields[0];
            this.getResults(this.field.dataType);
          }
        },
        error => {
          console.log(error);
        });
  }

  getResults(name: string) {
    this.treeService.getEntityValue(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.results = data.result;
          }
        },
        error => {
          console.log(error);
        });
  }


  onChange(value) {
    this.changeEmitter.emit(value);
  }
}