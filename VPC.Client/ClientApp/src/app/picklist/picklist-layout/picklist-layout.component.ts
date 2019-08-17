
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from "@angular/router";
import { GridDataResult } from "@progress/kendo-angular-grid";
import { Observable } from "rxjs";
import { first } from "rxjs/operators";
import { LayoutModel } from "../../model/layoutmodel";
import { PicklistLayoutService } from "./picklist-layout.service";
import swal from 'sweetalert2';
import { TosterService } from '../../services/toster.service';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';
import { PicklistService } from '../../picklist/picklist.service';

@Component({
  selector: 'app-picklist-layout',
  templateUrl: './picklist-layout.component.html',
  styleUrls: ['./picklist-layout.component.css']
})
export class PicklistLayoutComponent implements OnInit {
  private closeResult: string;
  private layoutModel = new LayoutModel();
  layoutForm: FormGroup;
  public showSubType: boolean = false;
  public subTypes = new Array<SubType>();
  public subtypename: string;
  private id: number = 0;
  modalReference: any;
  private metaDataName: string;
  private layoutList: LayoutModel[];
  gridData: any = this.layoutList;
  public layoutName: string = "";
  public drpType: string = "";
  public drpContext: string = "";
  submitted = false;
  private samelayoutTypeForDefaultCount: number = 0;
  public resource: Resource;
  private oldLayoutId: string = "";
  public showContext: boolean = false;
  public cloneLayoutName:string = "";
  public drpCloneContext:string = "";
  
  layoutTypes = [
    new LayoutType(1, 'View'),
    new LayoutType(2, 'Form'),
    new LayoutType(3, 'List')
  ];

  contexts = [
    new Context(1, 'Add'),
    new Context(2, 'Edit')
  ];

  constructor(
    private modalService: NgbModal,
    private layoutService: PicklistLayoutService,
    private picklistService:PicklistService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private toster: TosterService,
    private globalResourceService: GlobalResourceService,
  ) { this.createForm(); }

  ngOnInit() {
    // this.route.parent.url.subscribe((urlPath) => {
    //   this.metaDataName = urlPath[urlPath.length - 1].path;
    //   // this.getLayouts(this.metaDataName);
    // });

    this.resource = this.globalResourceService.getGlobalResources();
    this.route.parent.parent.url.subscribe((urlPath) => {
      this.metaDataName = urlPath[0].path;
    });
    this.getLayouts(this.metaDataName);
  }


  private getLayouts(name) {
    this.layoutService.getLayouts(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            data.forEach((item, index) => {
              if (item.modifiedDate) {
                item.modifiedDate = new Date(item.modifiedDate);
              }
            });

            this.gridData = data;

          }

        },
        error => {
          console.log(error);
        });
  }


  private createForm() {
    this.layoutForm = this.formBuilder.group({
      layoutName: '',
      drpType: '',
      drpSubType: '',
      drpContext: ''
    });
  }

  openLayoutPopup(content) {
    this.createForm();
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(content, ngbModalOptions);

  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  onTypeChange(value) {
    if (value == "2") {
      this.showSubType = true;
    } else {
      this.showSubType = false;
    }
  }



  private saveLayout(content) {
    let errorMessage: string = "";

    if (this.layoutName === "") {
      errorMessage += this.globalResourceService.requiredValidator("metadata_field_name")  + "<br/>";
    }
    if (this.drpType === "") {
      errorMessage += this.globalResourceService.requiredValidator("metadata_field_type") + "<br/>";
    }
    else if (this.drpType === "2") {
      if (this.drpContext === "") {
        errorMessage += this.globalResourceService.requiredValidator("metadata_field_context") + "<br/>";
      }
    }

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }

    this.layoutModel.name = this.layoutName;
    this.layoutModel.layoutType = parseInt(this.drpType);
    // this.layoutModel.Subtype = (this.layoutForm.value.drpSubType == '') ? 0 : this.layoutForm.value.drpSubType;  

    //this.setDefaultLayoutByLayoutTypeCount(); //set default layout based on layout type if not exists

    this.layoutModel.context = (this.drpContext == '') ? 0 : parseInt(this.drpContext);

    this.layoutService.saveLayout(this.layoutModel, this.metaDataName).subscribe(result => {
      this.modalReference.close();
      this.toster.showSuccess(this.getResourceValue("metadata_operation_save_success_message"));

      this.id = result;
      var type = "";
      this.samelayoutTypeForDefaultCount = 0; //Reset the count

      if (Number(this.layoutModel.layoutType) === 1) {
        type = "view";
        //this.router.navigate(["picklist/" + this.metaDataName + "/layout/" + this.id + "/view" ]);
      }
      else if (Number(this.layoutModel.layoutType) === 2) {
        type = "form";
        //this.router.navigate(["picklist/" + this.metaDataName + "/layout/" + this.id + "/form" ]);
      }
      else if (Number(this.layoutModel.layoutType) === 3) {
        type = "list";
        //this.router.navigate(["picklist/" + this.metaDataName + "/layout/" + this.id + "/list" ]);
      }
      this.router.navigate(["./" + type.toLowerCase() + "/" + this.id], { relativeTo: this.route }).then(x=>
        {
          this.picklistService.showToolbar.emit()
        });
    });


  }

  private setDefaultLayoutByLayoutTypeCount() {

    var pickListLayoutObject = this.gridData;

    if (pickListLayoutObject != null) {
      //console.log(pickListLayoutObject);
      for (var i = 0; i < pickListLayoutObject.length; i++) {
        var layoutType = pickListLayoutObject[i].layoutType;
        var defaultLayout = pickListLayoutObject[i].defaultLayout;
        if (layoutType === parseInt(this.drpType) && defaultLayout == true) {
          this.samelayoutTypeForDefaultCount++;
        }
      }
    }

    if (this.samelayoutTypeForDefaultCount == 0) {
      this.layoutModel.defaultLayout = true;
    }


  }

  private saveLayoutDefault(data) {
    this.layoutModel.id = data.id;
    this.layoutModel.EntityId = data.entityId;
    this.layoutModel.name = data.name;
    this.layoutModel.layoutType = data.layoutType;
    this.layoutModel.context = data.context;

    this.layoutService.saveLayoutDefault(this.layoutModel, this.metaDataName).subscribe(result => {
      if (result) {
        this.getLayouts(this.metaDataName);
      }
    });
  }

  private deleteLayout(data) {

    this.globalResourceService.openDeleteModal.emit()

    this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
      this.layoutService.deleteLayout(data.id, this.metaDataName).subscribe(result => {
        if (result) {
          this.getLayouts(this.metaDataName);
        }
      });
       
      })




    // swal({
    //   title: this.getResourceValue("common_message_areyousure"),
    //   text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
    //   type: "warning",
    //   showCancelButton: true,
    //   confirmButtonColor: '#3085d6',
    //   cancelButtonColor: '#d33',
    //   confirmButtonText: this.getResourceValue("common_message_yesdeleteit"),
    //   showLoaderOnConfirm: true,
    // })
    //   .then((willDelete) => {
    //     if (willDelete.value) {
    //       this.layoutService.deleteLayout(data.id, this.metaDataName).subscribe(result => {
    //         if (result) {
    //           this.getLayouts(this.metaDataName);
    //         }
    //       });

    //     } else {
    //       //write the code for cancel click
    //     }

    //   });
  }

  private goToLayoutDetail(id, type) {
    // this.router.navigate(["picklist/" + this.metaDataName + "/layout/" + id + "/" + type.toLowerCase()]);
    this.router.navigate(["./" + type.toLowerCase() + "/" + id], { relativeTo: this.route })
    .then(x=>
      {
        this.picklistService.showToolbar.emit()
      });
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  public cloneLayout(data, content) {
    //console.log('data -' + data);

    this.cloneLayoutName = data.name + " - copy";
    let cloneType: number = 0;
    cloneType = parseInt(data.layoutType);
    this.oldLayoutId = data.id;

    if (cloneType > 0) {
      if (cloneType == 2) {
        this.showContext = true;
      } else {
        this.showContext = false;
      }
    }

    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(content, ngbModalOptions);
  }

  public saveClone(content) {
    let errorMessage: string = "";

    if (this.cloneLayoutName === "") {
      errorMessage += this.globalResourceService.requiredValidator("metadata_field_name")  + "<br/>";
    }
    // if (this.drpType === "") {
    //   errorMessage += this.getResourceValue("common_validation_type") + "<br/>";
    // }
    // else if (this.drpType === "2") {
    //   if (this.drpContext === "") {
    //     errorMessage += this.getResourceValue("ContextIsRequired") + "<br/>";
    //   }
    // }

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }

    this.layoutModel.name = this.cloneLayoutName;
    //this.layoutModel.layoutType = parseInt(this.drpType);
    this.layoutModel.context = (this.drpCloneContext == '') ? 0 : parseInt(this.drpCloneContext);

    this.layoutService.cloneLayout(this.layoutModel, this.metaDataName, this.oldLayoutId).subscribe(result => {
      this.modalReference.close();
      this.toster.showSuccess(this.getResourceValue("metadata_operation_layoutclone_success_message"));      
      this.getLayouts(this.metaDataName);
    });

  }
}


export class LayoutType {
  constructor(public id: number, public name: string) { }
}

export class SubType {
  constructor(public id: number, public name: string) { }
}

export class Context {
  constructor(public id: number, public name: string) { }
}
