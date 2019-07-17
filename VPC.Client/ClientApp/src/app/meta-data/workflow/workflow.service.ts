import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { WorkFlow } from '../../model/workflow/workflow';

@Injectable({
  providedIn: 'root'
})



export class WorkFlowService {
  private workflow: string = '/api/workflow';
  private workflowSteps: string = '/api/workflow/steps';
  private workFlowInnerSteps: string ='/api/workflow/innerSteps'

  constructor(private http: HttpClient) { }

  addWorkFlowStep(workFlowStep): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps;
    return this.http.post(workFlowUrl, workFlowStep);
  }

  deleteWorkFlowStep(workFlowStepId,workFlowId): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps+'/'+workFlowStepId+'?workFlowId='+workFlowId;
    return this.http.delete<WorkFlow[]>(workFlowUrl);
  }

  getWorkFlow(entityName,subTypeName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflow+'/'+entityName+'/'+subTypeName;
    return this.http.get<WorkFlow[]>(workFlowUrl);
  }

  getWorkFlows(entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflow+'/'+entityName;
    return this.http.get<WorkFlow[]>(workFlowUrl);
  }

  getCurrentUserWorkflows(entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflow+'/currentWorkFlows/'+entityName;
    return this.http.get<WorkFlow[]>(workFlowUrl);
  }


  getInnerStep(entityname,transitionType): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps+'/'+entityname+'?transitionType='+transitionType;
    return this.http.get<WorkFlow[]>(workFlowUrl);
  }

  deleteWorkFlowInnerStep(workFlowInnerStepId)
  {
    var workFlowUrl = `${environment.apiUrl}` + this.workFlowInnerSteps+'/'+workFlowInnerStepId;
    return this.http.delete<WorkFlow[]>(workFlowUrl);
  }

  workFlowStep_UpdateSequence_Steps(workFlowSteps)
  {
    var workFlowUrl = `${environment.apiUrl}` +this.workflowSteps+'/sequence';
    return this.http.put(workFlowUrl, workFlowSteps);
  }

  workFlowStep_UpdateSequence_InnerSteps(workFlowInnerSteps)
  {
    var workFlowUrl = `${environment.apiUrl}` +this.workFlowInnerSteps+'/sequence';
    return this.http.put(workFlowUrl, workFlowInnerSteps);
  }

  getAllFromStep(workFlowId,entityId): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps+'/'+workFlowId+'/'+entityId;
    return this.http.get<WorkFlow[]>(workFlowUrl);
  }

  updateWorkFlowStep(step): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps;
    return this.http.put(workFlowUrl, step);
  }

  managerTransition(info)
  {
    var workFlowUrl = `${environment.apiUrl}` + this.workflow+'/transition';
    return this.http.patch(workFlowUrl, info);
  }

}


