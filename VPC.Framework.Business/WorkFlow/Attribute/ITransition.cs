
using VPC.Entities.WorkFlow;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    public interface ITransition
    {
        WorkFlowProcessMessage Execute(dynamic obj);
        // WorkFlowProcessMessage PreProcess(dynamic obj);
        // WorkFlowProcessMessage PostProcess(dynamic obj);
        // WorkFlowProcessMessage OnCompleteProcess(dynamic obj);
    }
}