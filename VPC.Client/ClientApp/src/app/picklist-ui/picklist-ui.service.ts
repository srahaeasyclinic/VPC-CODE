import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LayoutModel } from '../model/layoutmodel';

@Injectable({
  providedIn: 'root'
})
export class PicklistUiService {

  constructor(private http: HttpClient) { }

  private picklistApi: string = '/api/picklists';

  getDefaultLayout(entityName: string, layoutType: number, layoutContext: number): Observable<LayoutModel> {
    var picklistLayoutUrl = `${environment.apiUrl}` + this.picklistApi + '/' + entityName + '/layouts/types/' + layoutType + '/contexts/' + layoutContext;
    return this.http.get<LayoutModel>(picklistLayoutUrl);
  }

  getPicklistValues(entityName: string, fields: string, filters: string, pageIndex: number, pageSize: number, 
    maxResult: number, orderBy: string,searchText:string):Observable<any> {
    var resourceUrl = `${environment.apiUrl}` + this.picklistApi + '/'+ entityName +'/values/' +'?fields=' + fields + '&pageIndex=' + pageIndex+'&pageSize=' + pageSize;
    if(filters){
      resourceUrl +='&filters=' + filters;
    }

    if(maxResult > 0){
      resourceUrl +='&maxResult=' + maxResult;
    }

    if(orderBy){
      resourceUrl +='&orderBy=' + orderBy;
    }

    if(searchText){
      resourceUrl +='&searchText=' + searchText;
    }
    return this.http.get<any>(resourceUrl);
  }

  getPicklistValueById(entityName: string,id:string): Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName + '/values/' + id; 
    return this.http.get<any>(url);
  }

  getPeviousNextObject(entityName: string,fields:string,prevId:string,id:string,nextId:string,searchText:string, orderBy:string,
    filters:string,pageIndex:number,pageSize:number,itemIndex:number,totalRecords:number){
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName + '/item?';
    //var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName + '/item?fields=' + fields +'&prevId='+ prevId + '&id='+id + 
    //'&nextId='+nextId+ '&searchText='+searchText +'&orderBy='+orderBy + '&filters='+filters+'&pageIndex='+pageIndex+'&pageSize='+pageSize+
    //'&itemIndex='+itemIndex+'&totalRecords='+totalRecords ; 
    
    if(fields){
      url+='fields=' + fields; 
    }
    
    if(prevId && prevId!='00000000-0000-0000-0000-000000000000'){
      url+='&prevId='+ prevId;
    }

    if(id && id!='00000000-0000-0000-0000-000000000000'){
      url+='&id='+ id;
    }

    if(nextId && nextId!='00000000-0000-0000-0000-000000000000'){
      url+='&nextId='+ nextId;
    }

    if(searchText){
      url+='&searchText='+ searchText;
    }

    if(orderBy){
      url+='&orderBy='+ orderBy;
    }

    if(filters){
      url+='&filters='+ filters;
    }

    if(pageIndex){
      url+='&pageIndex='+ pageIndex;
    }

    if(pageSize){
      url+='&pageSize='+ pageSize;
    }

    if(itemIndex){
      url+='&itemIndex='+ itemIndex;
    }

    if(totalRecords){
      url+='&totalRecords='+ totalRecords;
    }
  
    return this.http.get<any>(url);
  }


  savePicklistValues(entityName:string, picklistObj:any) : Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName +'/values' ;
    return this.http.post(url, picklistObj);
  }

  updatePicklistValues(entityName:string, picklistObj:any,picklistEntityId:string) : Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName +'/values/'+ picklistEntityId;
    return this.http.put(url, picklistObj);
  }
  
  deletePicklistValues(entityName: string,id:string): Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName + '/values/' + id; 
    return this.http.delete(url);
  }

}
