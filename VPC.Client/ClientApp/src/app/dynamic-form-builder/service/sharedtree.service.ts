import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITreeNode } from '../tree.module';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MODALS } from '../tree.config';
import { TosterService } from 'src/app/services/toster.service';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Observable } from 'rxjs/Observable';
import { generateRandomNo } from 'src/app/model/treeNode';
@Injectable()
export class SharedTreeService {
  public forminstance: any;
  constructor(private modalService: NgbModal, private toster: TosterService
    , public globalResourceService: GlobalResourceService) { }

  public addFieldToLayout(sourceNode, targetNode) {
    var buildSource = "";
    var buildSourceIndex;
    var buildSourceContainer = "";
    var buildTargetIndex;
    var buildTargetContainer = "";

    var tempSource = [];
    var tempTarget = [];

    if (sourceNode["itemIndex"]) {
      var sourceIndex = sourceNode["itemIndex"].split(",");
      sourceIndex.forEach((element, index) => {
        if (index != 0) {
          tempSource.push(this.splitTextNumber(element));
        }
      });

      tempSource.forEach(element => {
        if (element[0][0].indexOf("s") > -1) {
          buildSource += '["fields"][' + element[0][1] + ']';
          buildSourceContainer += '["fields"][' + element[0][1] + ']';
        }
        else if (element[0][0].indexOf("tg") > -1) {
          buildSource += '["fields"][' + element[0][1] + ']';
          buildSourceContainer += '["fields"][' + element[0][1] + ']';
        }
        else if (element[0][0].indexOf("ti") > -1) {
          buildSource += '["tabs"][' + element[0][1] + ']';
          buildSourceContainer += '["tabs"][' + element[0][1] + ']';
        }
        else {
          buildSource += '["fields"][' + element[0][0] + ']';
          buildSourceIndex = parseInt(element[0][0]);
        }
      });
    }

    if (targetNode["itemIndex"]) {
      var targetIndex = targetNode["itemIndex"].split(",");
      targetIndex.forEach((element, index) => {
        if (index != 0) {
          tempTarget.push(this.splitTextNumber(element));
        }
      });

      tempTarget.forEach(element => {
        if (element[0][0].indexOf("s") > -1) {
          buildTargetContainer += '["fields"][' + parseInt(element[0][1]) + ']';
        }
        else if (element[0][0].indexOf("tg") > -1) {
          buildTargetContainer += '["fields"][' + parseInt(element[0][1]) + ']';
        }
        else if (element[0][0].indexOf("ti") > -1) {
          buildTargetContainer += '["tabs"][' + parseInt(element[0][1]) + ']';
        }
        else {
          buildTargetIndex = parseInt(element[0][0]);
        }
      });
    }
    /* Source Object, Source Index, Source Container, Target Index, Target Container */
    var s, si, sc, ti, tc;

    s = this.forminstance.selectedTreeNode;

    if (buildSourceIndex >= 0) {
      si = buildSourceIndex;
    }

    //console.log('buildSourceContainer ', buildSourceContainer);

    if (buildSourceContainer) {
      sc = eval("this.forminstance.tree" + buildSourceContainer + '["fields"]');
    }
    else {
      sc = eval("this.forminstance.tree" + '["fields"]');
    }

    if (buildTargetIndex >= 0) {
      ti = buildTargetIndex;
    }

    tc = eval("this.forminstance.tree" + buildTargetContainer + '["fields"]');
    if (si >= 0 && ti >= 0) {

      if (buildSourceContainer === buildTargetContainer) {
        if (ti < si) {
          tc.splice(si, 1);
          tc.splice(ti, 0, s);
        }
        else if (si < ti) {
          tc.splice(si, 1);
          tc.splice(ti, 0, s);
        }
      }
      else {
        sc.splice(si, 1);
        tc.splice(ti, 0, s);
      }
    }
    else if (si >= 0 && !ti) {
      //console.log('(si >= 0 && !ti)');
      sc.splice(si, 1);//removing dragged source element
      tc.push(s);//adding dragged source element in the section of target
    }
    else if (!si && !ti) {
      //console.log('!si && !ti');
      tc.push(s);//adding dragged source element in the section of target
      this.forminstance.selectedField.draggedItem = true;
      this.forminstance.selectedField = null;
    }
    else if (!si && ti >= 0) {
      //console.log('!si && ti >= 0');
      tc.splice(ti, 0, s);//placing dragged source element in the index of target
      this.forminstance.selectedField.draggedItem = true;
      this.forminstance.selectedField = null;
    }
    this.setItemIndex();

  }
  public setItemIndex() {
    let treeNode = this.forminstance.tree;
    treeNode["itemIndex"] = "s0";
    treeNode["visibility"] = true;
    if (treeNode.fields.length > 0) {
      this.findAndSetIndex(treeNode.fields, treeNode["itemIndex"]);
    }

  }

  public findAndSetIndex(argTreeNode, parentIndex) {
    argTreeNode.forEach((element, index) => {

      if (element.controlType.toLowerCase() === "section") {
        element["itemIndex"] = parentIndex + ",s" + index;
        element["visibility"] = true;
        if (element.fields.length > 0) {
          this.findAndSetIndex(element.fields, element["itemIndex"]);
        }
      }
      else if (element.controlType.toLowerCase() === "tabs") {
        element["itemIndex"] = parentIndex + ",tg" + index;
        element["visibility"] = true;
        this.findAndSetIndex(element.tabs, element["itemIndex"]);
      }
      else if (element.controlType.toLowerCase() === "tab") {
        element["itemIndex"] = parentIndex + ",ti" + index;
        element["visibility"] = true;
        this.findAndSetIndex(element.fields, element["itemIndex"]);
      }
      else {
        element["itemIndex"] = parentIndex + "," + index;
        element["visibility"] = true;
      }
    });
  }
  public splitTextNumber(inputText) {
    var output = [];
    var json = inputText.split(' ');
    json.forEach(function (item) {
      output.push(item.replace(/\'/g, '').split(/(\d+)/).filter(Boolean));
    });
    return output;
  }


  public stickFieldToolBar() {
    if (document.getElementById('page-content-wrapper') && document.getElementById('content-block-wrapper')) {
      if (!this.forminstance.isRendered) {
        this.forminstance.position = document.getElementById('content-block-wrapper').scrollTop
        let tracker = document.getElementById('page-content-wrapper');
        let windowYOffsetObservable = Observable.fromEvent(tracker, 'scroll').map(() => {
        });
        let scrollSubscription = windowYOffsetObservable.subscribe((scrollPos) => {
          this.getScrollPosition()
        });
        this.forminstance.isRendered = true;
      }
    }
    // this.forminstance.changeRef.detectChanges();
  }

  private getScrollPosition() {
    if (document.getElementById('page-content-wrapper')) {
      var element = document.getElementById('content-block-wrapper')
      if (element) {
        var fromabove = document.getElementById('page-content-wrapper').scrollTop;
        // if (fromabove > 80) {
        if (fromabove > this.forminstance.position) {

          element.classList.add('sticky')
        } else {
          element.classList.remove('sticky')
        }
      }
    }
  }


  public setidExistingfields(node: ITreeNode) {
    if (node) {
      node.refId = node.refId || generateRandomNo();
      node.fields.forEach(item => {
        this.setidExistingfields(item);
        if (item.fields) {
          item.fields.forEach(item3 => {
            this.setidExistingfields(item3);

          });
        }
      });
      if (node.tabs) {
        node.tabs.forEach(item2 => {
          this.setidExistingfields(item2);
          if (item2.fields) {
            item2.fields.forEach(item3 => {
              this.setidExistingfields(item3);
            });
          }
        });
      }
    }
  }
}

