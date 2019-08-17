import { Component, OnInit } from '@angular/core';
import { LayoutModel } from '../../../../model/layoutmodel';
import { PicklistService } from '../../../picklist.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { ActiveValue } from '../../../../model/activevalue';
import { GlobalResourceService } from '../../../../global-resource/global-resource.service';
import { Resource } from '../../../../model/resource';

@Component({
  selector: 'app-list-fields',
  templateUrl: './list-fields.component.html',
  styleUrls: ['./list-fields.component.css']
})
export class ListFieldsComponent implements OnInit {
  public fieldSource: any;
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
  public clickableColumn: string = '';

  public searchText: string;
  public resource: Resource;
  public clickableList: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private picklistService: PicklistService,
    private globalResourceService: GlobalResourceService,




  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.clickableList = [];
    this.addedItemToMainList = this.layoutInfo.listLayoutDetails.fields;
    this.pickListSource = [];



    this.activatedRoute.parent.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      this.getPicklistFieldsByName(this.name);
    });

    this.addedItemToMainList.forEach((item) => {
      if (!item.hidden) {
        this.clickableList.push(item);
      }
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
            //this.fieldSource = data.fields.filter(x => x.isQueryable);
            //this.fieldSource =  data.fields.filter(f => (f.accessibleLayoutTypes === undefined || f.accessibleLayoutTypes === null) || f.accessibleLayoutTypes.find(e => e === 3));;
            this.fieldSource = data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 3)));

            //for left side gray
            for (var i = 0; i < this.addedItemToMainList.length; i++) {
              for (var j = 0; j < this.fieldSource.length; j++) {
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
      for (var k = 0; k < array.length; k++) {
        array[k].isRowSelected = false;
      }
    }
  }

  public addFieldItemToSelected() {
    if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
      this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;

      this.addedItemToMainList.push(this.selectedItemFromMainList);
      this.clickableList.push(this.selectedItemFromMainList);
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

      for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
        if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
          this.isChecked = this.layoutInfo.listLayoutDetails.fields[i].hidden;
          //this.isClickableChecked = this.layoutInfo.listLayoutDetails.fields[i].clickable;
        }

        //set clickable column
        if (this.layoutInfo.listLayoutDetails.fields[i].clickable == true) {
          this.clickableColumn = this.layoutInfo.listLayoutDetails.fields[i].name;
        }
      }

      if (item.dataType === 'PickList') {
        //get picklist values
        //var name = this.getLastValue(item.name);
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
        //       if (data != null && data.length > 0) {             
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
    var arr = name.split(".");
    return arr[arr.length - 1];
    // string picklistValue = "";

    // if(name != null)
    // {
    //     int i = name.IndexOf('.');

    //     if(i > -1)
    //     {
    //          value = name.Split('.');   
    //     }
    // }

    // if(value != null && value.Length > 0)
    // {
    //     picklistValue = Convert.ToString(value[1]);
    // }  
    // else
    // {
    //     picklistValue = name;
    // }

  }

  private slectedFieldClick(item): void {
    this.processSelectedField(item);
  }

  public removeItemFromSelectedField() {
    if (this.selectedItemFromAddedList !== null && this.selectedItemFromAddedList !== undefined && this.selectedItemFromAddedList.name !== 'InternalId') {
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
        for (var k = 0; k < this.fieldSource.length; k++) {
          if (this.fieldSource[k].name == this.selectedItemFromAddedList.name) {
            this.fieldSource[k].isAdded = false;
          }
        }

        var clickableIndex = this.clickableList.indexOf(this.selectedItemFromAddedList);
        if (clickableIndex != -1) {
          this.clickableList.splice(clickableIndex, 1);
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
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      if (this.layoutInfo.listLayoutDetails.fields[i].name === this.selectedItemForColor) {
        //this.layoutInfo.listLayoutDetails.fields[i].defaultValue = data.text;
        //this.layoutInfo.listLayoutDetails.fields[i].properties = color;
        if (this.layoutInfo.listLayoutDetails.fields[i].values === undefined) {
          this.layoutInfo.listLayoutDetails.fields[i].values = [];
          var myObj: ActiveValue = {
            id: data.internalId,
            value: color
          };

          this.layoutInfo.listLayoutDetails.fields[i].values.push(myObj);
        }
        else {
          for (var j = 0; j < this.layoutInfo.listLayoutDetails.fields[i].values.length; j++) {
            if (this.layoutInfo.listLayoutDetails.fields[i].values[j].id === data.internalId) {
              this.isdataexistsvalues = true;
              this.layoutInfo.listLayoutDetails.fields[i].values[j].id = data.internalId;
              this.layoutInfo.listLayoutDetails.fields[i].values[j].value = color;
            }
          }
          if (this.isdataexistsvalues === false) {
            var myObj: ActiveValue = {
              id: data.internalId,
              value: color
            };

            this.layoutInfo.listLayoutDetails.fields[i].values.push(myObj);
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
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
        this.layoutInfo.listLayoutDetails.fields[i].hidden = ev.target.checked;

        if (ev.target.checked === true) {
          this.layoutInfo.listLayoutDetails.fields[i].clickable = false;

          var a: number = this.clickableList.findIndex(i => i.name === this.selectedItemFromAddedList.name);
          if (a > -1) {
            this.clickableList.splice(a, 1);
          }
        }
        else {

          var clickableIndex = this.clickableList.indexOf(this.selectedItemFromAddedList);
          if (clickableIndex == -1) {
            this.clickableList.push(this.selectedItemFromAddedList);
          }          
          this.reOrderClickableList();
        }
      }
    }
  }

  // onClickableEvent(ev) {
  //   for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
  //     this.layoutInfo.listLayoutDetails.fields[i].clickable = false;
  //     if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
  //       this.layoutInfo.listLayoutDetails.fields[i].clickable = ev.target.checked;

  //       if (ev.target.checked === true) {
  //         this.layoutInfo.listLayoutDetails.fields[i].hidden = false;
  //       }
  //     }
  //   }
  // }

  setCurrentColor(color) {
    let setcolor = 'text-less-important';
    if (color) {
      setcolor = color;
    }
    return setcolor;
  }

  onViewChange(ev) {
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
        this.layoutInfo.listLayoutDetails.fields[i].defaultView = ev.target.value;
      }
    }
  }

  public onChangeClickable(ev) {
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      this.layoutInfo.listLayoutDetails.fields[i].clickable = false;
      if (this.layoutInfo.listLayoutDetails.fields[i].name === ev.target.value) {
        if (this.layoutInfo.listLayoutDetails.fields[i].clickable == false) {
          this.layoutInfo.listLayoutDetails.fields[i].clickable = true;
        }
        else {
          this.layoutInfo.listLayoutDetails.fields[i].clickable = false;
        }

        if (this.layoutInfo.listLayoutDetails.fields[i].clickable == true) {
          this.layoutInfo.listLayoutDetails.fields[i].hidden = false;
        }

      }
    }
  }

  // public onFieldSearch(query: string) {
  //   this.fieldSource.pipe(
  //     map(items => {
  //       this.fieldSource = items.filter(items => items === query);
  //     }, error => error)
  //   );
  // }

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

  private reOrderClickableList() {
    let a = 0;
    this.clickableList.forEach((item) => {
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
