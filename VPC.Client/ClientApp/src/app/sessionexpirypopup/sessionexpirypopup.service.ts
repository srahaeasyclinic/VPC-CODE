import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SessionexpirypopupService {
  api = 'api/security/'
  constructor(private http: HttpClient) { }
  revokeAuthorization():any {
    return this.http.get(`${environment.apiUrl}` + '/'+this.api + 'renewauthorization')
  }
}
