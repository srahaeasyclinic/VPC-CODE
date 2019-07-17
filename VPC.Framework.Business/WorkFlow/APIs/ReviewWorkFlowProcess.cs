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
    public interface IReviewWorkFlowProcess
    {
       List<WorkFlowProcessInfo> GetWorkFlowProcess(Guid tenantId, Guid workFlowId);
       List<WorkFlowProcessInfo> GetWorkFlowProcessByOperationOrTransitionIds(Guid tenantId, List<Guid> operationOrTransactionIds);   
       List<WorkFlowProcessInfo> GetWorkFlowProcessByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);     
    }
    internal class ReviewWorkFlowProcess : IReviewWorkFlowProcess
    {
        private readonly DataWorkFlowProcess _dataWorkFlowProcess = new DataWorkFlowProcess();

        List<WorkFlowProcessInfo> IReviewWorkFlowProcess.GetWorkFlowProcess(Guid tenantId, Guid workFlowId)
        {
           return _dataWorkFlowProcess.GetWorkFlowProcess(tenantId,workFlowId);
        }

        List<WorkFlowProcessInfo> IReviewWorkFlowProcess.GetWorkFlowProcessByOperationOrTransitionIds(Guid tenantId, List<Guid> operationOrTransactionIds)
        {
                return _dataWorkFlowProcess.GetWorkFlowProcessByOperationOrTransitionIds(tenantId,operationOrTransactionIds);  
        }

        List<WorkFlowProcessInfo> IReviewWorkFlowProcess.GetWorkFlowProcessByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            return _dataWorkFlowProcess.GetWorkFlowProcessByWorkFlowIds(tenantId,workFlowIds);  
        }
    }
}