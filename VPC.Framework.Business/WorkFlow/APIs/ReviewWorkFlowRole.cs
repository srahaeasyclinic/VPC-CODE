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
    public interface IReviewWorkFlowRole
    {
      List<WorkFlowRoleInfo> GetWorkFlowRole(Guid tenantId, Guid workFlowId); 
      List<WorkFlowRoleInfo> GetWorkFlowRolesByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds); 
      List<WorkFlowRoleInfo> GetWorkFlowRolesByStepIds(Guid tenantId, List<Guid> stepIds);      
    }
    internal class ReviewWorkFlowRole : IReviewWorkFlowRole
    {
        private readonly DataWorkFlowRole _dataWorkFlowRole = new DataWorkFlowRole();

        List<WorkFlowRoleInfo> IReviewWorkFlowRole.GetWorkFlowRole(Guid tenantId, Guid workFlowId)
        {
           return _dataWorkFlowRole.GetWorkFlowRole(tenantId,workFlowId);
        }

         List<WorkFlowRoleInfo> IReviewWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
           return _dataWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(tenantId,workFlowIds); 
        }

        List<WorkFlowRoleInfo> IReviewWorkFlowRole.GetWorkFlowRolesByStepIds(Guid tenantId, List<Guid> stepIds)
        {
           return _dataWorkFlowRole.GetWorkFlowRolesByStepIds(tenantId,stepIds);  
        }
    }
}