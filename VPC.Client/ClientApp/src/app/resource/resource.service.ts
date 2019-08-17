import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Resource } from '../model/resource';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {
  private allResourcesUrl='/api/resources/all';
  private resourcesUrl: string = '/api/resources';
  private tenentUrl='/GetInfo/DefaultLanguage'
  private picklistUrl:string='/api/picklists/language/values?orderBy=text&pageIndex=1&pageSize=500'
  query: string = '?pageIndex=1&pageSize=10&orderBy=value';

  constructor(private http:HttpClient) { }

  getResources(language:string,orderby:string): Observable<any> {
    var resourceUrl = `${environment.apiUrl}` + this.resourcesUrl+'?orderBy='+orderby+'&language='+language;
    return this.http.get<any>(resourceUrl);
  }
  getLanguages():Observable<any>{
    var languageUrl=`${environment.apiUrl}`+this.picklistUrl;
    return this.http.get<any>(languageUrl);
  }

  getDefaultLanguage(){
    var defaultLangUrl=`${environment.apiUrl}` + this.tenentUrl;
    return this.http.get<any>(defaultLangUrl);
  }

  saveResource(resource:Resource){
    var saveResourceUrl=`${environment.apiUrl}`+this.resourcesUrl;
    return this.http.post<any>(saveResourceUrl,resource);
  }

  updateResource(resource:Resource){
    var updateResourceUrl=`${environment.apiUrl}`+this.resourcesUrl+'/'+resource.id;
    return this.http.put<any>(updateResourceUrl,resource);
  }

  deleteResource(id:any){
    var deleteResourceUrl=`${environment.apiUrl}`+this.resourcesUrl+'/'+id;
    return this.http.delete<any>(deleteResourceUrl);
  }
 getResourceByLanguageAndKey(key:string,language:string){
  var resourceUrl = `${environment.apiUrl}` + this.resourcesUrl+'/all'+'/'+key+'/'+language;
  return this.http.get<any>(resourceUrl);
 }
 ///api/resources/{key}/{language}

 getResourcesByAndOtherLanguage(key:string,language:string){
   var otherResourceUrl=`${environment.apiUrl}`+this.resourcesUrl+'/'+key+'/'+language;
   return this.http.get<any>(otherResourceUrl);
 }


 getAllResources(language): Observable<any> {
   if(language!=''){
    this.allResourcesUrl='/api/resources/all?language='+language;
   }
  var resourall = `${environment.apiUrl}` + this.allResourcesUrl;
  return this.http.get<any>(resourall);
}
ResetResourcesList(): Observable<any> {
 var resourceresetUrl = `${environment.apiUrl}` + '/api/resources/reset';
 return this.http.post<any>(resourceresetUrl,'');
}
RepairResourcesList(): Observable<any> {
var resourcrepaireUrl = `${environment.apiUrl}` + '/api/resources/repair';
return this.http.post<any>(resourcrepaireUrl,'');
}

}
