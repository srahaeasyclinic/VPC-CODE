import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BredcrumService {
  private queryUrl:string='/api/picklists/language/values?orderBy=text&pageIndex=1&pageSize=500'
  constructor(private httpClient:HttpClient) { }

  getNameById(id:string):Observable<any>{
    var languageUrl=`${environment.apiUrl}`+this.queryUrl;
    return this.httpClient.get<any>(languageUrl);
   }
}
