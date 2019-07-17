import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Rule, RuleType } from 'src/app/model/rule';
import { Target } from 'src/app/model/target';
import { MetadataService } from '../metadata.service';
import { TosterService } from '../../services/toster.service';
import { element } from '@angular/core/src/render3';
import{GlobalResourceService} from '../../global-resource/global-resource.service';

@Component({
  selector: 'rule-upsert',
  template: `
  <div class="modal-header">
  <label>{{globalResourceService.getResourceByKey(header)}}</label>
  <button type="button" class="close" aria-label="Close" (click)="modal.dismiss('Cross click')">
    <span aria-hidden="true">&times;</span>
  </button>
</div>
<div class="modal-body">
 
    <div class="row">

      <div class="col-md-12">
        <div class="form-group">
          <label for="ruleName">{{globalResourceService.getResourceByKey("Name")}}</label><label
            class="text-mandatory margin-left-3">*</label>
              <input type="text" [name]="ruleName" [(ngModel)]="data.ruleName" class="form-control"  #ruleName="ngModel" >
         </div>
      </div>


      <div class="col-md-12">
        <div class="form-group">
          <label for="ruleTypeName">{{globalResourceService.getResourceByKey("ruleType")}}</label><label
            class="text-mandatory margin-left-3">*</label>
          <select name="ruleTypeName" class="form-control"   [(ngModel)]="selectedRuleType" (ngModelChange)="onRuleTypeChange($event)" >
            <option *ngFor="let vals of ruleTypes" [ngValue]="vals">{{vals.text}}</option>
          </select>
        </div>
      </div>


      <div class="col-md-12">
        <div class="form-group">
          <label for="targetName">{{globalResourceService.getResourceByKey("Target")}}</label><label
            class="text-mandatory margin-left-3">*</label>
            <!--data.targetList-->
          <select class="form-control" name="targetName" [(ngModel)]="data.targetList[0].selectedItem" (ngModelChange)="onTargetChange($event)">
            <option *ngFor="let opt of fieldSource" [ngValue]="opt">
              {{opt.name}}
            </option>
          </select> 
        </div>
      </div>
    </div>
    <div>
      <label>{{globalResourceService.getResourceByKey("Sources")}}</label><label
        class="text-mandatory margin-left-3">*</label>
      <a class="margin-left-10 text-link"
        (click)="onSourceAddClick()">{{globalResourceService.getResourceByKey("AddSource")}}</a>
      <div class="row" *ngFor="let s of data.sourceList; let i = index">
        <div class="col-md-6">
        <div class="form-group">    
        <select name={{i}} [(ngModel)]="s.selectedItem"  class="input-control" (ngModelChange)="onSourceChange($event, s)">
          <option *ngFor="let item of fieldSource" [ngValue]="item">{{item.name}}</option>
        </select>
      </div>
        </div>
        <div class="col-md-5">
          <my-tree *ngIf="s.selectedItem!=null" [mode]="1" [rootNode]="s.selectedItem" [resource]="resource"
            [selectedNode]="s.value" class="tree-role">
          </my-tree>
        </div>
        <div class="col-md-1">
          <a class="btn-small-action pull-right margin-top-5" *ngIf="data.sourceList.length>1" 
          (click)="onSourceRemoveClick(s)"  ngbTooltip="{{globalResourceService.getResourceByKey('Delete')}}" container="body">
            <i class="fa fa-times"></i>
          </a>
        </div>
      </div>
    </div>
  
  <div class="modal-footer">
    <button type="button" class="btn btn-secondary"
      (click)="modal.dismiss('cancel click')">{{globalResourceService.getResourceByKey("Cancel")}}</button>
      <button class="btn btn-primary" (click)="upsertRule()">{{globalResourceService.getResourceByKey(button)}}</button>
  </div>
  
</div>
`
})
export class RuleUpsertComponent {
  @Input() name: string;
  @Input() resource: any;
  @Input() header: any;
  @Input() button: any;
  @Input() data: Rule;
  @Input() ruleTypes: RuleType[];
  @Input() fieldSource: any[];
  @Input() eventType: any;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  @Output() updateEvent: EventEmitter<any> = new EventEmitter();
  @Output() errorEvent: EventEmitter<any> = new EventEmitter();
  @Output() warningEvent: EventEmitter<any> = new EventEmitter();
  public sourceCheck: Target = null;
  public selectedRuleType: RuleType = new RuleType();
  constructor(
    public modal: NgbActiveModal, 
    private metadataService: MetadataService,
    private toster: TosterService,
    public globalResourceService:GlobalResourceService,
    ) {}
  ngOnInit(): void {
    if (this.data) {
      this.mapSources(this.data.sourceList);
      this.mapSources(this.data.targetList);
      this.mapRuleType();
    }
  }
  public mapRuleType() {
    if (this.ruleTypes) {
      this.ruleTypes.forEach(element => {
        if (element.internalId == this.data.ruleType) {
          this.selectedRuleType = element;
          return;
        }
      });
    }
  }
  public mapSources(mapper: Target[]) {
    if (mapper && this.fieldSource) {
      mapper.forEach(element => {
        this.fieldSource.forEach(field => {
          if (element.name == field.name) {
            element.selectedItem = field;
            element.selectedItem.value = element.value;
            return;
          }
        });
        if (element.selectedItem) {
          return;
        }
      });
    }
  }
  // public generateResourceName(word) {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }

  public onSourceAddClick() {
    var flag = this.validate();
    if (flag) {
      this.warningEvent.emit( this.globalResourceService.getResourceByKey("PleaseEnterProperSourceValueFirst"))
    }
    else {
      var source = new Target();
      source.controlType = "";
      source.name = "";
      source.id = Math.floor(Math.random() * 100) + 1;
      this.data.sourceList.push(source);
    }
  }

  public onSourceRemoveClick(source: Target) {
    this.data.sourceList.splice(this.data.sourceList.indexOf(source), 1);
  }

  public onSourceChange(selectedItem: any, mapdata: Target) {
    this.sourceCheck = this.data.sourceList.find(a => a.name == selectedItem.name);
    if (this.sourceCheck == null) {
      mapdata.name = selectedItem.name;
      mapdata.controlType = selectedItem.controlType;
      mapdata.value = null;
    }
    else {

     this.errorEvent.emit(this.sourceCheck.name+" " + this.globalResourceService.getResourceByKey("Alreadyadded"));
    }

  }
  public onTargetChange(selectedItem: any) {
    this.data.targetList[0].name = selectedItem.name;
    this.data.targetList[0].controlType = selectedItem.controlType;
    this.data.targetList[0].value = 'true';
  }
  public onRuleTypeChange(selectedItem: any) {
    this.data.ruleType = selectedItem.internalId;
  }
  public upsertRule(): void {
    let errorMessage: string = "";

    if (this.data.ruleName === "" || this.data.ruleName === undefined) {
      errorMessage +=  this.globalResourceService.getResourceByKey("Nameisrequired")+"<br/>";
    }
    if (this.data.ruleType === "" || this.data.ruleType === undefined) {
      errorMessage += this.globalResourceService.getResourceByKey("Ruletypeisrequired")+"<br/>";
    }   
    if ( this.data.targetList[0].name === "" ||  this.data.targetList[0].name === undefined) {
      errorMessage +=  this.globalResourceService.getResourceByKey("Targetisrequired")+"<br/>";
    }
    // if ( this.data.sourceList === null ||  this.data.sourceList === undefined) {
    //   errorMessage += "Sources is required!<br/>";
    // }

    this.data.sourceList.forEach(a => {
      if ((a.selectedItem==null || a.selectedItem.value == '' || a.selectedItem.value == null)) {
        errorMessage += this.globalResourceService.getResourceByKey("Sourceisrequired")+"<br/>";
      }
    });

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }
    var flag = this.validate();
    if (flag) {
      this.warningEvent.emit(this.globalResourceService.getResourceByKey("PleaseEnterProperSourceValueFirst"))
    }
    else {
      this.data.sourceList.forEach(a => {
        a.value =a.selectedItem.value;
      });
      if (this.data.id) {
        this.updateRule();
      } else {
        this.saveRule();
      }
    }
  }
  public saveRule() {
    this.metadataService.saveRule(this.name, this.data).subscribe(result => {
      if (result) {
        this.saveEvent.emit();
      }
    },
      error => {
        this.errorEvent.emit(error.error.text);
      });

  }

  public validate(): boolean {
    var flag: boolean = false;
    this.data.sourceList.forEach(a => {
      if ((a.selectedItem==null || a.selectedItem.value == '' || a.selectedItem.value == null)) {
        flag = true;
      }
    });
    return flag;
  }

  public updateRule() {
    this.metadataService.updateRule(this.name, this.data).subscribe(result => {
      if (result) {
        this.updateEvent.emit();
      }
    },
      error => {
        this.errorEvent.emit(error.error.text);
      });
  }



}