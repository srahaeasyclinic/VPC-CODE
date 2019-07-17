import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RoleService } from './role.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { MetadataService } from '../meta-data/metadata.service';
import { Entities } from '../model/entities';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-role-detail',
  templateUrl: './role-detail.component.html',
  styleUrls: ['./role-detail.component.css']
})
export class RoleDetailComponent implements OnInit {
  roleId: string;
  public roleInfo = { name: '', roleType: '' };
  public roleTypes = [];
  private entityList: Entities[];
  public resource: Resource;
  //public entityList: any[];
  public view: Observable<GridDataResult>;
  public gridData: any[] = this.entityList;
  panelOpenState = false;
  clickedOnEntity: string;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private roleService: RoleService, private modalService: NgbModal,
    private toster: TosterService, private metadataService: MetadataService, private globalResourceService: GlobalResourceService,
  )
  {

  }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.roleId = params['roleId'];
    });

    // this.activatedRoute.queryParams.filter(params => params.order).subscribe(params => { 
    //   this.order = params.order;    
    // });

    this.getRole();
    this.getRoleTypes();
    this.getEntities();
  }

  private getRole() {
    this.roleService.getRole(this.roleId)
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data) {
            this.roleInfo = data;
          }
        },
        error => {
          console.log(error);
        });
  }


  updateRole() {

    if (this.roleInfo.name == "") {
      this.toster.showWarning(this.getResourceValue("Validation_Field_Name_Required"));
      return false;
    }

    this.roleService.updateRole(this.roleInfo)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.toster.showSuccess(this.getResourceValue('Operation_Save_Successful_Message'));
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

  configToggle(mainStep) {
    mainStep.isConfigToggle = !mainStep.isConfigToggle;
    this.clickedOnEntity = mainStep.name;
  }


  private getEntities() {
    this.metadataService.getEntities()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.gridData = data.filter(workFlow => workFlow.supportWorkflow === true);
            // if(this.entityList.length>0)
            // {
            //  this.clickedOnEntity=this.entityList[0].name;
            //   this.entityList[0].navLinks=[ 
            //     {path:'entitysecurity/'+this.entityList[0].name, label:'Entity'  },
            //     {path:'workflowsecurity/'+this.entityList[0].name, label:'WorkFlow'},
            //   ]; 
            //   this.router.navigate(["entitysecurity/" + this.entityList[0].name ], {relativeTo: this.activatedRoute});

            // //   this.router.navigate(["entitysecurity/" + this.entityList[0].name ],  {
            // //     queryParams: {refresh: new Date().getTime()} 
            // //  });

            // }

            //       this.entityList.forEach(obj => {          

            //         obj.navLinks=[ 
            //           {path:'entitysecurity/'+obj.name, label:'Entity' , entityname:obj.name },
            //           {path:'workflowsecurity/'+obj.name, label:'WorkFlow' , entityname:obj.name},
            //         ];  
            //         //this.router.navigate(["entitysecurity/" + obj.name ]);       
            //  });
            //{ path: "contacts", component: ContactListComponent, outlet: "outlet1" }

          }
        },
        error => {
          console.log(error);
        });
  }

  // onClick_EntityTab(entity)
  // {
  //   this.clickedOnEntity=entity.name   ;
  //   //this.router.navigate(["entitysecurity/" + entity.name ]);
  // } 


  // onClick_EntityTab(entity)
  // {
  //   this.clickedOnEntity=entity.name
  //   if(this.entityList.length>0)
  //           {

  //             this.entityList.forEach(obj => { 
  //               if(obj.name==entity.name)
  //               {
  //                 obj.navLinks=[ 
  //                   {path:'entitysecurity/'+obj.name,  label:'Entity' , outlet: obj.name  },
  //                   {path:'workflowsecurity/'+obj.name, label:'WorkFlow' , outlet: obj.name },
  //                 ]; 


  //               }else{
  //                 delete obj.navLinks;
  //               }

  //             });
  //           }
  //         //   this.router.navigate(["entitysecurity/" + entity.name  ],  {
  //         //     queryParams: {refresh: new Date().getTime(),relativeTo: this.activatedRoute} 
  //         //  });
  //           this.router.navigate(["entitysecurity/" + entity.name ], {relativeTo: this.activatedRoute});
  // } 

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
