import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class BatchItemService {
  private batchItemRoute: string = '/api/batche/items';  

  constructor(private http: HttpClient) { }  

  getBatchItems(batchTypeId,itemType): Observable<any> {
     var batchItemUrl = `${environment.apiUrl}` + this.batchItemRoute+'/'+batchTypeId+'?itemType='+itemType;
     return this.http.get<any[]>(batchItemUrl);  
  }


}


