using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlowRole
    {
        Guid CreateWorkFlowRole(Guid tenantId, WorkFlowRoleInfo workFlowRoleInfo);
        bool CreateWorkFlowRoles(Guid tenantId, List<WorkFlowRoleInfo> workFlowRoleInfos);
        bool DeleteWorkFlowRole(Guid tenantId, Guid workFlowStepId, Guid roleId, Guid workFlowId, WorkFlowRoleType type);
        List<WorkFlowRoleInfo> GetWorkFlowRole(Guid tenantId, Guid workFlowId);
        List<WorkFlowRoleInfo> GetWorkFlowRolesByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);
        List<WorkFlowRoleInfo> GetWorkFlowRolesByStepIds(Guid tenantId, List<Guid> stepIds);
    }

    public class ManagerWorkFlowRole : IManagerWorkFlowRole
    {
        private readonly IAdminWorkFlowRole _adminWorkFlowRole = new AdminWorkFlowRole();
        private readonly IReviewWorkFlowRole _reviewWorkFlowRole = new ReviewWorkFlowRole();
        Guid IManagerWorkFlowRole.CreateWorkFlowRole(Guid tenantId, WorkFlowRoleInfo workFlowRoleInfo)
        {
            workFlowRoleInfo.RoleAssignmetId = Guid.NewGuid();
            _adminWorkFlowRole.CreateWorkFlowRole(tenantId, workFlowRoleInfo);
            return workFlowRoleInfo.RoleAssignmetId;
        }
        bool IManagerWorkFlowRole.CreateWorkFlowRoles(Guid tenantId, List<WorkFlowRoleInfo> workFlowRoleInfos)
        {
            return _adminWorkFlowRole.CreateWorkFlowRoles(tenantId,workFlowRoleInfos);  
        }

        bool IManagerWorkFlowRole.DeleteWorkFlowRole(Guid tenantId, Guid workFlowStepId, Guid roleId, Guid workFlowId, WorkFlowRoleType type)
        {
            return _adminWorkFlowRole.DeleteWorkFlowRole(tenantId, workFlowStepId, roleId, workFlowId, type);
        }

        List<WorkFlowRoleInfo> IManagerWorkFlowRole.GetWorkFlowRole(Guid tenantId, Guid workFlowId)
        {
            return _reviewWorkFlowRole.GetWorkFlowRole(tenantId, workFlowId);
        }

        List<WorkFlowRoleInfo> IManagerWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            return _reviewWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(tenantId, workFlowIds);
        }

        List<WorkFlowRoleInfo> IManagerWorkFlowRole.GetWorkFlowRolesByStepIds(Guid tenantId, List<Guid> stepIds)
        {
            return _reviewWorkFlowRole.GetWorkFlowRolesByStepIds(tenantId, stepIds);
        }
    }
}


