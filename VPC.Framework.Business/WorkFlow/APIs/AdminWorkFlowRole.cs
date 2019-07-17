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
    public interface IAdminWorkFlowRole
    {
       bool CreateWorkFlowRoles(Guid tenantId, List<WorkFlowRoleInfo> workFlowRoleInfos);
       bool CreateWorkFlowRole(Guid tenantId, WorkFlowRoleInfo workFlowRoleInfo);
       bool DeleteWorkFlowRole(Guid tenantId, Guid workFlowStepId,Guid roleId,Guid workFlowId,WorkFlowRoleType type);

        
    }
    internal class AdminWorkFlowRole : IAdminWorkFlowRole
    {
        private readonly DataWorkFlowRole _dataWorkFlowRole = new DataWorkFlowRole();

        bool IAdminWorkFlowRole.CreateWorkFlowRole(Guid tenantId, WorkFlowRoleInfo workFlowRoleInfo)
        {
            return _dataWorkFlowRole.CreateWorkFlowRole(tenantId,workFlowRoleInfo);
        }

        bool IAdminWorkFlowRole.CreateWorkFlowRoles(Guid tenantId, List<WorkFlowRoleInfo> workFlowRoleInfos)
        {
            return _dataWorkFlowRole.CreateWorkFlowRoles(tenantId,workFlowRoleInfos);
        }

        bool IAdminWorkFlowRole.DeleteWorkFlowRole(Guid tenantId, Guid workFlowStepId, Guid roleId,Guid workFlowId,WorkFlowRoleType type)
        {
            return _dataWorkFlowRole.DeleteWorkFlowRole(tenantId,workFlowStepId,roleId,workFlowId,type);
        }
    }
}