import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class SchedulerService {
  private schedulerRoute: string = '/api/schedulerConfigurations';  

  constructor(private http: HttpClient) { }
  //----------------------Scheduler-------------------------------------

  addScheduler(info): Observable<any> {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute;
    return this.http.post(schedulerUrl, info);
  }

  getScheduler(batchId): Observable<any> {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute+'/'+batchId ;
    return this.http.get<any>(schedulerUrl);
  } 

  getMonths()
  {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute+'/months';
    return this.http.get<any>(schedulerUrl);
  }

  getUnits()
  {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute+'/unit';
    return this.http.get<any>(schedulerUrl);
  }

  getWeekDays()
  {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute+'/weekdays';
    return this.http.get<any>(schedulerUrl);
  }

  getHours()
  {
    var schedulerUrl = `${environment.apiUrl}` + this.schedulerRoute+'/hours';
    return this.http.get<any>(schedulerUrl);
  }

}


