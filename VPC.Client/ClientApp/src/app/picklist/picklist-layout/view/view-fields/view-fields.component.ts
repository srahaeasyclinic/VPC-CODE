import { Component, OnInit } from '@angular/core';
import { LayoutModel } from '../../../../model/layoutmodel';
import { PicklistService } from '../../../picklist.service';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { ActiveValue } from '../../../../model/activevalue';
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';
import { Resource } from '../../../../model/resource';


@Component({
  selector: 'app-view-fields',
  templateUrl: './view-fields.component.html',
  styleUrls: ['./view-fields.component.css']
})
export class ViewFieldsComponent implements OnInit {
  public fieldSource: Array<any> = [];
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private name: string;
  private displaySimpleDetailsItem: boolean = false;
  public isShowFieldConfiguration: boolean = false;
  private isChecked: boolean = false;
  //private isClickableChecked: boolean = false;
  private selectedItemForColor: string;
  private pickListSource: Array<any> = [];
  private isdataexistsvalues: boolean = false;
  public layoutInfo: LayoutModel = new LayoutModel();
  private listsource: Array<any> = [];
  private displayView: boolean = false;
  private clickableColumn: string = '';
  public searchText: string;
  public resource: Resource;

  constructor(
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService,


  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();

    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.addedItemToMainList = this.layoutInfo.viewLayoutDetails.fields;
    this.pickListSource = [];


    this.activatedRoute.parent.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getPicklistFieldsByName(this.name);
    });

    if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
      this.addedItemToMainList[0].isRowSelected = true;
      this.processSelectedField(this.addedItemToMainList[0]);
    }
  }

  private getPicklistFieldsByName(name) {
    this.picklistService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.fields) {
            // this.fieldSource = data.fields.filter(x => x.isQueryable);

            //   this.fieldSource = data.fields.filter(f => (f.accessibleLayoutTypes === undefined && f.accessibleLayoutTypes === null) || f.accessibleLayoutTypes.find(e => e === 1));

            this.fieldSource = data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 1)));

            //for left side gray
            for (let i = 0; i < this.addedItemToMainList.length; i++) {
              for (let j = 0; j < this.fieldSource.length; j++) {
                if (this.addedItemToMainList[i].name === this.fieldSource[j].name) {
                  this.fieldSource[j].isRowSelected = true;
                  this.fieldSource[j].isAdded = true;
                }
              }
            }
          }
        },
        error => {
          console.log(error);
        });
  }

  private availableFieldsClick(item): void {
    if (item != null || !item.isAdded) {
      this.selectedItemFromMainList = item;
      this.resetOthers(this.fieldSource);
      this.selectedItemFromMainList.isRowSelected = true;
    }
  }

  private resetOthers(array) {
    if (array != null) {
      for (let k = 0; k < array.length; k++) {
        array[k].isRowSelected = false;
      }
    }
  }

  public addFieldItemToSelected() {
    if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
      this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;
      this.addedItemToMainList.push(this.selectedItemFromMainList);
      this.reOrderMainList();
      this.selectedItemFromMainList.isAdded = true;
      this.processSelectedField(this.selectedItemFromMainList)
    }
  }

  private processSelectedField(item): void {
    if (item != null) {
      this.selectedItemFromAddedList = item;
      this.resetOthers(this.addedItemToMainList);
      this.selectedItemFromAddedList.isRowSelected = true;
      this.displaySimpleDetailsItem = false;
      this.displayView = false;
      this.isShowFieldConfiguration = true;
      this.isChecked = false;

      for (let i = 0; i < this.layoutInfo.viewLayoutDetails.fields.length; i++) {
        if (this.selectedItemFromAddedList.name === this.layoutInfo.viewLayoutDetails.fields[i].name) {
          this.isChecked = this.layoutInfo.viewLayoutDetails.fields[i].hidden;
        }

        //set clickable column
        if (this.layoutInfo.viewLayoutDetails.fields[i].clickable == true) {
          this.clickableColumn = this.layoutInfo.viewLayoutDetails.fields[i].name;
        }
      }

      if (item.dataType === 'PickList') {
        //get picklist values
        //let name = this.getLastValue(item.name);
        let name = (item.typeOf != null) ? item.typeOf : item.name;
        this.picklistService.getPickListValues(name)
          .pipe(first())
          .subscribe(
            data => {
              if (data) {
                this.selectedItemForColor = item.name;
                this.pickListSource = data.result;
                if (item.values) {
                  item.values.forEach(values => {
                    this.pickListSource.forEach(picklist => {
                      if (values.id == picklist.internalId) {
                        picklist.color = values.value;
                      }
                    });
                  });
                }
              }
            },
            error => {
              console.log(error);
            });

        //get views
        // this.picklistService.getPickListViews(name)
        //   .pipe(first())
        //   .subscribe(
        //     data => {
        //       if (data) {             
        //         this.listsource = data;
        //         this.displayView = true;                
        //       }
        //     },
        //     error => {
        //       console.log(error);
        //     });

        this.displaySimpleDetailsItem = true;
      }

    }
  }
  getLastValue(name: any): any {
    let arr = name.split(".");
    return arr[arr.length - 1];
  }

  private slectedFieldClick(item): void {
    this.processSelectedField(item);
  }

  public removeItemFromSelectedField() {
    if (this.selectedItemFromAddedList !== null && this.selectedItemFromAddedList !== undefined && this.selectedItemFromAddedList.name !== 'InternalId') {
      let index = this.addedItemToMainList.indexOf(this.selectedItemFromAddedList);
      if (index != -1) {
        // for (let i = 0; i < this.addedItemToMainList.length; i++) {
        //   if (this.addedItemToMainList[i].name != this.selectedItemFromAddedList.name) continue;
        //   for (let j = 0; j < this.addedItemToMainList.length; j++) {
        //     if (this.addedItemToMainList[j].sequence > this.addedItemToMainList[i].sequence) {
        //       this.addedItemToMainList[j].sequence -= 1;
        //       break;
        //     }
        //   }
        // }
        this.addedItemToMainList.splice(index, 1);
        for (let k = 0; k < this.fieldSource.length; k++) {
          if (this.fieldSource[k].name === this.selectedItemFromAddedList.name) {
            this.fieldSource[k].isAdded = false;
          }
        }

        this.reOrderMainList();
      }

      this.isShowFieldConfiguration = false;
    }
  }

  public upItem() {
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

  public downItem() {
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

  setFieldColor(data, color) {
    for (let i = 0; i < this.layoutInfo.viewLayoutDetails.fields.length; i++) {
      if (this.layoutInfo.viewLayoutDetails.fields[i].name === this.selectedItemForColor) {
        if (this.layoutInfo.viewLayoutDetails.fields[i].values === undefined) {
          this.layoutInfo.viewLayoutDetails.fields[i].values = [];
          let myObj: ActiveValue = {
            id: data.internalId,
            value: color
          };

          this.layoutInfo.viewLayoutDetails.fields[i].values.push(myObj);
        }
        else {
          for (let j = 0; j < this.layoutInfo.viewLayoutDetails.fields[i].values.length; j++) {
            if (this.layoutInfo.viewLayoutDetails.fields[i].values[j].id === data.internalId) {
              this.isdataexistsvalues = true;
              this.layoutInfo.viewLayoutDetails.fields[i].values[j].id = data.internalId;
              this.layoutInfo.viewLayoutDetails.fields[i].values[j].value = color;
            }
          }
          if (this.isdataexistsvalues === false) {
            let myObj: ActiveValue = {
              id: data.internalId,
              value: color
            };

            this.layoutInfo.viewLayoutDetails.fields[i].values.push(myObj);
          }
        }
      }
    }
    this.pickListSource.forEach(item => {
      if (data.internalId == item.internalId) {
        item.color = color;
      }
    });

  }

  onChangeEvent(ev) {
    for (let i = 0; i < this.layoutInfo.viewLayoutDetails.fields.length; i++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.viewLayoutDetails.fields[i].name) {
        this.layoutInfo.viewLayoutDetails.fields[i].hidden = ev.target.checked;
      }

      if (ev.target.checked === true) {
        this.layoutInfo.viewLayoutDetails.fields[i].clickable = false;
      }
    }
  }

  setCurrentColor(color) {
    let setcolor = 'text-less-important';
    if (color) {
      setcolor = color;
    }
    return setcolor;
  }

  onViewChange(ev) {
    for (let i = 0; i < this.layoutInfo.viewLayoutDetails.fields.length; i++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.viewLayoutDetails.fields[i].name) {
        this.layoutInfo.viewLayoutDetails.fields[i].defaultView = ev.target.value;
      }
    }
  }

  private onChangeClickable(ev) {
    for (let i = 0; i < this.layoutInfo.viewLayoutDetails.fields.length; i++) {
      this.layoutInfo.viewLayoutDetails.fields[i].clickable = false;
      if (this.layoutInfo.viewLayoutDetails.fields[i].name === ev.target.value) {
        if (this.layoutInfo.viewLayoutDetails.fields[i].clickable == false) {
          this.layoutInfo.viewLayoutDetails.fields[i].clickable = true;
        }
        else {
          this.layoutInfo.viewLayoutDetails.fields[i].clickable = false;
        }

        if (this.layoutInfo.viewLayoutDetails.fields[i].clickable == true) {
          this.layoutInfo.viewLayoutDetails.fields[i].hidden = false;
        }

      }
    }
  }
generateResourceName(word) {
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
