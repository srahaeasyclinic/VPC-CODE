
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { MetadataService } from '../../../metadata.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-intializemetadataconfigurer',
  templateUrl: './intializemetadataconfigurer.component.html',
  styleUrls: ['./intializemetadataconfigurer.component.css'],
})
export class IntializeMetadataConfigurer implements OnInit {
  // pageInfo = { allowConfiguration: true, displaySortColumn: true, direction: true, maxResult: true, groupBy: true, clickableColumn: true }

  pageInfo:
    {
      config:
      {
        allowConfiguration: boolean, displaySortColumn: boolean, direction: boolean, maxResult: boolean,
        groupBy: boolean, clickableColumn: boolean
      }
      , selectedLayout: {},
      addedItemToMainList: [],
      fieldSource: [],
      type: string

    }

  layoutInfo: any;
  name: string;
  fieldSource: any = [];
  addedItemToMainList: any = [];
  type: string = "";
  config: { allowConfiguration: boolean; displaySortColumn: boolean; direction: boolean; maxResult: boolean; groupBy: boolean; clickableColumn: boolean; };
  layoutType: string;
  selectedLayout: any;
  dataLoad = false;

  constructor(private metadataService: MetadataService,
    private activatedRoute: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      if (this.type != params.type) {
        this.loadData()
      }
    });
    this.loadData()
  }

  loadData() {
    //debugger;
    this.dataLoad = false;
    this.type = this.activatedRoute.snapshot.url[0].path
    var arraytype = ['fields', 'textsearch', 'simplesearch', 'advancesearch', 'toolbar', 'toolbars', 'action', 'actions']
    this.type = (this.type == 'toolbars') ? 'toolbar' : this.type
    this.type = (this.type == 'actions') ? 'action' : this.type
    if (arraytype.includes(this.type)) {
      this.activatedRoute.parent.parent.parent.url.subscribe((urlPath1) => {
        this.name = urlPath1[0].path;
        this.getMetadataFieldsByName(this.name);
      });
    } else {
      this.location.back();
    }
  }
  setData(data) {
    this.activatedRoute.parent.url.subscribe((urlPath) => {
      this.layoutType = urlPath[0].path;
      this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
      this.selectedLayout = this.layoutType == "view" ? this.layoutInfo.viewLayoutDetails : this.layoutInfo.listLayoutDetails
      this.fieldSource = []
      this.addedItemToMainList = []
      if (this.type == 'fields') {
        this.addedItemToMainList = this.selectedLayout.fields;
        this.manipulateFieldData(data)
        if(data.versionControl !=null && data.versionControl !=undefined)
            this.manipulateFieldData(data.versionControl);
        // this.config = {
        //   allowConfiguration: true, displaySortColumn: true, direction: true, maxResult: true, groupBy: true, clickableColumn: true
        // }
        this.config = this.layoutType == "view" ? {
          allowConfiguration: true, displaySortColumn: true, direction: true, maxResult: false, groupBy: false, clickableColumn: true
        } : {
            allowConfiguration: true, displaySortColumn: true, direction: true, maxResult: true, groupBy: true, clickableColumn: true
          }
      } else if (this.type == 'textsearch') {
        this.addedItemToMainList = this.selectedLayout.searchProperties[0].properties;
        this.manipulateFreeTextSearchData(data)
        this.config = { allowConfiguration: false, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
      }
      else if (this.type == 'simplesearch') {
        this.addedItemToMainList = this.selectedLayout.searchProperties[1].properties;
        this.manipulateSimpleSearchData(data)
        this.config = { allowConfiguration: true, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
      }
      else if (this.type == 'advancesearch') {
        this.addedItemToMainList = this.selectedLayout.searchProperties[2].properties;
        this.manipulateAdvancedSearchData(data)
        this.config = { allowConfiguration: true, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
      }
      else if (this.type == 'toolbar') {
        this.addedItemToMainList = this.selectedLayout.toolbar;
        this.manipulateToolBar(data)
        this.config = { allowConfiguration: true, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
      }
      else if (this.type == 'action') {
        this.addedItemToMainList = this.selectedLayout.actions;
        this.manipulateActionData(data)
        this.config = { allowConfiguration: false, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
      }
      //for left side gray
      this.setFieldSourceProperties()
      this.setPageInfo()
    })

  }
  setPageInfo() {

    this.pageInfo = {
      config: this.config,
      selectedLayout: this.selectedLayout,
      addedItemToMainList: this.addedItemToMainList,
      fieldSource: this.fieldSource,
      type: this.type
    }
    this.dataLoad = true;
  }
  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.setData(data)

          }
        },
        error => {
          console.log(error);
        });
  }

  setFieldSourceProperties() {
    for (var i = 0; i < this.addedItemToMainList.length; i++) {
      for (var j = 0; j < this.fieldSource.length; j++) {
        if (this.addedItemToMainList[i].name === this.fieldSource[j].name) {
          this.fieldSource[j].isRowSelected = true;
          this.fieldSource[j].isAdded = true;
        }
      }
    }
  }
  manipulateAdvancedSearchData(data) {
    if (data && data.fields) {
      for (var k = 0; k < data.fields.length; k++) {
        if (data.fields[k].applicableForAdvanceSearch) {
          this.fieldSource.push(data.fields[k]);
        }
      }
    }
  }
  manipulateFreeTextSearchData(data) {
    if (data && data.fields) {
      for (var k = 0; k < data.fields.length; k++) {
        if (data.fields[k].applicableForFreeTextSearch) {
          this.fieldSource.push(data.fields[k]);
        }
      }
    }
  }
  manipulateFieldData(data) {
    var fileds=data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 3)));
    for (var k = 0; k < fileds.length; k++) {
      this.fieldSource.push(fileds[k]);
    }

    //this.fieldSource.p(data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 3))));
  }
  manipulateActionData(data) {
    if (data.operations) {
      for (var k = 0; k < data.operations.length; k++) {
        this.fieldSource.push(data.operations[k]);
      }
    }
  }
  manipulateSimpleSearchData(data) {
    for (var k = 0; k < data.fields.length; k++) {
      if (data.fields[k].applicableForSimpleSearch) {
        this.fieldSource.push(data.fields[k]);
      }
    }

  }
  manipulateToolBar(data) {
    if (data.operations) {
      for (var k = 0; k < data.operations.length; k++) {
        this.fieldSource.push(data.operations[k]);
      }
    }
    if (data.tasks) {
      for (var k = 0; k < data.tasks.length; k++) {
        this.fieldSource.push(data.tasks[k]);
      }
    }

  }

}
