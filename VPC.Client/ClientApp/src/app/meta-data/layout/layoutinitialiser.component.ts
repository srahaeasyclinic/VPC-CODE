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

@Component({
  selector: "layout-intialiser",
  templateUrl: "./layoutintialiser.component.html",
  styleUrls: ["./layout.component.css"]
})
export class LayoutIntialiserComponent implements OnInit {
  // private layoutList: LayoutModel[];
  // view: Observable<GridDataResult>;
  // gridData: any = this.layoutList;
  // private name: string;
  // private layoutModel = new LayoutModel();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private layoutService: LayoutService,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() { 
    //console.log('check');
  }

  

}
