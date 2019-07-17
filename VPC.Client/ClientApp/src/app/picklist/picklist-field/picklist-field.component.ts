import { Component, OnInit } from '@angular/core';
import { PicklistService } from '../picklist.service';
import { ResourceService } from '../../services/resource.service';
import { ActivatedRoute } from '@angular/router';
import { Entities } from '../../model/entities';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';
@Component({
  selector: 'app-picklist-field',
  templateUrl: './picklist-field.component.html',
  styleUrls: ['./picklist-field.component.css']
})
export class PicklistFieldComponent implements OnInit {
  private entity: Entities;
  public view: Observable<GridDataResult>;
  public gridData: any = this.entity;
  private name: string;
  public resourceData: any;
  public none : any;

  constructor(
    private route: ActivatedRoute,
    private picklistService: PicklistService,
    private resourceService: ResourceService,
    private globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {
    this.resourceData = this.globalResourceService.getGlobalResources();
    this.route.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 1].path;
      this.getMetadataFieldsByName(this.name);

    });
  }

  private getMetadataFieldsByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.gridData = data.fields.filter(x => x.isQueryable);
          }
          this.gridData.forEach(g_element => {
            g_element.displayName = this.resourceData[this.generateResourceName(g_element.name)]
          });
        },
        error => {
          console.log(error);
        });
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

  validationRule(validators) {

  }

  generateResourceName(word: string) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1); 
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
