import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Resource } from '../model/resource'; 
import { Communication } from '../model/communication';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})

export class CommunicationService {
  private communication: string = '/api/settings'; 
  private context: string = '/api/settings/context';
  //public EntityNameSource: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private http: HttpClient) { }


  getCommunicationContext(): Observable<any> {
    var contextUrl = `${environment.apiUrl}` + this.context;
    return this.http.get<any>(contextUrl);
  }


  getCommunication(): Observable<any> {
    var communicationUrl = `${environment.apiUrl}` + this.communication;
    return this.http.get<any>(communicationUrl);
  }

 

  public saveCommunication(model: Communication[]): Observable<any> {
    console.log('saveCommunication');
    var menuItemUrl = `${environment.apiUrl}` + this.communication;
    
    return this.http.post(menuItemUrl, model);
  }

  public UpdateCommunication(model: Communication[]): Observable<any> {
  // console.log(JSON.stringify(model));
    var menuItemUrl = `${environment.apiUrl}` + this.communication;
    return this.http.put(menuItemUrl, model);
  }

  public GetTagsByEntityname(entityname: string): Observable<any>{
    
    var entitytagsUrl = `${environment.apiUrl}/api/metadata/` + entityname;
    return this.http.get<any>(entitytagsUrl);
}

}
export class Tags{
name:string
}