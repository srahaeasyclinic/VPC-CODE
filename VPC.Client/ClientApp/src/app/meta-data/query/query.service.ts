import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Entities } from '../../model/entities';

@Injectable({
  providedIn: 'root'
})


export class QueryService {
  private entitiesApi: string = '/api/metadata';
  private dynamicQueryApi: string = '/api/query';
  private operatorApi: string = '/api/comparisons';

  constructor(private http: HttpClient) { }


  getEntityByName(name: string): Observable<any> {
    return this.http.get<Entities[]>(`${environment.apiUrl}` + this.entitiesApi + '/' + name);
  }

  getResult(entityName: string, query: string): Observable<any> {
    let entitiesUrl = `${environment.apiUrl}` + this.dynamicQueryApi + '/' + entityName + '/execute?' + query;
    return this.http.get<Entities[]>(entitiesUrl);
  }

  buildQuery(entityName: string, query: string): Observable<any> {
    let entitiesUrl = `${environment.apiUrl}` + this.dynamicQueryApi + '/' + entityName + '?'+ query;
    return this.http.get<Entities[]>(entitiesUrl);
  }

  getOperators(): Observable<any> {
    return this.http.get<Entities[]>(`${environment.apiUrl}` + this.operatorApi);
  }
}

