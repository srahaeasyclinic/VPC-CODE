import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs'; 
import { PicklistService } from './picklist.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';

import { Picklists } from '../model/picklists';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-picklist',
  templateUrl: './picklist.component.html',
  styleUrls: ['./picklist.component.css']
})
export class PicklistComponent implements OnInit {
  private picklistList: Picklists[];
 

  public view: Observable<GridDataResult>;
  public gridData: any = this.picklistList;
  public resourceData: any ;
  public none : any;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {
    this.resourceData = this.globalResourceService.getGlobalResources();
    console.log("Picklist component called");
    this.getPicklists();
  }

 

  private getPicklists() {
    this.picklistService.getPicklists()
      .pipe(first())
      .subscribe(
        data => { 
          if(data && data){
            this.gridData = data;
          }
          
        },
        error => {
          console.log(error);
        });
  }

  goToPicklistDetails(name) {
    //this.router.navigate(['picklist/', name.toLowerCase()]);
    var currentUrl = this.router.url+"/"+name.toLowerCase();
    this.router.navigate([currentUrl]);
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
