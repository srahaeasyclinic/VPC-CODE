import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Entities } from '../model/entities';
import { MetadataService } from './metadata.service';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
//import { MenuService } from '../services/menu.service';
import { BreadcrumbsService } from '../bread-crumb/BreadcrumbsService';


@Component({
  selector: 'metadatadetail',
  templateUrl: './metadatadetail.component.html'
})

export class MetadataDetailComponent implements OnInit {

  private entityDetail = new Entities();
  entityInfo: any = this.entityDetail;
  public resource: Resource;
  public isSticky: boolean = false;
  navLinks = [
    { path: 'fields', label: 'Fields' },
    // { path: 'relations', label: 'Relations' },
    { path: 'relatedentities', label: 'RelatedEntities' },
    { path: 'query', label: 'Query' },
    { path: 'operations', label: 'Operations' },
    { path: 'workflow', label: 'WorkFlows' },
    { path: 'layout', label: 'Layouts' },
    { path: 'rules', label: 'Rules' },
    { path: 'rolesecurity', label: 'Security' },
  ];
  showToolbar: boolean = false;

  constructor(
    private metadataService: MetadataService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public globalResourceService: GlobalResourceService,
    private breadcrumsService: BreadcrumbsService
  ) {
  }
  metadataname: string;
  
  ngOnInit() {

    this.metadataService.showToolbar.subscribe(x=>{
      this.showToolbar= true
    })

    this.activatedRoute.params.subscribe((params: Params) => {
      this.metadataname = params['entityName'];
    });
    // let result=this.menuService.getMenuconext();
    //   this.metadataname = result.param_name;
    // element push in the breadcums array.
    this.breadcrumsService.setchildMenuBreadcums([{ elementName: this.metadataname, 'elementURL': "", 'isGroup': false }], "metadata");

    this.getResource();
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getEntitiesByName(this.metadataname);
  }

  private getEntitiesByName(name) {
    if (this.metadataService.get_metadataByName(name)) {
      this.entityInfo = this.metadataService.get_metadataByName(name);

      //hide related entities
      if (this.entityInfo !== null) {
        if (this.entityInfo.detailEntities !== null && this.entityInfo.detailEntities.length === 0) {
          const index = this.navLinks.findIndex(item => item.label.toLowerCase() === 'relatedentities');
          if (index !== -1) {
            this.navLinks.splice(index, 1);
          }
        }
      }
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.entityInfo = data;

              //hide related entities
              if (this.entityInfo !== null) {
                if (this.entityInfo.detailEntities !== null && this.entityInfo.detailEntities.length === 0) {
                  const index = this.navLinks.findIndex(item => item.label.toLowerCase() === 'relatedentities');
                  if (index !== -1) {
                    this.navLinks.splice(index, 1);
                  }
                }
              }

              this.metadataService.set_metadataByName(data, name);
            }
          },
          error => {
            console.log(error);
          });
    }
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}









