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
    public interface IReviewWorkFlowOperation
    {
       List<WorkFlowOperationInfo> GetWorkFlowOperations(Guid tenantId, Guid workFlowId)  ;  
        List<WorkFlowOperationInfo> GetWorkFlowOperationsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);   
    }
    internal class ReviewWorkFlowOperation : IReviewWorkFlowOperation
    {
        private readonly DataWorkFlowOperation _dataWorkFlowOperation = new DataWorkFlowOperation();

        List<WorkFlowOperationInfo> IReviewWorkFlowOperation.GetWorkFlowOperations(Guid tenantId, Guid workFlowId)
        {
            return _dataWorkFlowOperation.GetWorkFlowOperations(tenantId,workFlowId);
        }

        List<WorkFlowOperationInfo> IReviewWorkFlowOperation.GetWorkFlowOperationsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
          return _dataWorkFlowOperation.GetWorkFlowOperationsByWorkFlowIds(tenantId,workFlowIds);  
        }
    }
}