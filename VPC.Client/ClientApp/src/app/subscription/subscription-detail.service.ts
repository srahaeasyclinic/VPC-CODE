import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class SubscriptionDetailService {
  private subscriptionEntityRoute: string = '/api/subscription/entities';  
  private subscriptionDetailRoute: string = '/api/subscription/entity/details';  
  private subscriptionFeatureRoute: string = '/api/setting'; 

  constructor(private http: HttpClient) { }

  //Entity
  addSubscriptionEntity(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute;
    return this.http.post(subscriptionUrl, info);
  }

  updateSubscriptionEntity(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute;
    return this.http.put(subscriptionUrl, info);
  }

  deleteSubscriptionEntity(subscriptionEntityId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute+'/'+subscriptionEntityId;
    return this.http.delete<string>(subscriptionUrl);
  }
  

  getSubscriptionEntities(subscriptionId): Observable<any> {
     var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute+'/'+subscriptionId;
     return this.http.get<any[]>(subscriptionUrl);  
  }

  

  getSubscriptionEntity(subscriptionId,subscriptionEntityId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute+'/'+subscriptionId+'/'+subscriptionEntityId;
    return this.http.get<any>(subscriptionUrl);
  } 

  getLimitCount(): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionEntityRoute+'/limitCount';
    return this.http.get<any[]>(subscriptionUrl);  
 }

  // Detail

  addEntityDetail(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionDetailRoute;
    return this.http.post(subscriptionUrl, info);
  }

  updateEntityDetail(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionDetailRoute;
    return this.http.put(subscriptionUrl, info);
  }

  deleteEntityDetail(subscriptionEntityDetailId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionDetailRoute+'/'+subscriptionEntityDetailId;
    return this.http.delete<string>(subscriptionUrl);
  }
  

  getEntityDetails(subscriptionEntityId): Observable<any> {
     var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionDetailRoute+'/'+subscriptionEntityId;
     return this.http.get<any[]>(subscriptionUrl);  
  }  

  getFeatures(entityId): Observable<any> {
    var url = `${environment.apiUrl}` + this.subscriptionFeatureRoute+'/features/'+entityId;
    return this.http.get<any>(url);
  } 

  getReports(entityId): Observable<any> {
    var url = `${environment.apiUrl}` + this.subscriptionFeatureRoute+'/reports/'+entityId;
    return this.http.get<any>(url);
  } 

  getDashlets(entityId): Observable<any> {
    var url = `${environment.apiUrl}` + this.subscriptionFeatureRoute+'/dashlets/'+entityId;
    return this.http.get<any>(url);
  } 

 



}


