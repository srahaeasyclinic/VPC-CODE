import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Picklists } from '../model/picklists'; 
import { Entities } from '../model/entities'; 
import { DebugRenderer2 } from '@angular/core/src/view/services';


@Injectable({
  providedIn: 'root'
})



export class PicklistService {
  private picklists: string = '/api/picklists';
  private entities: string = '/api/metadata';
  private resources: string = '/api/resources';

  query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
  showToolbar=  new EventEmitter();
  constructor(private http: HttpClient) { }

  private picklistApi: string = '/api/picklists';

  getPicklists(): Observable<any> {
    var picklistsUrl = `${environment.apiUrl}` + this.picklists;
    return this.http.get<Picklists[]>(picklistsUrl);
  }

  getPicklistByName(name): Observable<any> {
    var picklistsUrl = `${environment.apiUrl}` + this.picklists + '/' + name;
    return this.http.get<Picklists[]>(picklistsUrl);
  }

  getMetadataByName(name): Observable<any> {
    var entitiesUrl = `${environment.apiUrl}` + this.entities + '/' + name;
    return this.http.get<Entities[]>(entitiesUrl);
  }

 
  getPickListValues(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + '/api/picklists' + '/' + name + '/values';
    return this.http.get<any[]>(layoutUrl);
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

  // getPickListViews(name): Observable<any> {
  //   var layoutUrl = `${environment.apiUrl}` + '/api/meta-data' + '/' + name + '/layouts?type=View';
  //   return this.http.get<any[]>(layoutUrl);
  // }

  getPickListViews(name): Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + '/api/picklists' + '/' + name + '/layouts?type=View';
    return this.http.get<any[]>(layoutUrl);
  }

  getLayoutById(id): Observable<any> {
   // debugger;
    var layoutById = `${environment.apiUrl}` + this.picklists + '/layouts/' + id;
    return this.http.get<any>(layoutById);
  }

  displayPreview(name, fields): Observable<any> {
  
    var layoutUrl = `${environment.apiUrl}` + '/api/picklists' + '/' + name + '/values' + '?fields=' + fields;
    return this.http.get<any[]>(layoutUrl);
  }



}

