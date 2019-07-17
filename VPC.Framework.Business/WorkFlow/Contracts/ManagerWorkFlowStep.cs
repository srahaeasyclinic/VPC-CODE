using System;
using System.Collections.Generic;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlowStep
    {
        bool CreateWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStepInfos);
        Guid CreateWorkFlowStep(Guid tenantId, WorkFlowStepInfo workFlowStep);
        bool UpdateWorkFlowSteps(Guid tenantId, WorkFlowStepInfo workFlowStep);
        bool DeleteWorkFlowSteps(Guid tenantId, Guid workFlowStepId, Guid workFlowId);
        bool MoveUpDownWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowSteps);
        List<WorkFlowStepInfo> GetWorkFlowSteps(Guid tenantId, Guid workFlowId);
        List<WorkFlowStepInfo> GetWorkFlowStepsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);
        List<WorkFlowStepInfo> GetWorkFlowStepsByUserId(Guid tenantId, Guid userId,bool isSuperAdmin);
        List<Guid> GetAssignedWorkFlowStepsOfUser(Guid tenantId, Guid userId, string entityId, string subTypeCode);
    }

    public class ManagerWorkFlowStep : IManagerWorkFlowStep
    {
        private readonly IAdminWorkFlowStep _adminWorkFlowStep = new AdminWorkFlowStep();
        private readonly IReviewWorkFlowStep _reviewWorkFlowStep = new ReviewWorkFlowStep();

        Guid IManagerWorkFlowStep.CreateWorkFlowStep(Guid tenantId, WorkFlowStepInfo workFlowStep)
        {
            workFlowStep.WorkFlowStepId = Guid.NewGuid();
            _adminWorkFlowStep.CreateWorkFlowStep(tenantId, workFlowStep);
            return workFlowStep.WorkFlowStepId;
        }

         bool IManagerWorkFlowStep.CreateWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStepInfos)
         {
           return _adminWorkFlowStep.CreateWorkFlowSteps(tenantId, workFlowStepInfos);  
         }

        bool IManagerWorkFlowStep.DeleteWorkFlowSteps(Guid tenantId, Guid workFlowStepId, Guid workFlowId)
        {
            return _adminWorkFlowStep.DeleteWorkFlowSteps(tenantId, workFlowStepId, workFlowId);
        }
        bool IManagerWorkFlowStep.MoveUpDownWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowSteps)
        {
            return _adminWorkFlowStep.MoveUpDownWorkFlowSteps(tenantId, workFlowSteps);
        }

        bool IManagerWorkFlowStep.UpdateWorkFlowSteps(Guid tenantId, WorkFlowStepInfo workFlowStep)
        {
            return _adminWorkFlowStep.UpdateWorkFlowSteps(tenantId, workFlowStep);
        }

        List<WorkFlowStepInfo> IManagerWorkFlowStep.GetWorkFlowSteps(Guid tenantId, Guid workFlowId)
        {
            return _reviewWorkFlowStep.GetWorkFlowSteps(tenantId, workFlowId);
        }

        List<WorkFlowStepInfo> IManagerWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            return _reviewWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(tenantId, workFlowIds);
        }

        List<WorkFlowStepInfo> IManagerWorkFlowStep.GetWorkFlowStepsByUserId(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
            return _reviewWorkFlowStep.GetWorkFlowStepsByUserId(tenantId, userId,isSuperAdmin);
        }

        List<Guid> IManagerWorkFlowStep.GetAssignedWorkFlowStepsOfUser(Guid tenantId, Guid userId, string entityId, string subTypeCode)
        {
           return _reviewWorkFlowStep.GetAssignedWorkFlowStepsOfUser(tenantId, userId,entityId,subTypeCode); 
        }
    }
}


