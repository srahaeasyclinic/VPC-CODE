import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class EntityValueService {

  constructor(private http: HttpClient) { }

  private api: string = '/api/entities/';
  private apiMetadata: string = '/api/metadata/';
  private relations: string = '/api/relations/';
  private pageindex: number = 1;
  private pageSize: number = 10;
  

  getEntityValues(entityName, fields): Observable<any> {
    let url = `${environment.apiUrl}` +this.api + entityName + '?fields=' + fields;
    return this.http.get<any[]>(url);
  }

  getEntityDetails(entityName: string,id:string,subtype:string): Observable<any> {
    let url = `${environment.apiUrl}` + this.api  + entityName + '/' + id +'?subType=' + subtype; 
    return this.http.get<any[]>(url);
  }


  saveEntityValue(entityName: string, entitySubtype: string, objValue: any): Observable<any> {
    let url = `${environment.apiUrl}` + this.api + entityName + '?subType=' + entitySubtype;
    return this.http.post(url, objValue);
  }

  updateEntityValues(entityName: string, metaDataObj: any, id: string, subType: string): Observable<any> {
    let url = `${environment.apiUrl}` + this.api + entityName + '/' + id + '?subType=' + subType;
    return this.http.put(url, metaDataObj);
  }

  deleteEntityValue(entityName: string,id:string): Observable<any> {
    let url = `${environment.apiUrl}` + this.api + entityName + '/' + id; 
    return this.http.delete(url);
  }

  getEntitySubTypes(entityName): Observable<any> {
    let url = `${environment.apiUrl}` +this.apiMetadata + entityName +'/sub-types' ;
    return this.http.get<any[]>(url);
  }

  getDetailEntities(entityName:string, id:string, detailEntityName:string,query:string) : Observable<any>
  {
    //var queryString = "?pageIndex=" + this.pageindex + "&pageSize=" + this.pageSize;
    var layoutUrl = `${environment.apiUrl}` + this.api + entityName + '/' + id + '/' + detailEntityName +  query;
    return this.http.get<any[]>(layoutUrl);
  }

  // updateIntersect(entityName: string, id: string, intersectEntityName: string, linkerEntityName:string, obj:object): Observable<any> {
  //   let url = `${environment.apiUrl}` + this.relations + entityName + '/' + id + '/'+intersectEntityName+"/"+linkerEntityName;

  //   return this.http.put(url, obj);
  // }

}