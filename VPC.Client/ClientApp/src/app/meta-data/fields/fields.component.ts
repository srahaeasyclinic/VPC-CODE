import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { MetadataService } from '../metadata.service';
import { Entities } from '../../model/entities';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-field',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.css']
})

export class FieldsComponent implements OnInit {
  private entity: Entities;
  public view: Observable<GridDataResult>;
  public gridData: any = this.entity;
  public name: string;
  public resource: Resource;
  constructor(
    private route: ActivatedRoute,
    private metadataService: MetadataService,
    // private resourceService: ResourceService,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.route.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
    });
    this.getResource();
  }

  private getMetadataFieldsByName(name) {
    // this.metadataService.getMetadataByName(name)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data && data) {
    //         this.gridData = data.fields.filter(x => x.isQueryable);// data.fields;
    //       }
    //     },
    //     error => {
    //       console.log(error);
    //     });
    //console.log(name);
    //console.log('data fields ', this.metadataService.get_metadataByName(name));
    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      this.gridData = data.fields.filter(x => x.isQueryable);// data.fields;
      //console.log('if');
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              this.gridData = data.fields.filter(x => x.isQueryable);
            }
          },
          error => {
            console.log(error);
          });
      //console.log('else');
    }
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getMetadataFieldsByName(this.name);
  }

  getRequired(validators) {
    var isRequired = false;
    if (validators != null && validators.length > 0) {
      for (var i = 0; i < validators.length; i++) {
        if (validators[i].name == "RequiredFieldValidator") {
          isRequired = true;
          break;
        }
      }
    }
    return isRequired;
  }

  validationRule(data) {
    alert('Need discussion');
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
