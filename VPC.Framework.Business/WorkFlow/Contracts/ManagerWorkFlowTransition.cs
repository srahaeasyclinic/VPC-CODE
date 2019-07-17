using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlowTransition
    {
        bool CreateTransitionHistory(Guid tenantId, WorkFlowTransition trasition);
        bool UpdateTransitionHistory(Guid tenantId, WorkFlowTransition trasition);
        List<WorkFlowTransition> GetTransitionHistoryByRefId(Guid tenantId, Guid refId);
    }

    public class ManagerWorkFlowTransition : IManagerWorkFlowTransition
    {
        private readonly IAdminWorkFlowTransition _adminTransition = new AdminWorkFlowTransition();
        private readonly IReviewWorkFlowTransition _reviewTransition = new ReviewWorkFlowTransition();

        bool IManagerWorkFlowTransition.CreateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {
            return _adminTransition.CreateTransitionHistory(tenantId, trasition);
        }

        bool IManagerWorkFlowTransition.UpdateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {
            return _adminTransition.UpdateTransitionHistory(tenantId, trasition);
        }

        List<WorkFlowTransition> IManagerWorkFlowTransition.GetTransitionHistoryByRefId(Guid tenantId, Guid refId)
        {
            return _reviewTransition.GetTransitionHistoryByRefId(tenantId, refId);
        }


    }
}


