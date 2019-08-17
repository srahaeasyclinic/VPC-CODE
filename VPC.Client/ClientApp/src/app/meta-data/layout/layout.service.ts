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
    layout.context = layoutModel.context;
    return this.http.post(layoutUrl, layout);    
  }

  saveLayoutDefault(layoutModel, name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';

    return this.http.patch(layoutUrl, layoutModel);
  }


  updateFormLayout(entityname: string, layoutId: string, layoutInfo: LayoutModel): any {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + entityname + '/layouts/' + layoutId;
    //return this.http.post(layoutUrl, layoutInfo);
    var layout1 = new LayoutModel();
    layout1 = layoutInfo;   
    return this.http.put(layoutUrl, layout1);
  }
  
 
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

  getLayoutListForm(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts?type=Form';
    return this.http.get<any[]>(layoutUrl);
  }

  getLayoutListList(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts?type=List';
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

  cloneLayout(layoutModel:LayoutModel, name:string, id:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id + '/clone';

    var layout = new LayoutModel();
    layout.name = layoutModel.name;
    //layout.layoutType = layoutModel.layoutType;
    //layout.Subtype = layoutModel.Subtype;
    layout.subtypeeName = layoutModel.subtypeeName;
    layout.context = layoutModel.context;
    return this.http.post(layoutUrl, layout);    
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

