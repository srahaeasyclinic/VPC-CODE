using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions; 
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Data;

namespace VPC.Framework.Business.WorkFlow.APIs
{
    public interface IReviewWorkFlowStep
    {
      List<WorkFlowStepInfo> GetWorkFlowSteps(Guid tenantId, Guid workFlowId);  
      List<WorkFlowStepInfo> GetWorkFlowStepsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);      
      List<WorkFlowStepInfo> GetWorkFlowStepsByUserId(Guid tenantId,Guid userId,bool isSuperAdmin);
      List<Guid> GetAssignedWorkFlowStepsOfUser(Guid tenantId, Guid userId,string entityId,string subTypeCode);
    }
    internal class ReviewWorkFlowStep : IReviewWorkFlowStep
    {
        private readonly DataWorkFlowSteps _dataWorkFlowSteps = new DataWorkFlowSteps();
        List<WorkFlowStepInfo> IReviewWorkFlowStep.GetWorkFlowSteps(Guid tenantId, Guid workFlowId)
        {
           return _dataWorkFlowSteps.GetWorkFlowSteps(tenantId,workFlowId);
        }

        List<WorkFlowStepInfo> IReviewWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
               return _dataWorkFlowSteps.GetWorkFlowStepsByWorkFlowIds(tenantId,workFlowIds);
        }

        List<WorkFlowStepInfo> IReviewWorkFlowStep.GetWorkFlowStepsByUserId(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
            return _dataWorkFlowSteps.GetWorkFlowStepsByUserId(tenantId,userId, isSuperAdmin); 
        }

        List<Guid> IReviewWorkFlowStep.GetAssignedWorkFlowStepsOfUser(Guid tenantId, Guid userId, string entityId, string subTypeCode)
        {
           return _dataWorkFlowSteps.GetAssignedWorkFlowStepsOfUser(tenantId,userId,entityId,subTypeCode);   
        }
    }
}