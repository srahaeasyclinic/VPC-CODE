import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Entities } from '../model/entities';
import { MetadataService } from './metadata.service';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';


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
    { path: 'relations', label: 'Relations' },
    { path: 'query', label: 'Query' },
    { path: 'operations', label: 'Operations' },
    { path: 'workflow', label: 'WorkFlow' },
    { path: 'layout', label: 'Layouts' },
    { path: 'rules', label: 'Rules' },
    { path: 'rolesecurity', label: 'Security' },
  ];

  constructor(
    private metadataService: MetadataService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public globalResourceService: GlobalResourceService,
  ) {
  }
  metadataname: string;


  ngOnInit() {

    this.activatedRoute.params.subscribe((params: Params) => {
      this.metadataname = params['entityName'];
    });
    this.getResource();
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getEntitiesByName(this.metadataname);
  }

  private getEntitiesByName(name) {
    if (this.metadataService.get_metadataByName(name)) {
      this.entityInfo = this.metadataService.get_metadataByName(name);
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.entityInfo = data;
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









