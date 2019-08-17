import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LayoutService } from '../../../meta-data/layout/layout.service';
import { first } from 'rxjs/operators';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { ResourceService } from '../../../services/resource.service';
import {CommonService} from 'src/app/services/common.service';
import {TosterService} from 'src/app/services/toster.service';
import { element } from '@angular/core/src/render3/instructions';
import { ValidationService } from 'src/app/services/validation.service';
import { GlobalResourceService } from '../../../global-resource/global-resource.service';

@Component({
  selector: 'detailentity-setting',
  template: `
  <div class="modal-header">
    <label id="modal-title">{{getResourceValue(node.entityName.toLowerCase()+'_field_'+node.name.toLowerCase())}}</label> <!-- headerString -->
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
      <!--<my-tree [rootNode]="tree"  [resource]="resource" [mode]="1" *ngIf="isTreeReady" class="detail-entity-modal">-->
      <div *ngIf="selectedFormOrList==='1'">
      <my-tree [rootNode]="tree"  [resource]="resource" [entityName]="detailEntityName.toLowerCase()" [mode]="1" [displayRule]="displayRule" *ngIf="isTreeReady" class="detail-entity-modal">
      </my-tree>
      <div class="modal-footer">
      <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue("operation_submit")}}</button>
      <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue("task_cancel")}}</button>
      </div>
    </div>

    <div *ngIf="selectedFormOrList==='2'">    
    <app-general-ui-list entityName="{{detailEntityName.toLowerCase()}}" layoutType="List" ></app-general-ui-list>
    </div>

  </div>
  
  `
})
export class DetailEntityComponent {
  public resource: any;
  public tree: ITreeNode;  
  public isTreeReady:boolean = false;  
  private editdata: any;
  public headerString: string;
  public gridData:any;
  public displayRule: any;
  public selectedFormOrList:string;

  @Input() field: any;
  @Input() entityName: string;
  @Input() userid: string;
  @Input() detailEntityName: string;
  @Input() id: string;
//@Input() eventType: any;
  @Input() subType: string;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();  
  public validateMessages: Array<string> = [];

  constructor( 
    public modal: NgbActiveModal, 
    private layoutService: LayoutService, 
    private resourceService: ResourceService, 
    private toster: TosterService, 
    private commonService: CommonService,
    private validateService: ValidationService,
    private modalService: NgbModal,
  private globalResourceService: GlobalResourceService) { }

  ngOnInit(): void {
    this.getResource();    
  }

    private getResource() {
      this.resource =this.resourceService.getResources();

      this.selectedFormOrList=this.field.selectedFormOrList;

              //console.log("resource found", data);
              
              if(this.id != '' && this.id.length > 0)
              {
                this.headerString = this.getResourceValue("metadata_label_edit_detailentity");
                this.layoutService.getDetailEntityById(this.entityName, this.userid, this.detailEntityName, this.id)
                .pipe(first())
                .subscribe(
                  data => {
                    if (data) {                      
                      this.editdata = data;
                      if(this.field.selectedFormOrList==="1")                     
                          this.getDefaultLayout(this.detailEntityName, "Form", this.subType, "Edit");
                       else 
                          this.getDefaultLayout(this.detailEntityName, "List", "", "");
                    }
                  },
                  error => {
                    console.log(error);
                  });   
              }
              else
              {
                this.headerString = this.getResourceValue("metadata_label_add_detailentity");
                if(this.field.selectedFormOrList==="1")   
                      this.getDefaultLayout(this.detailEntityName, "Form", this.subType, "Add");
                   else 
                      this.getDefaultLayout(this.detailEntityName, "List", "", "");
              }

    }

    private getDefaultLayout(name:string, type:string, subtype:string, context:string) {
    this.layoutService.getDefaultLayout(name, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            if(this.id != '' && this.id.length > 0)
            {
              if(this.field.selectedFormOrList==="1")   
              {
                data.formLayoutDetails.fields.forEach(element => {
                  if(element.name in this.editdata)
                  {
                    element.value = this.editdata[element.name];
                  }
                });
                this.tree = data.formLayoutDetails;
                this.isTreeReady = true;
              }else{               
                this.gridData = data.listLayoutDetails;
              } 
              
            }
            else
            {

              if(this.field.selectedFormOrList==="1")   
              {
                this.tree = data.formLayoutDetails;
                this.isTreeReady = true;
              }else{               
                this.gridData = data.listLayoutDetails;             

              }
            }  
          //  this.tree.name = '';
          }
        },
        error => {
          console.log(error);
        });   
    }

  // private saveDetailEntity()
  // {
  //   var value = {};
  //   this.createKeyValue(this.tree.fields, value);

  //   if(this.id != '' && this.id.length > 0)
  //   {
  //      this.layoutService.updateDetailEntity(this.entityName, this.userid, this.detailEntityName, this.id, "EN10003-ST01", value)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         this.toster.showSuccess('Detail entity added successfully.');              
  //       },
  //       error => {
  //         console.log(error);
  //       });
  //   }
  //   else
  //   {
  //     this.layoutService.addDetailEntity(this.entityName, this.userid, this.detailEntityName, "EN10003-ST01", value)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         this.toster.showSuccess('Detail entity updated successfully.');              
  //       },
  //       error => {
  //         console.log(error);
  //       });
  //   }   
  // }

  public saveSattings() {
    // console.log(JSON.stringify(this.tree));
    
    this.validateMessages = [];
    let errorMessage: string = "";
    this.validateMessages = this.validateService.validate(this.tree.fields,this.entityName);
    if (this.validateMessages.length > 0) {
      this.validateMessages.forEach(element=>{
        //errorMessage += element +  this.getResourceValue("Requriedmessage")+"<br/>";
        errorMessage +=this.getResourceValue(element)+"<br/>";
      });

      // this.modalService.open(content);
      this.toster.showError(errorMessage);
      return
    }
   
    let value = {};
    value = this.commonService.buildkey(this.tree.fields, value, this.detailEntityName);
    //value = this.commonService.createKeyValue(this.tree.fields, value);
    // this.createKeyValue(this.tree.fields, value);

    // this.tree.fields.forEach(element =>
    //   {
    //   if(element.value === ""){
    //   errorMessage += element.name +  " is required !</br>"
     
    // }
    // });
   
    // if (errorMessage != "") {
    //   this.toster.showError(errorMessage);
    //   return;
    // }
    

    if(this.id != '' && this.id.length > 0)
    {
       //this.layoutService.updateDetailEntity(this.entityName, this.userid, this.detailEntityName, this.id, "EN10003-ST01", value)
       this.layoutService.updateDetailEntity(this.entityName, this.userid, this.detailEntityName, this.id, this.subType, value)
      .pipe(first())
      .subscribe(
        data => {
          this.toster.showSuccess(this.globalResourceService.updateSuccessMessage("detailentity_displayname"));              
        },
        error => {
          console.log(error);
        });
    }
    else
    {
      //this.layoutService.addDetailEntity(this.entityName, this.userid, this.detailEntityName, "EN10003-ST01", value)
      this.layoutService.addDetailEntity(this.entityName, this.userid, this.detailEntityName, this.subType, value)
      .pipe(first())
      .subscribe(
        data => {
          this.toster.showSuccess(this.globalResourceService.saveSuccessMessage("detailentity_displayname"));              
        },
        error => {
          console.log(error);
        });
    }   

    this.saveEvent.emit(this.entityName);
  }

  // private createKeyValue(data: TreeNode[], savedData: any) {
  //   data.forEach(element => {
  //       if (element.controlType.toLocaleLowerCase() != "section") {
  //           if (element.controlType.toLocaleLowerCase() != "tabs") {
  //               savedData[element.name] = element.value
  //           }
  //       }
  //       if (element.fields) {
  //           this.createKeyValue(element.fields, savedData);
  //       }
  //   });
  //}


  getResourceValue(key) {
    key=key.replace('.', '_');
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
