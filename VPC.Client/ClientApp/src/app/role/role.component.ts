import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RoleService } from './role.service';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import { MenuService } from '../services/menu.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  public view: Observable<GridDataResult>;
  public gridRoleData: any[];
  public roleInfo = { name: '', roleType: '' };
  private roleTypes = [];
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private roleService: RoleService, private modalService: NgbModal,
    private toster: TosterService, private globalResourceService: GlobalResourceService, public menuService: MenuService,
  ) { }




  ngOnInit() {
    this.getRoles();
    this.getRoleTypes();
  }
  addRole() {
    let errorMessage: string = "";


    if (this.roleInfo.name === "") {
      errorMessage += this.getResourceValue("Nameisrequired") + "<br/>";
    }
    if (this.roleInfo.roleType === "") {
      errorMessage += this.getResourceValue("TypeIsRequired") + "<br/>";
    }

    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }

    // if(this.roleInfo.name=="")
    // {
    //   this.toster.showWarning('Please enter name.');
    //   return false;
    // }else if(this.roleInfo.roleType=="")
    // {
    //   this.toster.showWarning('Please select role type.');
    //   return false;
    // }

    this.roleService.addRole(this.roleInfo)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getRoles();
            this.modalService.dismissAll();
            this.toster.showSuccess(this.getResourceValue('RoleAddedSuccessfully') + "<br/>");
          }
        },
        error => {
          console.log(error);
        });
  }
  open(content) {
    this.roleInfo = { name: '', roleType: '' };
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalService.open(content, ngbModalOptions);
  }

  private getRoles() {
    this.roleService.getRoles()
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data) {
            this.gridRoleData = data;
          }
        },
        error => {
          console.log(error);
        });
  }

  private getRoleTypes() {
    this.roleService.getRoleTypes()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.roleTypes = data;
          }
        },
        error => {
          console.log(error);
        });
  }

  //  openAddRoleModel(content)
  //   {
  //     this.modalService.open(content);  
  //   }

  goToRoleDetail(roleInfo) {
    // this.router.navigate(["roles/" + roleInfo.roleId ]);
    this.router.navigate([roleInfo.roleId], { relativeTo: this.activatedRoute });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
