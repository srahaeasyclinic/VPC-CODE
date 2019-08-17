import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
import { GridDataResult } from "@progress/kendo-angular-grid";
import { Observable } from "rxjs";
import { first } from "rxjs/operators";
import { LayoutModel } from "../../model/layoutmodel";
import { LayoutService } from "./layout.service";
import { ModalLayoutComponent } from "./modal-layout/modal-layout.component";
import swal from 'sweetalert2';
import { Resource } from "src/app/model/resource";
import { GlobalResourceService } from "src/app/global-resource/global-resource.service";
import { TosterService } from '../../services/toster.service';
import { MetadataService } from '../metadata.service';
import { MenuService } from "../../services/menu.service";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"]
})
export class LayoutComponent implements OnInit {
  private layoutList: LayoutModel[];
  view: Observable<GridDataResult>;
  gridData: any = this.layoutList;
  public name: string;
  private layoutModel = new LayoutModel();
  public resource: Resource;
  layoutForm: FormGroup;
  modalReference: any;
  public subTypes = new Array<SubType>();
  private subtypename: string;
  submitted = false;
  public showSubType: boolean = false;
  private oldLayoutId: string = "";

  layoutTypes = [
    new LayoutType(1, 'View'),
    new LayoutType(2, 'Form'),
    new LayoutType(3, 'List')
  ];

  contexts = [
    new Context(1, 'Add'),
    new Context(2, 'Edit'),
    new Context(3, 'Quick Add')
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private layoutService: LayoutService,
    private modalService: NgbModal,
    public globalResourceService: GlobalResourceService,
    private formBuilder: FormBuilder,
    private metadataService: MetadataService,
    private toster: TosterService,
    private menuService: MenuService
  ) {
  }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.route.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getLayouts(this.name);
      this.getMetadataFieldsByName(this.name);
    });
  }

  get f() { return this.layoutForm.controls; }

  loadLayout(name) {
    this.layoutService.getLayouts(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.gridData = data;
          }
        },
        error => {
          console.log(error);
        });
  }

  private getLayouts(name) {
    this.layoutService.getLayouts(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.gridData = data;
          }

        },
        error => {
          console.log(error);
        });
  }

  private saveLayoutDefault(data) {
    this.layoutModel.id = data.id;
    this.layoutModel.EntityId = data.entityId;
    this.layoutModel.name = data.name;
    this.layoutModel.layoutType = data.layoutType;
    this.layoutModel.Subtype = data.subtype;
    this.layoutModel.context = data.context;

    this.layoutService.saveLayoutDefault(this.layoutModel, this.name).subscribe(result => {
      if (result) {
        this.getLayouts(this.name);
      }
    });
  }

  private deleteLayout(data) {

    this.globalResourceService.openDeleteModal.emit()

    this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
      this.layoutService.deleteLayout(data.id, this.name).subscribe(result => {
        if (result) {
          this.getLayouts(this.name);
        }
      });
      })






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
    //       this.layoutService.deleteLayout(data.id, this.name).subscribe(result => {
    //         if (result) {
    //           this.getLayouts(this.name);
    //         }
    //       });

    //     } else {
    //       //write the code for cancel click
    //     }

    //   });
  }

  open() {
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    const modalRef = this.modalService.open(ModalLayoutComponent, ngbModalOptions);
    modalRef.componentInstance.title = this.getResourceValue("metadata_addlayout");
    modalRef.componentInstance.layout = this;
    modalRef.componentInstance.metaDataName = this.name;
    modalRef.componentInstance.saveEvent.subscribe((saveData) => {
      //console.log("saveData", saveData);
      var id = "";
      var type = "";
      if (saveData && saveData.id) {
        id = saveData.id;
        if (Number(saveData.layoutType) === 1) {
          type = "view";
        }
        else if (Number(saveData.layoutType) === 2) {
          type = "form";
        }
        else if (Number(saveData.layoutType) === 3) {
          type = "list";
        }
        this.router.navigate(["./" + type.toLowerCase() + "/" + id], { relativeTo: this.route }).then(x=>
          {
            this.metadataService.showToolbar.emit()
          });
      }


    });
  }
  goToLayoutDetail(id, type) {
    this.router.navigate(["../" + type.toLowerCase() + "/" + id], { relativeTo: this.route }).then(x=>
      {
        this.metadataService.showToolbar.emit()
      });
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  getRequiredValidatorMessageFromResource(field: string) {
    return this.globalResourceService.requiredValidator(field);
  }

  public cloneLayout(data, content) {
    //console.log('data -' + data);
    let cloneType: number = 0;
    cloneType = parseInt(data.layoutType);
    this.oldLayoutId = data.id;

    this.createForm(data);

    if (cloneType > 0) {
      if (cloneType == 2) {
        this.showSubType = true;
        this.formTypeValidation();
      } else {
        this.showSubType = false;
      }
    }

    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(content, ngbModalOptions);
  }

  private createForm(data: any) {
    this.layoutForm = this.formBuilder.group({
      layoutName: [data.name + " - copy", Validators.required]
    });
  }

  private getSubTypeName(id) {
    for (var k = 0; k < this.subTypes.length; k++) {
      if (this.subTypes[k].id === id) {
        return this.subtypename = this.subTypes[k].name;
      }
    }
  }

  public submitForm() {
    this.submitted = true;

    if (this.layoutForm.invalid) {
      return;
    }

    this.layoutModel.name = this.layoutForm.value.layoutName;
    this.layoutModel.subtypeeName = (this.layoutForm.value.drpSubtype == '') ? '' : this.getSubTypeName(parseInt(this.layoutForm.value.drpSubtype));
    this.layoutModel.context = (this.layoutForm.value.drpContext == '') ? 0 : this.layoutForm.value.drpContext;

    this.layoutService.cloneLayout(this.layoutModel, this.name, this.oldLayoutId).subscribe(result => {
      this.toster.showSuccess(this.getResourceValue("layout_operation_clone_success_message"));
      this.modalService.dismissAll();
      this.getLayouts(this.name);
    });

  }

  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data && data.subtypes) {
            for (var k = 0; k < data.subtypes.length; k++) {
              this.subTypes.push(new SubType(k, data.subtypes[k]));
            }
          }
        },
        error => {
          console.log(error);
        });
  }

  private onTypeChange(value) {
    //console.log(value);
    if (value == "2") {
      this.showSubType = true;
      this.formTypeValidation();
    } else {
      this.showSubType = false;
    }
  }

  private formTypeValidation() {
    this.layoutForm.addControl('drpSubtype', new FormControl('', Validators.required));
    this.layoutForm.addControl('drpContext', new FormControl('', Validators.required));
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

