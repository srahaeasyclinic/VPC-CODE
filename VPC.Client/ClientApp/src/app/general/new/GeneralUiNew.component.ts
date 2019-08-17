import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutModel } from '../../model/layoutmodel';
import { ITreeNode } from '../../dynamic-form-builder/tree.module';
import { ResourceService } from '../../services/resource.service';
import { TosterService } from '../../services/toster.service';
import { first } from "rxjs/operators";
import { LayoutService } from '../../meta-data/layout/layout.service';
import { EntityValueService } from '../entityValue.service';
import 'rxjs/add/operator/filter';
import { CommonService } from 'src/app/services/common.service';
import { ValidationService } from 'src/app/services/validation.service';
import { RequiredfieldService } from '../../services/requiredfield.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { MenuService, MenuType } from 'src/app/services/menu.service';

@Component({
  selector: 'app-generalUi-new',
  templateUrl: './GeneralUiNew.component.html',
  styleUrls: ['./GeneralUiNew.component.css']
})
export class GeneralUiNewComponent implements OnInit {

  //public layoutInfo: LayoutModel = new LayoutModel();
  public layoutInfo: LayoutModel;
  public tree: ITreeNode;
  public selectedTreeNode: ITreeNode | null;
  public isTreeReady: boolean = false;

  public entityName: string;
  private resource: any;
  private layoutType: string = "Form";//New page 
  private layoutContext: string = "Add";
  private layoutSubType: string = '';
  public validateMessages: Array<string> = [];

  public displayRule: any;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private toster: TosterService,
    private resourceService: ResourceService,
    private route: ActivatedRoute,
    private layoutService: LayoutService,
    private entityValueService: EntityValueService,
    private commonService: CommonService,
    private validateService: ValidationService,
    private requiredfieldService: RequiredfieldService,
    private modalService: NgbModal,
    private globalResourceService: GlobalResourceService,
    private menuService: MenuService
  ) { }

  ngOnInit() {
    let result = this.menuService.getMenuconext();
    this.entityName = result.param_name;
    if (result && result.menuType == MenuType.Entity) {
      // this.menuService.setchildMenuBreadcums([{ elementName:"new", 'elementURL':""}]);
      this.getResource();
    }

  }

  private getResource(): void {
    //this.resource = this.resourceService.getResources();
    this.resource = this.globalResourceService.getGlobalResources();
    this.processUrl();

  }

  private getRuleList(entityName: string): void {
    this.commonService.getRuleList(entityName)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            //console.log('data', data);
            this.displayRule = data.retVal;
            this.displayRule.map(function (i) { return i["source"] = i.sourceList.map(function (e) { return e.name }).join() });
            this.displayRule.map(function (i) { return i["target"] = i.targetList.map(function (t) { return t.name }).join() });
          }
        },
        error => {
          console.log(error)
        }
      );
  }

  private processUrl(): void {
    let result = this.menuService.getMenuconext();
    this.entityName = result.param_name;
    this.route.parent.params.subscribe((urlPath) => {
      // this.entityName = urlPath["name"]; 

      this.route.queryParams
        .filter(params => params.subType)
        .subscribe(params => {
          //console.log(params);
          this.layoutSubType = params.subType;
          //console.log(this.layoutSubType);

          if (this.entityName && this.layoutSubType) {
            this.getDefaultLayout(this.entityName, this.layoutType, this.layoutSubType, this.layoutContext);
          } else {
            this.toster.showWarning(this.getResourceValue('metadata_operation_alert_warning_message'));
          }
        });
      this.getRuleList(this.entityName);
    });

  }

  private getDefaultLayout(name: string, type: string, subtype: string, context: string): void {
    this.layoutService.getDefaultLayout(name, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutInfo = new LayoutModel();
            this.layoutInfo = data;

            if (data.formLayoutDetails) {
              this.mapData1(data.formLayoutDetails.fields);
              this.tree = data.formLayoutDetails;
              this.isTreeReady = true;
            } else {
              this.toster.showWarning(this.getResourceValue('metadata_warning_nodefaulttemplate_message'));
            }
          }
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }


  private mapData1(fields) {

    fields.forEach((item, index) => {

      if (item.supportedQuickAddModes) {
        item.supportedQuickAddModes.forEach(layoutType => {
          var types = layoutType.toString().split('');
          if (types.length > 1) {

            item.isQuickAddSupported = false;
            if (types[0] == this.layoutInfo.layoutType.toString()) {
              if (types[1] == this.layoutInfo.context.toString()) {
                item.isQuickAddSupported = true;
                return;
              }
            }
          }
        });
      }
      
      if (item.validators) {
        item.validators.forEach(element => {
          if (element.name.toLowerCase() == 'defaultvaluevalidator') {
            item.value=element.options[0].value
          }
        });
      }

      if (item.fields != null && item.fields.length > 0) {
        this.mapData1(item.fields);
      }
      if (item.tabs != null && item.tabs.length > 0) {
        this.mapData1(item.tabs);
      }
    });
  }

  //public save(content: string): string {
  //  this.validateMessages = [];
  //  this.validateMessages = this.validateService.validate(this.tree.fields,this.entityName);
  //  let ngbModalOptions: NgbModalOptions = {
  //    backdrop : 'static',
  //    keyboard : false
  //  };
  //  if (this.validateMessages.length > 0) {
  //    this.modalService.open(content, ngbModalOptions);
  //    return
  //  }
  //  let value = {};
  //  value = this.commonService.createKeyValue(this.tree.fields, value);

  //  // console.log('Tree save ' + JSON.stringify(this.tree.fields));
  //   // console.log('Tree value '+JSON.stringify(value));

  //   this.entityValueService.saveEntityValue(this.entityName, this.layoutSubType, value)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         this.toster.showSuccess(this.entityName + ' saved successfully.');
  //        // this.router.navigate(['ui/' + this.entityName]);
  //        this.router.navigate(["../"], { relativeTo: this.activatedRoute });
  //       },
  //       error => {
  //         if (error.status===501) 
  //       {
  //           this.toster.showError(error.message);
  //       }
  //       });
  // }

  generateResourceName(word: string) {
    return this.commonService.generateResourceNameWithHierarchy(word);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}



