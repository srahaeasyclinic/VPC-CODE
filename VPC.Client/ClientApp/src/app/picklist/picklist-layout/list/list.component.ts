import { Component, OnInit } from '@angular/core';
import { LayoutModel } from '../../../model/layoutmodel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PicklistLayoutService } from "../../picklist-layout/picklist-layout.service";
import { ToastrManager } from 'ng6-toastr-notifications';
import { TosterService } from '../../../services/toster.service';
import { GlobalResourceService } from '../../../global-resource/global-resource.service';
import { Resource } from '../../../model/resource';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public layoutInfo: LayoutModel = new LayoutModel();
  id: string;
  public picklistName: string = '';
  public defaultLayout: boolean = false;
  public modifiedDate: Date;
  public resource: Resource;

  navLinks = [
    { path: 'fields', label: 'Fields' },
    { path: 'textsearch', label: 'Free text search' },
    { path: 'simplesearch', label: 'Simple search' },
    { path: 'toolbars', label: 'Toolbar' },
    { path: 'actions', label: 'Actions' }
  ];


  constructor(
    private activatedRoute: ActivatedRoute,
    private layoutService: PicklistLayoutService,
    private toster: TosterService,
    private router: Router,
    private globalResourceService: GlobalResourceService,

  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];

    // this.activatedRoute.params.subscribe((params: Params) => {
    //   this.id = params['id'];
    //   this.picklistName = params['name'];
    // });
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
    });

    this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
      this.picklistName = params['picklistName'];
    });
    if (this.layoutInfo) {
      this.defaultLayout = this.layoutInfo.defaultLayout;
      this.modifiedDate = this.layoutInfo.modifiedDate;
    }
  }


  public updateLayoutDetails() {
    this.layoutService.updateLayout(this.layoutInfo, this.picklistName, this.id).subscribe(result => {
      this.toster.showSuccess(this.getResourceValue("LayoutSavedSuccessfully"));
    });
  }

  listPreviewPage() {
    this.router.navigate(["picklist/" + this.picklistName + "/layout/" + this.id + "/" + this.layoutInfo.layoutTypeName + "/preview"]);
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
