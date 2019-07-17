import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Resource } from '../model/resource'; 
import {GlobalResourceService} from '../global-resource/global-resource.service';

@Injectable({
  providedIn: 'root'
})

export class ResourceService {
  private resources: string = '/api/resources/all';
  private picklistUrl:string='/api/picklists/language/values?orderBy=text&pageIndex=1&pageSize=500';
  private currentLanguage: string;
  query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
  constructor(private http: HttpClient,private globalResourceService:GlobalResourceService) { }

  getResources(){
    return this.globalResourceService.getGlobalResources();
  }

  // getLanguages():Observable<any>{
  //   var languageUrl=`${environment.apiUrl}`+this.picklistUrl;
  //   return this.http.get<any>(languageUrl);
  // }

  public setCurrentLanguage(lan)
  {
    this.currentLanguage = lan;
  }
}
