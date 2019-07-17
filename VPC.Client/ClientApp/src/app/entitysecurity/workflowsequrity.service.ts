import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})



export class WorkFlowSecurityService {
  private workflowSecurity: string = '/api/workflow/steps/roles';  

  constructor(private http: HttpClient) { }
  

  addWorkFlowRoleSecurity(workFlowRole): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSecurity;
    return this.http.post(workFlowUrl, workFlowRole);
  }

  getWorkFlowSecurity(entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSecurity+'/'+entityName;
    return this.http.get<any[]>(workFlowUrl);
  }

  deleteWorkFlowRoleSecurity(workFlowStepId,roleId,workFlowId,type): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSecurity+'/'+workFlowStepId+'?roleId='+roleId+'&workFlowId='+workFlowId+'&type='+type;
    return this.http.delete<string>(workFlowUrl);
  }

  

 

}


