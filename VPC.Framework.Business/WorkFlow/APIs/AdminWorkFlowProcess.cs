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
    public interface IAdminWorkFlowProcess
    {
       bool CreateWorkFlowProcess(Guid tenantId, List<WorkFlowProcessInfo> workFlowSteps);        
    }
    internal class AdminWorkFlowProcess : IAdminWorkFlowProcess
    {
        private readonly DataWorkFlowProcess _dataWorkFlowProcess = new DataWorkFlowProcess();

        bool IAdminWorkFlowProcess.CreateWorkFlowProcess(Guid tenantId, List<WorkFlowProcessInfo> workFlowSteps)
        {
            return _dataWorkFlowProcess.CreateWorkFlowProcess(tenantId,workFlowSteps);
        }
    }
}