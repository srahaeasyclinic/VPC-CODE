import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { GlobalResourceService } from '../global-resource/global-resource.service';

import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MenuService } from '../services/menu.service';
import { TosterService } from '../services/toster.service';


@Component({
  selector: 'app-deletepopup',
  templateUrl: './deletepopup.component.html',
  styleUrls: ['./deletepopup.component.css']

})
export class DeletepopupComponent {
  @ViewChild("deleteModal") deleteModal;
  modalReference: any;
  isRendered = false
  constructor( private modalService: NgbModal,
               private globalResourceService: GlobalResourceService,
               private toster: TosterService,
               public menuService: MenuService) { }

  public openModal(): void {
    if (!this.isRendered) {
      let ngbModalOptions: NgbModalOptions = {
        backdrop: 'static',
        keyboard: false
      };
      // console.log('openModal')
      this.modalReference = this.modalService.open(this.deleteModal, ngbModalOptions);
      this.isRendered = true
    }

  }
  closeModalAfterConfirmDelete() {
    this.globalResourceService.notifyConfirmationDelete.emit();
    this.toster.showSuccess(this.getResourceValue("Deleted successfully"));
    this.closeModal()
  }

  closeModal() {
    // this.modalReference.close();
    this.modalService.dismissAll();
    this.isRendered = false
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
