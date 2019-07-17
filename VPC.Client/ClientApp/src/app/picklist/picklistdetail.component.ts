import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Entities } from '../model/entities';
import { PicklistService } from './picklist.service';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'picklistdetail',
  templateUrl: './picklistdetail.component.html'
})

export class PicklistDetailComponent implements OnInit {
  private entityDetail = new Entities();
  entityInfo: any = this.entityDetail;
  public resource: Resource;
  picklistName: string;
  public isSticky: boolean = false;
  navLinks = [
    { path: 'fields', label: 'Fields' },
    //{ path: 'relations', label: 'Relation' },
    { path: 'operations', label: 'Operations' },
    { path: 'layouts', label: 'Layouts' },
    //{ path: 'workflows', label: 'WorkFlow' }
  ];

  constructor(
    private picklistService: PicklistService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private globalResourceService: GlobalResourceService) {
  }



  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.picklistName = params['picklistName'];
      this.getEntitiesByName(this.picklistName);
    });
  }

  private getEntitiesByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.entityInfo = data;
          }

        },
        error => {
          console.log(error);
        });
  }

  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
