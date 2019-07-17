
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.Email
{    
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._fail,Context = WorkFlowEngine._email,
     Key = "EmailDraftToFailPre", Id = "990A07B3-969B-45EA-963F-F4CD14B08FB7",ProcessType=WorkFlowProcessType.PreProcess)]
    public class EmailDraftToFailPre : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
