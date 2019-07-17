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
    public interface IAdminWorkFlowStep
    {
        bool CreateWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStepInfos);
        bool CreateWorkFlowStep(Guid tenantId, WorkFlowStepInfo workFlowStep);
        bool UpdateWorkFlowSteps(Guid tenantId, WorkFlowStepInfo workFlowStep);
        bool DeleteWorkFlowSteps(Guid tenantId, Guid workFlowStepId, Guid workFlowId);
        bool MoveUpDownWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowSteps);


    }
    internal class AdminWorkFlowStep : IAdminWorkFlowStep
    {
        private readonly DataWorkFlowSteps _dataWorkFlowSteps = new DataWorkFlowSteps();

        bool IAdminWorkFlowStep.CreateWorkFlowStep(Guid tenantId, WorkFlowStepInfo workFlowStep)
        {
            return _dataWorkFlowSteps.CreateWorkFlowStep(tenantId, workFlowStep);
        }

        bool IAdminWorkFlowStep.CreateWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStepInfos)
        {
            return _dataWorkFlowSteps.CreateWorkFlowSteps(tenantId, workFlowStepInfos);
        }

        bool IAdminWorkFlowStep.DeleteWorkFlowSteps(Guid tenantId, Guid workFlowStepId, Guid workFlowId)
        {
            return _dataWorkFlowSteps.DeleteWorkFlowSteps(tenantId, workFlowStepId, workFlowId);
        }

        bool IAdminWorkFlowStep.MoveUpDownWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStep)
        {
            return _dataWorkFlowSteps.MoveUpDownWorkFlowSteps(tenantId, workFlowStep);
        }

        bool IAdminWorkFlowStep.UpdateWorkFlowSteps(Guid tenantId, WorkFlowStepInfo workFlowStep)
        {
            return _dataWorkFlowSteps.UpdateWorkFlowSteps(tenantId, workFlowStep);
        }
    }
}