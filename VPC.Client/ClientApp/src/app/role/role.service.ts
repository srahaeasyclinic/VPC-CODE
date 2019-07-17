import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class RoleService {
  private roleRoute: string = '/api/role';  

  constructor(private http: HttpClient) { }

  addRole(roleInfo): Observable<any> {
    var roleUrl = `${environment.apiUrl}` + this.roleRoute;
    return this.http.post(roleUrl, roleInfo);
  }

  updateRole(roleInfo): Observable<any> {
    var roleUrl = `${environment.apiUrl}` + this.roleRoute;
    return this.http.put(roleUrl, roleInfo);
  }

  deleteRole(roleId): Observable<any> {
    var roleUrl = `${environment.apiUrl}` + this.roleRoute+'/'+roleId;
    return this.http.delete<string>(roleUrl);
  }

  getRoles(): Observable<any> {
     var roleUrl = `${environment.apiUrl}` + this.roleRoute;
     return this.http.get<any[]>(roleUrl);  
  }

  

  getRole(roleId): Observable<any> {
    var roleUrl = `${environment.apiUrl}` + this.roleRoute+'/'+roleId;
    return this.http.get<any>(roleUrl);
  } 

  getRoleTypes(): Observable<any> {
    var roleUrl = `${environment.apiUrl}` + this.roleRoute+'/type';
    return this.http.get<any>(roleUrl);
  } 

}


