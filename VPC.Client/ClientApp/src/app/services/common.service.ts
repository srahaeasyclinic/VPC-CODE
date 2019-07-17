import { Injectable } from '@angular/core';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';

import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Rule } from '../model/rule';
import { HttpClient } from '@angular/common/http';
import { TreeNode } from '@angular/router/src/utils/tree';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private rules: string = '/api/rules';
  public displayName4Breadcrumb: string;
  constructor(private http: HttpClient) { }
  camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
      if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
      return index == 0 ? match.toLowerCase() : match.toUpperCase();
    });
  }
  generateResourceName(word) {
    if (!word) { return word; }
    return word[0].toLowerCase() + word.substr(1);
  }
  defaultDateformat() {
    ///return 'dd-MMM-yyyy HH:mm:ss';
    //return 'dd-MM-yyyy HH:mm:ss';
    return 'dd-MM-yyyy';

  }
  // createKeyValue(data: TreeNode[], savedData: any): any {
  //   console.log('data', data);
  //   data.forEach(element => {
  //     if (element.controlType.toLocaleLowerCase() != "section") {
  //       if (element.controlType.toLocaleLowerCase() != "tabs") {
  //         if(element.value instanceof Array){
  //           var splitStr = element.dataType+"|";
  //           var values = [];
  //           element.value.forEach(element => {
  //             values.push(element.internalId)
  //           });
  //           savedData[element.name] =splitStr +values.join();
  //         }else{
  //           savedData[element.name] = element.value;
  //         }

  //       }
  //     }
  //     if (element.fields) {
  //       this.createKeyValue(element.fields, savedData);
  //     }
  //   });
  //   return savedData;
  // }
  createKeyValue(data: ITreeNode[], savedData: any): any {

    data.forEach(element => {
      // if (element.controlType.toLocaleLowerCase() != "section") {
      //   if (element.controlType.toLocaleLowerCase() != "tabs") {

      //   } else {
      //     this.createKeyValue(element.tabs, savedData);
      //   }
      // } else {
      //   if (element.fields) {
      //     this.createKeyValue(element.fields, savedData);
      //   }
      //   if (element.tabs) {
      //     this.createKeyValue(element.tabs, savedData);
      //   }
      // }

      this.addValue(savedData, element);
      if (element.fields) {
        this.createKeyValue(element.fields, savedData);
      }
      if (element.tabs) {
        this.createKeyValue(element.tabs, savedData);
      }
    });
    return savedData;
  }
  addValue(savedData: any, element: ITreeNode) {

    // if (element.value == null || element.value == "") return;

    if ((element.value != null && element.value != "" && element.value != undefined) || (element != null && element.typeOf != undefined && element.typeOf != null && element.typeOf.toLocaleLowerCase() == "booleantype")) {
      if (element.value instanceof Array) {
        var splitStr = element.dataType + "|";
        var values = [];
        element.value.forEach(element => {
          values.push(element.internalId)
        });
        savedData[element.name] = splitStr + values.join();
      } else {
        savedData[element.name] = element.value;
      }

    }
  }
  getLastValue(name: string): string {
    var arr = name.split(".");
    return arr[arr.length - 1];
  }

  generateResourceNameWithHierarchy(word: string) {
    if (!word) return word;

    let hierarchyPresent = word.split(".");
    if (hierarchyPresent.length == 1) {
      return word[0].toLowerCase() + word.substr(1);
    }
    else if (hierarchyPresent.length > 1) {
      let lastItem = hierarchyPresent[hierarchyPresent.length - 1];
      if (lastItem)
        return lastItem[0].toLowerCase() + lastItem.substr(1);
    }
  }

  public getRuleList(entityName): Observable<any> {
    var ruleUrl = `${environment.apiUrl}` + this.rules + "/" + entityName;
    //console.log('ruleUrl ', ruleUrl);
    return this.http.get<Rule[]>(ruleUrl);
  }

  public executeTask(entityName, taskAttribute, object): Observable<any> {
    var taskUrl = `${environment.apiUrl}` + '/api/' + entityName + "/" + taskAttribute.name;
    if (taskAttribute.taskVerb === "Post") {
      return this.http.post(taskUrl, object);

    } if (taskAttribute.taskVerb === "Put") {

      return this.http.put(taskUrl, object);

    }
    // if(taskAttribute.taskVerb==="Patch")
    // {

    // }else  if(taskAttribute.taskVerb==="Get")
    // {

    //   return this.http.get<any>(taskUrl);
    // }

  }

  //----------------------------------------DYNAMIC MENU ---------------------------------\\\
  public virtualGroup: Array<string> = ["", "object-manager", "picklist-manager", "entity-designer#picklist-designer", "configuration-manager"];
  public getVirtualGroup(menuTypeId: number, actionTypeId: number = null) {
    var virtualGroupStr = this.virtualGroup[menuTypeId];
    if (virtualGroupStr && actionTypeId != null && menuTypeId == 3) {
      var arr = virtualGroupStr.split("#");
      return arr[actionTypeId - 2];
    }
    return virtualGroupStr;
  }
  public getTrimStr(str: string): string {
    return str
      .replace(/\s/g, "")
      .trim();
  }

  public setDisplayName4Breadcrumb(value) {
    this.displayName4Breadcrumb = value;
  }

  public getDisplayName4Breadcrumb() {
    return this.displayName4Breadcrumb;
  }


  public getCookie(name: string) {
    let ca: Array<string> = document.cookie.split(';');
    let caLen: number = ca.length;
    let cookieName = `${name}=`;
    let c: string;

    for (let i: number = 0; i < caLen; i += 1) {
      c = ca[i].replace(/^\s+/g, '');
      if (c.indexOf(cookieName) == 0) {
        return c.substring(cookieName.length, c.length);
      }
    }
    return '';
  }

  public deleteCookie(name) {
    this.setCookie(name, '', -1);
  }
  public deleteAllCookie() {
    if (document.cookie.includes(";")) {
      var cookies = document.cookie.split(";");
      for (var i = 0; i < cookies.length; i++) {
        if (cookies[i].split("=")[0].trim() == 'userInfo' || cookies[i].split("=")[0].trim() == 'TokenInfo') {
          this.deleteCookie(cookies[i].split("=")[0]);
        }
      }
    }
  }
  public setCookie(name: string, value: string, expireDays: number, path: string = '') {
    let d: Date = new Date();
    d.setTime(d.getTime() + expireDays * 24 * 60 * 60 * 1000);
    let expires: string = `expires=${d.toUTCString()}`;
    let cpath: string = path ? `; path=${path}` : '';
    document.cookie = `${name}=${value}; ${expires}${cpath}`;
  }

}
