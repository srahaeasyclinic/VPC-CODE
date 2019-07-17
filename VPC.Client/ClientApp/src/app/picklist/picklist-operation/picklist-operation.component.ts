import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { PicklistService } from '../picklist.service';
import { Entities } from '../../model/entities';
import { ResourceService } from '../../services/resource.service';
import { GlobalResourceService } from '../../global-resource/global-resource.service'
import { Resource } from '../../model/resource';


@Component({
  selector: 'app-picklist-operation',
  templateUrl: './picklist-operation.component.html',
  styleUrls: ['./picklist-operation.component.css']
})
export class PicklistOperationComponent implements OnInit {
  private entity: Entities;
  public gridData: any = this.entity;
  public operationList = [];
  private name: string;
  public resource: any;

  constructor(
    private route: ActivatedRoute,
    private picklistService: PicklistService, 
    private resourceService: ResourceService,
    private globalResourceService: GlobalResourceService,
    ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.route.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
      this.getMetadataFieldsByName(this.name);
      //this.getMetadataFieldsByName(this.name);
      // this.getResource();
    });
  }

  // private getResource() {
  //   this.resourceService.getResources()
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.resource = data;
  //           this.getMetadataFieldsByName(this.name);
  //         }
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }


  private getMetadataFieldsByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            if (data.operations && data.operations.length > 0) {
              data.operations.forEach(value => {
                value.type = "Operation";
                this.operationList.push(value);
              });
            }

            if (data.tasks && data.tasks.length > 0) {
              data.tasks.forEach(value => {
                this.operationList.push(value);
              });
            }

            this.gridData = this.operationList;
          }

        },
        error => {
          console.log(error);
        });
  }

  generateResourceName(word: string) {
    if (!word) return word;

    let hierarchyPresent = word.split(".");
    if (hierarchyPresent.length == 1) {
      return word[0].toLowerCase() + word.substr(1);
    }
    else if (hierarchyPresent.length > 1) {
      let lastItem = hierarchyPresent[hierarchyPresent.length - 1];
      if (lastItem)
        return lastItem[0].toLowerCase() + lastItem.substr(1);
    }
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
