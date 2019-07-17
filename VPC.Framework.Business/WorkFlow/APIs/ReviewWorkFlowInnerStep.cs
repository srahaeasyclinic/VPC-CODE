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
    public interface IReviewWorkFlowInnerStep
    {
        List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep(Guid tenantId, Guid workFlowId); 
        List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepTransactionType(Guid tenantId, Guid transactionType,Guid workFlowId);  
        List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepIds(Guid tenantId, List<Guid> stepIds);
        List<WorkFlowInnerStepInfo> GetWorkFlowInnerStepByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);
        
    }
    internal class ReviewWorkFlowInnerStep : IReviewWorkFlowInnerStep
    {
        private readonly DataWorkFlowInnerStep _dataWorkFlowInnerStep = new DataWorkFlowInnerStep();        

        List<WorkFlowInnerStepInfo> IReviewWorkFlowInnerStep.GetWorkFlowInnerStep(Guid tenantId, Guid workFlowId)
        {
           return _dataWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId,workFlowId);
        }

        List<WorkFlowInnerStepInfo> IReviewWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepIds(Guid tenantId, List<Guid> stepIds)
        {
           return _dataWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepIds(tenantId,stepIds);
        }

        List<WorkFlowInnerStepInfo> IReviewWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepTransactionType(Guid tenantId, Guid transactionType,Guid workFlowId)
        {
            return _dataWorkFlowInnerStep.GetWorkFlowInnerStep_ByStepTransactionType(tenantId,transactionType,workFlowId);
        }
        List<WorkFlowInnerStepInfo> IReviewWorkFlowInnerStep.GetWorkFlowInnerStepByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds){
                return _dataWorkFlowInnerStep.GetWorkFlowInnerStepByWorkFlowIds(tenantId,workFlowIds);
        }

      
    }
}