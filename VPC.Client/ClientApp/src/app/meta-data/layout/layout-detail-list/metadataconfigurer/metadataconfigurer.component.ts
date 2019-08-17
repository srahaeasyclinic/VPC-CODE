
import { Component, OnInit, Input, SimpleChanges, SimpleChange } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutService } from '../../layout.service';
import { MetadataService } from '../../../metadata.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';
import { KeyValue } from '../../../../model/keyvalue';
import { ActiveValue } from 'src/app/model/activevalue';
@Component({
  selector: 'app-metadataconfigurer',
  templateUrl: './metadataconfigurer.component.html',
  styleUrls: ['./metadataconfigurer.component.css'],
})


export class MetaDataConfigurer implements OnInit {
  @Input() pageInfo =
    {
      config:
      {
        allowConfiguration: false, displaySortColumn: false, direction: false, maxResult: false,
        groupBy: false, clickableColumn: false
      }
      , selectedLayout: {},
      addedItemToMainList: [],
      fieldSource: [],
      type: ""

    }
  public selectedLayout: any;
  public addedItemToMainList: any;
  public fieldSource: any;
  public type: any
  private selectedItemFromMainList: any;
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
  public clickableColumn: string = '';
  public searchText: string = '';
  public resource: Resource;
  public clickableList: any = []
  config: { allowConfiguration: boolean; displaySortColumn: boolean; direction: boolean; maxResult: boolean; groupBy: boolean; clickableColumn: boolean; };
  simpleSearchDefaultValue: any;
  simpleSearchControlType: any;
  private selectedTask: string;
  private toolbarGroup: string;

  public fieldColor = [

    {
      css: 'blue-color-block',
      text: this.getResourceValue("metadata_colour_blue")
    }

    , {
      css: 'green-color-block',
      text: this.getResourceValue("metadata_colour_green")
    },
    {
      css: 'red-color-block',
      text: this.getResourceValue("metadata_colour_red")
    }
    ,
    {
      css: 'gray-color-block',
      text: this.getResourceValue("metadata_colour_gray")
    }
    ,
    {
      css: 'yellow-color-block',
      text: this.getResourceValue("metadata_colour_yellow")
    }
    ,
    {
      css: 'orange-color-block',
      text: this.getResourceValue("metadata_colour_orange")
    }


  ]

  constructor(
    private layoutService: LayoutService,
    private activatedRoute: ActivatedRoute,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }
  ngOnChanges(changes: SimpleChanges) {

    if (changes.pageInfo && changes.pageInfo.previousValue) {
      if (changes.pageInfo.previousValue.type) {
        if (changes.pageInfo.previousValue.type != changes.pageInfo.currentValue.type) {
          this.loadData()
        }
      }
    }

  }
  ngOnInit() {
    this.loadData()
    this.resource = this.globalResourceService.getGlobalResources();
  }
  loadData() {

    this.type = this.pageInfo.type
    this.addedItemToMainList = this.pageInfo.addedItemToMainList
    this.fieldSource = this.pageInfo.fieldSource
    this.config = this.pageInfo.config
    this.selectedLayout = this.pageInfo.selectedLayout

    this.clickableList = [];
    this.pickListSource = [];
    this.displaySimpleDetailsItem = false;
    this.addedItemToMainList.forEach(item => {
      item.isRowSelected = '';
    });
    if (this.pageInfo.config.clickableColumn) {
      this.addedItemToMainList.forEach((item) => {
        if (!item.hidden) {
          this.clickableList.push(item);
        }
      });
    }
    if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
      this.addedItemToMainList[0].isRowSelected = true;
      this.processSelectedField(this.addedItemToMainList[0]);
    } else {
      this.displayFieldsmodal = false;
    }
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
        if (this.pageInfo.config.clickableColumn) {
          this.clickableList.push(this.selectedItemFromMainList);
        }
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
      this.displayFieldsmodal = true;
      if (this.type == 'toolbar') {
        this.selectedTask = item.name;
        this.displayFieldsmodal = false;
        if (item.type === 'task') {
          this.displayFieldsmodal = true;
          for (var i = 0; i < this.selectedLayout.toolbar.length; i++) {
            if (this.selectedLayout.toolbar[i].type === 'task') {
              if (this.selectedTask === this.selectedLayout.toolbar[i].name) {
                this.toolbarGroup = this.selectedLayout.toolbar[i].group;
              }
            }
          }
        }
      } else if (this.type == 'fields' || this.type == 'simplesearch' || this.type == 'advancesearch') {
        this.setDataOnLayoutInfo(item);
        this.displaySimpleDetailsItem = false;
        if (item.dataType === 'PickList') {
          let name = (item.typeOf != null) ? item.typeOf : item.name;
          this.layoutService.getPickList(name)
            .pipe(first())
            .subscribe(
              data => {
                if (data) {
                  this.setData(item, data)
                }
              },
              error => {
                console.log(error);
              });
          this.displaySimpleDetailsItem = true;
        }

        if (this.type == 'simplesearch' || this.type == 'advancesearch' || this.type == 'toolbar') {
          if (item.dataType === 'PickList') {
            this.displayFieldsmodal = true;
          }
          else {
            this.displayFieldsmodal = false;
          }
        }
      }



    }
  }
  setData(item, data) {
    if (this.type == 'fields') {
      this.setPickListForFields(item, data)
    } else if (this.type == 'simplesearch') {
      this.setPickListForSimpleSearch(item, data)
    } else if (this.type == 'advancesearch') {
      this.setPickListForAdvancedSearch(item, data)
    }


  }
  setDataOnLayoutInfo(item) {
    if (this.type == 'fields') {
      for (var i = 0; i < this.selectedLayout.fields.length; i++) {
        if (this.selectedItemFromAddedList.name === this.selectedLayout.fields[i].name) {
          this.isChecked = this.selectedLayout.fields[i].hidden;

        }
        if (this.pageInfo.config.clickableColumn) {
          //set clickable column
          if (this.selectedLayout.fields[i].clickable == true) {
            this.clickableColumn = this.selectedLayout.fields[i].name;
          }
        }
      }
    } else if (this.type == 'simplesearch') {
      for (var k = 0; k < this.selectedLayout.searchProperties[1].properties.length; k++) {
        if (item.name === this.selectedLayout.searchProperties[1].properties[k].name) {
          this.simpleSearchControlType = this.selectedLayout.searchProperties[1].properties[k].controlType;
          this.simpleSearchDefaultValue = this.selectedLayout.searchProperties[1].properties[k].defaultValue;
        }
      }
    } else if (this.type == 'advancesearch') {
      for (var k = 0; k < this.selectedLayout.searchProperties[2].properties.length; k++) {
        if (item.name === this.selectedLayout.searchProperties[2].properties[k].name) {
          this.simpleSearchControlType = this.selectedLayout.searchProperties[2].properties[k].controlType;
          this.simpleSearchDefaultValue = this.selectedLayout.searchProperties[2].properties[k].defaultValue;
        }
      }
    }
  }
  setPickListForFields(item, data) {
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
  setPickListForSimpleSearch(item, data) {
    this.pickListSource = data.result;
    for (var k = 0; k < this.pickListSource.length; k++) {
      this.pickListSource[k].isEnabled = false;
      if (this.pickListSource[k].internalId === this.simpleSearchDefaultValue) {
        this.pickListSource[k].isEnabled = true;
      }
    }

    if (this.selectedLayout.searchProperties[1].properties) {
      for (var i = 0; i < this.selectedLayout.searchProperties[1].properties.length; i++) {
        if (item.name === this.selectedLayout.searchProperties[1].properties[i].name) {
          if (this.selectedLayout.searchProperties[1].properties[i].values) {
            for (var j = 0; j < this.selectedLayout.searchProperties[1].properties[i].values.length; j++) {
              for (var k = 0; k < this.pickListSource.length; k++) {
                if (this.selectedLayout.searchProperties[1].properties[i].values[j].value === this.pickListSource[k].text) {
                  this.pickListSource[k].isChecked = true;
                }
              }
            }
          }
        }
      }
    }
  }
  setPickListForAdvancedSearch(item, data) {
    if (data) {
      this.pickListSource = data.result;
      for (var k = 0; k < this.pickListSource.length; k++) {
        this.pickListSource[k].isEnabled = false;
        if (this.pickListSource[k].internalId === this.simpleSearchDefaultValue) {
          this.pickListSource[k].isEnabled = true;
        }
      }

      //new
      for (var i = 0; i < this.selectedLayout.searchProperties[2].properties.length; i++) {
        if (item.name === this.selectedLayout.searchProperties[2].properties[i].name) {
          if (this.selectedLayout.searchProperties[2].properties[i].values) {
            for (var j = 0; j < this.selectedLayout.searchProperties[2].properties[i].values.length; j++) {
              for (var k = 0; k < this.pickListSource.length; k++) {
                if (this.selectedLayout.searchProperties[2].properties[i].values[j].value === this.pickListSource[k].text) {
                  this.pickListSource[k].isChecked = true;
                }
              }
            }
          }
        }
      }
    }


  }

  public removeItem() {
    if (this.selectedItemFromAddedList !== null && this.selectedItemFromAddedList !== undefined && this.selectedItemFromAddedList.name !== 'InternalId') {
      this.displayFieldsmodal = false;
      var index = this.addedItemToMainList.indexOf(this.selectedItemFromAddedList);
      if (index != -1) {

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
    }
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

  public upItem() {
    if (this.selectedItemFromAddedList) {
      let selectedsequence = 0;

      this.reOrderMainList();
      let deferPositioning = false
      this.addedItemToMainList.forEach((item) => {
        if (item.name === this.selectedItemFromAddedList.name) {
          selectedsequence = item.sequence;
          if (selectedsequence == 0) {
            deferPositioning = true
          } else {
            item.sequence = selectedsequence - 1;
            this.addedItemToMainList[item.sequence].sequence = selectedsequence;
          }

        }
      });
      if (!deferPositioning) {
        this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
      }

    }
  }



  public downItem() {
    if (this.selectedItemFromAddedList) {
      let selectedsequence = 0;
      this.reOrderMainList();
      let deferPositioning = false
      this.addedItemToMainList.forEach((item) => {
        if (item.name === this.selectedItemFromAddedList.name) {
          selectedsequence = item.sequence;
          if (selectedsequence == this.addedItemToMainList.length - 1) {
            deferPositioning = true
          } else {
            item.sequence = selectedsequence + 1;
            this.addedItemToMainList[item.sequence].sequence = selectedsequence;
          }
        }
      });
      if (!deferPositioning) {
        this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
          return a.sequence - b.sequence;
        });
      }
    }
  }

  private setFieldColor(data, color, name): void {
    for (var i = 0; i < this.selectedLayout.fields.length; i++) {
      if (this.selectedLayout.fields[i].name === this.selectedItemForColor) {
        if (this.selectedLayout.fields[i].values === undefined) {
          this.selectedLayout.fields[i].values = [];
          var myObj: ActiveValue = {
            id: data.internalId,
            value: color,
            name: name
          };

          this.selectedLayout.fields[i].values.push(myObj);
        }
        else {
          for (var j = 0; j < this.selectedLayout.fields[i].values.length; j++) {
            if (this.selectedLayout.fields[i].values[j].id === data.internalId) {
              this.isdataexistsvalues = true;
              this.selectedLayout.fields[i].values[j].id = data.internalId;
              this.selectedLayout.fields[i].values[j].value = color;
              this.selectedLayout.fields[i].values[j].name = name;
            }
          }
          if (this.isdataexistsvalues === false) {
            var myObj: ActiveValue = {
              id: data.internalId,
              value: color,
              name: name
            };

            this.selectedLayout.fields[i].values.push(myObj);
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

    if (this.type == 'toolbar') {
      for (var i = 0; i < this.selectedLayout.toolbar.length; i++) {
        if (this.selectedLayout.toolbar[i].type === 'task') {
          if (this.selectedTask === this.selectedLayout.toolbar[i].name) {
            this.selectedLayout.toolbar[i].group = ev.target.value;
          }
        }
      }
    }
    else if (this.type == 'advancesearch' || this.type == 'simplesearch') {
      var resolve = {
        'simplesearch': this.selectedLayout.searchProperties[1],
        'advancesearch': this.selectedLayout.searchProperties[2]
      }
      for (var k = 0; k < resolve[this.type].properties.length; k++) {
        if (this.selectedItemFromAddedList.name === resolve[this.type].properties[k].name) {
          resolve[this.type].properties[k].controlType = ev.target.value;
        }
      }
    }
    else {
      for (var i = 0; i < this.selectedLayout.fields.length; i++) {
        if (this.selectedItemFromAddedList.name === this.selectedLayout.fields[i].name) {
          this.selectedLayout.fields[i].hidden = ev.target.checked;

          if (ev.target.checked === true) {
            this.selectedLayout.fields[i].clickable = false;
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

  }



  private setCurrentColor(color) {
    let setcolor = 'text-less-important';
    if (color) {
      setcolor = color;
    }
    return setcolor;
  }

  public onChangeClickable(ev) {
    for (var i = 0; i < this.selectedLayout.fields.length; i++) {
      this.selectedLayout.fields[i].clickable = false;
      if (this.selectedLayout.fields[i].name === ev.target.value) {
        if (this.selectedLayout.fields[i].clickable == false) {
          this.selectedLayout.fields[i].clickable = true;
        }
        else {
          this.selectedLayout.fields[i].clickable = false;
        }

        if (this.selectedLayout.fields[i].clickable == true) {
          this.selectedLayout.fields[i].hidden = false;
        }

      }
    }
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  public resetAvailableFilter() {
    this.searchText = '';
  }

  public resetItemFilter() {
    this.addedItemToMainList.name = '';
  }
  // -------------------------------------
  // simplesearch && advanced search
  onCheckboxChecked(ev, selecteditem) {
    var resolve = {
      'simplesearch': this.selectedLayout.searchProperties[1],
      'advancesearch': this.selectedLayout.searchProperties[2]
    }


    this.isdataexistsvalues = false;
    for (var k = 0; k < resolve[this.type].properties.length; k++) {
      if (this.selectedItemFromAddedList.name === resolve[this.type].properties[k].name) {
        if (resolve[this.type].properties[k].values === undefined) {
          resolve[this.type].properties[k].values = [];
          var myObj: KeyValue = {
            id: selecteditem.internalId,
            value: selecteditem.text
          };
          resolve[this.type].properties[k].values.push(myObj);
        }
        else {
          for (var i = 0; i < resolve[this.type].properties[k].values.length; i++) {
            if (resolve[this.type].properties[k].values[i].value === selecteditem.text) {
              this.isdataexistsvalues = true;
              resolve[this.type].properties[k].values.splice(i, 1);
            }
          }
          if (this.isdataexistsvalues === false) {
            var myObj: KeyValue = {
              id: selecteditem.internalId,
              value: selecteditem.text
            };
            resolve[this.type].properties[k].values.push(myObj);
          }
        }
      }
    }
  }
  setDefaultStatusValue(item) {
    for (var k = 0; k < this.pickListSource.length; k++) {
      this.pickListSource[k].isEnabled = false;
      if (this.pickListSource[k].internalId === item.internalId) {
        this.pickListSource[k].isEnabled = true;
      }
    }
  }
}
