import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { MetadataService } from '../metadata.service';
import { Entities } from '../../model/entities';
import { Rule, RuleType } from '../../model/rule';
import { Target } from '../../model/target';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { Resource } from '../../model/resource';
import { ITreeNode } from "../../dynamic-form-builder/tree.module";
import { LayoutService } from '../layout/layout.service';
import { TosterService } from 'src/app/services/toster.service';
import { PicklistService } from '../../picklist/picklist.service';
import { CommonService } from '../../services/common.service'
import { GlobalResourceService } from '../../global-resource/global-resource.service'
import { RuleUpsertComponent } from 'src/app/meta-data/rule/ruleupsert.component';
import { MenuService } from 'src/app/services/menu.service';


export class SourceList {
  public selectedItem: any;
  public tree: ITreeNode;
  public id: number;
}

@Component({
  selector: 'app-rule',
  templateUrl: './rule.component.html',
  styleUrls: ['./rule.component.css']
})

export class RuleComponent implements OnInit {

  public tree: ITreeNode;
  private entity: Entities;
  public gridData: any = this.entity;
  private name: string;
  public fieldSource: any;
  public target: any;
  public source: any;
  public value: string = "";
  public sourceValue: string = "";
  public pickListSource: any;
  public addRuleLabel: string = "Add Rule";
  private modalReference: any;
  public ruleTypes: RuleType[] | null;
  public targetList = [];
  public ruleTarget = {};
  private ruleModel = new Rule();
  public sourceList: SourceList[] | null;
  public resource: Resource;
  public saveUpdateLabel = "Create";

  public buttonLabel = "";

  private _rules: Rule[] | null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private metadataService: MetadataService,
    private modalService: NgbModal,
    private layoutService: LayoutService,
    private toster: TosterService,
    private pickListService: PicklistService,
    private commonService: CommonService,
    public globalResourceService: GlobalResourceService,
    public menuService: MenuService
  ) {





  }

  ngOnInit() {
    //this.getResource();
    //this.resource = this.globalResourceService.getGlobalResources();
    //console.log('Rule : '+JSON.stringify( this.resource));
    this.buttonLabel = this.getResourceValue('metadata_task_add');
    this.getRuleTypeList ("RuleType");
    this.activatedRoute.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
      this.getMetadataFieldsByName(this.name);
    });


  }
  public handleSelection(node, i): void {
    //console.log('selected Item' + JSON.stringify(node) + 'Insex=' + i);
  }

  // private getResource(): void {
  //   this.resourceService.getResources()
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.resource = data;
  //           this.buttonLabel = this.resource["addRule"];
  //           this.getRuleTypeList("RuleType");
  //         }
  //       },
  //       error => {
  //         console.log(error)
  //       }
  //     );
  // }

  private getRuleTypeList(pickListName: string): void {
    this.pickListService.getPickListValues(pickListName)
      .pipe(first())
      .subscribe(data => {
        if (data && data) {
          this.ruleTypes = data.result;

          //console.log('data.result ', data.result);

          // let obj:any = {
          //   active: true,
          //   flagged: false,
          //   internalId: "2",
          //   isDeletetd: false,
          //   text: "Unique"
          // }
          // let obj2:any = {
          //   active: true,
          //   flagged: false,
          //   internalId: "3",
          //   isDeletetd: false,
          //   text: "Disable"
          // }
          // this.ruleTypes.push(obj);
          // this.ruleTypes.push(obj2);

          this.getRuleList(this.name);
        }
      }
        , error => {
          console.log(error);
        });
  }
  private getRuleList(entityName: string): void {
    this.commonService.getRuleList(entityName)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {

            //mapper for grid
            this.gridData = data.retVal;
            this.gridData.map(function (i) {
              if (i.sourceList != null && i.sourceList != undefined)
              {
                 return i["source"] = i.sourceList.map(function (e) { return e.name }).join()
              }
             
            });
            this.gridData.map(function (i) {
              if (i.targetList != null && i.targetList != undefined) {
                return i["target"] = i.targetList.map(function (t) { return t.name }).join()
              }
            });

            //-----
            this._rules = data.retVal;
          }

        },
        error => {
          console.log(error)
        }
      );
  }

  public addRulePopup(): void {
    var rule = new Rule();
    var source = new Target();
    rule.sourceList = [];
    rule.sourceList.push(source);
    //---------------------------------------------
    var target = new Target();
    rule.targetList = [];
    rule.targetList.push(target);
    this.openPopup(rule);
  }

  private ngbModalOptions: NgbModalOptions = {
    backdrop: 'static',
    keyboard: false
  };
  private editRulePopup(id): void {
    this.metadataService.getRuleById(this.name, id)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.openPopup(data);
          }
        },
        error => {
          console.log(error);
        });
  }
  private openPopup(data: Rule) {
    const modalRef = this.modalService.open(RuleUpsertComponent, this.ngbModalOptions);
    modalRef.componentInstance.resource = this.resource;
    modalRef.componentInstance.ruleTypes = this.ruleTypes;
    modalRef.componentInstance.fieldSource = this.fieldSource;
    modalRef.componentInstance.name = this.name;//resource_operation_update_success_message
    if (data && data.id) {
      modalRef.componentInstance.header = this.getResourceValue ("metadata_label_editrule");
      modalRef.componentInstance.button = this.getResourceValue("rule_operation_update");
    } else {
      modalRef.componentInstance.header = this.getResourceValue("metadata_label_addrule");
      modalRef.componentInstance.button = this.getResourceValue("rule_operation_create");
    }
    modalRef.componentInstance.data = data;
    modalRef.componentInstance.saveEvent.subscribe((saveData) => {
      this.toster.showSuccess(this.globalResourceService.saveSuccessMessage("rule_displayname"));
      modalRef.close();
      this.getRuleList(this.name);
    });
    modalRef.componentInstance.updateEvent.subscribe((updateData) => {
      this.toster.showSuccess(this.globalResourceService.updateSuccessMessage("rule_displayname"));
      modalRef.close();
      this.getRuleList(this.name);
    });
    modalRef.componentInstance.errorEvent.subscribe((error) => {
      this.toster.showError(error);
    });
    modalRef.componentInstance.warningEvent.subscribe((error) => {
      this.toster.showWarning(error);
    });
  }

  // private bindGetRulesData(data: any) {
  //   this.ruleModel = data;
  //   this.ruleModel.ruleType = data.ruleType.toString();
  //   this.targetList[0] = this.ruleModel.targetList[0];
  //   var terg = this.ruleModel.targetList[0].name;
  //   this.ruleTarget = this.target.find(a => a.name === terg);
  //   this.sourceList = [];
  //   this.ruleModel.sourceList.forEach(i => {
  //     var source: any = {};
  //     var s = this.target.find(a => a.name == i.name);
  //     source.selectedItem = s;
  //     source.tree = s;
  //     source.tree.name = i.name;
  //     source.tree.value = i.value;
  //     source.controlType = i.controlType;
  //     source.name = i.name;
  //     source.value = i.value;
  //     this.sourceList.push(source);
  //   });
  // }

  private getMetadataFieldsByName(name) {
    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      this.target = data.fields;
      this.fieldSource = data.fields;
      //console.log('if');
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              this.target = data.fields;
              this.fieldSource = data.fields;
            }
          },
          error => {
            console.log(error);
          });
      //console.log('else');
    }






    // this.metadataService.getMetadataByName(name)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data) {
    //         this.target = data.fields;
    //         this.fieldSource = data.fields;
    //       }
    //     },
    //     error => {
    //       console.log(error);
    //     });
  }



  private deleteRule(data): void {

    this.globalResourceService.openDeleteModal.emit()

    this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
      this.metadataService.deleteRule(this.name, data).subscribe(result => {
        if (result) {
          this.getRuleList(this.name);
        }
      });

    });
    // swal({
    //   title: this.getResourceValue("common_message_areyousure"),
    //   text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
    //   type: 'warning',
    //   showCancelButton: true,
    //   confirmButtonColor: '#3085d6',
    //   cancelButtonColor: '#d33',
    //   confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
    //   showLoaderOnConfirm: true,
    // })
    //   .then((willDelete) => {
    //     if (willDelete.value) {
    //       this.metadataService.deleteRule(this.name, data).subscribe(result => {
    //         if (result) {
    //           this.getRuleList(this.name);
    //         }
    //       });

    //     } else {
    //     }

    //   });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
