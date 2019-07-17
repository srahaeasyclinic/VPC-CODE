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
import { HierarchyDropdownService } from '../../services/hierarchy-dropdown.service';

// <div  *ngIf="field.dataType.toLowerCase() == 'lookup' || field.dataType.toLowerCase() == 'picklist'">

@Component({
    selector: 'hierarchy-dropdown',
    template: `
  <div *ngIf="mode!==2">
    <select class="input-control" [id]="field.name" [(ngModel)]="field.value" [disabled] = "field.readOnly" (change)="onChange($event)">
      <ng-template ngFor let-item [ngForOf]="pickresults">
            <optgroup *ngIf="item.items" label="{{item.group.name+' ('+item.group.totalchild+')'}}">
                <option *ngFor="let child of item.items" [value]="child.internalId">
                <span>{{child.text}}</span>
                </option>
            </optgroup>

            <option *ngIf="!item.items" [value]="item.group.id"><span>{{item.group.name}}</span>

            </option>
     </ng-template>

    </select>
    </div>
  
  <div *ngIf="mode===2"> 
    <label class="text-important text-bold">{{dropdownValue}}</label>
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

export class HierarchyDropDownComponent implements OnInit {

    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() mode: Number;
    @Output() changeEmitter: EventEmitter<any>

    // @Output() OnMetadatapicklistChangeevent = new EventEmitter<string>();
    pickresults: any = [];
    testValue: any;
    private nodeName: string = "";
    messages: any;
    public dropdown: any;
    public dropdownValue: string = "";
    private previousNodeName: string;

    constructor(
        private treeService: TreeService,
        public broadcaster: Broadcaster,
        private communicationService: CommunicationService,
        private dropdownservice: HierarchyDropdownService) {
 this.changeEmitter = new EventEmitter();
    }
    ngOnInit(): void {
      
        if (!this.mode) return;
        this.nodeName = this.getName(this.field);
        this.previousNodeName = this.nodeName;
        if (!this.field.receivingType) {
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
        //------------------------------------------------------------------------
    }
    onChange(value) {

       // console.log("field value checking::::", this.field);
       // console.log("broadcastingTypes value checking::::", this.field.broadcastingTypes);
        if (this.field.broadcastingTypes) {
            for (let i = 0; i < this.field.broadcastingTypes.length; i++) {
                this.sendMessages(this.field.broadcastingTypes[i]);
            }
        }
        this.changeEmitter.emit(value);
    }
    
    sendMessages(methodName: string) {
        var payload = new Payload();
        payload.name = this.getLastValue(this.field.name);
        payload.data = this.field.value;
        payload.method = methodName;
        //console.log("send message", payload);
        this.broadcaster.setDependency(payload);
    }
    
    getLastValue(name: string): string {
        var arr = name.split(".");
        return arr[arr.length - 1];
    }
    
    prepareDependedData() {
        if ((this.messages.method != null && this.field.receivingTypes)) {
            this.field.receivingTypes.forEach(element => {
                if (this.messages.method.toLowerCase() == element.toLowerCase()) {
                    //console.log("this.messages", this.messages);
                    var query = this.messages.name + "," + this.messages.data;
                    this.loadData(query);
                }
            });
        }
    }
    loadData(query: string) {
    
        // if (this.field && ((this.field.dataType.toLowerCase() == "picklisthierarchy"))) {
            this.pickListhierarchyValues(this.nodeName, query, this.field);
        //}
    
    }
    pickListhierarchyValues(name: string, query: string, fieldObj: any) {
        console.log('name-'+JSON.stringify(name)+'|||query-'+JSON.stringify(query));
        this.treeService.getPickListValues(name, query)
            .pipe(first())
            .subscribe(
                data => {
                     // console.log('hierarchy');
                    if (data) {
                        if (data.result != null) {
              
                            data.result.forEach(element => {
                                if (element.internalId == fieldObj.value) {
                                    this.dropdownValue = element.text;
                                }
                            });
//console.log('data.result-'+JSON.stringify(data.result));
                            this.pickresults = this.dropdownservice.dropdowndatagrouping(data.result, "parentId");
                             //console.log('field-'+JSON.stringify(this.field));
                          // console.log(JSON.stringify(this.pickresults));
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

   
}

