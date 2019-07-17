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
    public interface IReviewWorkFlowTransition
    {
        List<WorkFlowTransition> GetTransitionHistoryByRefId(Guid tenantId, Guid refId );
              
    }
    internal class ReviewWorkFlowTransition : IReviewWorkFlowTransition
    {
        private readonly DataWorkFlowTransition _dataTransition = new DataWorkFlowTransition();

        List<WorkFlowTransition> IReviewWorkFlowTransition.GetTransitionHistoryByRefId(Guid tenantId, Guid refId)
        {
            return _dataTransition.GetTransitionHistoryByRefId(tenantId,refId);
        }
    }
}