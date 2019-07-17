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
    public interface IAdminWorkFlow
    {        
        bool CreateWorkFlow(Guid tenantId, WorkFlowInfo workflowInfo);    
        bool CreateWorkFlows(Guid tenantId, List<WorkFlowInfo> workflowInfos);    
    }
    internal  class AdminWorkFlow : IAdminWorkFlow
    {
        private readonly DataWorkFlow _dataWorkFlow = new DataWorkFlow(); 
        bool IAdminWorkFlow.CreateWorkFlow(Guid tenantId, WorkFlowInfo workflowInfo)
        {
            return _dataWorkFlow.CreateWorkFlow(tenantId, workflowInfo);
        }

        bool IAdminWorkFlow.CreateWorkFlows(Guid tenantId, List<WorkFlowInfo> workflowInfos)
        {
            return _dataWorkFlow.CreateWorkFlows(tenantId, workflowInfos);
        }
    }
}