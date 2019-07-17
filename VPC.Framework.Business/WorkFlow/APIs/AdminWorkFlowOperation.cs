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
    public interface IAdminWorkFlowOperation
    {
       bool CreateWorkFlowOperations(Guid tenantId, List<WorkFlowOperationInfo> workFlowOperations);        
    }
    internal class AdminWorkFlowOperation : IAdminWorkFlowOperation
    {
        private readonly DataWorkFlowOperation _dataWorkFlowOperation = new DataWorkFlowOperation();
        bool IAdminWorkFlowOperation.CreateWorkFlowOperations(Guid tenantId, List<WorkFlowOperationInfo> workFlowOperations)
        {
            return _dataWorkFlowOperation.CreateWorkFlowOperations(tenantId,workFlowOperations);
        }
    }
}