import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutService } from '../layout.service';
import { LayoutModel } from '../../../model/layoutmodel';
import { TosterService } from '../../../services/toster.service';
import { Resource } from 'src/app/model/resource';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { MenuService } from '../../../services/menu.service';

@Component({
  selector: 'app-layout-detail-view',
  templateUrl: './layout-detail-view.component.html',
  styleUrls: ['./layout-detail-view.component.css']
})
export class LayoutDetailViewComponent implements OnInit {
  public layoutInfo: LayoutModel = new LayoutModel();
  private id: number;
  public entityname: string;
  public resource: Resource;
  navLinks = [
    { path: 'fields', label: 'Fields' },
    { path: 'action', label: 'Action' }
  ];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private toster: TosterService,
    public globalResourceService: GlobalResourceService,
    private menuService: MenuService
  ) {
 }
 
  ngOnInit() {
    this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];
    // this.activatedRoute.params.subscribe((params: Params) => {
    //   this.id = params['id'];
    //   this.entityname = params['name'];
    // });  
    this.activatedRoute.params.subscribe((params: Params) => {
			this.id = params['id'];
		  });   
	  
		  // this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
			// this.entityname = params['entityName'];
      // });  
    let result=this.menuService.getMenuconext();
      this.entityname = result.param_name;

  }

  updateLayoutDetails() {
    this.layoutService.updateLayout(this.entityname, this.id, this.layoutInfo).subscribe(result => {
      this.toster.showSuccess(this.getResourceValue("metadata_operation_save_success_message"));  
    });
  }

  listPreviewPage() {
    this.router.navigate(["metadata/" + this.entityname + "/layout/" + this.id + "/" + this.layoutInfo.layoutTypeName + "/preview"]);
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
