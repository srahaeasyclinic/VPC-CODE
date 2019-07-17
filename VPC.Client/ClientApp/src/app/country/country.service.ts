import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LayoutModel } from '../model/layoutmodel';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private http: HttpClient) { }

  private picklistApi: string = '/api/picklists';


  getDefaultLayout(entityName: string, layoutType: number, layoutContext: number): Observable<LayoutModel> {
    var picklistLayoutUrl = `${environment.apiUrl}` + this.picklistApi + '/' + entityName + '/layouts/types/' + layoutType + '/contexts/' + layoutContext;
    return this.http.get<LayoutModel>(picklistLayoutUrl);
  }

  getCountries(entityName: string, fields: string, filters: string, pageIndex: number, pageSize: number, 
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

  saveCountry(entityName:string, countryObj:any) : Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName +'/values' ;
    return this.http.post(url, countryObj);
  }

  
  deleteCountry(entityName: string,id:string): Observable<any> {
    var url = `${environment.apiUrl}` + this.picklistApi +'/' + entityName + '/values/' + id; 
    return this.http.delete(url);
  }

}
