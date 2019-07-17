
using System;
using VPC.Entities.WorkFlow;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    public interface ITransitionFlowEngine
    {       
        WorkFlowResultMessage<WorkFlowProcessMessage> PreProcess(Guid tenantId, TransitionWapper transitionWapper);
        WorkFlowResultMessage<WorkFlowProcessMessage> PostProcess(Guid tenantId, TransitionWapper transitionWapper);
        WorkFlowResultMessage<WorkFlowProcessMessage> Process(Guid tenantId,TransitionWapper transitionWapper);
        
    }
}