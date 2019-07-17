import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { WorkFlowStep } from '../../../model/workflow/workflow-step';

@Injectable({
  providedIn: 'root'
})



export class ModalOperationProcessService {
  private workflowProcess: string = '/api/workflow/operation/process'; 

  constructor(private http: HttpClient) { }

  getAllOperationProcess(workFlowId): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcess+'/'+workFlowId;
    return this.http.get<any[]>(workFlowUrl);
  }

  getAllOperationProcessTask(operationName,entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcess+'/'+operationName+'/'+entityName;
    return this.http.get<any[]>(workFlowUrl);
  }

  getAllSavedOperationProcessTask(workFlowOperationId,entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcess+'/task/'+workFlowOperationId+'?entityName='+entityName;
    return this.http.get<any[]>(workFlowUrl);
  }



  // getAllInnerStepTask(innerStepId,entityName): Observable<any> {
  //   var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask+'/'+innerStepId+'?entityName='+entityName;;
  //   return this.http.get<any[]>(workFlowUrl);
  // }


  // addProcessSteps(processSteps): Observable<any> {
  //   var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask;
  //   return this.http.post(workFlowUrl, processSteps);
  // }

  // deleteProcessSteps(innerStepId): Observable<any> {
  //   var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask+'/'+innerStepId;
  //   return this.http.delete<any[]>(workFlowUrl);
  // }

}


