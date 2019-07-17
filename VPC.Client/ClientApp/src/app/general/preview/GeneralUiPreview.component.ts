import { Component, Input, Output, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../model/layoutmodel';
import { LayoutService } from '../../meta-data/layout/layout.service';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { EntityValueService } from '../entityValue.service';
import { TosterService } from '../../services/toster.service';
import { ResourceService } from '../../services/resource.service';
import { Data } from '../../services/storage.data';
import { CommonService } from 'src/app/services/common.service';
import { ValidationService } from 'src/app/services/validation.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';

@Component({
  selector: 'app-general-ui-preview',
  templateUrl: './GeneralUiPreview.component.html',
  styleUrls: ['./GeneralUiPreview.component.css']
})
export class GeneralUiPreviewComponent implements OnInit {
  //public layoutInfo: LayoutModel = new LayoutModel();
  public layoutInfo : LayoutModel;
  public entityInfo: any;
  public tree: ITreeNode;
  public selectedTreeNode: ITreeNode | null;
  public isTreeReady: boolean = false;
  public transitObject: any;
  public isPreviousIdAvailable: boolean = false;
  public isNexIdAvailable: boolean = false;

  private resource: any;
  private entityName: string;
  private groupName: string;
  private layoutType: string = "Form";
  private layoutContext: string = "Edit";
  private layoutSubType: string = '';
  public validateMessages: Array<string> = [];

  public displayRule: any;
  public entityId;
  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private resourceService: ResourceService,
    private toster: TosterService,
    private entityValueService: EntityValueService,
    private data: Data,
    private commonService: CommonService,
    private validateService: ValidationService,
    private modalService: NgbModal,
    private globalResourceService: GlobalResourceService) { }
  //private previousClick: boolean = false;

  ngOnInit() {
    this.transitObject = this.data.storage;
    
    this.getResource();
    
  }

  // Method sections
  private getResource(): void {
    //this.resource =this.resourceService.getResources();
    this.resource = this.globalResourceService.getGlobalResources();
    this.processUrl();

  }


  private processUrl(): void {

      this.activatedRoute.parent.params.subscribe((urlPath) => {
        this.entityName = urlPath["name"];
        this.groupName = urlPath["group"];

      this.activatedRoute.queryParams
        .filter(params => params.subType)
        .subscribe(params => {
          this.layoutSubType = params.subType;
          this.getPreviousNextData();
          if (this.entityName && this.layoutSubType) {
            this.getDefaultLayout(this.entityName, this.layoutType, this.layoutSubType, this.layoutContext);
          } else {
            this.toster.showWarning('Url tempered! or no entity name found!');
          }
        }, error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }

        });
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
            this.activatedRoute.params.subscribe((params: Params) => {
              let entityValueId = params['id'];
              this.entityId = entityValueId;
              if (entityValueId) {
                this.getEntityDetails(this.entityName, entityValueId);
              }
            });
          }
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }

        });
  }

  private getEntityDetails(entityName: string, entityValueId: string) {
    this.entityValueService.getEntityDetails(entityName, entityValueId, this.layoutSubType)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.entityInfo = data;
            //console.log('this.entityInfo ', this.entityInfo);
            if (this.entityInfo && this.layoutInfo && this.layoutInfo.formLayoutDetails) {
              //console.log("this.layoutInfo.formLayoutDetails.fields", this.layoutInfo.formLayoutDetails.fields);
              //console.log("this.entityInfo", this.entityInfo);
              this.mapData(this.layoutInfo.formLayoutDetails.fields, this.entityInfo);
              this.tree = this.layoutInfo.formLayoutDetails;
              this.isTreeReady = true;
              //console.log("after mapping tree is ", JSON.stringify(this.tree));
            }
          }
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }

  private mapData(fields, whichObject) {
    fields.forEach((item, index) => {
      Object.keys(whichObject).forEach(function (key, index) {
        if (key.toLocaleLowerCase() == item.name.toLocaleLowerCase()) {
          item.value = whichObject[key];
        }
      });
      if (item.fields != null && item.fields.length > 0) {
        this.mapData(item.fields, whichObject);
      }
      if (item.tabs != null && item.tabs.length > 0) {
        this.mapData(item.tabs, whichObject);
      }
    });
  }

  private getPreviousNextData(): void {
    if (this.data) {
      //Get the data for the previous next operation
      this.transitObject = this.data.storage;
      let recordNo : number = 0;
      let currentPage : number = 0;
      currentPage = this.transitObject.pageIndex - 1;

      if(!this.transitObject.recordNo)
      {
        recordNo = (currentPage * 10) + (this.transitObject.itemIndex) + 1;
        this.transitObject.recordNo = recordNo;        
      }     
      else
      {
        recordNo = this.transitObject.recordNo;
      }  

      this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
      this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));
    }
  }


  private getEntityValueId(): string {
    let entityValueId: string = '';
    this.activatedRoute.params.subscribe((params: Params) => {
      entityValueId = params['id'];
    });
    return entityValueId;
  }

  private getQuery(): string {
    // "ItemName,Code,OrgNo,IsLegalEntity,IsOrganization,UpdatedOn,SubType,TenantType,Active&searchText=qua&pageIndex=1&pageSize=10&filters=Active,1"

    let pagesize: number = 1;
    let maxresult: number = 1;
    let query: string = '';

    if (this.transitObject && this.transitObject.fields) {
      query = this.transitObject.fields + '&';
    }

    // if (this.transitObject && this.transitObject.freetextsearch) {
    //   query += 'searchText=' + this.transitObject.freetextsearch + '&';
    // }

    if (this.transitObject && this.transitObject.searchText) {
      query += this.transitObject.searchText + '&';
    }

    // if (this.transitObject && this.transitObject.itemIndex) {
    //   query += 'pageIndex=' + this.transitObject.itemIndex + '&';
    // }
    if(this.transitObject.recordNo)
    {
      query += 'pageIndex=' + this.transitObject.recordNo + '&';
    }

    query += 'pageSize=' + pagesize + '&';

    if (this.transitObject && this.transitObject.filters) {
      query += 'filters=' + this.transitObject.filters + '&';
    }

    if (this.transitObject && this.transitObject.orderBy) {
      query += 'orderBy=' + this.transitObject.orderBy + '&';
    }

    query += 'maxResult=' + maxresult;

    return query;

  }
  // Events




  redirectToListPageWithError(): void {
    this.toster.showError(this.entityName + ' relation not created');
    this.router.navigate(['ui/' + this.entityName]);
  }

  private redirectToListPage() {
    this.toster.showSuccess(this.entityName + ' updated successfully.');
    this.router.navigate(['ui/' + this.entityName]);
  }

  public nextData(): void {
    if (this.transitObject && this.transitObject.fields) {

      this.transitObject.recordNo = this.transitObject.recordNo + 1;
      this.transitObject.nextClick = true;     

      let query: string = this.getQuery();
      this.entityValueService.getEntityValues(this.entityName, query)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result.length > 0) {
                let id = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

                //this.getEntityDetails(this.entityName, id);

                this.data.storage = this.transitObject;
                //this.router.navigate(["ui/" + this.entityName + "/preview/" + id], { queryParams: { subType: this.layoutSubType } });
                this.router.navigate(["../../preview", id], { queryParams: {subType: this.layoutSubType }, relativeTo: this.activatedRoute });
              }

            }
          },
          error => {
            if (error.status === 501) {
              this.toster.showError(error.message);
            }


          });
    }
  }

  public previousData(): void {
    if (this.transitObject && this.transitObject.fields && this.transitObject.orderBy) {

      this.transitObject.recordNo = this.transitObject.recordNo - 1;
      this.transitObject.previousClick = true;

      let query: string = this.getQuery();
      this.entityValueService.getEntityValues(this.entityName, query)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result.length > 0) {
                let id = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

                //this.getEntityDetails(this.entityName,id);
                this.data.storage = this.transitObject;
                //this.router.navigate(["ui/" + this.entityName + "/preview/" + id], { queryParams: { subType: this.layoutSubType } });
                this.router.navigate(["../../preview", id], { queryParams: {subType: this.layoutSubType }, relativeTo: this.activatedRoute });
              }
            }
          },
          error => {
            if (error.status === 501) {
              this.toster.showError(error.message);
            }

          });
    }
  }

  public update(content: string): string {

    this.validateMessages = [];
    this.validateMessages = this.validateService.validate(this.tree.fields);
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };

    if (this.validateMessages.length > 0) {
      this.modalService.open(content, ngbModalOptions);
      return
    }
    let value: any = {};
    value = this.commonService.createKeyValue(this.tree.fields, value);
    let id = this.getEntityValueId();
    this.entityValueService.updateEntityValues(this.entityName, value, id, this.layoutSubType)
      .pipe(first())
      .subscribe(
        data => {
          this.redirectToListPage();
        },
        error => {
          if (error.status === 501) {
            this.toster.showError(error.message);
          }
        });
  }
  generateResourceName(word: string) {
    return this.commonService.generateResourceNameWithHierarchy(word);
  }

  public edit(): void {

    // let transitObject = {
    //   name: this.entityName,
    //   fields: this.transitObject.fields,
    //   searchText: this.transitObject.searchText,
    //   orderBy: this.transitObject.orderBy,
    //   filters: this.transitObject.filters,
    //   pageIndex: this.transitObject.pageIndex,
    //   pageSize: this.transitObject.pageSize,
    //   itemIndex: this.transitObject.itemIndex,
    //   totalRecords: this.transitObject.totalRecords
    // };

    // this.data.storage = transitObject;
    this.router.navigate(["../../edit", this.entityId], { relativeTo: this.activatedRoute, queryParams: { subType: this.layoutSubType } });

    // this.activatedRoute.params.subscribe((params: Params) => {
    //   let entityValueId = params['id'];
    //   if (entityValueId) {
    //     this.router.navigate(["ui/" + this.entityName + "/edit/" + entityValueId], { queryParams: { subType: this.layoutSubType } });
    //   }
    // });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
