

using VPC.Entities.WorkFlow;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    public interface IOperation
    {
        // WorkFlowProcessMessage PreProcess(dynamic obj);
        // WorkFlowProcessMessage PostProcess(dynamic obj);
        // WorkFlowProcessMessage OnCompleteProcess(dynamic obj);

        WorkFlowProcessMessage Execute(dynamic obj);
    }
}