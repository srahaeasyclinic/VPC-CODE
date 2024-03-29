import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LayoutModel } from '../../model/layoutmodel';

@Injectable({
  providedIn: 'root'
})


export class PicklistLayoutService {
  private layout: string = '/api/picklists';

  constructor(private http: HttpClient) { }

  getLayouts(name): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';
    return this.http.get<LayoutModel[]>(layoutUrl);
  }

  saveLayout(layoutModel, name): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts'; 
    return this.http.post(layoutUrl, layoutModel);
  }

  updateLayout(layoutModel, name, id): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id; 
    return this.http.put(layoutUrl, layoutModel);
  }
  updateFormLayout(layoutModel, name, id,type): Observable<any> {
    let layouturl=""
    layouturl=type=='picklist'?'/api/picklists':'/api/meta-data'
    let layoutUrl = `${environment.apiUrl}` + layouturl + '/' + name + '/layouts/' + id; 
    return this.http.put(layoutUrl, layoutModel);
  }
  saveLayoutDefault(layoutModel, name): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts'; 
    return this.http.patch(layoutUrl, layoutModel);
  }


  deleteLayout(id, name): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id; 
    return this.http.delete(layoutUrl);
  }

  cloneLayout(layoutModel:LayoutModel, name:string, id:string): Observable<any> {
    let layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id + '/clone';
    return this.http.post(layoutUrl, layoutModel);
  }

}
