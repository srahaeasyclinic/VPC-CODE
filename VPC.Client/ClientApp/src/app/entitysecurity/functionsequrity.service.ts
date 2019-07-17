import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class FunctionSecurityService {
  private entitySecurity: string = '/api/functionSecurity';  

  constructor(private http: HttpClient) { }
  
  addFunctionSecurity(entityName,info): Observable<any> {
    var url = `${environment.apiUrl}` + this.entitySecurity+'/'+entityName;
    return this.http.post(url, info);
  }

  updateFunctionSecurity(entityName,info): Observable<any> {
    var url = `${environment.apiUrl}` + this.entitySecurity+'/'+entityName;
    return this.http.put(url, info);
  }

  getFunctionSecurity(entityName,roleId): Observable<any> {
    var url = `${environment.apiUrl}` + this.entitySecurity+'/'+entityName+'?roleId='+roleId;
    return this.http.get<any[]>(url);
  }
 

}


