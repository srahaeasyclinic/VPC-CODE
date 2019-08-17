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
import { error, stringify } from '@angular/compiler/src/util';
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
import { generateRandomNo } from '../model/treeNode';

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
  private entityName: string = 'ApplicationMenuGroup';
  public gridData: any = this.menuList;
  private name: string;
  private defaultLayout: LayoutModel = new LayoutModel();
  private orderBy: string = '';
  private sort: SortDescriptor[];
  private selectedFields: string = '';
  private results: any;
  private totalRecords: number = 0;
  private pageindex: number = 1;
  private pageSize: number = this.commonService.defaultPageSize();
  public menuGroupList: NewMenuItem[];
  public menuType: string = "";
  public referenceEntity: any;
  public layoutlist: any;
  private menuItemModel = new NewMenuItem();
  public menuItemName: string = "";
  public menugroupId: string = "";
  public menuSubgroupId: string = null;
  public menuTypeId: number;
  public referenceEntityId: string = "";
  public layoutId: string = ""
  public actionTypeId: number = 0;
  public menuId: string = "";
  public addMenuLabel: string = "";
  public wellKnownLink: string = "";
  public editUpdate: string = "";
  public menuItemSort: number = -0;
  public displayRule: any;
  public menuSubGroupList: any[];
  public ItemIcon: string = "";
  public menucode: string = "";
  //private parentmenugroup: NewMenuItem;
  

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
    
    this.resource = this.globalResourceService.getGlobalResources();//data;
    this.getDefaultLayout(this.entityName);
    this.getMenuList();
     this.getMenuGroup("maingroup");
    
  }


  private getMenuList(): void {
    this.menuService.getMenuList()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
           // this.lastRowIndex = data.length;
            this.gridData = data.filter(w => w.isMenuGroup != true);
            this.lastRowIndex = this.gridData.length;
            //console.log('data ',JSON.stringify(data));
            
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

    this.globalResourceService.openDeleteModal.emit()


    this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
      this.menuService.deleteMenu(data.id).subscribe(result => {
        if (result) {
          this.getMenuList();
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
    //       this.menuService.deleteMenu(data.id).subscribe(result => {
    //         if (result) {
    //           this.getMenuList();
    //         }
    //       });

    //     } else {
    //       //write the code for cancel click
    //     }

    //   });
  }

  public addMenuPopup(menu): void {
    this.menuId = "";
     
   // this.menuSubgroupId = "";

    this.addMenuLabel = this.getResourceValue("menuitem_label_add");
    this.editUpdate = this.getResourceValue("menuitem_operation_update");
    this.clearModal();
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(menu, ngbModalOptions);
  }

  private editMenuPopup(menu, id): void {
    
   // this.menuSubgroupId = "";
    this.editUpdate = this.getResourceValue("menuitem_operation_update");
    this.menuService.getMenuById(id)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.menuId = id;
            this.menuItemName = data.name;

             this.getMenuGroup("maingroup");

           
            this.setEditpopup(data.parentId);
            
            this.menuSubgroupId = data.parentId;
            
            this.menuTypeId = data.menuTypeId;
           
            this.menuItemSort = data.sortItem;
            this.ItemIcon = data.menuIcon;
            this.menucode = data.menucode;

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

            this.addMenuLabel = this.getResourceValue("menuitem_label_edit");

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
          this.toster.showSuccess(this.globalResourceService.updateSuccessMessage("applicationmenugroup_displayname"));
        },
        error => {

          console.log(error);
        });
  }

  public saveMenu(): void {

    let errorMessage: string = "";
    let mainsubgroup = (this.menuSubGroupList!=undefined && this.menuSubGroupList!=null)?this.menuSubGroupList.find(w => w.id == this.menuSubgroupId): undefined;
   
    if (this.menugroupId == "" || this.menugroupId == null || this.menugroupId == "00000000-0000-0000-0000-000000000000") {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_menugroup") + "<br/>";
    }
    if (this.menuSubgroupId === "" || this.menuSubgroupId == null || this.menuSubgroupId == "00000000-0000-0000-0000-000000000000" || mainsubgroup==undefined) {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_subgroup") + "<br/>";
   }
   
    if (this.menuItemName === "") {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_name") + "<br/>";
    }
    if (this.menuTypeId <= 0) {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_type") + "<br/>";
    }
    else if (this.menuTypeId > 0) {
      if (this.menuTypeId === 3) {
        if (this.actionTypeId <= 0) {
          errorMessage += this.globalResourceService.requiredValidator("menuitem_field_actiontype") + "<br/>";
        }
      }
      else if (this.menuTypeId === 4 && this.wellKnownLink === "") {
        
          errorMessage += this.globalResourceService.requiredValidator("menuitem_field_path") + "<br/>";
        
      }
      else {
        if (this.menuTypeId != 4 && this.referenceEntityId === "") {
          errorMessage += this.globalResourceService.requiredValidator("menuitem_field_entityreference") + "<br/>";
        }

        if ( this.menuTypeId != 4 && this.layoutId === "") {
          errorMessage += this.globalResourceService.requiredValidator("menuitem_field_layout") + "<br/>";
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
   let maingroup = this.menuGroupList.find(w => w.id == this.menugroupId);
   

  //  console.log("this.menuSubGroupList", this.menuSubGroupList);
  //  console.log("this.menuGroupList", this.menuGroupList);

  //  console.log("this.menuSubgroupId", this.menuSubgroupId);
  // console.log("this.menuSubgroupId", this.menuSubgroupId);
   
    this.menuItemModel.name = this.menuItemName;
    this.menuItemModel.groupId = maingroup.groupId;
    this.menuItemModel.menuTypeId = this.menuTypeId;
   this.menuItemModel.actionTypeId = this.actionTypeId;
   this.menuItemModel.parentId = this.menuSubgroupId;
   this.menuItemModel.isMenuGroup = false;
   this.menuItemModel.sortItem = this.menuItemSort;
   
   this.menuItemModel.menucode = "menu_" + this.commonService.getTrimmenuStr(maingroup.name).toLowerCase() +
     "_" + this.commonService.getTrimmenuStr(mainsubgroup.name).toLowerCase() +
     "_" + this.commonService.getTrimmenuStr(this.menuItemName).toLowerCase();
    
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
          this.toster.showSuccess(this.globalResourceService.updateSuccessMessage("menuitem_displayname"));
          this.getMenuList();
        }
      });
    }
    else {
      this.menuService.saveMenuItem(this.entityName, this.menuItemModel).subscribe(result => {
        if (result) {
          this.clearModal();
          this.modalReference.close();
          this.toster.showSuccess(this.globalResourceService.createSuccessMessage("menuitem_displayname"));
          this.getMenuList();
        }
      });
    }

  }


  private getMenuGroup(type:string): void {
    let menulist;
    this.menuService.getMenuList()
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            menulist = data.filter(w => w.isMenuGroup == true);
            
            if (type.toLowerCase() == "maingroup")
            {
              
              this.menuGroupList = menulist.filter(w => w.parentId == "00000000-0000-0000-0000-000000000000");
              
            } 
            
            if (type.toLowerCase() == "subgroup")
            {
              this.menuSubGroupList = menulist.filter(w => w.parentId == this.menugroupId);
              
            } else {
              this.menuSubGroupList = [];
            }
            
            
          }
        },
        error => {
          console.log(error)
        }
    );

 }

  private setEditpopup(id:string):void
  {
    this.menuService.getMenuById(id)
      .pipe(first())
      .subscribe(
        data => {
          if (data!=null && data.id!="00000000-0000-0000-0000-000000000000") {
          //     console.log('id',id);
          //   console.log('setEditpopup(id:string):void',data);
          //  // this.parentmenugroup = data;
            this.menugroupId = data.parentId;
            this.onGroupChange(this.menugroupId);
          } else if(this.menuGroupList!=undefined && this.menuGroupList.length>0)
          {
            this.onGroupChange(this.menuGroupList[0].id);
          }
            
      });
   
}


  private onMenuTypeChange(event): void {
    if (event) {
      this.menuTypeId = Number(event);
      this.referenceEntity = [];

      //entity
      if (this.menuTypeId === 1) {
        this.menuType = "Entity";
        this.metadataService.getEntities("primaryentity")
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

  private onGroupChange(event): void { 
    if (event) { 
      this.getMenuGroup("subgroup");
    }

  }

  private clearModal(): void {
    this.menuItemName = "";
   // this.menugroupId = "";
    this.menuTypeId = 0;
    this.referenceEntityId = "";
    this.actionTypeId = 0;
    this.layoutId = "";
    this.menugroupId = null;
    this.menuItemSort = 0;
    this.menuSubGroupList = null;
    this.menucode = "";
  }

  private refreshMenuItemModel(): void {
    this.menuItemModel.name = "";
    this.menuItemModel.groupId = "";
    this.menuItemModel.menuTypeId = null;
    this.menuItemModel.actionTypeId = null;
    this.menuItemModel.referenceEntityId = "";
    this.menuItemModel.layoutId = null;
    this.menuItemModel.wellKnownLink = "";
    this.menuItemModel.sortItem = 0;
    this.menuItemModel.parentId = null;
     this.menucode = "";

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
