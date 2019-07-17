import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class SubscriptionService {
  private subscriptionsRoute: string = '/api/subscriptions';  

  constructor(private http: HttpClient) { }

  addSubscription(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute;
    return this.http.post(subscriptionUrl, info);
  }

  updateSubscription(info): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute;
    return this.http.put(subscriptionUrl, info);
  }

  deleteSubscription(subscriptionId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute+'/'+subscriptionId;
    return this.http.delete<string>(subscriptionUrl);
  }

  statusSubscription(subscriptionId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute+'/'+subscriptionId;
    return this.http.patch(subscriptionUrl,subscriptionId);
  }

  getSubscriptions(): Observable<any> {
     var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute;
     return this.http.get<any[]>(subscriptionUrl);  
  }

  getDurations(): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute+'/duration';
    return this.http.get<any[]>(subscriptionUrl);  
 }
  

  getSubscription(subscriptionId): Observable<any> {
    var subscriptionUrl = `${environment.apiUrl}` + this.subscriptionsRoute+'/'+subscriptionId;
    return this.http.get<any>(subscriptionUrl);
  } 

  getSubscriptionGroup(searchText): Observable<any> {
    var url = `${environment.apiUrl}` +'/api/picklists/subscriptiongroup/values?pageIndex=1&pageSize=100';
    return this.http.get<any>(url);
  } 


}


