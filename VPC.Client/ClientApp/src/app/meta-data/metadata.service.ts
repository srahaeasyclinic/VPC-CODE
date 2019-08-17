import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Entities } from '../model/entities';
import { Rule } from '../model/rule';

@Injectable({
  providedIn: 'root'
})



export class MetadataService {
  private entities: string = '/api/metadata';
  private rules: string = '/api/rules';
  private metadata: any = null;
  private metadataByName: any = {};

  query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
  showToolbar=  new EventEmitter();
  constructor(private http: HttpClient) { }

  public getEntities(entityType:string=""): Observable<any> {
    var entitiesUrl = `${environment.apiUrl}` + this.entities;
    return this.http.get<Entities[]>(entitiesUrl, {params: {entityType: entityType}});
  }


  public getMetadataByName(name): Observable<any> {
    var entitiesUrl = `${environment.apiUrl}` + this.entities + '/' + name;
    return this.http.get<Entities[]>(entitiesUrl);
  }

  public set_metadata(p_metadata) {
    this.metadata = p_metadata;
  }

  public get_metadata() {
    return this.metadata;
  }

  public set_metadataByName(p_metadataByName, p_name) {    
    this.metadataByName[p_name] = p_metadataByName;
    //console.log('set this.metadataByName ', this.metadataByName);
  }

  public get_metadataByName(p_name) {
    //console.log('get this.metadataByName ', this.metadataByName[p_name]);
    if(this.metadataByName[p_name])
    {
      return this.metadataByName[p_name];
    }
    else
    {
      return undefined;
    }
  }


  public clearCacheMetadata()
  {
    this.metadataByName = {};
  }


  public getRuleList(entityName: string): Observable<any> {
    var ruleUrl = `${environment.apiUrl}` + this.rules + "/" + entityName + this.query;
    return this.http.get<Rule[]>(ruleUrl);
  }

  public saveRule(entityName: string, ruleModel: Rule): Observable<any> {
    var ruleUrl = `${environment.apiUrl}` + this.rules + "/" + entityName;
    return this.http.post(ruleUrl, ruleModel);
  }
  public updateRule(entityName: string, ruleModel: Rule): Observable<any> {
    var ruleUrl = `${environment.apiUrl}` + this.rules + "/" + entityName;
    return this.http.put(ruleUrl, ruleModel);
  }

  public deleteRule(entityName: string, id): Observable<any> {
    var ruleUrl = `${environment.apiUrl}` + this.rules + '/' + entityName + "/" + id;
    return this.http.delete(ruleUrl);
  }


  public getRuleById(entityName, id) {
    var ruleUrl = `${environment.apiUrl}` + this.rules + '/' + entityName + '/' + id;
    return this.http.get<Rule>(ruleUrl);
  }

}

