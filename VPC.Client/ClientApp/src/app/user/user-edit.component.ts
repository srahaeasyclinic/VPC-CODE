import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { LayoutService } from '../meta-data/layout/layout.service';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { UserService } from './user.service';
import { ResourceService } from '../services/resource.service';
import {TosterService} from 'src/app/services/toster.service';
import {CommonService} from 'src/app/services/common.service';

@Component({
    selector: 'user-edit',
    templateUrl: './user-edit.component.html',
})
export class UserEditComponent implements OnInit {

    public layoutInfo: LayoutModel = new LayoutModel();
    public entityInfo: any;
    public tree: ITreeNode;
    public selectedTreeNode: ITreeNode | null;
    public isTreeReady:boolean = false;
    id: string;
    entityName: string;
    entityType: string;
    subType: string;
    context: string;
    currentPage: number = 1;
    pageSize: number = 10;
    result: any;
    public resource: any;

    public displayRule: any;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private layoutService: LayoutService,
        private userService:UserService,
        private resourceService: ResourceService,
        private toster: TosterService,
        private commonService: CommonService
    ) 
    {

    }

    ngOnInit() {
        this.getResource();
    }

    private getResource() {
      this.resource =this.resourceService.getResources();

                this.init()
      }

      private getRuleList(entityName:string): void {
    
        this.commonService.getRuleList(entityName)
          .pipe(first())
          .subscribe(
            data => {
              console.log('getRuleList UEdit ', data);
              if (data && data) {            
                this.displayRule = data.retVal;
                if(this.displayRule)
                {
                  this.displayRule.map(function (i) { return i["source"] = i.sourceList.map(function (e) { return e.name }).join() });
                  this.displayRule.map(function (i) { return i["target"] = i.targetList.map(function (t) { return t.name }).join() });
                }            
              }
            },
            error => {
              console.log(error)
            }
          );
      }

      private init(){
        this.entityName = "User";
        this.entityType = "Form";
        this.subType = "Employee";
        this.context = "Edit";
        this.getDefaultLayout(this.entityName, this.entityType, this.subType, this.context);
        this.getRuleList(this.entityName);

       
      }
      private getDefaultLayout(name:string, type:string, subtype:string, context:string) {
        this.layoutService.getDefaultLayout(name, type, subtype, context)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.layoutInfo = data;     
                this.getMetadataDetails();               
              }
            },
            error => {
              console.log(error);
            });   
      }

      private getMetadataDetails(){
        this.activatedRoute.params.subscribe((params: Params) => {
          this.id = params['id'];
    
          if (this.id) {
            this.getMetaDataDetails(this.entityName, this.id);
          }
        });
      }
      private mapData(fields, whichObject){
        fields.forEach((item, index) => {
          Object.keys(whichObject).forEach(function (key, index) {
            if (key.toLocaleLowerCase() == item.name.toLocaleLowerCase()) {
              item.value = whichObject[key];
            }
          });
          if(item.fields!=null && item.fields.length > 0){
            this.mapData(item.fields, whichObject);
          }
          if(item.tabs!=null && item.tabs.length > 0){
            this.mapData(item.tabs, whichObject);
          }
        });
      }

      private getMetaDataDetails(entityName: string,id:string) {
        this.layoutService.getEntityDetails(entityName, id)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.entityInfo = data;   
                if (this.entityInfo && this.layoutInfo && this.layoutInfo.formLayoutDetails) { 
                  //console.log("this.layoutInfo.formLayoutDetails.fields", this.layoutInfo.formLayoutDetails.fields);
                  //console.log("this.entityInfo", this.entityInfo);
                  this.mapData(this.layoutInfo.formLayoutDetails.fields, this.entityInfo);
                  this.tree = this.layoutInfo.formLayoutDetails;
                  this.isTreeReady = true;
                  //console.log("after mapping tree is ", this.tree);
                }                 
              }
            },
            error => {
              console.log(error);
            });   
      }

      public updateMetaDataValues(): void {
        //console.log("while click on fields::", this.tree.fields);

        // var value = {};
        // this.createKeyValue(this.tree.fields, value);

        let value = {};
        value = this.commonService.createKeyValue(this.tree.fields, value);

        console.log(value);
        this.layoutService.updateMetaDataValues(this.entityName, value, this.id, this.subType)
          .pipe(first())
          .subscribe(
            data => {
              this.toster.showSuccess(this.entityName + this.resource[this.generateResourceName("UpdatedSuccessfully")]);
              this.router.navigate(['users']);
            },
            error => {
              console.log(error);
            });
      }

      // private createKeyValue(data: TreeNode[], savedData: any) {
      //   data.forEach(element => {
      //     if (element.controlType.toLocaleLowerCase() != "section" && element.controlType.toLocaleLowerCase() != "tabs") {
      //       if(element.value instanceof Array){
      //         var valueArr = [];
      //         element.value.forEach(key => {
      //           valueArr.push(key.code);
      //         });
      //         savedData[element.name] = valueArr;
      //       }else{
      //         savedData[element.name] = element.value;
      //       }
      //     }
      //     if (element.fields) {
      //       this.createKeyValue(element.fields, savedData);
      //     }
      //     //not required ... need to save 
      //     // if (element.tabs) {
      //     //   this.createKeyValue(element.tabs, savedData);
      //     // }
      //   });
      // }    

      generateResourceName(word)
 {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }
}
