import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { first } from 'rxjs/operators';
import { LayoutModel } from '../model/layoutmodel';
import { LayoutService } from '../meta-data/layout/layout.service';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { UserService } from './user.service';

// import { LayoutModel } from '../../../model/layoutmodel';
// import { LayoutService } from '../layout.service';
//import {TosterService} from '../../../services/toster.service';
import { ResourceService } from '../services/resource.service';
import { TosterService } from 'src/app/services/toster.service';
import {ValidationService} from 'src/app/services/validation.service';
import {RequiredfieldService } from '../services/requiredfield.service';

import {CommonService} from 'src/app/services/common.service';



@Component({
    selector: 'user-create',
    templateUrl: './user-create.component.html',
})
export class UserCreateComponent implements OnInit {

    public layoutInfo: LayoutModel = new LayoutModel();
    public tree: ITreeNode;
    public selectedTreeNode: ITreeNode | null;
    public isTreeReady: boolean = false;
    public errorMessage:string="";
    id: string;
    entityname: string;
    currentPage: number = 1;
    pageSize: number = 10;
    result: any;
    public resource: any;
    public displayRule: any;
    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private layoutService: LayoutService,
        private userService: UserService,
        private toster: TosterService,
        private resourceService: ResourceService,
        private validateService:ValidationService,
        private requiredfieldService:RequiredfieldService,
        private commonService: CommonService
    ) {

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
              console.log('getRuleList GUIE ', data);
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

    private init() {
        

        this.entityname = "User";
        this.getDefaultLayout(this.entityname, "Form", "employee", "Add");
        this.getRuleList(this.entityname);
    }
    private getDefaultLayout(name: string, type: string, subtype: string, context: string) {
        this.layoutService.getDefaultLayout(name, type, subtype, context)
            .pipe(first())
            .subscribe(
                data => {
                    if (data) {
                        if (data.formLayoutDetails) {
                            this.tree = data.formLayoutDetails;
                            this.isTreeReady = true;
                        } else {
                            this.toster.showError(this.resource[this.generateResourceName("DefaultTemplateNotFound")]);
                        }
                    }
                },
                error => {
                    console.log(error);
                });
    }


    private getLayoutById(name, layoutId) {
        this.layoutService.getLayoutById(name, layoutId)
            .pipe(first())
            .subscribe(
                data => {
                    if (data) {

                    }
                },
                error => {
                    console.log(error);
                });
    }
    private getQuery() {
        var queryString = "";
        if (this.layoutInfo && this.layoutInfo.listLayoutDetails.fields) {
            for (var k = 0; k < this.layoutInfo.listLayoutDetails.fields.length; k++) {
                queryString += this.layoutInfo.listLayoutDetails.fields[k].name + ",";
            }
            queryString = queryString.substring(0, queryString.length - 1);
        }
        queryString += "&pageIndex=" + this.currentPage + "&pageSize=" + this.pageSize;
        return queryString;
    }

    public saveUser() {  
                     
       /* var validateMessage = this.validateService.validate(this.tree.fields);
        if(validateMessage!=""){
            //console.log("validateMessage", validateMessage);
            return this.toster.showWarning(validateMessage);
        }*/

    }

    private createKeyValue(data: ITreeNode[], savedData: any) {
        data.forEach(element => {
            if (element.controlType.toLocaleLowerCase() != "section") {
                if (element.controlType.toLocaleLowerCase() != "tabs") {
                    savedData[element.name] = element.value
                }
            }
            if (element.fields) {
                this.createKeyValue(element.fields, savedData);
            }
        });
        return savedData;
    }
    generateResourceName(word)
 {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }
}
