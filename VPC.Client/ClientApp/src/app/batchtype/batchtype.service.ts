import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class BatchTypeService {
  private batchRoute: string = '/api/batches';  

  constructor(private http: HttpClient) { }

  addBatch(info): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute;
    return this.http.post(batchUrl, info);
  }

  updateBatch(info): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute;
    return this.http.put(batchUrl, info);
  }

  deleteBatch(batchTypeId): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute+'/'+batchTypeId;
    return this.http.delete<string>(batchUrl);
  }

  updateStatus(batchTypeId): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute+'/'+batchTypeId;
    return this.http.patch(batchUrl,batchTypeId);
  }

  getBatches(): Observable<any> {
     var batchUrl = `${environment.apiUrl}` + this.batchRoute;
     return this.http.get<any[]>(batchUrl);  
  }

  

  getBatch(batchTypeId): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute+'/'+batchTypeId;
    return this.http.get<any>(batchUrl);
  } 

  updateBatchItemNextRunTime(): Observable<any> {
    var batchUrl = `${environment.apiUrl}` + this.batchRoute+'/item/nextRunTime';
    return this.http.put(batchUrl, '');
  } 

 

}


