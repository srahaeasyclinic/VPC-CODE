import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { MetadataService } from './metadata.service';
import { AlertService } from '../services/alert.service';
import { Entities } from '../model/entities';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { ResourceService } from '../services/resource.service';

import { MenuItemComponent } from '../menu-item/menu-item.component';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-metadata',
  templateUrl: './metadata.component.html',
  styleUrls: ['./metadata.component.css']
})
export class MetadataComponent implements OnInit {
  private entityList: Entities[];
  public view: Observable<GridDataResult>;
  public gridData: any = this.entityList;
  public resource: Resource;
  private name: string ="";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    //private alertService: AlertService,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {

    // this.route.url.subscribe((urlPath) => {
    //   this.name = urlPath[urlPath.length - 1].path;
    // });

    // this.route.params.subscribe((params: Params) => {
    //   this.name = params['name'];
    // });   

    // this.router.config.push(
    //   {
    //     path: 'view/menu-item', component: MenuItemComponent
    //   }
    // );

    this.getResource();
  }

  private getEntities() {
    
    if (this.metadataService.get_metadata()) {
      this.gridData = this.metadataService.get_metadata();
    }
    else {
      this.metadataService.getEntities()
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.gridData = data;
              
              this.metadataService.set_metadata(this.gridData);
            }
          },
          error => {
            console.log(error);
          });
    }
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();  
    this.getEntities();
       
  }

  goToMetaDataDetails(name) {
   // this.router.navigate(['/metadata', name.toLowerCase()]);
    // var currentUrl = this.router.url+"/"+name.toLowerCase();
    // this.router.navigate([currentUrl]);

   //  var currentUrl = this.router.url+"/"+name.toLowerCase();
    this.router.navigate([this.router.url, name.toLowerCase()]);
  }

  clickWorkflow(data) {
    alert(data.name);
  }


  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}



