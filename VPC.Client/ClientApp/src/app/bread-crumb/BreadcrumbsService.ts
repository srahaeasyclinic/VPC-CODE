import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CommonService } from '../services/common.service';
import { MenuContextObject } from '../services/menu.service';


@Injectable({
  providedIn: 'root'
})
export class BreadcrumbsService {
  private queryUrl: string = '/api/picklists/language/values?orderBy=text&pageIndex=1&pageSize=500';
  public breadcrumb: Array<Ibreadcrumbs>;
  constructor(private httpClient: HttpClient, private commonService: CommonService,) { }

  getNameById(id: string): Observable<any> {
    var languageUrl = `${environment.apiUrl}` + this.queryUrl;
    return this.httpClient.get<any>(languageUrl);
  }
/////////////////////////////////////////BreadCrums Logic///////////////////////////////////////////////////////////////////////
  setGroupMenuBreadcums(obj: MenuContextObject) {
    this.breadcrumb = [];
    let bread = new breadcrumbs();
    bread.elementName = "Dashboard";
    bread.elementURL = "/dashboard";
    this.breadcrumb.push(bread);
    this.breadcrumb.push({ 'elementName': obj.topmainGroup, 'elementURL': obj.topmainGroupId, 'isGroup': true });
    this.breadcrumb.push({ 'elementName': obj.topgroupName, 'elementURL': "", 'isGroup': false });
    this.breadcrumb.push({ 'elementName': obj.leftgroupName, 'elementURL': '', 'isGroup': false });
    //console.log("this.breadcrumb ",this.breadcrumb);
  }
  setchildMenuBreadcums(objectname: Ibreadcrumbs[], menutype: string = "") {
    if (this.breadcrumb != undefined && this.breadcrumb != null && this.breadcrumb.length > 1) {
      let addleftmenulink = this.commonService.getTrimmenuStr(menutype.toLowerCase()) + "/" +
        this.commonService.getTrimmenuStr(this.breadcrumb[1].elementName.toLowerCase()) + "/" +
        this.commonService.getTrimmenuStr(this.breadcrumb[2].elementName.toLowerCase()) + "/" +
        this.commonService.getTrimmenuStr(this.breadcrumb[3].elementName.toLowerCase());
      this.breadcrumb[3].elementURL = addleftmenulink;
      if (this.breadcrumb.length > 3) {
        this.breadcrumb.splice(4);
      }
    }
    objectname.forEach(ele => {
      this.breadcrumb.push({ 'elementName': ele.elementName, 'elementURL': ele.elementURL, 'isGroup': false });
    });
    //console.log("this.breadcrumb ",this.breadcrumb);
  }
  setBreadcums_at_last(objectname: Ibreadcrumbs, menutype: string = "") {
    if (this.breadcrumb != undefined && this.breadcrumb != null && this.breadcrumb.length > 1) {
      this.breadcrumb[this.breadcrumb.length - 1].elementName = objectname.elementName;
      this.breadcrumb[this.breadcrumb.length - 1].elementURL = objectname.elementURL;
    }
  }
  seterrorbreadcrums(objerror)
  {
    this.breadcrumb = [];
    let bread = new breadcrumbs();
    bread.elementName = "Dashboard";
    bread.elementURL = "/dashboard";
    this.breadcrumb.push(bread);
    this.breadcrumb.push({ 'elementName':objerror, 'elementURL': "", 'isGroup': false });
  }

  getbreadcums() {
    return this.breadcrumb;
  }
  splicebreadcrums(element: Ibreadcrumbs,deleteCurrentElement:boolean=false) {
    if (this.breadcrumb != undefined && this.breadcrumb != null) {
      let objcurrentIndex = this.breadcrumb.findIndex(w => w.elementName.toLowerCase() == element.elementName.toLowerCase() && w.elementURL.toLowerCase()==element.elementURL.toLowerCase() && w.isGroup==element.isGroup);
      this.breadcrumb[objcurrentIndex].elementURL = "";

      deleteCurrentElement?this.breadcrumb.splice(objcurrentIndex):this.breadcrumb.splice(objcurrentIndex+1);
    }
  }

 
  
}


///////////////////////////end///////////////////////////

///////////////////////////BraedCrums class and Interface and const Value/////////////////////////////////////////////////////////////
export interface Ibreadcrumbs{
    elementName: string;
    elementURL: string;
    isGroup: boolean;
}
export class breadcrumbs implements Ibreadcrumbs
{
    isGroup: boolean=false;
    elementURL: string;
    elementName : string;
}



export const defaultURL = "dashboard";

//////////////////////////////////////////////END//////////////////////////////////////////////////////////////////////////////////////
