import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { LayoutModel } from '../../model/layoutmodel';

import { ListLayoutDetails } from '../../model/listlayoutdetails';
import { OrderDetails } from '../../model/orderdetails';
import { ViewLayoutDetails } from 'src/app/model/viewlayoutdetails';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';

@Injectable({
  providedIn: 'root'
})



export class LayoutService {
  

  private layout: string = '/api/meta-data';
  private cacheLayoutDetail: LayoutModel;
  private objcount: number=0;
  query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
  constructor(private http: HttpClient) { }

  getLayouts(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';
    return this.http.get<LayoutModel[]>(layoutUrl);
  }

  saveLayout(layoutModel, name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';

    var layout = new LayoutModel();
    layout.name = layoutModel.name;
    layout.layoutType = layoutModel.layoutType;
    //layout.Subtype = layoutModel.Subtype;
    layout.subtypeeName = layoutModel.subtypeeName;
    layout.Context = layoutModel.Context;
    return this.http.post(layoutUrl, layout);
    //return this.http.post(layoutUrl, layout)
    //  .subscribe(
    //    data => {
    //      console.log("POST Request is successful ", data);
    //    },
    //    error => {
    //      console.log("Error", error);
    //    }

    //  );
  }

  saveLayoutDefault(layoutModel, name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';

    return this.http.patch(layoutUrl, layoutModel);
  }


  updateFormLayout(entityname: string, layoutId: string, layoutInfo: LayoutModel): any {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + entityname + '/layouts/' + layoutId;
    //return this.http.post(layoutUrl, layoutInfo);
    return this.http.put(layoutUrl, layoutInfo);
  }
  
  // updateLayout(name, layoutId, layoutDetails): Observable<any> {
  //   var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + layoutId;

  //   var layout1 = new LayoutModel();
  //   layout1.name = layoutDetails.name;
  //   layout1.layoutType = layoutDetails.layoutType;
  //   layout1.layoutTypeName = layoutDetails.layoutTypeName;
  //   //layout1.listLayoutDetails.Fields = [];
  //   layout1.listLayoutDetails = new ListLayoutDetails();
  //   layout1.listLayoutDetails.fields = [];

  //   if (layoutDetails != null && layoutDetails.layoutType == 3 && layoutDetails.listLayoutDetails) {

  //     if (layoutDetails.listLayoutDetails.fields != null) {
  //       var fields = [];
  //       for (var j = 0; j < layoutDetails.listLayoutDetails.fields.length; j++) {
  //         var myObj1 = {
  //           Name: layoutDetails.listLayoutDetails.fields[j].name,
  //           Sequence: layoutDetails.listLayoutDetails.fields[j].sequence,
  //           Hidden: layoutDetails.listLayoutDetails.fields[j].hidden,
  //           DataType: layoutDetails.listLayoutDetails.fields[j].dataType,
  //           RefId: layoutDetails.listLayoutDetails.fields[j].refId,
  //           DefaultValue: layoutDetails.listLayoutDetails.fields[j].defaultValue,
  //           Properties: layoutDetails.listLayoutDetails.fields[j].properties,
  //           Values: layoutDetails.listLayoutDetails.fields[j].values,
  //           Clickable: layoutDetails.listLayoutDetails.fields[j].clickable,
  //           Defaultview: layoutDetails.listLayoutDetails.fields[j].defaultView
  //         };
  //         fields.push(myObj1);
  //       }
  //       layout1.listLayoutDetails.fields = [];
  //       layout1.listLayoutDetails.fields = fields;
  //     }

  //     if (layoutDetails.listLayoutDetails.defaultSortOrder != null) {
  //       var defaultSortOrder = {
  //         name: "",
  //         value: ""
  //       };
  //       if (layoutDetails.listLayoutDetails.defaultSortOrder.name) {
  //         defaultSortOrder.name = layoutDetails.listLayoutDetails.defaultSortOrder.name;
  //       }
  //       if (layoutDetails.listLayoutDetails.defaultSortOrder.value) {
  //         defaultSortOrder.value = layoutDetails.listLayoutDetails.defaultSortOrder.value;
  //       }
  //       //layout1.ListLayoutDetails.DefaultSortOrder = {};
  //       layout1.listLayoutDetails.defaultSortOrder = new OrderDetails();
  //       layout1.listLayoutDetails.defaultSortOrder = defaultSortOrder;
  //     }

  //     if (layoutDetails.listLayoutDetails.defaultGroupBy !== null) {
  //       layout1.listLayoutDetails.defaultGroupBy = layoutDetails.listLayoutDetails.defaultGroupBy;
  //     }

  //     if (layoutDetails.listLayoutDetails.maxResult !== null) {
  //       layout1.listLayoutDetails.maxResult = layoutDetails.listLayoutDetails.maxResult;
  //     }

  //     if (layoutDetails.listLayoutDetails.toolbar !== null) {
  //       var buttonList = [];
  //       for (var k = 0; k < layoutDetails.listLayoutDetails.toolbar.length; k++) {
  //         var myObj12 = {
  //           Name: layoutDetails.listLayoutDetails.toolbar[k].name,
  //           //status: layoutDetails.listLayoutDetails.toolbar[k].status,
  //           Type: layoutDetails.listLayoutDetails.toolbar[k].type,
  //           Sequence: layoutDetails.listLayoutDetails.toolbar[k].sequence,
  //           Group: layoutDetails.listLayoutDetails.toolbar[k].group,
  //           Properties: layoutDetails.listLayoutDetails.toolbar[k].properties
  //         };
  //         buttonList.push(myObj12);
  //       }
  //       layout1.listLayoutDetails.toolbar = [];
  //       layout1.listLayoutDetails.toolbar = buttonList;
  //     }

  //     if (layoutDetails.listLayoutDetails.actions !== null) {
  //       var actionList = [];
  //       for (var x = 0; x < layoutDetails.listLayoutDetails.actions.length; x++) {
  //         var myObj11 = {
  //           name: layoutDetails.listLayoutDetails.actions[x].name,
  //           status: layoutDetails.listLayoutDetails.actions[x].status,
  //           sequence: layoutDetails.listLayoutDetails.actions[x].sequence
  //         };
  //         actionList.push(myObj11);
  //       }
  //       layout1.listLayoutDetails.actions = [];
  //       layout1.listLayoutDetails.actions = actionList;
  //     }

  //     if (layoutDetails.listLayoutDetails.searchProperties !== null) {
  //       layout1.listLayoutDetails.searchProperties = layoutDetails.listLayoutDetails.searchProperties;
  //     }
  //   }
  //   else if(layoutDetails != null && layoutDetails.layoutType == 1 && layoutDetails.viewLayoutDetails)
  //   {
  //     layout1.viewLayoutDetails = new ViewLayoutDetails();
  //     layout1.viewLayoutDetails = layoutDetails.viewLayoutDetails;
  //   }

  //   //return this.http.post(layoutUrl, layout1);
  //   return this.http.put(layoutUrl, layout1);

  // }

  // updateLayout(name, layoutId, layoutDetails): Observable<any> {
  //   var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + layoutId;

  //   var layout1 = new LayoutModel();
  //   layout1.name = layoutDetails.name;
  //   layout1.layoutType = layoutDetails.layoutType;
  //   layout1.layoutTypeName = layoutDetails.layoutTypeName;
  //   //layout1.listLayoutDetails.Fields = [];
  //   layout1.listLayoutDetails = new ListLayoutDetails();
  //   layout1.listLayoutDetails.fields = [];

  //   if (layoutDetails != null && layoutDetails.layoutType == 3 && layoutDetails.listLayoutDetails) {

  //     if (layoutDetails.listLayoutDetails.fields != null) {
  //       // var fields = [];
  //       // for (var j = 0; j < layoutDetails.listLayoutDetails.fields.length; j++) {
  //       //   var myObj1 = {
  //       //     Name: layoutDetails.listLayoutDetails.fields[j].name,
  //       //     Sequence: layoutDetails.listLayoutDetails.fields[j].sequence,
  //       //     Hidden: layoutDetails.listLayoutDetails.fields[j].hidden,
  //       //     DataType: layoutDetails.listLayoutDetails.fields[j].dataType,
  //       //     RefId: layoutDetails.listLayoutDetails.fields[j].refId,
  //       //     DefaultValue: layoutDetails.listLayoutDetails.fields[j].defaultValue,
  //       //     Properties: layoutDetails.listLayoutDetails.fields[j].properties,
  //       //     Values: layoutDetails.listLayoutDetails.fields[j].values,
  //       //     Clickable: layoutDetails.listLayoutDetails.fields[j].clickable,
  //       //     Defaultview: layoutDetails.listLayoutDetails.fields[j].defaultView
  //       //   };
  //       //   fields.push(myObj1);
  //       // }
  //       //layout1.listLayoutDetails.fields = [];
  //       layout1.listLayoutDetails.fields = layoutDetails.listLayoutDetails.fields;
  //     }

  //     if (layoutDetails.listLayoutDetails.defaultSortOrder != null) {
  //       // var defaultSortOrder = {
  //       //   name: "",
  //       //   value: ""
  //       // };
  //       // if (layoutDetails.listLayoutDetails.defaultSortOrder.name) {
  //       //   defaultSortOrder.name = layoutDetails.listLayoutDetails.defaultSortOrder.name;
  //       // }
  //       // if (layoutDetails.listLayoutDetails.defaultSortOrder.value) {
  //       //   defaultSortOrder.value = layoutDetails.listLayoutDetails.defaultSortOrder.value;
  //       // }
  //       // //layout1.ListLayoutDetails.DefaultSortOrder = {};
  //       layout1.listLayoutDetails.defaultSortOrder = new OrderDetails();
  //       layout1.listLayoutDetails.defaultSortOrder = layoutDetails.listLayoutDetails.defaultSortOrder;
  //     }

  //     if (layoutDetails.listLayoutDetails.defaultGroupBy !== null) {
  //       layout1.listLayoutDetails.defaultGroupBy = layoutDetails.listLayoutDetails.defaultGroupBy;
  //     }

  //     if (layoutDetails.listLayoutDetails.maxResult !== null) {
  //       layout1.listLayoutDetails.maxResult = layoutDetails.listLayoutDetails.maxResult;
  //     }

  //     if (layoutDetails.listLayoutDetails.toolbar !== null) {
  //       // var buttonList = [];
  //       // for (var k = 0; k < layoutDetails.listLayoutDetails.toolbar.length; k++) {
  //       //   var myObj12 = {
  //       //     Name: layoutDetails.listLayoutDetails.toolbar[k].name,
  //       //     //status: layoutDetails.listLayoutDetails.toolbar[k].status,
  //       //     Type: layoutDetails.listLayoutDetails.toolbar[k].type,
  //       //     Sequence: layoutDetails.listLayoutDetails.toolbar[k].sequence,
  //       //     Group: layoutDetails.listLayoutDetails.toolbar[k].group,
  //       //     Properties: layoutDetails.listLayoutDetails.toolbar[k].properties
  //       //   };
  //       //   buttonList.push(myObj12);
  //       // }
  //       //layout1.listLayoutDetails.toolbar = [];
  //       layout1.listLayoutDetails.toolbar = layoutDetails.listLayoutDetails.toolbar;
  //     }

  //     if (layoutDetails.listLayoutDetails.actions !== null) {
  //       // var actionList = [];
  //       // for (var x = 0; x < layoutDetails.listLayoutDetails.actions.length; x++) {
  //       //   var myObj11 = {
  //       //     name: layoutDetails.listLayoutDetails.actions[x].name,
  //       //     status: layoutDetails.listLayoutDetails.actions[x].status,
  //       //     sequence: layoutDetails.listLayoutDetails.actions[x].sequence
  //       //   };
  //       //   actionList.push(myObj11);
  //       // }
  //       //layout1.listLayoutDetails.actions = [];
  //       layout1.listLayoutDetails.actions = layoutDetails.listLayoutDetails.actions;
  //     }

  //     if (layoutDetails.listLayoutDetails.searchProperties !== null) {
  //       layout1.listLayoutDetails.searchProperties = layoutDetails.listLayoutDetails.searchProperties;
  //     }
  //   }
  //   else if(layoutDetails != null && layoutDetails.layoutType == 1 && layoutDetails.viewLayoutDetails)
  //   {
  //     layout1.viewLayoutDetails = new ViewLayoutDetails();
  //     layout1.viewLayoutDetails = layoutDetails.viewLayoutDetails;
  //   }

  //   //return this.http.post(layoutUrl, layout1);
  //   return this.http.put(layoutUrl, layout1);

  // }

  updateLayout(name, layoutId, layoutDetails): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + layoutId;

    var layout1 = new LayoutModel();
    layout1 = layoutDetails;   

    //return this.http.post(layoutUrl, layout1);
    return this.http.put(layoutUrl, layout1);

  }

  getLayoutById(name, id): Observable<any> {
    var layoutById = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id;
    return this.http.get<LayoutModel[]>(layoutById);
  }


  getDefaultLayout(name: string, type: string, subtype: string, context: string):  Observable<any> {
    var layoutById = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/default?type='+type+"&subtype="+subtype+"&context="+context;
    return this.http.get<LayoutModel[]>(layoutById);
  }

  getLayoutListViews(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts?type=View';
    return this.http.get<any[]>(layoutUrl);
  }

  displayPreview(name, fields): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + '/api/entities' + '/' + name + '?fields=' + fields;
    return this.http.get<any[]>(layoutUrl);
  }

  deleteLayout(id, name): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
    };

    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id;
    return this.http.delete(layoutUrl);
  }

  getPickList(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + '/api/picklists' + '/' + name + '/values';
     // console.log('layoutUrl '+layoutUrl);
    return this.http.get<any[]>(layoutUrl);
  }

  deleteUserValues(entityName: string,id:string): Observable<any> {
    var url = `${environment.apiUrl}` + '/api/entities' +'/' + entityName + '/' + id; 
    return this.http.delete(url);
  }

  getEntityDetails(entityName: string,id:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + '/api/entities' +'/' + entityName + '/' + id+"?subtype=Employee"; 
    return this.http.get<any[]>(layoutUrl);
  }

  // updateMetaDataValues(entityName:string, metaDataObj:any,id:string) : Observable<any> {
  //   var url = `${environment.apiUrl}` + '/api/entities'  +'/' + entityName +'/values/'+ id;
  //   return this.http.put(url, metaDataObj);
  // }

  updateMetaDataValues(entityName:string, metaDataObj:any,id:string, subType:string) : Observable<any> {
    var url = `${environment.apiUrl}` + '/api/entities'  + '/' + entityName +'/'+ id + '?subType=' + subType;
    return this.http.put(url, metaDataObj);
  }

  addDetailEntity(entityName:string, id:string, detailEntityName:string, subType:string, layoutDetails:any)
  {
    var layoutUrl = `${environment.apiUrl}` + '/api/entities'  + '/' + entityName +'/'+ id + '/'+ detailEntityName + '?subType=' + subType;
    return this.http.post(layoutUrl, layoutDetails);
  }

  updateDetailEntity(entityName:string, userid:string, detailEntityName:string, id:string, subType:string, layoutDetails:any)
  {
    var layoutUrl = `${environment.apiUrl}` + '/api/entities'  + '/' + entityName +'/'+ userid + '/'+ detailEntityName + '/' + id + '?subType=' + subType;
    return this.http.put(layoutUrl, layoutDetails);
  }

  getDetailEntityById(entityName:string, userid:string, detailEntityName:string, id:string)
  {
    var layoutUrl = `${environment.apiUrl}` + '/api/entities' +'/' + entityName + '/' + userid + '/' + detailEntityName + '/' + id; 
    return this.http.get<any[]>(layoutUrl);
  }

  // getnoOfcontroltype(tree:TreeNode,types:string):number
  // {
  //   this.objcount = 0;
  //   this.checkcount(tree,types);
  //   console.log(this.objcount);
  //   return this.objcount;
  // }

  // private checkcount(tree:TreeNode,types:string)
  // {
  //   if (tree.controlType==types)
  //   {
  //     this.objcount++;
  //   }
    
  //    tree.fields.forEach(item => {
  //     if (item.controlType===types)
  //     {
  //       this.objcount++;
  //     }
  //      if (item.fields)
  //      {
  //        this.checkcount(item,types);
  //      }
  //      if (item.tabs)
  //      {
  //        item.tabs.forEach(t => { 
            
  //          this.checkcount(t,types);
  //        });
         
  //      }
  //   });
    
  // }

}

