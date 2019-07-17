import { Component, OnInit } from '@angular/core'; 
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PicklistService } from '../../../picklist.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';
import { Resource } from '../../../../model/resource';

@Component({
  selector: 'app-list-free-text-search',
  templateUrl: './list-free-text-search.component.html',
  styleUrls: ['./list-free-text-search.component.css']
})
export class ListFreeTextSearchComponent implements OnInit {
  public freeTextSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private name: string;
  private isdatamainlist: boolean = false;
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
    this.freeTextSource = [];
    if (this.layoutInfo && this.layoutInfo.listLayoutDetails) {
      if (this.layoutInfo.listLayoutDetails.searchProperties && this.layoutInfo.listLayoutDetails.searchProperties.length > 0) {
        this.addedItemToMainList = this.layoutInfo.listLayoutDetails.searchProperties[0].properties; 
      } 
    } 

    // this.activatedRoute.parent.url.subscribe((urlPath) => {
    //   this.name = urlPath[urlPath.length - 4].path;
    //   this.getPicklistFieldsByName(this.name);
    // });


    this.activatedRoute.parent.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getPicklistFieldsByName(this.name);
    });

    this.addedItemToMainList.forEach(item => {
      item.isRowSelected = '';
    });

  }

  private getPicklistFieldsByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.fields) {
            for (var k = 0; k < data.fields.length; k++) {
              if (data.fields[k].applicableForFreeTextSearch) {
                this.freeTextSource.push(data.fields[k]);
                 //for left side gray
                 for (var i = 0; i < this.addedItemToMainList.length; i++) {
                  for (var j = 0; j < this.freeTextSource.length; j++) {
                    if (this.addedItemToMainList[i].name === this.freeTextSource[j].name) {
                      this.freeTextSource[j].isRowSelected = true;
                      this.freeTextSource[j].isAdded = true;
                    }
                  }
                }
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
      this.resetOthers(this.freeTextSource);
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


  // addItem() {
  //   if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
  //     this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;

  //     this.addedItemToMainList.push(this.selectedItemFromMainList);
  //     this.selectedItemFromMainList.isAdded = true;
  //   }
  // }

  addItem() {
    this.isdatamainlist = false;
    if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
      for (var k = 0; k < this.addedItemToMainList.length; k++) {
        if (this.addedItemToMainList[k].name === this.selectedItemFromMainList.name) {
          this.isdatamainlist = true;
        }
      }

      if (this.isdatamainlist === false) {
        this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;
        this.addedItemToMainList.push(this.selectedItemFromMainList);
        this.reOrderMainList();
        this.selectedItemFromMainList.isAdded = true;
      }      
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
      // for (var i = 0; i < this.addedItemToMainList.length; i++) {
      //   if (this.addedItemToMainList[i].name != this.selectedItemFromAddedList.name) continue;
      //   for (var j = 0; j < this.addedItemToMainList.length; j++) {
      //     if (this.addedItemToMainList[j].sequence > this.addedItemToMainList[i].sequence) {
      //       this.addedItemToMainList[j].sequence -= 1;
      //       break;
      //     }
      //   }
      // }
      this.addedItemToMainList.splice(index, 1);
      for (var k = 0; k < this.freeTextSource.length; k++) {
        if (this.freeTextSource[k].name == this.selectedItemFromAddedList.name) {
          this.freeTextSource[k].isAdded = false;
        }
      }

      this.reOrderMainList();
    }

  }

  upItem() {
    if (this.selectedItemFromAddedList) {
      let selectedsequence = 0;
      this.reOrderMainList();

      this.addedItemToMainList.forEach((item) => {
        if (item.name === this.selectedItemFromAddedList.name) {
          selectedsequence = item.sequence;
          item.sequence = selectedsequence - 1;
          this.addedItemToMainList[item.sequence].sequence = selectedsequence;
        }
      });

      this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
        return a.sequence - b.sequence;
      });
    }
  }

  downItem() {
    if (this.selectedItemFromAddedList) {
      let selectedsequence = 0;
      this.reOrderMainList();

      this.addedItemToMainList.forEach((item) => {
        if (item.name === this.selectedItemFromAddedList.name) {
          selectedsequence = item.sequence;
          item.sequence = selectedsequence + 1;
          this.addedItemToMainList[item.sequence].sequence = selectedsequence;
        }
      });

      this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
        return a.sequence - b.sequence;
      });
    }
  }

  generateResourceName(word)
  {
     if (!word) return word;
     return word[0].toLowerCase() + word.substr(1);
   }

   getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  private reOrderMainList() {
    let a = 0;
    this.addedItemToMainList.forEach((item) => {
      item.sequence = a;
      a++;
    });
  }

}
