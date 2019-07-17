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
    <label id="modal-title">{{headerString}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
    
      <!--<my-tree [rootNode]="tree"  [resource]="resource" [mode]="1" *ngIf="isTreeReady" class="detail-entity-modal">-->
      <my-tree [rootNode]="tree"  [resource]="resource" [mode]="1" [displayRule]="displayRule" *ngIf="isTreeReady" class="detail-entity-modal">
      </my-tree>
      <div class="modal-footer">
      <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue("Cancel")}}</button>
      <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue("Submit")}}</button>
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

  public displayRule: any;

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

              //console.log("resource found", data);
              
              if(this.id != '' && this.id.length > 0)
              {
                this.headerString = this.getResourceValue("Editdetailentity");
                this.layoutService.getDetailEntityById(this.entityName, this.userid, this.detailEntityName, this.id)
                .pipe(first())
                .subscribe(
                  data => {
                    if (data) {                      
                      this.editdata = data;
                      //this.getDefaultLayout(this.detailEntityName, "Form", "EN10003-ST01", "Edit");                      
                      this.getDefaultLayout(this.detailEntityName, "Form", this.subType, "Edit");
                    }
                  },
                  error => {
                    console.log(error);
                  });   
              }
              else
              {
                this.headerString = this.getResourceValue("Adddetailentity");
                //this.getDefaultLayout(this.detailEntityName, "Form", "EN10003-ST01", "New");
                this.getDefaultLayout(this.detailEntityName, "Form", this.subType, "New");
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
              data.formLayoutDetails.fields.forEach(element => {
                if(element.name in this.editdata)
                {
                  element.value = this.editdata[element.name];
                }
              });

              this.tree = data.formLayoutDetails;
              this.isTreeReady = true;
            }
            else
            {
              this.tree = data.formLayoutDetails;
              this.isTreeReady = true;
            }    
            
            this.tree.name = '';
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
    this.validateMessages = this.validateService.validate(this.tree.fields);
    if (this.validateMessages.length > 0) {
      this.validateMessages.forEach(element=>{
        errorMessage += element +  this.getResourceValue("Requriedmessage")+"<br/>";
      });

      // this.modalService.open(content);
      this.toster.showError(errorMessage);
      return
    }
   
    let value = {};
    
    value = this.commonService.createKeyValue(this.tree.fields, value);
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
          this.toster.showSuccess(this.getResourceValue("DetailEntityUpdatedSuccessfully"));              
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
          this.toster.showSuccess(this.getResourceValue("DetailEntityAddedSuccessfully"));              
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
    return this.globalResourceService.getResourceValueByKey(key);
  }
}