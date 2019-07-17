import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutModel } from '../../model/layoutmodel';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { TosterService } from 'src/app/services/toster.service';
import { PicklistUiService } from "../../picklist-ui/picklist-ui.service";
import { ResourceService } from '../../services/resource.service';
import {CommonService} from 'src/app/services/common.service';

import { first } from "rxjs/operators";
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';

@Component({
  selector: 'app-picklist-new',
  templateUrl: './picklist-new.component.html',
  styleUrls: ['./picklist-new.component.css']
})
export class PicklistNewComponent implements OnInit {

  //public layoutInfo: LayoutModel = new LayoutModel();
  public layoutInfo: LayoutModel;
  public tree: ITreeNode;
  public selectedTreeNode: ITreeNode | null;
  public isTreeReady: boolean = false;
  id: string;
  entityName: string;  
  result: any;
  private layoutType: number = 2;//New page 
  private layoutContext: number = 1;  
  resource:Resource;
  public displayRule: any;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistUiService,
    private resourceService: ResourceService,
    private toster: TosterService,   
    private commonService: CommonService,
    private globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {

    // this.getResource();    
    this.resource = this.globalResourceService.getGlobalResources();
    this.activatedRoute.parent.params.subscribe((urlPath) => {
      this.entityName = urlPath["name"]; 

  
      if (this.entityName) {
        this.getDefaultLayout(this.entityName);
      } else {
        this.toster.showWarning(this.getResourceValue("UrlTemperedorNoEntityNameoridFoundorEntityNotYetDecorated"));
      }
    });
   
    //this.getDefaultLayout(this.entityName);
  }

 

  

  private getDefaultLayout(entityName) {
    this.picklistService.getDefaultLayout(entityName, this.layoutType, this.layoutContext)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutInfo = new LayoutModel();
            this.layoutInfo = data;
            
            this.tree = data.formLayoutDetails;
            this.isTreeReady = true;
          }
        },
        error => {
          console.log(error);
        });
  }

  public savePicklistValue() {
    // var value = {};
    // this.createKeyValue(this.tree.fields, value);

    let value = {};
    value = this.commonService.createKeyValue(this.tree.fields, value);

    this.picklistService.savePicklistValues(this.entityName, value)
      .pipe(first())
      .subscribe(
        data => {
     
          this.toster.showSuccess(this.entityName + this.getResourceValue("SavedSuccessfully"));
         // this.router.navigate(['picklist/ui/' + this.entityName.toLowerCase()]);
          this.router.navigate(["../"], { relativeTo: this.activatedRoute });
        },
        error => {
          
          console.log(error);
        });
  }

  // private createKeyValue(data: TreeNode[], savedData: any) {
  //   data.forEach(element => {
  //     if (element.controlType.toLocaleLowerCase() != "section" && element.controlType.toLocaleLowerCase() != "tabs") {
  //       //console.log(element);
  //       savedData[element.name] = element.value;
  //     }
  //     if (element.fields) {
  //       this.createKeyValue(element.fields, savedData);
  //     }
  //   });
  // }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}

