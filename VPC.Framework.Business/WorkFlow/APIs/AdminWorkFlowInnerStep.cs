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
    public interface IAdminWorkFlowInnerStep
    {
        bool CreateWorkFlowInnerStep(Guid tenantId, WorkFlowInnerStepInfo workFlowInnerStep);
        bool CreateWorkFlowInnerSteps(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowInnerSteps);
        bool DeleteWorkFlowInnerStep(Guid tenantId, Guid innerStepId);
        bool MoveUpDownWorkFlowInnerStep(Guid tenantId,  List<WorkFlowInnerStepInfo> workFlowSteps );
        
    }
    internal class AdminWorkFlowInnerStep : IAdminWorkFlowInnerStep
    {
        private readonly DataWorkFlowInnerStep _dataWorkFlowInnerStep = new DataWorkFlowInnerStep();

        bool IAdminWorkFlowInnerStep.CreateWorkFlowInnerStep(Guid tenantId, WorkFlowInnerStepInfo workFlowInnerStep)
        {
            return _dataWorkFlowInnerStep.CreateWorkFlowInnerStep(tenantId,workFlowInnerStep);
        }
        bool IAdminWorkFlowInnerStep.CreateWorkFlowInnerSteps(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowInnerSteps)
        {
            return _dataWorkFlowInnerStep.CreateWorkFlowInnerSteps(tenantId,workFlowInnerSteps);
        }

        bool IAdminWorkFlowInnerStep.DeleteWorkFlowInnerStep(Guid tenantId, Guid innerStepId)
        {
                return _dataWorkFlowInnerStep.DeleteWorkFlowInnerStep(tenantId,innerStepId);
        }

        bool IAdminWorkFlowInnerStep.MoveUpDownWorkFlowInnerStep(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowSteps)
        {
               return _dataWorkFlowInnerStep.MoveUpDownWorkFlowInnerStep(tenantId,workFlowSteps);
        }
    }
}