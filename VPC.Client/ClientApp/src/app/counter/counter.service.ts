import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CounterService {
  private counterUrl:string='/api/counters';

  query: string = '?pageIndex=1&pageSize=10&orderBy=value';


  constructor(private http:HttpClient) { }


  getCounters(): Observable<any> {
    var getCountersUrl = `${environment.apiUrl}` + this.counterUrl;
    return this.http.get<any>(getCountersUrl);
  }
  getCountersByEntity(entityName:string): Observable<any> {
    var getCountersByEntityUrl = `${environment.apiUrl}` + this.counterUrl+'/'+entityName;
    return this.http.get<any>(getCountersByEntityUrl);
  }
  getCounterById(counterId:number): Observable<any> {
    var getCounterByIdUrl = `${environment.apiUrl}` + this.counterUrl+'/'+counterId;
    return this.http.get<any>(getCounterByIdUrl);
  }

  saveCounter(counter:any):Observable<any>{
    var saveCounterUrl=`${environment.apiUrl}`+this.counterUrl;
    return this.http.post<any>(saveCounterUrl,counter);
  }

  updateCounter(counter:any):Observable<any>{
    var updateCounterUrl=`${environment.apiUrl}`+this.counterUrl;
    return this.http.put<any>(updateCounterUrl,counter);
  }

  deleteCounter(counterId:string):Observable<any>{
    var deleteCounterUrl=`${environment.apiUrl}`+this.counterUrl+'/'+counterId;
    return this.http.delete<any>(deleteCounterUrl);
  }
  

}
