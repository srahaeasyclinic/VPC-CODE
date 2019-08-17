import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { LayoutService } from '../../layout.service';
import { MetadataService } from '../../../metadata.service';
import { LayoutModel } from '../../../../model/layoutmodel';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-layoutaction',
  templateUrl: './layoutaction.component.html',
  styleUrls: ['./layoutaction.component.css']
})
export class LayoutactionComponent implements OnInit {

  public actionSource: any;
  private selectedItemFromMainList: any;
  public addedItemToMainList: any;
  private selectedItemFromAddedList: any;
  private name: string;
  private isdatamainlist: boolean = false;
  public layoutInfo: LayoutModel = new LayoutModel();
  public resource: Resource;
  public searchText: string = '';

  constructor(
    private layoutservice: LayoutService,
    private activatedRoute: ActivatedRoute,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {

    this.activatedRoute.parent.parent.parent.url.subscribe((urlPath) => {
      this.name = urlPath[0].path;
      
    });
    this.layoutInfo = this.activatedRoute.snapshot.parent.data['layoutDetails'];
    this.addedItemToMainList = [];
    this.actionSource = [];
    this.addedItemToMainList.forEach(item => {
      item.isRowSelected = '';
    });
    if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
      this.addedItemToMainList[0].isRowSelected = true;
      this.processSelectedField(this.addedItemToMainList[0]);
    }
    this.getResource();
  }

  private getResource() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.getMetadataFieldsByName(this.name);
    this.addedItemToMainList = this.layoutInfo.listLayoutDetails.actions;
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          if (data.operations) {
            for (var k = 0; k < data.operations.length; k++) {
              this.actionSource.push(data.operations[k]);
            }
          }

          if (data.tasks) {
            for (var k = 0; k < data.tasks.length; k++) {
              this.actionSource.push(data.tasks[k]);
            }
          }

          //for left side gray
          for (var i = 0; i < this.addedItemToMainList.length; i++) {
            for (var j = 0; j < this.actionSource.length; j++) {
              if (this.addedItemToMainList[i].name === this.actionSource[j].name) {
                this.actionSource[j].isRowSelected = true;
                this.actionSource[j].isAdded = true;
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
      this.resetOthers(this.actionSource);
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
      for (var k = 0; k < this.actionSource.length; k++) {
        if (this.actionSource[k].name == this.selectedItemFromAddedList.name) {
          this.actionSource[k].isAdded = false;
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
