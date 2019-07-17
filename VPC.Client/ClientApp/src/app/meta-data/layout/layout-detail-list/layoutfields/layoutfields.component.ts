
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutService } from '../../layout.service';
import { MetadataService } from '../../../metadata.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { ActiveValue } from '../../../../model/activevalue';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';
@Component({
  selector: 'app-layoutfields',
  templateUrl: './layoutfields.component.html',
  styleUrls: ['./layoutfields.component.css'],
})


export class LayoutfieldsComponent implements OnInit {
  public fieldSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private pickListSource: Array<any> = [];
  private name: string;
  private maxResult: string = '';
  private defaultSortOrderValue: number;
  private defaultSortOrderName: string;
  private isdatamainlist: boolean = false;
  public displayFieldsmodal: boolean = false;
  private displaySimpleDetailsItem: boolean = false;
  private selectedItemForColor: string;
  private isChecked: boolean = false;
  //private isClickableChecked: boolean = false;
  private isdataexistsvalues: boolean = false;
  public layoutInfo: LayoutModel = new LayoutModel();
  public clickableColumn: string = '';
  public searchText: any;
  public resource: Resource;
  constructor(
    private layoutService: LayoutService,
    private activatedRoute: ActivatedRoute,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];

    this.addedItemToMainList = [];
    this.addedItemToMainList = this.layoutInfo.listLayoutDetails.fields;
    this.pickListSource = [];
    // console.log(JSON.stringify(this.layoutInfo.listLayoutDetails.fields));
    this.resource = this.globalResourceService.getGlobalResources();
    // this.activatedRoute.parent.url.subscribe((urlPath) => {
    //   this.name = urlPath[urlPath.length - 4].path;
    //   this.getMetadataFieldsByName(this.name);
    // });

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

  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            //   this.fieldSource = [];
            //  // data.fields = data.fields.filter(x => x.isQueryable);
            //   data.fields.forEach(field => {
            //     this.fieldSource.push(field);
            //   });
            //this.fieldSource = data.fields.filter(f => (f.accessibleLayoutTypes === undefined || f.accessibleLayoutTypes === null) || f.accessibleLayoutTypes.find(e => e === 3));;
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


  private mainItemClickEvent(item): void {
    if (item != null && (typeof item.isAdded === "undefined" || !item.isAdded)) {
      this.selectedItemFromMainList = item;
      this.resetOthers(this.fieldSource);
      this.selectedItemFromMainList.isRowSelected = true;
    }
  }

  private resetOthers(array): void {
    if (array != null) {
      for (var k = 0; k < array.length; k++) {
        array[k].isRowSelected = false;
      }
    }
  }

  public addItem(): void {
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
        this.processSelectedField(this.selectedItemFromMainList)
      }
    }
  }


  private slectedFieldClick(item): void {
    this.processSelectedField(item);
  }

  private processSelectedField(item): void {
    // console.log('processSelectedField '+JSON.stringify(item));
    if (item != null) {
      this.selectedItemFromAddedList = item;
      this.resetOthers(this.addedItemToMainList);
      this.selectedItemFromAddedList.isRowSelected = true;

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

      this.displaySimpleDetailsItem = false;
      this.displayFieldsmodal = true;

      if (item.dataType === 'PickList') {
        //debugger;
        //console.log('active'+JSON.stringify(item));
        let name = (item.typeOf != null) ? item.typeOf : item.name;
        this.layoutService.getPickList(name)
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
        this.displaySimpleDetailsItem = true;
      }
    }
  }

  public removeItem(): void {
    if (this.selectedItemFromAddedList !== null && this.selectedItemFromAddedList !== undefined && this.selectedItemFromAddedList.name !== 'InternalId') {
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
        for (var k = 0; k < this.fieldSource.length; k++) {
          if (this.fieldSource[k].name == this.selectedItemFromAddedList.name) {
            this.fieldSource[k].isAdded = false;
          }
        }

        this.reOrderMainList();
      }
    }
  }

  // public upItem() {
  //   if (this.selectedItemFromAddedList) {
  //     for (var i = 0; i < this.addedItemToMainList.length; i++) {
  //       if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
  //         for (var j = 0; j < this.addedItemToMainList.length; j++) {
  //           if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence - 1)) {
  //             this.addedItemToMainList[j].sequence += 1;
  //             break;
  //           }
  //         }
  //         this.addedItemToMainList[i].sequence -= 1;
  //         break;
  //       }
  //     }

  //     this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
  //       return a.sequence - b.sequence;
  //     });
  //   }
  // }

  private reOrderMainList() {
    let a = 0;
    this.addedItemToMainList.forEach((item) => {
      item.sequence = a;
      a++;
    });
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

  // public downItem() {
  //   if (this.selectedItemFromAddedList) {

  //     for (var i = 0; i < this.addedItemToMainList.length; i++) {
  //       if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
  //         for (var j = 0; j < this.addedItemToMainList.length; j++) {
  //           if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence + 1)) {
  //             this.addedItemToMainList[j].sequence -= 1;
  //             break;
  //           }
  //         }
  //         this.addedItemToMainList[i].sequence += 1;
  //         break;
  //       }
  //     }

  //     this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
  //       return a.sequence - b.sequence;
  //     });

  //   }
  // }

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

  private setFieldColor(data, color): void {
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      if (this.layoutInfo.listLayoutDetails.fields[i].name === this.selectedItemForColor) {
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

  private onChangeEvent(ev): void {
    for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
      if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
        this.layoutInfo.listLayoutDetails.fields[i].hidden = ev.target.checked;

        if (ev.target.checked === true) {
          this.layoutInfo.listLayoutDetails.fields[i].clickable = false;
        }
      }
    }
  }

  // private onClickableEvent(ev): void {
  //   for (var i = 0; i < this.layoutInfo.listLayoutDetails.fields.length; i++) {
  //     this.layoutInfo.listLayoutDetails.fields[i].clickable = false;
  //     if (this.selectedItemFromAddedList.name === this.layoutInfo.listLayoutDetails.fields[i].name) {
  //       this.layoutInfo.listLayoutDetails.fields[i].clickable = ev.target.checked;

  //       if(ev.target.checked === true)
  //       {
  //         this.layoutInfo.listLayoutDetails.fields[i].hidden = false;
  //       }
  //     }
  //   }
  // }

  private setCurrentColor(color) {
    let setcolor = 'text-less-important';
    if (color) {
      setcolor = color;
    }
    return setcolor;
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
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
