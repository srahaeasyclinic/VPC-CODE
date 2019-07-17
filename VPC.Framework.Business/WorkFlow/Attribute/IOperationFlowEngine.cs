
using System;
using VPC.Entities.WorkFlow;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    public interface IOperationFlowEngine
    {       
        WorkFlowResultMessage<WorkFlowProcessMessage> PreProcess(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties);
        WorkFlowResultMessage<WorkFlowProcessMessage> PostProcess(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties);
        WorkFlowResultMessage<WorkFlowProcessMessage> Process(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties);
        void FirstOperation(Guid tenantId, WorkFlowProcessProperties properties);
    }
}