import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PicklistService } from '../../../picklist.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { first } from 'rxjs/operators';
import { KeyValue } from '../../../../model/keyvalue';
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';
import { Resource } from '../../../../model/resource';


@Component({
  selector: 'app-list-simple-search',
  templateUrl: './list-simple-search.component.html',
  styleUrls: ['./list-simple-search.component.css']
})
export class ListSimpleSearchComponent implements OnInit {

  public simpleSearchSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private name: string;
  public layoutInfo: LayoutModel = new LayoutModel();
  private pickListSource: any;
  public displaySimpleDetailsItem: boolean = false;
  private simpleSearchControlType: string;
  private simpleSearchDefaultValue: string;
  private values: any;
  private isChecked: boolean = false;
  private isdataexistsvalues: boolean = false;
  private isdatamainlist: boolean = false;
  public resource: Resource;
  public searchText: string ='';

  constructor(
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources()
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.simpleSearchSource = [];
    this.pickListSource = [];
    this.values = [];

    if (this.layoutInfo && this.layoutInfo.listLayoutDetails.searchProperties && this.layoutInfo.listLayoutDetails.searchProperties.length > 0) {
      this.addedItemToMainList = this.layoutInfo.listLayoutDetails.searchProperties[1].properties;

      //if (this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length > 0) {
      //  this.simpleSearchControlType = this.layoutInfo.listLayoutDetails.searchProperties[1].properties[0].controlType;
      //  this.simpleSearchDefaultValue = this.layoutInfo.listLayoutDetails.searchProperties[1].properties[0].defaultValue;
      //}
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
              if (data.fields[k].applicableForSimpleSearch) {
                this.simpleSearchSource.push(data.fields[k]);
                //for left side gray
                for (var i = 0; i < this.addedItemToMainList.length; i++) {
                  for (var j = 0; j < this.simpleSearchSource.length; j++) {
                    if (this.addedItemToMainList[i].name === this.simpleSearchSource[j].name) {
                      this.simpleSearchSource[j].isRowSelected = true;
                      this.simpleSearchSource[j].isAdded = true;
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
      this.resetOthers(this.simpleSearchSource);
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

      for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length; k++) {
        if (item.name === this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].name) {
          this.simpleSearchControlType = this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].controlType;
          this.simpleSearchDefaultValue = this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].defaultValue;
        }
      }

      if (item.dataType === 'PickList') {
        this.picklistService.getPickListValues(item.name)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.pickListSource = data.result;
                for (var k = 0; k < this.pickListSource.length; k++) {
                  this.pickListSource[k].isEnabled = false;
                  if (this.pickListSource[k].internalId === this.simpleSearchDefaultValue) {
                    this.pickListSource[k].isEnabled = true;
                  }
                }

                for (var i = 0; i < this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length; i++) {
                  if (item.name === this.layoutInfo.listLayoutDetails.searchProperties[1].properties[i].name) {
                    if(this.layoutInfo.listLayoutDetails.searchProperties[1].properties[i].values)
                    {
                      for (var j = 0; j < this.layoutInfo.listLayoutDetails.searchProperties[1].properties[i].values.length; j++) {
                        for (var k = 0; k < this.pickListSource.length; k++) {
                          if (this.layoutInfo.listLayoutDetails.searchProperties[1].properties[i].values[j].value === this.pickListSource[k].text) {
                            this.pickListSource[k].isChecked = true;
                          }
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
        this.displaySimpleDetailsItem = true;
      }
      else {
        this.displaySimpleDetailsItem = false;
      }

    }
  }


  removeItem() {
    this.displaySimpleDetailsItem = false;
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
      for (var k = 0; k < this.simpleSearchSource.length; k++) {
        if (this.simpleSearchSource[k].name == this.selectedItemFromAddedList.name) {
          this.simpleSearchSource[k].isAdded = false;
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

  setDefaultStatusValue(item) {
    for (var k = 0; k < this.pickListSource.length; k++) {
      this.pickListSource[k].isEnabled = false;
      if (this.pickListSource[k].internalId === item.internalId) {
        this.pickListSource[k].isEnabled = true;
        //if (this.layoutInfo && this.layoutInfo.listLayoutDetails.searchProperties && this.layoutInfo.listLayoutDetails.searchProperties.length > 0)
        //this.layoutInfo.listLayoutDetails.searchProperties[1].properties[0].defaultValue = item.text;
      }
    }

    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].name) {
        // this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].defaultValue = item.text;
        this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].defaultValue = item.internalId;
      }
    }
  }

  onChangeEvent(ev) {
    //this.layoutInfo.listLayoutDetails.searchProperties[1].properties[0].controlType = ev.target.value;
    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].name) {
        this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].controlType = ev.target.value;
      }
    }
  }

  onCheckboxChecked(ev, selecteditem) {
    this.isdataexistsvalues = false;
    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[1].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].name) {
        if (this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values === undefined) {
          this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values = [];
          var myObj: KeyValue = {
            id : selecteditem.internalId,
            value : selecteditem.text
        };
         
          this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values.push(myObj);
        }
        else {
          for (var i = 0; i < this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values.length; i++) {
            if (this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values[i].value === selecteditem.text) {
              this.isdataexistsvalues = true;
              this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values.splice(i, 1);
            }
          }
          if (this.isdataexistsvalues === false) {
            var myObj: KeyValue = {
              id : selecteditem.internalId,
              value : selecteditem.text
          };

            this.layoutInfo.listLayoutDetails.searchProperties[1].properties[k].values.push(myObj);
          }
        }
      }
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

  public resetAvailableFilter(){
    this.searchText='';
  }

  public resetItemFilter(){
    this.addedItemToMainList.name='';
  }
 
}
