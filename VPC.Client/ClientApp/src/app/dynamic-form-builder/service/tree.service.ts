import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Broadcaster } from '../messaging/broadcaster';
import {MessageEvent} from "../messaging/message.event"
@Injectable()
export class TreeService {
  private picklists: string = '/api/picklists';
  private layout:string = "/api/meta-data";
  private entitties:string = "/api/entities";
  query: string = '&pageIndex=1' + '&pageSize=100';
  constructor(private http: HttpClient) { }
  getPickListValues(picklistName:string, dynamicQuery:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.picklists + '/' + picklistName + '/values?'+this.query;
    if(dynamicQuery){
      layoutUrl=layoutUrl+"&filters="+dynamicQuery;
    }
   // console.log('Tree '+layoutUrl);
    return this.http.get<any[]>(layoutUrl);
  }
  getLookupValue(lookupName, dynamicQuery:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.entitties + '/' + lookupName + '?'+this.query;
    if(dynamicQuery){
      layoutUrl=layoutUrl+"&filters="+dynamicQuery;
    }
    return this.http.get<any[]>(layoutUrl);
  }
  getViewLayout(entityName:string, layoutId:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + entityName + '/layouts/'+layoutId;
    return this.http.get<any[]>(layoutUrl);
  }

  getMetaDataPicklistValues(metaUrl):Observable<any>{
    var layoutUrl = `${environment.apiUrl}` + metaUrl;
    return this.http.get<any[]>(layoutUrl);
  }

  getEntityValue(entityName:string): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.entitties + '/' + entityName;
    return this.http.get<any[]>(layoutUrl);
  }

  getLastValue(name: any): any {
    var arr = name.split(".");
    return arr[arr.length-1];        
  }
}

