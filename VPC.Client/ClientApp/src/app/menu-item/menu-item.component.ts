import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ResourceService } from '../services/resource.service';
import { first } from 'rxjs/operators';
import { PicklistUiService } from "../picklist-ui/picklist-ui.service";
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { CommonService } from 'src/app/services/common.service';
import { TosterService } from 'src/app/services/toster.service';
import { error } from '@angular/compiler/src/util';
import { MenuService } from '../services/menu.service';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import swal from 'sweetalert2';
import { from } from 'rxjs';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor, GroupDescriptor } from '@progress/kendo-data-query';
import { MenuGroup } from '../model/menuGroup';
import { MetadataService } from '../meta-data/metadata.service';
import { PicklistService } from '../picklist/picklist.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.css']
})
export class MenuItemComponent implements OnInit {
  private modalReference: any;
  //menuGroupForm: FormGroup;
  public submitted = false;
  public lastRowIndex: number;
  MenuGroup: string;
  public resource: Resource;
  public tree: ITreeNode;
  public isTreeReady: boolean = false;
  private layoutType: number = 2;
  private layoutContext: number = 1;
  public view: Observable<GridDataResult>;
  private menuList: MenuItem[];
  private entityName: string = 'MenuGroup';
  public gridData: any = this.menuList;
  private name: string;
  private defaultLayout: LayoutModel = new LayoutModel();
  private orderBy: string = '';
  private sort: SortDescriptor[];
  private selectedFields: string = '';
  private results: any;
  private totalRecords: number = 0;
  private pageindex: number = 1;
  private pageSize: number = 10;
  public menuGroupList: MenuGroup[];
  public menuType: string = "";
  public referenceEntity: any;
  public layoutlist: any;
  private menuItemModel = new NewMenuItem();
  public menuItemName: string = "";
  public groupId: string = "";
  public menuTypeId: number;
  public referenceEntityId: string = "";
  public layoutId: string = ""
  public actionTypeId: number = 0;
  public menuId: string = "";
  public addMenuLabel: string = "";
  public wellKnownLink: string = "";
  public editUpdate: string = "";

  public displayRule: any;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private modalService: NgbModal,
    private resourceService: ResourceService,
    private picklistUiService: PicklistUiService,
    //private formBuilder: FormBuilder,
    private commonService: CommonService,
    private toster: TosterService,
    public menuService: MenuService,
    private metadataService: MetadataService,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {
    //   this.menuGroupForm = this.formBuilder.group({
    //     groupName: ['', Validators.required],     
    // });
    this.getResource();

  }

  private getResource(): void {
    // this.resourceService.getResources()
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data) {
    this.resource = this.globalResourceService.getGlobalResources();//data;
    this.getDefaultLayout(this.entityName);
    this.getMenuList();
    this.getMenuGroup(this.entityName);
    //this.getRuleList(this.entityName);
    //     }
    //   },
    //   error => {
    //     console.log(error)
    //   }
    // );
  }


  private getMenuList(): void {
    this.menuService.getMenuList()
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.lastRowIndex = data.length;
            this.gridData = data;
          }
        },
        error => {
          console.log(error)
        }
      );
  }


  public goToMetaDataDetails(name): void {
    this.router.navigate(['/menu', name.toLowerCase()]);
  }


  private deleteMenu(data): void {
    swal({
      title: this.getResourceValue("Areyousure"),
      text: this.getResourceValue("Youwntbeabletorevertthis"),
      type: this.getResourceValue('warning'),
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: this.getResourceValue('Yesdeleteit'),
      showLoaderOnConfirm: true,
    })
      .then((willDelete) => {
        if (willDelete.value) {
          this.menuService.deleteMenu(data.id).subscribe(result => {
            if (result) {
              this.getMenuList();
            }
          });

        } else {
          //write the code for cancel click
        }

      });
  }

  public addMenuPopup(menu): void {
    this.addMenuLabel = this.getResourceValue("AddMenu");
    this.editUpdate = this.getResourceValue("Save");
    this.clearModal();
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(menu, ngbModalOptions);
  }

  private editMenuPopup(menu, id): void {
    this.editUpdate = this.getResourceValue("Update");
    this.menuService.getMenuById(id)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.menuId = id;
            this.menuItemName = data.name;
            this.groupId = data.groupId;
            this.menuTypeId = data.menuTypeId;
            if (this.menuTypeId) {
              this.onMenuTypeChange(this.menuTypeId);
            }
            this.referenceEntityId = data.referenceEntityId;
            if (this.referenceEntityId) {
              this.onReferenceEntityChange(this.referenceEntityId);
            }
            this.layoutId = data.layoutId;
            this.actionTypeId = data.actionTypeId;
            this.wellKnownLink = data.wellKnownLink;

            this.addMenuLabel = this.getResourceValue("EditMenu");

            let ngbModalOptions: NgbModalOptions = {
              backdrop: 'static',
              keyboard: false
            };
            this.modalReference = this.modalService.open(menu, ngbModalOptions);
          }
        },
        error => {
          console.log(error);
        });
  }

  private getDefaultLayout(entityName): void {
    this.picklistUiService.getDefaultLayout(entityName, this.layoutType, this.layoutContext)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.tree = data.formLayoutDetails;
            this.isTreeReady = true;
          }
        },
        error => {
          console.log(error);
        });
  }

  public addMenuGroupPopup(menuGroup): void {

    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(menuGroup, ngbModalOptions);
  }

  public savePicklistValue(): void {
    //let value = { Key: '', Text: '' };
    let value = {};
    let errormessage: string = "";
    value = this.commonService.createKeyValue(this.tree.fields, value);

    // if (value.Key === "") {
    //   errormessage += "Key is required.<br/>"
    // }

    // if (value.Text === "") {
    //   errormessage += "Text is required.<br/>"
    // }

    // if (errormessage != "") {
    //   this.toster.showError(errormessage);
    //   return;
    // }

    this.picklistUiService.savePicklistValues(this.entityName, value)
      .pipe(first())
      .subscribe(
        data => {
          this.modalReference.close();
          this.toster.showSuccess(this.entityName + ' ' + this.getResourceValue("SavedSuccessfully"));
        },
        error => {

          console.log(error);
        });
  }

  public saveMenu(): void {

    let errorMessage: string = "";

    if (this.menuItemName === "") {
      errorMessage += this.getResourceValue("NameIsRequired") + "<br/>";
    }

    if (this.groupId === "") {
      errorMessage += this.getResourceValue("GroupIsRequired") + "<br/>";
    }

    if (this.menuTypeId <= 0) {
      errorMessage += this.getResourceValue("MenuTypeIsRequired") + "<br/>";
    }
    else if (this.menuTypeId > 0) {
      if (this.menuTypeId === 3) {
        if (this.actionTypeId <= 0) {
          errorMessage += this.getResourceValue("ActionTypeIsRequired") + "<br/>";
        }
      }
      else if (this.menuTypeId === 4) {
        if (this.wellKnownLink === "") {
          errorMessage += this.getResourceValue("WellknownIsRequired") + "<br/>";
        }
      }
      else {
        if (this.referenceEntityId === "") {
          errorMessage += this.getResourceValue("ReferenceEntityIsRequired") + "<br/>";
        }

        if (this.layoutId === "") {
          errorMessage += this.getResourceValue("LayoutIsRequired") + "<br/>";
        }
      }
    }

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }

    this.refreshMenuItemModel();

    // this.menuItemModel.name = this.menuItemName;
    // this.menuItemModel.groupId = this.groupId;
    // this.menuItemModel.menuTypeId = this.menuTypeId;
    // this.menuItemModel.referenceEntityId = this.referenceEntityId;
    // this.menuItemModel.layoutId = this.layoutId;
    // this.menuItemModel.actionTypeId = this.actionTypeId;
    // this.menuItemModel.wellKnownLink = this.wellKnownLink;

    this.menuItemModel.name = this.menuItemName;
    this.menuItemModel.groupId = this.groupId;
    this.menuItemModel.menuTypeId = this.menuTypeId;
    this.menuItemModel.actionTypeId = this.actionTypeId;

    if (this.menuTypeId === 3 || this.menuTypeId === 4) {
      this.menuItemModel.wellKnownLink = this.wellKnownLink;
      //this.menuItemModel.referenceEntityId = this.referenceEntityId;
      this.menuItemModel.layoutId = "00000000-0000-0000-0000-000000000000";
    }
    else {

      this.menuItemModel.referenceEntityId = this.referenceEntityId;
      this.menuItemModel.layoutId = this.layoutId;

    }

    if (this.menuId && this.menuId !== "") {
      this.menuItemModel.id = this.menuId;

      this.menuService.updateMenuItem(this.entityName, this.menuId, this.menuItemModel).subscribe(result => {
        if (result) {
          this.clearModal();
          this.modalReference.close();
          this.toster.showSuccess(this.getResourceValue('MenuUpdatedSuccessfully'));
          this.getMenuList();
        }
      });
    }
    else {
      this.menuService.saveMenuItem(this.entityName, this.menuItemModel).subscribe(result => {
        if (result) {
          this.clearModal();
          this.modalReference.close();
          this.toster.showSuccess(this.getResourceValue('MenuSavedSuccessfully'));
          this.getMenuList();
        }
      });
    }

  }

  private getMenuGroup(entityName: string): void {
    this.picklistUiService.getDefaultLayout(entityName, 3, 0)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.defaultLayout = data;
            if (this.defaultLayout) {

              //generate the default orderby
              if (this.defaultLayout.listLayoutDetails) {
                if (this.defaultLayout.listLayoutDetails.defaultSortOrder) {

                  this.orderBy = this.defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
                  //{dir: "asc", field: "text"}
                  //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
                  if (!this.sort)
                    this.sort = [];

                  this.sort.length = 0;
                  this.sort.push({ dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
                }
              }
              this.generateListlayout(this.defaultLayout, entityName);
            }
          }
        },
        error => {
          console.log(error);
        });
  }

  private generateListlayout(layout: LayoutModel, entityName: string): void {

    let isvalid: boolean = true;

    let filters: string = '';

    let maxResult: number;

    if (layout.listLayoutDetails) {

      maxResult = layout.listLayoutDetails.maxResult;

      if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
        layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
        this.selectedFields = '';
        layout.listLayoutDetails.fields.forEach((item, index) => {
          if (!this.selectedFields) {
            this.selectedFields = item.name;
          }
          else {
            this.selectedFields += ',' + item.name;
          }
        });

      } else {
        isvalid = false;
        this.toster.showWarning(this.getResourceValue('NoFieldsFound'));
      }

      if (layout.listLayoutDetails.searchProperties && layout.listLayoutDetails.searchProperties.length > 0) {
        layout.listLayoutDetails.searchProperties.forEach(element => {
          element.properties.forEach(prop => {
            if (prop.value != null) {
              filters += prop.name + ',' + prop.value + '|';
            }
          });
        });

        if (filters != "") {
          filters = filters.substring(0, filters.length - 1);
        }
      }

      if (isvalid) {
        this.menuGroupList = [];
        this.totalRecords = 0;

        this.picklistUiService.getPicklistValues(entityName, this.selectedFields, filters, this.pageindex, this.pageSize, maxResult, this.orderBy, '')

          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
                this.menuGroupList = data.result;

                //below values are requred for kendo grid dynamic paging
                this.totalRecords = data.totalRow;





              }
            },
            error => {
              console.log(error);
            });
      }
    } else {
      //No layout found
      this.menuGroupList = [];
      this.totalRecords = 0;
    }
  }

  private onMenuTypeChange(event): void {
    if (event) {
      this.menuTypeId = Number(event);
      this.referenceEntity = [];

      //entity
      if (this.menuTypeId === 1) {
        this.menuType = "Entity";
        this.metadataService.getEntities()
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                //this.referenceEntity = data;
                data.forEach(item => {
                  if (item.type === "PrimaryEntity") {
                    this.referenceEntity.push(item);
                  }
                });
              }
            },
            error => {
              console.log(error);
            });
      }
      //picklist
      else if (this.menuTypeId === 2) {
        this.menuType = "Picklist";
        this.picklistService.getPicklists()
          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
                this.referenceEntity = data;
              }
            },
            error => {
              console.log(error);
            });
      }
    }
  }

  private onReferenceEntityChange(event): void {
    this.layoutlist = [];
    let a = this.menuType;
    this.referenceEntityId = event;
    this.menuService.getLayoutList(event, this.menuTypeId)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutlist = data;
          }
        },
        error => {
          console.log(error);
        });
  }

  private clearModal(): void {
    this.menuItemName = "";
    this.groupId = "";
    this.menuTypeId = 0;
    this.referenceEntityId = "";
    this.actionTypeId = 0;
    this.layoutId = "";
  }

  private refreshMenuItemModel(): void {
    this.menuItemModel.name = "";
    this.menuItemModel.groupId = "";
    this.menuItemModel.menuTypeId = null;
    this.menuItemModel.actionTypeId = null;
    this.menuItemModel.referenceEntityId = "";
    this.menuItemModel.layoutId = null;
    this.menuItemModel.wellKnownLink = "";
  }

  moveUpAndDown(data, currentIndex, nextIndex) {
    if (currentIndex !== -1) {
      this.gridData.splice(currentIndex, 1);
      this.gridData.splice(nextIndex, 0, data);
      this.gridData = this.gridData.slice(0);
    }
  }
  
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
