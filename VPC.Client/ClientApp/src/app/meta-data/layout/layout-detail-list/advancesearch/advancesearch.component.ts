import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutService } from '../../layout.service';
import { MetadataService } from '../../../metadata.service';
import { LayoutModel } from '../../../../model/layoutmodel';
//import { KeyValue } from 'src/app/model/properties';
import { KeyValue } from '../../../../model/keyvalue';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-advancesearch',
  templateUrl: './advancesearch.component.html',
  styleUrls: ['./advancesearch.component.css']
})
export class AdvanceSearchComponent implements OnInit {

  public advanceSearchSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private pickListSource: any;
  private values: any;
  private name: string;
  private isdatamainlist: boolean = false;
  public displaySimpleDetailsItem: boolean = false;
  private simpleSearchControlType: string;
  private simpleSearchDefaultValue: string;
  private isdataexistsvalues: boolean = false;
  public layoutInfo: LayoutModel = new LayoutModel();
  public resource: Resource;
  constructor(
    private layoutservice: LayoutService,
    private activatedRoute: ActivatedRoute,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.advanceSearchSource = [];
    this.pickListSource = [];
    this.values = [];
    this.addedItemToMainList = this.layoutInfo.listLayoutDetails.searchProperties[2].properties;
    this.resource = this.globalResourceService.getGlobalResources();
    this.activatedRoute.parent.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getMetadataFieldsByName(this.name);
    });

    this.addedItemToMainList.forEach(item => {
      item.isRowSelected = '';
    });

    if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
      this.addedItemToMainList[0].isRowSelected = true;
      this.processSelectedField(this.addedItemToMainList[0]);
    }
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.fields) {
            for (var k = 0; k < data.fields.length; k++) {
              if (data.fields[k].applicableForAdvanceSearch) {
                this.advanceSearchSource.push(data.fields[k]);

                //for left side gray
                for (var i = 0; i < this.addedItemToMainList.length; i++) {
                  for (var j = 0; j < this.advanceSearchSource.length; j++) {
                    if (this.addedItemToMainList[i].name === this.advanceSearchSource[j].name) {
                      this.advanceSearchSource[j].isRowSelected = true;
                      this.advanceSearchSource[j].isAdded = true;
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

  public mainItemClickEvent(item): void {
    if (item != null && (typeof item.isAdded === "undefined" || !item.isAdded)) {
      this.selectedItemFromMainList = item;
      this.resetOthers(this.advanceSearchSource);
      this.selectedItemFromMainList.isRowSelected = true;
    }
  }

  public resetOthers(array): void {
    if (array != null) {
      for (var k = 0; k < array.length; k++) {
        array[k].isRowSelected = false;
      }
    }
  }


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
        this.processSelectedField(this.selectedItemFromMainList);
      }
    }
  }

  slectedItemClickEvent(item): void {
    this.processSelectedField(item);
  }

  private processSelectedField(item): void {
    if (item != null) {
      this.selectedItemFromAddedList = item;
      this.resetOthers(this.addedItemToMainList);
      this.selectedItemFromAddedList.isRowSelected = true;

      for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[2].properties.length; k++) {
        if (item.name === this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].name) {
          this.simpleSearchControlType = this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].controlType;
          this.simpleSearchDefaultValue = this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].defaultValue;
        }
      }

      if (item.dataType === 'PickList') {
        this.layoutservice.getPickList(item.name)
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

                //new
                for (var i = 0; i < this.layoutInfo.listLayoutDetails.searchProperties[2].properties.length; i++) {
                  if (item.name === this.layoutInfo.listLayoutDetails.searchProperties[2].properties[i].name) {
                    if (this.layoutInfo.listLayoutDetails.searchProperties[2].properties[i].values) {
                      for (var j = 0; j < this.layoutInfo.listLayoutDetails.searchProperties[2].properties[i].values.length; j++) {
                        for (var k = 0; k < this.pickListSource.length; k++) {
                          if (this.layoutInfo.listLayoutDetails.searchProperties[2].properties[i].values[j].value === this.pickListSource[k].text) {
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
      for (var k = 0; k < this.advanceSearchSource.length; k++) {
        if (this.advanceSearchSource[k].name == this.selectedItemFromAddedList.name) {
          this.advanceSearchSource[k].isAdded = false;
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
      }
    }

    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[2].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].name) {
        this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].defaultValue = item.internalId;
      }
    }
  }

  onChangeEvent(ev) {
    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[2].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].name) {
        this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].controlType = ev.target.value;
      }
    }
  }

  onCheckboxChecked(ev, selecteditem) {
    this.isdataexistsvalues = false;
    for (var k = 0; k < this.layoutInfo.listLayoutDetails.searchProperties[2].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].name) {
        if (this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values === undefined) {
          this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values = [];
          var myObj: KeyValue = {
            id: selecteditem.internalId,
            value: selecteditem.text
          };

          this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values.push(myObj);
        }
        else {
          for (var i = 0; i < this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values.length; i++) {
            if (this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values[i].value === selecteditem.text) {
              this.isdataexistsvalues = true;
              this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values.splice(i, 1);
            }
          }
          if (this.isdataexistsvalues === false) {
            var myObj: KeyValue = {
              id: selecteditem.internalId,
              value: selecteditem.text
            };
            this.layoutInfo.listLayoutDetails.searchProperties[2].properties[k].values.push(myObj);
          }
        }
      }
    }
  }

  private reOrderMainList() {
    let a = 0;
    this.addedItemToMainList.forEach((item) => {
      item.sequence = a;
      a++;
    });
  }

}
