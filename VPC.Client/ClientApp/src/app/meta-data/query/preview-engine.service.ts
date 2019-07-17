import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Entities } from '../../model/entities';

@Injectable({
  providedIn: 'root'
})
export class PreviewEngineService {
  private queryentities:string = '/api/query/user/execute';
  
  constructor(private http: HttpClient) { }

  etResult(fields,orderBy,pageIndex,pageSize, filter, maxCount ): Observable<any> {
    
    var entitiesUrl = `${environment.apiUrl}` + this.queryentities + '?fields='+ fields + '&orderBy='+ orderBy + '&pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&filter=' + filter + '&max='+ maxCount;
    //console.log(entitiesUrl);
    return this.http.get<Entities[]>(entitiesUrl);
  }


}
