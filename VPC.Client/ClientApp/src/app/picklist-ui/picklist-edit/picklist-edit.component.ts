import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../model/layoutmodel';
import { PicklistUiService } from '../picklist-ui.service';
import { TosterService } from '../../services/toster.service';
import { CommonService } from '../../services/common.service';
import { ResourceService } from '../../services/resource.service';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { Data } from '../../services/storage.data';
import { GlobalResourceService } from '../../global-resource/global-resource.service';

@Component({
  selector: 'app-picklist-edit',
  templateUrl: './picklist-edit.component.html',
  styleUrls: ['./picklist-edit.component.css']
})
export class PicklistEditComponent implements OnInit {

  constructor(private router: Router, private picklistService: PicklistUiService,
    private toster: TosterService, private resourceService: ResourceService,
    private commonService: CommonService, private route: ActivatedRoute, private data: Data,
    private globalResourceService: GlobalResourceService,


    ) { }

  private entityName: string = '';
  private entityId: string = '';
  private resource: any;
  private layoutType: number = 2; // Form page
  private layoutContext: number = 2; // Edit
  public tree: ITreeNode;
  //public defaultLayout: LayoutModel = new LayoutModel();
  public defaultLayout: LayoutModel;
  public picklistValue: any;
  public isTreeReady: boolean = false;
  public isPreviousIdAvailable: boolean = false;
  public isNexIdAvailable: boolean = false;
  public transitObject: any;
  private doCheck: boolean;

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    // this.getResource();
    // Get the picklist entity name from URL route
    this.route.params.subscribe(params => {
      this.entityId = params["id"];
    })

    this.route.parent.params.subscribe((urlPath) => {
      // this.entityId = urlPath[urlPath.length - 1].path;
      this.entityName = urlPath["name"];     
    });

    this.getPicklist();

  }

  private loadRouteData(): void {
    if (!this.doCheck) {
      this.doCheck = true;

      this.route.params.subscribe(params => {
        this.entityId = params["id"];
      })

      // this.route.parent.params.subscribe((urlPath) => {
      //   this.entityName = urlPath["name"];
      // });

      this.getPicklist();

    }
  }

  private getPicklist() {
    if (this.data) {
      //Get the data for the previous next operation
      this.transitObject = this.data.storage;

      let recordNo : number = 0;
      let currentPage : number = 0;
      currentPage = this.transitObject.pageIndex - 1;
    
      if (!this.transitObject.recordNo) {
        recordNo = (currentPage * 10) + (this.transitObject.itemIndex) + 1;
        this.transitObject.recordNo = recordNo;
      }
      else {
        recordNo = this.transitObject.recordNo;
      }

      this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
      this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));
    }

    if (this.entityName && this.entityId) {
      this.getDefaultLayout(this.entityName);

    } else {
      this.toster.showWarning(this.getResourceValue("UrlTemperedorNoEntityNameoridFoundorEntityNotYetDecorated"));
    }
  }

  // Events section
  public updatePicklistValue(): void {
    // var value = {};
    // this.createKeyValue(this.tree.fields, value);

    let value = {};
    value = this.commonService.createKeyValue(this.tree.fields, value);

    this.picklistService.updatePicklistValues(this.entityName, value, this.entityId)
      .pipe(first())
      .subscribe(
        data => {
          this.toster.showSuccess(this.getResourceValue("UpdatedSuccessfully"));
          this.router.navigate(["../../"], { relativeTo: this.route });
        },
        error => {
          console.log(error);
        });
  }


  public nextData(): void {
    if (this.transitObject && this.transitObject.fields && this.transitObject.orderBy) {

      this.transitObject.recordNo = this.transitObject.recordNo + 1;
      this.transitObject.nextClick = true;

      let pagesize: number = 1;
      let maxresult: number = 1;

      this.picklistService.getPicklistValues(this.entityName, this.transitObject.fields, this.transitObject.filters, this.transitObject.recordNo, pagesize, maxresult, this.transitObject.orderBy, this.transitObject.freetextsearch)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result[0]) {
                this.entityId = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));

                this.data.storage = this.transitObject;
                //this.router.navigate(["picklist/ui/" + this.entityName + "/edit/" + this.entityId]);
                this.doCheck = false;
                this.router.navigate(["../", this.entityId], { relativeTo: this.route });
                this.loadRouteData();

              }

            }
          },
          error => {
            console.log(error);
          });
    }
  }

  public previousData(): void {
    if (this.transitObject && this.transitObject.fields && this.transitObject.orderBy) {

      this.transitObject.recordNo = this.transitObject.recordNo - 1;
      this.transitObject.previousClick = true;

      let pagesize: number = 1;
      let maxresult: number = 1;

      this.picklistService.getPicklistValues(this.entityName, this.transitObject.fields, this.transitObject.filters, this.transitObject.recordNo, pagesize, maxresult, this.transitObject.orderBy, this.transitObject.freetextsearch)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              if (data && data.result[0]) {
                this.entityId = data.result[0].internalId;

                this.isPreviousIdAvailable = (this.transitObject && (this.transitObject.recordNo > 1));
                this.isNexIdAvailable = (this.transitObject && this.transitObject.itemIndex >= 0 && (this.transitObject.recordNo < this.transitObject.totalRecords));
                //this.getDefaultLayout(this.entityName);

                this.data.storage = this.transitObject;
                // this.router.navigate(["picklist/ui/" + this.entityName + "/edit/" + this.entityId]);
                this.doCheck = false;
                this.router.navigate(["../", this.entityId], { relativeTo: this.route });
                this.loadRouteData();
              }

            }
          },
          error => {
            console.log(error);
          });


    }
  }


  //Methods section
  // private getResource(): void {
  //   this.resourceService.getResources()
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.resource = data;
  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }

  private getDefaultLayout(entityName: string): void {
    this.picklistService.getDefaultLayout(entityName, this.layoutType, this.layoutContext)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.defaultLayout = new LayoutModel();
            this.defaultLayout = data;
            if (this.defaultLayout) {
              this.getPicklistValueById(entityName, this.entityId);
            }
          }
        },
        error => {
          console.log(error);
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
        this.mapData(item.fields, whichObject)
      }
    });
  }

  private getPicklistValueById(entityName: string, entityId: string): void {
    this.picklistService.getPicklistValueById(entityName, entityId)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.picklistValue = data;
            if (this.picklistValue && this.defaultLayout && this.defaultLayout.formLayoutDetails) {
              this.mapData(this.defaultLayout.formLayoutDetails.fields, this.picklistValue);
              this.tree = this.defaultLayout.formLayoutDetails;
              this.isTreeReady = true;
            }

          }
        },
        error => {
          console.log(error);
        }
      );

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
