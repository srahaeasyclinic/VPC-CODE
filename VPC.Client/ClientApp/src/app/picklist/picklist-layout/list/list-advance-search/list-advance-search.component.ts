import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { PicklistService } from '../../../picklist.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';
import { Resource } from '../../../../model/resource';


@Component({
  selector: 'app-list-advance-search',
  templateUrl: './list-advance-search.component.html',
  styleUrls: ['./list-advance-search.component.css']
})
export class ListAdvanceSearchComponent implements OnInit {

  public advanceSearchSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private name: string;
  public layoutInfo: LayoutModel = new LayoutModel();
  public resource: Resource;

  constructor(
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.advanceSearchSource = [];
    if (this.layoutInfo && this.layoutInfo.listLayoutDetails) {
      if (this.layoutInfo.listLayoutDetails.searchProperties && this.layoutInfo.listLayoutDetails.searchProperties.length > 0) {
        this.addedItemToMainList = this.layoutInfo.listLayoutDetails.searchProperties[2].properties;
      } 
    }
   

    this.activatedRoute.parent.url.subscribe((urlPath) => {
      this.name = urlPath[urlPath.length - 4].path;
      this.getPicklistFieldsByName(this.name);
    });

  }


  private getPicklistFieldsByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.fields) {
            for (var k = 0; k < data.fields.length; k++) {
              if (data.fields[k].applicableForAdvanceSearch) {
                this.advanceSearchSource.push(data.fields[k]);
              }
            }
          }
        },
        error => {
          console.log(error);
        });
  }

  mainItemClickEvent(item) {
    if (item != null && (typeof item.isAdded === "undefined" || !item.isAdded)) {
      this.selectedItemFromMainList = item;
      this.resetOthers(this.advanceSearchSource);
      this.selectedItemFromMainList.isRowSelected = true;
    }
  }

  resetOthers(array) {
    if (array != null) {
      for (var k = 0; k < array.length; k++) {
        array[k].isRowSelected = false;
      }
    }
  }


  addItem() {
    if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
      this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;

      this.addedItemToMainList.push(this.selectedItemFromMainList);
      this.selectedItemFromMainList.isAdded = true;
    }
  }

  slectedItemClickEvent(item) {
    if (item != null) {
      this.selectedItemFromAddedList = item;
      this.resetOthers(this.addedItemToMainList);
      this.selectedItemFromAddedList.isRowSelected = true;
    }
  }


  removeItem() {
    var index = this.addedItemToMainList.indexOf(this.selectedItemFromAddedList);
    if (index != -1) {
      for (var i = 0; i < this.addedItemToMainList.length; i++) {
        if (this.addedItemToMainList[i].name != this.selectedItemFromAddedList.name) continue;
        for (var j = 0; j < this.addedItemToMainList.length; j++) {
          if (this.addedItemToMainList[j].sequence > this.addedItemToMainList[i].sequence) {
            this.addedItemToMainList[j].sequence -= 1;
            break;
          }
        }
      }
      this.addedItemToMainList.splice(index, 1);
      for (var k = 0; k < this.advanceSearchSource.length; k++) {
        if (this.advanceSearchSource[k].name == this.selectedItemFromAddedList.name) {
          this.advanceSearchSource[k].isAdded = false;
        }
      }
    }

  }

  upItem() {
    if (this.selectedItemFromAddedList) {
      for (var i = 0; i < this.addedItemToMainList.length; i++) {
        if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
          for (var j = 0; j < this.addedItemToMainList.length; j++) {
            if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence - 1)) {
              this.addedItemToMainList[j].sequence += 1;
              break;
            }
          }
          this.addedItemToMainList[i].sequence -= 1;
          break;
        }
      }

      this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
        return a.sequence - b.sequence;
      });
    }
  }

  downItem() {
    if (this.selectedItemFromAddedList) {
      for (var i = 0; i < this.addedItemToMainList.length; i++) {
        if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
          for (var j = 0; j < this.addedItemToMainList.length; j++) {
            if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence + 1)) {
              this.addedItemToMainList[j].sequence -= 1;
              break;
            }
          }
          this.addedItemToMainList[i].sequence += 1;
          break;
        }
      }

      this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
        return a.sequence - b.sequence;
      });
    }
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
   
}
