import { Component, Input, OnInit, Output, EventEmitter, ChangeDetectorRef, DoCheck } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { nodeName } from '../helper/utils';
import { TreeService } from '../service/tree.service';
import { first } from 'rxjs/operators';
import { CommunicationService } from '../../services/communication.service';
import { AtomBase } from './atombase';
import { Broadcaster } from '../messaging/broadcaster';
import { MessageEvent } from '../messaging/message.event';
import { Payload } from '../messaging/payload';


@Component({
  selector: 'dropdown',
  template: `
  
  <div *ngIf="mode!==2 && !field.readOnly">

  <div *ngIf="(field.dataType.toLowerCase() == 'lookup' || field.dataType.toLowerCase() == 'picklist') && (field.defaultValue==null || (field.defaultValue!=null && field.defaultValue.toLowerCase() != 'parentsgroup'))">
      <select class="input-control" [id]="field.name" [(ngModel)]="field.value" [disabled] = "field.readOnly" (change)="onChange($event)">
        <option *ngFor="let opt of results" [value]="opt.internalId">
            <span *ngIf="field.dataType.toLowerCase() == 'lookup'">{{opt.itemName}}</span>
            <span *ngIf="field.dataType.toLowerCase() == 'picklist'">{{opt.text}}</span>
        </option>
      </select>
    </div>

    <div  *ngIf="field.dataType.toLowerCase() == 'picklist' && field.defaultValue!=null && field.defaultValue.toLowerCase() == 'parentsgroup'">
       <hierarchy-dropdown [field]="field" [mode]="mode" (change)="onChange($event)" ></hierarchy-dropdown>
    </div>

    <div *ngIf="field.dataType.toLowerCase() == 'metadatapicklist'">
     <select class="input-control" [id]="field.name" [(ngModel)]="field.value" (change)="onChange($event)">
        <option *ngFor="let opt of results" [value]="opt.name">
        {{opt.displayName}}
        </option>
      </select>
    </div>
  </div>

  <div *ngIf="mode!==2 && field!=null && field.readOnly!=undefined && field.readOnly">
       <label class="text-label-preview">{{dropdownValue}}</label>
  </div>

 <div *ngIf="mode===2"> 
    <label class="text-label-preview">{{dropdownValue}}</label>
  </div>
    `,
  styles: [`
     
  div.dropdown-wrapper select {  
      background-color:transparent; 
      background-image:none; 
      -webkit-appearance: none; 
      border:none; 
      box-shadow:none; 
      padding:0px; 
  }
  `]
})

// <div *ngIf="field.dataType.toLowerCase() == 'picklist'">
// <select class="input-control" [id]="field.name" [(ngModel)]="field.value">
//   <option *ngFor="let opt of results" [value]="opt.internalId">
//   {{opt.text}}
//   </option>
// </select>
// </div>
export class DropDownComponent implements OnInit, DoCheck {

  @Input() field: any = {};
  @Input() form: FormGroup;
  @Input() mode: Number;

  public entityname: any;

  @Output() changeEmitter: EventEmitter<any>


  // @Output() OnMetadatapicklistChangeevent = new EventEmitter<string>();
  results: [any];
  testValue: any;
  private nodeName: string = "";
  messages: any;
  public dropdown: any;
  public dropdownValue: string = "";
  private previousNodeName: string;

  constructor(
    private refChangedetect: ChangeDetectorRef,
    private treeService: TreeService,
    public broadcaster: Broadcaster,
    private communicationService: CommunicationService
  ) {
    this.changeEmitter = new EventEmitter();
  }
  ngOnInit(): void {
    //console.log('daField-' + JSON.stringify(this.field));
    //console.log(this.mode);
    this.nodeName = this.getName(this.field);
    this.previousNodeName = this.nodeName;
    if (!this.field.receivingType && (this.mode != null || this.mode != undefined)) {
      this.loadData(null);
    }
    //------------------------------------------------------------------------
    this.broadcaster.dependencyRules$.subscribe(
      t => {
        // this.history.push(`${astronaut} confirmed the mission`);
        this.messages = t;
        if (this.messages) {
          this.prepareDependedData();
        }
      });

    // if (this.field.broadcastingTypes) {
    //   for (let i = 0; i < this.field.broadcastingTypes.length; i++) {
    //     this.sendMessages(this.field.broadcastingTypes[i]);
    //   }
    // }
    //------------------------------------------------------------------------

    // This is for temporary, once broadcast finalize will implement otherway.
    if (this.field.broadcastingTypes) {
      for (let i = 0; i < this.field.broadcastingTypes.length; i++) {
        //console.log('ded '+this.field.broadcastingTypes[i]);
        if (this.field.broadcastingTypes.length > 0 && this.field.broadcastingTypes[i] == "EntityRichTextBox")
        {
           localStorage.setItem('tagsname', this.field.value);
        }
      }
    }
///-------------------------------------------------
  }
  onChange(value) {

    //console.log("field value checking::::", this.field);
    //console.log("broadcastingTypes value checking::::", this.field.broadcastingTypes);
    if (this.field.broadcastingTypes) {
      for (let i = 0; i < this.field.broadcastingTypes.length; i++) {
        this.sendMessages(this.field.broadcastingTypes[i]);
      }
    }
    localStorage.removeItem('tagsname');
    //for rule changeEmitter is emitting the value to tree-node
    this.changeEmitter.emit(value);
  }

  ngDoCheck() {
    if (!this.mode) return;
    this.nodeName = this.getName(this.field);
    if (this.previousNodeName != this.nodeName) {
      this.previousNodeName = this.nodeName
      if (!this.field.receivingType && (this.mode != null || this.mode != undefined)) {
        this.loadData(null);
      }
    }

  }
  sendMessages(methodName: string) {
    var payload = new Payload();
    payload.name = this.getLastValue(this.field.name);
    payload.data = this.field.value;
    payload.method = methodName;
    payload.group = this.getFirstValue(this.field.name);
    // console.log("send message", payload);
    this.broadcaster.setDependency(payload);
  }
  getLastValue(name: string): string {
    var arr = name.split(".");
    return arr[arr.length - 1];
  }
  getFirstValue(name: string): string {
    var arr = name.split(".");
    return arr[0];
  }
  prepareDependedData() {
    var belognGroup = this.getFirstValue(this.field.name);
    if ((this.messages.method != null && this.field.receivingTypes && this.messages.group != null)) {
      if (this.messages.group.toLowerCase() == belognGroup.toLowerCase()) {
        this.field.receivingTypes.forEach(element => {
          if (this.messages.method.toLowerCase() == element.toLowerCase()) {
            console.log("field checking:::::", this.field);
            var query = this.messages.name + "," + this.messages.data;
            this.loadData(query);
          }
        });
      }

    }
  }

  loadData(query: string) {
    //console.log(this.field.dataType.toLowerCase());

    if (this.field && ((this.field.dataType.toLowerCase() == "picklist")) && (this.mode != null || this.mode != undefined)) {
      this.pickListValues(this.nodeName, query, this.field);
    } else if (this.field.dataType.toLowerCase() == "lookup" && (this.mode != null || this.mode != undefined)) {
      this.lookUpValues(this.nodeName, query, this.field);
    } else if (this.field.dataType.toLowerCase() == "metadatapicklist" && (this.mode != null || this.mode != undefined)) {
      this.metadatapicklistValues(this.field.defaultValue, this.field);

    }
  }


  lookUpValues(name: string, query: string, fieldObj: any) {
    this.treeService.getLookupValue(name, query)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.results = data.result;


            this.results.forEach(element => {
              if (element.internalId == fieldObj.value) {
                this.dropdownValue = element.itemName;
              }
            });

          }
        },
        error => {
          console.log(error);
        });
  }

  pickListValues(name: string, query: string, fieldObj: any) {
    //console.log('name-'+JSON.stringify(name)+'|||query-'+JSON.stringify(query));
    this.treeService.getPickListValues(name, query)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.results = data.result;
            if (this.results != null) {
              // this.dropdown = this.results.find(a => a.internalId === fieldObj.value);

              // if (this.dropdown) {
              //   this.dropdownValue = this.dropdown.text;
              // }
              // console.log(this.field.name);
              //console.log(JSON.stringify(this.results));
              // console.log(JSON.stringify(this.field));

              if (this.field.defaultValue != null && this.field.defaultValue.toLowerCase() == "onlyparents") {

                this.results = data.result.filter(s => s.parentPicklist == null);

              }


              this.results.forEach(element => {
                if (element.internalId == fieldObj.value) {
                  this.dropdownValue = element.text;
                }
              });

            }
          }
        },
        error => {
          console.log(error);
        });
  }

  metadatapicklistValues(url: string, fieldObj: any) {
    // console.log(url);
    // console.log("metadatapicklist-");
    var field = fieldObj;
    this.treeService.getMetaDataPicklistValues(url)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.results = data;
            //console.log("metadatapicklist-"+JSON.stringify( this.results));

            if (fieldObj && fieldObj.value) {
              this.dropdownValue = fieldObj.value;
            }
          }
        },
        error => {
          console.log(error);
        });
  }


  getName(fieldObj: any): string {
    var name = (fieldObj.typeOf != null) ? fieldObj.typeOf : fieldObj.name;
    return this.treeService.getLastValue(name);
  }

  // public OnMetadatapicklistChange(event): void {  // event will give you full breif of action
  //   const newVal = event.target.value;
  //   this.OnMetadatapicklistChangeevent.emit(event.target.value)
  //   console.log('metadatachange ' + newVal);
  //   //this.communicationService.EntityNameSource.next(newVal);
  // }


}
