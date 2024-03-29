import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutService } from '../layout.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../../model/layoutmodel';
import { MetadataService } from "../../metadata.service";
import { Observable } from 'rxjs/Observable';
import { TosterService } from '../../../services/toster.service';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';
import { MenuService } from '../../../services/menu.service';

@Component({
  selector: 'app-layout-detail-list',
  templateUrl: './layout-detail-list.component.html',
  styleUrls: ['./layout-detail-list.component.css']
})
export class LayoutDetailListComponent implements OnInit {

  public layoutInfo: LayoutModel = new LayoutModel();

  id: string;
  private layoutName: string = '';
  public defaultLayout : boolean = false;
  public modifiedDate : Date;
  public resource: Resource;
  navLinks = [
    { path: 'fields', label: 'Fields' },
    { path: 'textsearch', label: 'Freetextsearch' },
    { path: 'simplesearch', label: 'Simplesearch' },
    { path: 'advancesearch', label: 'Advancesearch' },
    { path: 'toolbar', label: 'Toolbar' },
    { path: 'action', label: 'Actions' }
  ];
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private metadataService: MetadataService,
    private toster: TosterService,
    public globalResourceService: GlobalResourceService,
    private menuService: MenuService
  ) {
 }
 entityname: string;
  ngOnInit() {
    //debugger;
    this.resource = this.globalResourceService.getGlobalResources();
    this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];

    // this.activatedRoute.params.subscribe((params: Params) => {
    //   this.id = params['id'];
    //   this.entityname = params['name'];
    // });   
    this.activatedRoute.params.subscribe((params: Params) => {
			this.id = params['id'];
		  });   
	  
		  this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
			this.entityname = params['entityName'];
      });  
    // let result=this.menuService.getMenuconext();
    // this.entityname = result.param_name;
    
    if(this.layoutInfo)
    {
      this.defaultLayout = this.layoutInfo.defaultLayout;
      this.modifiedDate = this.layoutInfo.modifiedDate;
    }
  }

  private getLayoutById(layoutId) {
    this.layoutService.getLayoutById(this.entityname, layoutId)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.layoutInfo = data;
          }

        },
        error => {
          console.log(error);
        });
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
