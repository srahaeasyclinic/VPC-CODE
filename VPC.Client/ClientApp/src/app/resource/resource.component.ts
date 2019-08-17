import { Component, OnInit } from '@angular/core';
import { ResourceService } from './resource.service';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
//import { NewResource } from '../model/resource';
import { first } from 'rxjs/operators';
import swal from 'sweetalert2';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { TosterService } from 'src/app/services/toster.service';
import { Resource } from '../model/resource';
import { promise } from 'protractor';
import { resolve } from 'dns';
import { reject } from 'q';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { MenuService } from '../services/menu.service';
//Added by Tanmoy
import { MetadataService } from 'src/app/meta-data/metadata.service';
import { Entities } from '../model/entities';
import { CommonService } from '../services/common.service';
//End

export class Language {
  tenantId: string;
  internalId: string;
  key: string;
  text: string;
}
export class NewResource {
  id: string;
  key: string;
  language: string;
  languageName: string;
  value: string
}



@Component({
  selector: 'app-resource',
  templateUrl: './resource.component.html',
  styleUrls: ['./resource.component.css']
})
export class ResourceComponent implements OnInit {
  public resources: NewResource[] | null;
  private languages: Language[] | null;
  private defaultLanguage: any;
  private translate: boolean = false;
  private resource: NewResource;
  private newResource: any;
  public resourceList: NewResource[];
  public addUpdateLabel: string;
  private pageindex: number = 1;
  public pageSize: number = this.commonService.defaultPageSize();
  private totalRecords: number = 0;
  public gridDataResult: GridDataResult;
  public view: Observable<GridDataResult>;
  public gridData: any[] = [];
  public sortOrder: string;
  public newlanguage: Language;
  public skip = 0;
  public editUpdate: string = "";
  public globalresource: Resource;
  public isLoadFirst: boolean;
  //Added by Tanmoy
  private name: string;
  private entityList: Entities[];
  public entityLst: any = this.entityList;
  public entities: Entities;
  public mode: any;
  //End

  constructor(
    private commonService: CommonService,
    private metadataService: MetadataService, //Added by Tanmoy
    private resourceService: ResourceService,
    private modalService: NgbModal,
    private toster: TosterService,
    private globalResourceService: GlobalResourceService,
    public menuService: MenuService
  ) { }

  ngOnInit() {
    this.isLoadFirst = false;
    this.globalresource = this.globalResourceService.getGlobalResources();
    this.translate = false;
    this.newResource = new NewResource();
    this.sortOrder = 'key'
    this.newlanguage = new Language();
    this.GetTenantLanguage();
    //Changed and added by Tanmoy
    //console.log('menuService.getCacheMenu().name ', this.menuService.getCacheMenu().name);    
    this.resetAndLoadEntity();
    this.mode = null;
    //End   
  }

  //Added by Tanmoy
  private resetAndLoadEntity() {
    this.entities = new Entities();
    this.entityList = null;
    this.getEntities();
  }
  //End

  private ngbModalOptions: NgbModalOptions = {
    backdrop: 'static',
    keyboard: false,
    size: 'lg',
  };
  private checkChange(event) {
    if (!event) {
      this.translate = false;
      this.newResource = new NewResource();
    }
    else {
      this.newResource = new NewResource();
      this.newResource.value = '';
    }
  }
  private getResource(): void {
    this.resourceService.getResources(this.defaultLanguage.key, this.sortOrder)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            //this.gridData=[];
            if (data.resources)
              this.resources = data.resources;
            else
              this.resources = data;
            if (this.isLoadFirst) {
              this.getGlobalResource();
            }
            // this.totalRecords = data.totalRow;
            // this.gridDataResult = { data: this.resources, total: this.totalRecords }
            // this.gridData.push(this.gridDataResult)
            //
          }
        },
        error => {
          // console.log(error)
          this.toster.showError(error.error.text);
        }
      );
  }
  private GetTenantLanguage() {
    this.resourceService.getDefaultLanguage()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            if (data.key == '') {
              this.toster.showWarning(this.getResourceValue('resource_validation_message_nodefaultlanguage'));
            }
            this.defaultLanguage = data;
            //console.log(this.defaultLanguage.key);
            this.getLanguage();
          }
        },
        error => {
          console.log(error);
          this.toster.showError(error.error.text);
        }
      );
  }

  //Added by Tanmoy
  private getEntities() {
    this.metadataService.getEntities("primaryentity")
      .pipe(first())
      .subscribe(
        data => {
          if (data != null) {
            this.entityLst = data;
          }
        },
        error => {
          console.log(error);
        });
  }
  //End

  private getLanguage(): void {
    this.resourceService.getLanguages()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.languages = data.result.filter(a => a.key != this.defaultLanguage.key);
            this.getResource()
          }
        },
        error => {
          console.log(error);
        }
      )
  }
  private getDefaultLanguage(resourceDetails, r: NewResource) {
    this.newlanguage = new Language();
    this.translate = false;
    this.getResourcesByKeyAndOtherThanDefaultLanguage(r.key, this.defaultLanguage.key);
    this.addUpdateLabel = this.getResourceValue("resource_label_edit");
    this.editUpdate = this.getResourceValue("resource_operation_update");
    this.resource = { ...this.resources.find(a => a.key == r.key && a.language == this.defaultLanguage.key) };
    //Added by Tanmoy
    this.mode = "edit";
    if (this.resource.key != null && this.resource.key != "") {
      var entityName = this.resource.key.substr(0, this.resource.key.indexOf('_'));
      this.entities = { ... this.entityLst.find(e => e.name == entityName) };
    }

    //End
    this.newResource = new NewResource();
    const modalRef = this.modalService.open(resourceDetails, this.ngbModalOptions);
  }

  public addResource(resourceDetails) {

    this.addUpdateLabel = this.getResourceValue("resource_label_add");
    this.editUpdate = this.getResourceValue("resource_operation_update");
    this.resource = new NewResource();
    this.resource.language = this.defaultLanguage.key;
    this.resource.languageName = this.defaultLanguage.text;
    this.translate = false;
    this.newResource = new NewResource();
    const modalRef = this.modalService.open(resourceDetails, this.ngbModalOptions);
  }

  private languageChange(event) {
    this.resourceService.getResourceByLanguageAndKey(this.resource.key, event.key)
      .pipe(first())
      .subscribe(
        data => {
          if (data.length > 0) {
            this.newResource = data[0];
            this.newlanguage.key = this.newResource.language;
            this.newResource.isActive = true;
          }
          else {
            this.newResource = new NewResource();
          }
        },
        error => {
          //console.log(error);
          this.toster.showError(error.error.text);
        }
      );
  }

  private async saveResource() {
    let errorMessage: string = "";


    if (this.resource.key === "" || this.resource.key === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("resource_field_key") + "</br>"
    }
    if (this.resource.value === "" || this.resource.value === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("resource_field_text") + "</br>"
    }
    if ((this.newResource.value != '' && this.newResource.value != undefined)) {
      if (this.newlanguage.key === null || this.newlanguage.key === undefined) {
        errorMessage += this.globalResourceService.requiredValidator("resource_field_language") + "</br>"
      }
      // if (this.newResource.value === null || this.newResource.value === undefined  ) {
      //   errorMessage +=this.globalresource[this.generateResourceName("Nameisrequired")]+"</br>"
      // }
    }

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }



    this.resourceList = [];
    if (this.newResource.value != null) {
      this.newResource.key = this.resource.key;
      this.newResource.language = this.newlanguage.key;
      //if (this.newResource.language === undefined) return;
      this.resourceList.push(this.newResource);
    }
    this.resourceList.push(this.resource);
    var flag: boolean = false;
    console.log(this.resourceList);
    for (var r of this.resourceList) {
      if (r.id != '' && r.id != undefined && (r.value.trim() != '')) {
        //For Update
        await new Promise((resolve, reject) => {
          this.resourceService.updateResource(r)
            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  flag = data;
                  this.isLoadFirst = true;
                  resolve();
                  //this.getResource();
                }
              },
              error => {
                //console.log('gg '+JSON.stringify(error));
                //this.toster.showError(error.error.text);
              }
            );
        });
      }
      else if ((r.id != '' || r.id != undefined) && (r.value.trim() == '')) {
        //For Delete
        await new Promise((resolve, reject) => {
          this.resourceService.deleteResource(r.id).subscribe(result => {
            if (result) {
              flag = true;
              this.isLoadFirst = true;
              resolve();
              //this.getResource();
            }
          });
        });
      }
      else if ((r.id == null || r.id == '') && (r.value.trim() != '')) {
        //For Save
        await new Promise((resolve, reject) => {
          this.resourceService.saveResource(r)
            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  flag = data;
                  this.isLoadFirst = true;
                  resolve();
                  // this.getResource();
                }
              },
              error => {
                //console.log('GG'+JSON.stringify(error));
                //this.toster.showError(error.error.text);
              }
            );
        });

      }
    }
    if (flag) {
      this.toster.showSuccess(this.globalResourceService.updateSuccessMessage('resource_displayname'));
    }
    // else{
    //   this.toster.showError('Resource has been saved successfully.');
    // }
    this.resetAndLoadEntity(); //Added by Tanmoy
    this.getResource();
    const modalRef = this.modalService.dismissAll();
  }
  public deleteResourceByKey(key: string) {
      this.globalResourceService.openDeleteModal.emit()
      this.globalResourceService.notifyConfirmationDelete.subscribe(x=>{

        this.resourceService.deleteResource(key).subscribe(result => {
          if (result) {
            this.isLoadFirst = true;
            this.getResource();
              //this.toster.showSuccess(this.globalResourceService.deleteSuccessMessage('resource_displayname'));
           
          }
        },
          error => {
            console.log(error.error.text);
            this.toster.showError(error.error.text);
          });
      });











    // swal({
    //   title: this.getResourceValue("common_message_areyousure"),
    //   text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
    //   type: ('warning'),
    //   showCancelButton: true,
    //   confirmButtonColor: '#3085d6',
    //   cancelButtonColor: '#d33',
    //   confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
    //   showLoaderOnConfirm: true,
    // })
    //   .then((willDelete) => {
    //     if (willDelete.value) {
    //       this.resourceService.deleteResource(key).subscribe(result => {
    //         if (result) {
    //           this.isLoadFirst = true;
    //           this.toster.showSuccess(this.getResourceValue('resource_operation_delete_success_message'));
    //           this.getResource();
    //         }
    //       },
    //         error => {
    //           console.log(error.error.text);
    //           this.toster.showError(error.error.text);
    //         });

    //     } else {
    //       //write the code for cancel click
    //     }

    //   });

  }



  private preDeleteResourceCheck(resource: NewResource): boolean {
    var isDeletable = true;
    if (resource.language == this.defaultLanguage.key) {
      if (this.resources.some(a => a.key == resource.key && a != resource)) {
        isDeletable = false;
      }
    }
    return isDeletable;
  }

  public pageChange(state: DataStateChangeEvent): void {
    this.skip = state.skip;
    if (state.skip == 0) {
      this.pageindex = 1;
    } else {
      this.pageindex = (state.skip / this.pageSize) + 1;
    }
    this.getResource();
  }

  public getResourcesByKeyAndOtherThanDefaultLanguage(key: string, defaultLanguage: string) {
    this.resourceService.getResourcesByAndOtherLanguage(key, defaultLanguage)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.translate = data.length > 0 ? true : false;
          }
        },
        error => {
          console.log(error);
        }
      )
  }

  public getGlobalResource() {
    this.resourceService.getAllResources('')
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.globalresource = data;
            this.isLoadFirst = false;
            this.globalResourceService.setGlobalresources(this.globalresource);
          }
        },
        error => {
          console.log(error)
        }
      );
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }


  public RepairResourcesList() {
    swal({
      title: this.getResourceValue("common_message_areyousure"),
      text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText:this.getResourceValue('task_cancel'),
      confirmButtonText: this.getResourceValue('common_task_yes'),
      showLoaderOnConfirm: true,
    })
      .then((willDelete) => {
        if (willDelete.value) {
          this.resourceService.RepairResourcesList()
            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  this.isLoadFirst = true;
                  this.getResource();
                  this.toster.showSuccess(this.globalResourceService.repairSuccessMessage('resource_displayname'));
                  //this.getResource()
                }
              },
              error => {
                console.log(error);
              });
        }
        else {
          //write the code for cancel click
        }
      });
  }

  public ResetResourcesList() {
    swal({
      title: this.getResourceValue("common_message_areyousure"),
      text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText:this.getResourceValue('task_cancel'),
      confirmButtonText: this.getResourceValue('common_task_yes'),
      showLoaderOnConfirm: true,
    })
      .then((willDelete) => {
        if (willDelete.value) {
          this.resourceService.ResetResourcesList()
            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  this.isLoadFirst = true;
                  this.getResource();
                  this.toster.showSuccess(this.globalResourceService.resetSuccessMessage('resource_displayname'));
                }
              },
              error => {
                console.log(error);
              });
        }
        else {
          //write the code for cancel click
        }
      });
  }

}
