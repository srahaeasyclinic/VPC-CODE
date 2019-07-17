
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.Email
{
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._fail,Context = WorkFlowEngine._email,
     Key = "EmailDraftToFailProcess", Id = "28E2C92F-0415-4C5D-B182-872E74A6ED0D",ProcessType=WorkFlowProcessType.Process)]
    public class EmailDraftToFailProcess : ITransition
    {
       WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
