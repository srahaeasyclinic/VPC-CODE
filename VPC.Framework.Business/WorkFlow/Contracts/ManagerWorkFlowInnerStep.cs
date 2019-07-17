using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions; 
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.APIs;
using VPC.Framework.Business.WorkFlow.Attribute;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
public interface IManagerWorkFlowInnerStep
{
   
    WorkFlowInnerStepInfo CreateWorkFlowInnerStep(Guid tenantId, WorkFlowInnerStepInfo workFlowInnerStep);
    bool CreateWorkFlowInnerSteps(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowInnerSteps);
    bool DeleteWorkFlowInnerStep(Guid tenantId, Guid innerStepId);
    bool MoveUpDownWorkFlowInnerStep(Guid tenantId,  List<WorkFlowInnerStepInfo> workFlowSteps );  
    List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep(Guid tenantId, Guid workFlowId);
    List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepTransactionType(Guid tenantId, Guid transactionType,Guid workFlowId); 
    List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepIds(Guid tenantId, List<Guid> stepIds);
     List<WorkFlowInnerStepInfo> GetWorkFlowInnerStepByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);
    
  //  List<object> GetPossibleInnerSteps(Guid tenantId, string entityName, string subTypeName, Guid refId);

    }

public class ManagerWorkFlowInnerStep : IManagerWorkFlowInnerStep
{       
       // private readonly IManagerWorkFlow _managerWorkFlow=new ManagerWorkFlow();
        private readonly IAdminWorkFlowInnerStep _adminWorkFlowInnerStep=new AdminWorkFlowInnerStep();
        private readonly IReviewWorkFlowInnerStep _reviewWorkFlowInnerStep=new ReviewWorkFlowInnerStep();
        private readonly IManagerWorkFlowProcess _managerWorkFlowProcess=new ManagerWorkFlowProcess();
        private IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();         
        private readonly IManagerWorkFlowStep _managerWorkFlowStep=new ManagerWorkFlowStep();
        private IManagerWorkFlowTransition _managerTrasition=new ManagerWorkFlowTransition();
     
        WorkFlowInnerStepInfo IManagerWorkFlowInnerStep.CreateWorkFlowInnerStep(Guid tenantId, WorkFlowInnerStepInfo workFlowInnerStep)
        {            
            workFlowInnerStep.InnerStepId=Guid.NewGuid();
            _adminWorkFlowInnerStep.CreateWorkFlowInnerStep(tenantId,workFlowInnerStep);
            var workFlowProcess=new List<WorkFlowProcessInfo>();
            workFlowProcess.Add(new WorkFlowProcessInfo{WorkFlowProcessId=Guid.NewGuid(),WorkFlowId=workFlowInnerStep.WorkFlowId,
                                OperationOrTransactionId=workFlowInnerStep.InnerStepId,
                                OperationOrTransactionType=(int)OperationOrTransactionType.Transaction,ProcessType=(int)WorkFlowProcessType.PreProcess });
            workFlowProcess.Add(new WorkFlowProcessInfo{WorkFlowProcessId=Guid.NewGuid(),WorkFlowId=workFlowInnerStep.WorkFlowId,
                                OperationOrTransactionId=workFlowInnerStep.InnerStepId,
                                OperationOrTransactionType=(int)OperationOrTransactionType.Transaction,ProcessType=(int)WorkFlowProcessType.Process });
            workFlowProcess.Add(new WorkFlowProcessInfo{WorkFlowProcessId=Guid.NewGuid(),WorkFlowId=workFlowInnerStep.WorkFlowId,
                                OperationOrTransactionId=workFlowInnerStep.InnerStepId,
                                OperationOrTransactionType=(int)OperationOrTransactionType.Transaction,ProcessType=(int)WorkFlowProcessType.PostProcess });
             _managerWorkFlowProcess.CreateWorkFlowProcess(tenantId,workFlowProcess);
             workFlowInnerStep.WorkFlowProcess=workFlowProcess;
             return workFlowInnerStep; 

        }

        bool IManagerWorkFlowInnerStep.CreateWorkFlowInnerSteps(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowInnerSteps)
        {
              return _adminWorkFlowInnerStep.CreateWorkFlowInnerSteps(tenantId,workFlowInnerSteps);    
        }

        bool IManagerWorkFlowInnerStep.DeleteWorkFlowInnerStep(Guid tenantId, Guid innerStepId)
        {
             return _adminWorkFlowInnerStep.DeleteWorkFlowInnerStep(tenantId,innerStepId);
        }
        bool IManagerWorkFlowInnerStep.MoveUpDownWorkFlowInnerStep(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowSteps)
        {
             return _adminWorkFlowInnerStep.MoveUpDownWorkFlowInnerStep(tenantId,workFlowSteps);
        }
        List<WorkFlowInnerStepInfo> IManagerWorkFlowInnerStep.GetWorkFlowInnerStep(Guid tenantId, Guid workFlowId)
        {
             return _reviewWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId,workFlowId);
        }

        List<WorkFlowInnerStepInfo> IManagerWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepTransactionType(Guid tenantId, Guid transactionType,Guid workFlowId)
        {
             return _reviewWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepTransactionType(tenantId,transactionType,workFlowId); 
        }

        List<WorkFlowInnerStepInfo> IManagerWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepIds(Guid tenantId, List<Guid> stepIds)
        {
              return _reviewWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepIds(tenantId,stepIds); 
        }

         List<WorkFlowInnerStepInfo> IManagerWorkFlowInnerStep.GetWorkFlowInnerStepByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
         {
               return _reviewWorkFlowInnerStep.GetWorkFlowInnerStepByWorkFlowIds(tenantId,workFlowIds); 
         }

        //    List<object> IManagerWorkFlowInnerStep.GetPossibleInnerSteps(Guid tenantId, string entityName, string subTypeName,Guid refId)
        //    {
        //        var obj = new List<object>();
        //        var entityId= iMetadataManager.GetEntityContextByEntityName(entityName); 
        //        var subTypeCode=iMetadataManager.GetSubTypeId(subTypeName);
        //        var workFlow= _managerWorkFlow.GetWorkFlow(tenantId,entityId,subTypeCode);
        //        WorkFlowTransition currentWorkflowItem = _managerTrasition.GetCurrentStepByRefId(tenantId,refId);
        //        var itsCurrentStep=_managerWorkFlowStep.GetWorkFlowSteps(tenantId,workFlow.WorkFlowId).FirstOrDefault(x => x.TransitionType.Id == currentWorkflowItem.WorkFlowStepId);
        //        List<WorkFlowInnerStepInfo> innerSteps = _reviewWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId,workFlow.WorkFlowId).FindAll(x => x.WorkFlowStepId == itsCurrentStep.WorkFlowStepId).ToList();

        //        foreach(var innerStep in innerSteps)
        //        {
        //              obj.Add(new
        //                          {
        //                               StepId = innerStep.InnerStepId,
        //                               WorkFlowId = innerStep.WorkFlowId,
        //                               WorkFlowStepId=innerStep.WorkFlowStepId,
        //                               Key = WorkFlowHelper.GetKeyByContext(entityId, innerStep.TransitionType.Id),
        //                               Value = WorkFlowHelper.GetValueByContext(entityId, innerStep.TransitionType.Id),
        //                               //AssignedRoles = _reviewWorkFlow.GetsConfigurationRoleAssignments(tenantId, workFlowStep.WorkFlowId, workFlowStep.WorkFlowStepId, new Guid(WorkFlowConfigurationRoleTypeContext.AssignedRoles), new Guid(WorkFlowExtensionStepTypeContext.ForUser)).RoleId,
        //                              // TransitionLabelKey = ApiUtility.GetTransitionLabelKeyByContext(workFlowcontext, workFlowStep.Context),                                            
        //                              // userOperator.IsAssignmentMandatory
        //                          });
        //        }
        //        return obj;
        //    }

    }
}
  

