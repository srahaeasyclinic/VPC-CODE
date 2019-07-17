import { Component, OnInit } from "@angular/core";
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

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"]
})
export class LayoutComponent implements OnInit {
  private layoutList: LayoutModel[];
  view: Observable<GridDataResult>;
  gridData: any = this.layoutList;
  private name: string;
  private layoutModel = new LayoutModel();
  public resource: Resource;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private layoutService: LayoutService,
    private modalService: NgbModal,
    public globalResourceService: GlobalResourceService
  ) {
  }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.route.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getLayouts(this.name);
    });
  }

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
    this.layoutModel.Context = data.context;

    this.layoutService.saveLayoutDefault(this.layoutModel, this.name).subscribe(result => {
      if (result) {
        this.getLayouts(this.name);
      }
    });
  }

  private deleteLayout(data) {
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
          this.layoutService.deleteLayout(data.id, this.name).subscribe(result => {
            if (result) {
              this.getLayouts(this.name);
            }
          });

        } else {
          //write the code for cancel click
        }

      });
  }

  open() {
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    const modalRef = this.modalService.open(ModalLayoutComponent, ngbModalOptions);
    modalRef.componentInstance.title = "Add layout";
    modalRef.componentInstance.layout = this;
    modalRef.componentInstance.metaDataName = this.name;
    modalRef.componentInstance.saveEvent.subscribe((saveData) => {
      console.log("saveData", saveData);
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
        this.router.navigate(["./" + type.toLowerCase() + "/" + id], { relativeTo: this.route });
      }
      

    });
  }
  goToLayoutDetail(id, type) {
    this.router.navigate(["../" + type.toLowerCase() + "/" + id], { relativeTo: this.route });
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
