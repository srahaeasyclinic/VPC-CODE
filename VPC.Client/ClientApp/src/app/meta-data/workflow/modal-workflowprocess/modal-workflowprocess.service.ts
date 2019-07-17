import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { WorkFlowStep } from '../../../model/workflow/workflow-step';

@Injectable({
  providedIn: 'root'
})



export class ModalWorkFlowProcessService {
  private workflowProcessTask: string = '/api/workflow/innerSteps/process/task'; 

  constructor(private http: HttpClient) { }

  getAllDefinedProcessTask(fromStep,toStep,entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask+'/'+fromStep+'/'+toStep+'/'+'?entityName='+entityName;
    return this.http.get<any[]>(workFlowUrl);
  }

  getAllInnerStepTask(innerStepId,entityName): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask+'/'+innerStepId+'?entityName='+entityName;
    return this.http.get<any[]>(workFlowUrl);
  }


  addProcessSteps(processSteps): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask;
    return this.http.post(workFlowUrl, processSteps);
  }

  deleteProcessSteps(innerStepId): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowProcessTask+'/'+innerStepId;
    return this.http.delete<any[]>(workFlowUrl);
  }

}


