import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Picklists } from '../model/picklists'; 
import { Entities } from '../model/entities';
import { Resource } from '../model/resource'; 


@Injectable({
  providedIn: 'root'
})



export class UserService {
  private entityResource: string = '/api/entities/user?subType=EN10003';


  query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
  constructor(private http: HttpClient) { }



  saveUser(userObj:any) : Observable<any> {
    var layoutUrl = `${environment.apiUrl}` + this.entityResource;
    return this.http.post(layoutUrl, userObj);
  }

}

