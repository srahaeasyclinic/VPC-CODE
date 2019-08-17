import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ResourceService } from '../resource/resource.service';
import { CommonService } from '../services/common.service';
import { TosterService } from '../services/toster.service';
import { MenuService } from '../services/menu.service';
import { MetadataService } from '../meta-data/metadata.service';
import { PicklistService } from '../picklist/picklist.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { first } from 'rxjs/operators';
import {  NewMenuItem } from '../model/menuItem';
import { Resource } from '../model/resource';
import { template } from '@angular/core/src/render3';
import { MenuGroup } from '../model/menuGroup';
import { LayoutModel } from '../model/layoutmodel';
import { SortDescriptor } from '@progress/kendo-data-query';
import { PicklistUiService } from '../picklist-ui/picklist-ui.service';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Rx';
import swal from 'sweetalert2';

@Component({
  selector: 'app-menu-group',
  templateUrl: './menu-group.component.html',
  styleUrls: ['./menu-group.component.css']
})
export class MenuGroupComponent implements OnInit {
  
  public gridData: NewMenuItem[];
  public resource: Resource;
  //public lastRowIndex: number;
  public menugroupModel: NewMenuItem;
  // public menuId: string;
  public addMenuLabel: any;
  public editUpdate: any;
  private modalReference: any;
  public menuGroupList: any[];
  private orderBy: string = '';
  private sort: SortDescriptor[];
  private parentmenuGroupList: NewMenuItem[];
  public view: Observable<GridDataResult>;
  public showsection = true;
  private menucode: string="";
  public parentmenu: string;
  
  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private modalService: NgbModal,
    private resourceService: ResourceService,
    private commonService: CommonService,
    private toster: TosterService,
    public menuService: MenuService,
    private metadataService: MetadataService,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService,  private picklistUiService: PicklistUiService) { }

  ngOnInit() {

    this.getResource();
  }


private getResource(): void {
  this.resource = this.globalResourceService.getGlobalResources();
  this.getMenuList();
  this.getMenuGroup();

  
}
  
///////////////////////// All methods////////////////////////////////////////////
private getMenuList(): void {
    this.menuService.getMenuList()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.parentmenuGroupList = data.filter(w=>w.isMenuGroup==true && w.parentId=="00000000-0000-0000-0000-000000000000");
           // this.lastRowIndex = data.length;
            this.mapGriddata(data);
           
          }
        },
        error => {
          console.log(error)
        }
      );
  }

  mapGriddata(data:any[])
  {
    let tempobject = data.filter(w=>w.isMenuGroup==true && w.isMenuGroup!=null);
    data.filter(w=>w.ismenugroup==true && w.isMenuGroup!=null).forEach(element => {
      if (element.parentId!=null && element.parentId!="00000000-0000-0000-0000-000000000000")
      {
        let parentname = tempobject.find(e => e.id = element.parentId);
        if (parentname != undefined && parentname != null)
        {
          element.parentId = parentname.name;
        }
        
       }
      
    });

    this.gridData = data.filter(w => w.isMenuGroup == true);
    this.gridData.forEach(element => {
      if (element.parentId == "00000000-0000-0000-0000-000000000000")
      {
        element.parentId = "";
      } else {
        let parentmenuname = data.find(w => w.id == element.parentId);
        if (parentmenuname != undefined && parentmenuname != null)
        {
          element.parentId = parentmenuname.name;
        }
      }
    });

  }
  public addMenuPopup(menu): void {
    this.showsection = true;
    this.menugroupModel = new NewMenuItem();
    this.menugroupModel.groupId = (this.menuGroupList != null && this.menuGroupList.length > 1) ? this.menuGroupList[0].internalId : "";
   // this.menugroupModel.parentId = "";
    this.addMenuLabel = this.getResourceValue("menuitem_label_add");
    this.editUpdate = this.getResourceValue("menuitem_operation_update");

    this.menugroupModel.parentId="00000000-0000-0000-0000-000000000000";
    this.parentmenu = this.menugroupModel.parentId;

    this.clearModal();
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(menu, ngbModalOptions);
  }

private editMenuPopup(menu, id): void {
    this.editUpdate = this.getResourceValue("menuitem_operation_update");
    this.menuService.getMenuById(id)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {

            this.menugroupModel = data;
            if (this.menugroupModel!=null && this.menugroupModel!=undefined && this.menugroupModel.parentId!=null && this.menugroupModel.parentId!="00000000-0000-0000-0000-000000000000")
            {
              this.showsection = false;
            } else {
              this.showsection = true;
            }
            this.parentmenu = this.menugroupModel.parentId;
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

  private clearModal(): void {
    this.menugroupModel = new NewMenuItem();
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

   private generateListlayout(layout: LayoutModel, entityName: string): void {

    let isvalid: boolean = true;

    let filters: string = '';

  let maxResult: number;
  let selectedFields = '';

    if (layout.listLayoutDetails) {

      maxResult = layout.listLayoutDetails.maxResult;

      if (layout.listLayoutDetails.fields || layout.listLayoutDetails.fields.length > 0) {
        layout.listLayoutDetails.fields = layout.listLayoutDetails.fields.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
     
        layout.listLayoutDetails.fields.forEach((item, index) => {
          if (!selectedFields) {
            selectedFields = item.name;
          }
          else {
            selectedFields += ',' + item.name;
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
       
        this.picklistUiService.getPicklistValues(entityName, selectedFields, filters, 1, 100, maxResult, this.orderBy, '')

          .pipe(first())
          .subscribe(
            data => {
              if (data && data) {
                this.menuGroupList = data.result;
              }
            },
            error => {
              console.log(error);
            });
      }
    } else {
      //No layout found
      this.menuGroupList = [];
    }
   }
  private getMenuGroup(entityName: string="ApplicationMenuGroup"): void {
    let defaultLayout:any;
    this.picklistUiService.getDefaultLayout(entityName, 3, 0)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
           let defaultLayout = data;
            if (defaultLayout) {

              //generate the default orderby
              if (defaultLayout.listLayoutDetails) {
                if (defaultLayout.listLayoutDetails.defaultSortOrder) {

                  this.orderBy = defaultLayout.listLayoutDetails.defaultSortOrder.name + ',' + defaultLayout.listLayoutDetails.defaultSortOrder.value.toUpperCase();
                  //{dir: "asc", field: "text"}
                  //var short:SortDescriptor=[{dir: this.defaultLayout.listLayoutDetails.defaultSortOrder.value.toLowerCase(), field: this.defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase()}];
                  if (!this.sort)
                    this.sort = [];

                  this.sort.length = 0;
                  this.sort.push({ dir: defaultLayout.listLayoutDetails.defaultSortOrder.value.toLocaleUpperCase() == 'ASC' ? 'asc' : 'desc', field: defaultLayout.listLayoutDetails.defaultSortOrder.name.toLowerCase() });
                }
              }
              this.generateListlayout(defaultLayout, entityName);
          
            }
          }
        },
        error => {
          console.log(error);
        });
  
  }
 public saveMenu(): void {

    let errorMessage: string = "";

    if (this.menugroupModel==null  || this.menugroupModel.name==undefined || this.menugroupModel.name === "") {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_title") + "<br/>";
    }
   if (this.showsection == true && (this.menugroupModel.groupId ===null ||this.menugroupModel.groupId ==="" || this.menugroupModel.groupId == undefined)) {
      errorMessage += this.globalResourceService.requiredValidator("menuitem_field_sectiongroup") + "<br/>";
   }
   
    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }
   
   let groupname = this.parentmenuGroupList.find(w => w.id == this.parentmenu);
   let virtualgroup = this.menuGroupList.find(w => w.internalId == this.menugroupModel.groupId);
   if (virtualgroup!=null && virtualgroup!=undefined)
   {
     this.menucode = virtualgroup.text;
   }
   if (groupname!=null && groupname!=undefined)
   {
     this.menucode = groupname.name;
     this.menugroupModel.groupId = groupname.groupId;
   }
   
   this.menugroupModel.parentId = this.parentmenu!="" && this.parentmenu!=null?this.parentmenu:"00000000-0000-0000-0000-000000000000";
   this.menugroupModel.isMenuGroup = true;
   
   let menuPrefixname = (this.menugroupModel.parentId !=null && this.menugroupModel.parentId != "00000000-0000-0000-0000-000000000000") ? "menusubgroup_" : "menugroup_";
   this.menugroupModel.menucode = menuPrefixname+
     this.commonService.getTrimmenuStr(this.menugroupModel.name).toLowerCase() + "_" +
     this.commonService.getTrimmenuStr(this.menucode).toLowerCase();
   
  //  console.log('this.menugroupModel',this.menugroupModel);
    if (this.menugroupModel && this.menugroupModel.id !== undefined && this.menugroupModel.id !== null) {
      this.menuService.updateMenuItem("MenuGroup",this.menugroupModel.id ,this.menugroupModel).subscribe(result => {
        if (result) {
          this.clearModal();
          this.modalReference.close();
          this.toster.showSuccess(this.globalResourceService.updateSuccessMessage('applicationmenugroup_displayname'));
          this.getMenuList();
        }
      });
    }
    else {
     
      this.menuService.saveMenuItem("MenuGroup", this.menugroupModel).subscribe(result => {
        if (result) {
          this.clearModal();
          this.modalReference.close();
          this.toster.showSuccess(this.globalResourceService.createSuccessMessage('applicationmenugroup_displayname'));
          this.getMenuList();
        }
      });
    }

 }
  private onGroupChange(event): void {
    
    if (event!=null && event!=0 && event!="00000000-0000-0000-0000-000000000000") { 
      
      this.showsection = false;
    } else {
      this.showsection = true;
    }

  }
  public deleteMenu(data): void {
    this.globalResourceService.openDeleteModal.emit()
    this.globalResourceService.notifyConfirmationDelete.subscribe(x=>{
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
 

}