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
    public interface IAdminWorkFlowTransition
    {
      bool CreateTransitionHistory(Guid tenantId, WorkFlowTransition trasition);
      bool UpdateTransitionHistory(Guid tenantId, WorkFlowTransition trasition);
        
    }
    internal class AdminWorkFlowTransition : IAdminWorkFlowTransition
    {
        private readonly DataWorkFlowTransition _dataTransition = new DataWorkFlowTransition();

        bool IAdminWorkFlowTransition.CreateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {        
            return _dataTransition.CreateTransitionHistory(tenantId,trasition);
        }

        bool IAdminWorkFlowTransition.UpdateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {
           return _dataTransition.UpdateTransitionHistory(tenantId,trasition);
        }
    }
}