import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Rule, RuleType } from 'src/app/model/rule';
import { Target } from 'src/app/model/target';
import { MetadataService } from '../metadata.service';
import { TosterService } from '../../services/toster.service';
import { element } from '@angular/core/src/render3';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { jsonpCallbackContext } from '@angular/common/http/src/module';

@Component({
  selector: 'rule-upsert',
  templateUrl: './ruleupsert.component.html',
  styles: [`
    .enabled{
      display:block;
    }
    .not-enabled{
      display:none;
    }
  `]
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
  public sourceList: Target[];
  public targetList: Target[];
  public selectedRuleType: RuleType = new RuleType();
  public newFieldSource: Target[];
  private noSourceRuleTypes: any[];
  private noTargetRuleTypes: any[];
  private clonetargetList: any[];
  constructor(
    public modal: NgbActiveModal,
    private metadataService: MetadataService,
    private toster: TosterService,
    private globalResourceService: GlobalResourceService,
  ) { }
  ngOnInit(): void {
    // this.fieldSource.forEach(a => {
    //   a['useIndex'] = 0;
    // });
    // this.sourceList=[...this.fieldSource];
    // this.targetList=[...this.fieldSource.filter(a => a.required != true)];

    if (this.data) {
      this.mapSources(this.data.targetList);
      this.clonetargetList = this.data.targetList;
      this.mapSources(this.data.sourceList);
      this.mapRuleType();
      //this.checkFieldSource();
    }
    this.noSourceRuleTypes = [2];
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
  public mapTarget(mapper: Target[]) {
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
  public onTargetAddClick() {
    //if (this.targetList.filter(a => a['useIndex'] != 1).length > 0) {
    var flag = this.validTarget();
    if (flag) {
      this.warningEvent.emit(this.getResourceValue("rule_field_target_invalid_message"))
    }
    else {
      var target = new Target();
      target.controlType = "";
      target.name = "";
      target.id = Math.floor(Math.random() * 100) + 1;
      this.data.targetList.push(target);
    }
    // } else {
    //   this.warningEvent.emit(this.getResourceValue("rule_field_metadetails_notavailable_message"));
    // }

  }

  public onSourceAddClick() {
    //if (this.sourceList.filter(a => a['useIndex'] != 1).length > 0) {
    var flag = this.validateSource();
    if (flag) {
      this.warningEvent.emit(this.getResourceValue("rule_field_source_invalid_message"));
    }
    else {
      var source = new Target();
      source.controlType = "";
      source.name = "";
      source.id = Math.floor(Math.random() * 100) + 1;
      this.data.sourceList.push(source);
    }
    // } else {
    //   this.warningEvent.emit(this.getResourceValue("rule_field_metadetails_notavailable_message"));
    // }

  }

  public onSourceRemoveClick(source: Target) {
    this.data.sourceList.splice(this.data.sourceList.indexOf(source), 1);
    //this.checkFieldSource();
  }
  public onTargetRemoveClick(t: Target) {
    this.data.targetList.splice(this.data.targetList.indexOf(t), 1);
    //this.checkFieldSource();
  }

  public onSourceChange(selectedItem: any, mapdata: Target) {
    mapdata.name = selectedItem.name;
    mapdata.controlType = selectedItem.controlType;
    mapdata.value = null;
    //this.checkFieldSource();

  }
  public onTargetChange(selectedItem: any, index: any) {
    this.data.targetList[index].name = selectedItem.name;
    this.data.targetList[index].controlType = selectedItem.controlType;
    this.data.targetList[index].value = 'true';
    //this.checkFieldSource();
  }
  public onRuleTypeChange(selectedItem: any) {
    this.data.ruleType = selectedItem.internalId;
    //console.log('this.data ', this.data);
    if (this.checkSourceRequired(this.data.ruleType) == 2) {
      this.data.sourceList = null;
      this.data.targetList = this.clonetargetList.filter(d=>d.name.indexOf(".")>-1);
    }
    else {
      this.data.sourceList = [new Target()];
      this.data.targetList = this.clonetargetList;
    }
  }
  private checkSourceRequired(argRuleType) {
    if(this.noSourceRuleTypes.length > 0)
    {
      return this.noSourceRuleTypes.findIndex(t => (t.toString() === argRuleType));
    }
    else
    {
      return -1;
    }
  }
  private checkTargetRequired(argRuleType) {
    if(this.noSourceRuleTypes.length > 0)
    {
      return this.noTargetRuleTypes.findIndex(t => (t.toString() === argRuleType));
    }
    else
    {
      return -1;
    }
  }
  public upsertRule(): void {
    let errorMessage: string = "";

    if (this.data.ruleName === "" || this.data.ruleName === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("rule_field_name") + "<br/>";
    }
    if (this.data.ruleType === "" || this.data.ruleType === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("rule_field_ruletype") + "<br/>";
    }
    if (this.data.sourceList !== null) {
      if (this.data.sourceList.find(a => a.selectedItem == null || a.selectedItem.value == '' || a.selectedItem.value == null)) {
        errorMessage += this.globalResourceService.requiredValidator("rule_field_source") + "<br/>";
      }
    }

    if (this.data.targetList.find(a => a.name === "" || a.name === undefined)) {
      errorMessage += this.globalResourceService.requiredValidator("rule_field_target") + "<br/>";
    }


    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }
    else {
      if (this.data.sourceList !== null) {
        this.data.sourceList.forEach(a => {
          a.value = a.selectedItem.value;
          a.name = a.selectedItem.name;
        });
      }

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

  public validateSource(): boolean {
    var flag: boolean = false;
    this.data.sourceList.forEach(a => {
      if ((a.selectedItem == null || a.selectedItem.value == '' || a.selectedItem.value == null)) {
        flag = true;
      }
    });
    return flag;
  }
  private validTarget(): boolean {
    var flag: boolean = false;
    this.data.targetList.forEach(a => {
      if ((a.selectedItem == null || a.selectedItem.name == '')) {
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
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }


  checkFieldSource() {
    this.newFieldSource = [];
    this.newFieldSource = this.data.sourceList.map(function (item) { return item.selectedItem });
    this.newFieldSource = this.newFieldSource.concat(this.data.targetList.map(function (item) { return item.selectedItem }));
    this.newFieldSource = this.newFieldSource.filter(a => a);

    if (this.newFieldSource) {
      this.sourceList.forEach(element => {
        if (this.newFieldSource.find(a => a.name == element.name)) {
          element['useIndex'] = 1;
        } else {
          element['useIndex'] = 0;
        }
      });
      this.targetList.forEach(element => {
        if (this.newFieldSource.find(a => a.name == element.name)) {
          element['useIndex'] = 1;
        } else {
          element['useIndex'] = 0;
        }
      });
    }

  }
}