import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LayoutService } from '../../../meta-data/layout/layout.service';
import { first } from 'rxjs/operators';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { ResourceService } from '../../../services/resource.service';
import { CommonService } from 'src/app/services/common.service';
import { TosterService } from 'src/app/services/toster.service';
import { element } from '@angular/core/src/render3/instructions';
import { ValidationService } from 'src/app/services/validation.service';
import { GlobalResourceService } from '../../../global-resource/global-resource.service';

@Component({
  selector: 'quickadd-setting',
  template: `
  
  <div class="modal-header">
    <label id="modal-title">{{getResourceValue("metadata_label_add")}} {{entityName}}</label>
    <button type="button" class="close" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
      <my-tree [rootNode]="tree"  [resource]="resource" [entityName]="entityName" [mode]="1" [displayRule]="null" *ngIf="isTreeReady" class="detail-entity-modal">
      </my-tree>
      <div class="modal-footer">
      <button type="button" class="btn btn-primary" (click)="saveSattings()">{{getResourceValue("operation_submit")}}</button>
      <button type="button" class="btn btn-secondary" (click)="modal.dismiss('cancel click')">{{getResourceValue("task_cancel")}}</button>
     
    </div>
  </div>
  
  `
})
export class QuickAddSettingComponent {
  public resource: any;
  public tree: ITreeNode;
  public isTreeReady: boolean = false;
  private editdata: any;
  public headerString: string;

  public displayRule: any;

  @Input() entityName: string;
  @Input() userid: string;
  // @Input() detailEntityName: string;
  // @Input() id: string;
  // //@Input() eventType: any;
  // @Input() subType: string;
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  // public validateMessages: Array<string> = [];

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
    this.resource = this.resourceService.getResources();
    this.getDefaultLayout(this.entityName, "Form", "", "QuickAdd");
  }

  private getDefaultLayout(name: string, type: string, subtype: string, context: string) {
    this.layoutService.getDefaultLayout(name, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            if (data && data.formLayoutDetails && data.formLayoutDetails.fields && data.formLayoutDetails.fields.length > 0) {
              this.tree = data.formLayoutDetails;
              this.isTreeReady = true;
            }
          }
        },
        error => {
          console.log(error);
        });
  }


  public saveSattings() {
    this.saveEvent.emit(this.tree);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}