import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { SubscriptionService } from './subscription.service';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MenuService } from '../services/menu.service';
import{GlobalResourceService} from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';


@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css']
})
export class SubscriptionComponent implements OnInit {
  modalReference: any;
  public gridData: any[];
  resource:Resource;
  public subscriptionInfo = { name: '', group: { id: '', name: '' } };
  groupTypes = [];
  subsGroups = [];

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
      private modalService: NgbModal, private subscriptionService: SubscriptionService, private toster: TosterService, private globalResourceService: GlobalResourceService, public menuService: MenuService
  ) { }

  ngOnInit() {
    this.resource=this.globalResourceService.getGlobalResources();
    this.getSubscriptions();
    this.getGroupTypes();

  }

  getSubscriptions() {
    this.subscriptionService.getSubscriptions()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.gridData = data;
          }
        },
        error => {
          console.log(error);
        });
  }

  addSubscription() {

    let errorMessage: string = "";


    if (this.subscriptionInfo.name === "") {
      errorMessage += this.getResourceValue("Nameisrequired")+"<br/>";
    }
    else if (this.subscriptionInfo.group == null) {
      if (this.subscriptionInfo.group.id == "")
        errorMessage +=this.getResourceValue("GroupIsRequired")+"<br/>";
    }

    if (this.subscriptionInfo.group.name === "") {
      errorMessage +=this.getResourceValue("GroupIsRequired")+"<br/>";
    }


    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }



    this.subscriptionService.addSubscription(this.subscriptionInfo)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getSubscriptions();
            this.modalService.dismissAll();
            this.toster.showSuccess(this.getResourceValue('SubscriptionAddedSuccessfully.'));
          }
        },
        error => {
          console.log(error);
        });
  }

  addSubscriptionNext() {

    let errorMessage: string = "";


    if (this.subscriptionInfo.name === "") {
      errorMessage += this.getResourceValue("Nameisrequired")+"<br/>";
    }
    else if (this.subscriptionInfo.group == null) {
      if (this.subscriptionInfo.group.id == "")
        errorMessage += this.getResourceValue("GroupIsRequired")+"<br/>";
    }

    if (this.subscriptionInfo.group.name === "") {
      errorMessage += this.getResourceValue("GroupIsRequired")+"<br/>";
    }


    if (errorMessage != "") {
      this.toster.showError(errorMessage);
      return;
    }


    // if (this.subscriptionInfo.name == "") {
    //   this.toster.showWarning('Please enter name.');
    //   return false;
    // } else if (this.subscriptionInfo.group == null) {
    //   this.toster.showWarning('Please select group.');
    //   return false;
    // } else if (this.subscriptionInfo.group.id == "") {
    //   this.toster.showWarning('Please select group.');
    //   return false;
    // }

    this.subscriptionService.addSubscription(this.subscriptionInfo)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getSubscriptions();
            this.subscriptionInfo = { name: '', group: { id: '', name: '' } };
            this.toster.showSuccess(this.getResourceValue('SubscriptionAddedSuccessfully'));
          }
        },
        error => {
          console.log(error);
        });
  }

  private getGroupTypes() {
    this.subscriptionService.getSubscriptionGroup(null).pipe(first()).subscribe(data => {
      if (data) {
        this.groupTypes = data.result;
        this.subsGroups = this.groupTypes.slice();
      }
    },
      error => {
        console.log(error);
      });
  }

  handleFilter(value) {
    this.subsGroups = this.groupTypes.filter((s) => s.text.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  onGroupChange(groupId) {
    if (groupId == "") {
      return false;
    }
    var selectedGroup = this.groupTypes.filter((s) => s.text.toLowerCase().indexOf(groupId.toLowerCase()) !== -1);
    if (selectedGroup.length == 1) {
      this.subscriptionInfo.group.id = selectedGroup[0].internalId;
    }

  }


  deleteSubscription(tenantSubscriptionId) {
    this.subscriptionService.deleteSubscription(tenantSubscriptionId)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getSubscriptions();
          }
        },
        error => {
          console.log(error);
        });

  }

  statusSubscription(tenantSubscriptionId) {
    this.subscriptionService.statusSubscription(tenantSubscriptionId)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getSubscriptions();
          }
        },
        error => {
          console.log(error);
        });

  }

  open(content) {
    this.subscriptionInfo = { name: '', group: { id: '', name: '' } };
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalService.open(content, ngbModalOptions);
  }

  goToSubscriptionDetails(info) {
    this.router.navigate([info.tenantSubscriptionId], { relativeTo: this.activatedRoute });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
