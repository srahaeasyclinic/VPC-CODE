import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { WorkFlowStep } from '../../../model/workflow/workflow-step';

@Injectable({
  providedIn: 'root'
})



export class ModalWorkFlowInnerStepService {
  private workflowSteps: string = '/api/workflow/steps';
  private workflowInnerSteps: string = '/api/workflow/innerSteps';

  constructor(private http: HttpClient) { }

  getInnerStep(entityname,transitionType,workFlowId): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowSteps+'/'+entityname+'?transitionType='+transitionType+'&workFlowId='+workFlowId;
    return this.http.get<WorkFlowStep[]>(workFlowUrl);
  }

  addInnerStep(workFlowInnerStep): Observable<any> {
    var workFlowUrl = `${environment.apiUrl}` + this.workflowInnerSteps;
    return this.http.post(workFlowUrl, workFlowInnerStep);
  }


}


