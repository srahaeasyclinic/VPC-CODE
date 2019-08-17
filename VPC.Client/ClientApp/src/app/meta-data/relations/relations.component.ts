import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { MetadataService } from '../metadata.service';
import { Relation } from '../../model/relation';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-relations',
  templateUrl: './relations.component.html',
  styleUrls: ['./relations.component.css']
})
export class RelationsComponent implements OnInit {

  private relation: Relation;
  public view: Observable<GridDataResult>;
  public gridData: any = this.relation;
  public name: string;
  public resource: Resource;
  public relationSource: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.route.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
      //this.getMetadataFieldsByName(this.name);
    });

    this.relationSource = [];
    this.getResource();
  }

  private getMetadataFieldsByName(name) {

    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      if (data.relatedEntities != null && data.relatedEntities.length > 0) {
        for (var k = 0; k < data.relatedEntities.length; k++) {
          this.relationSource.push(data.relatedEntities[k]);
        }
      }

      if (data.detailEntities != null && data.detailEntities.length > 0) {
        for (var k = 0; k < data.detailEntities.length; k++) {
          this.relationSource.push(data.detailEntities[k]);
        }
      }

      if (this.relationSource != null && this.relationSource.length > 0) {
        this.gridData = this.relationSource;
      }
      //console.log('if');
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              if (data.relatedEntities != null && data.relatedEntities.length > 0) {
                for (var k = 0; k < data.relatedEntities.length; k++) {
                  this.relationSource.push(data.relatedEntities[k]);
                }
              }

              if (data.detailEntities != null && data.detailEntities.length > 0) {
                for (var k = 0; k < data.detailEntities.length; k++) {
                  this.relationSource.push(data.detailEntities[k]);
                }
              }

              if (this.relationSource != null && this.relationSource.length > 0) {
                this.gridData = this.relationSource;
              }
            }
          },
          error => {
            console.log(error);
          });
      //console.log('else');
    }



    // this.metadataService.getMetadataByName(name)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       //console.log("data", data);
    //       if (data) {
    //         //this.gridData = data.relations;

    //         if (data.relatedEntities != null && data.relatedEntities.length > 0) {
    //           for (var k = 0; k < data.relatedEntities.length; k++) {
    //             this.relationSource.push(data.relatedEntities[k]);
    //           }
    //         }

    //         if (data.detailEntities != null && data.detailEntities.length > 0) {
    //           for (var k = 0; k < data.detailEntities.length; k++) {
    //             this.relationSource.push(data.detailEntities[k]);
    //           }
    //         }

    //         if (this.relationSource != null && this.relationSource.length > 0) {
    //           this.gridData = this.relationSource;
    //         }

    //       }
    //     },
    //     error => {
    //       console.log(error);
    //     });
  }

  private getResource() {
    //this.resource = this.globalResourceService.getGlobalResources();
    this.getMetadataFieldsByName(this.name);

  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
