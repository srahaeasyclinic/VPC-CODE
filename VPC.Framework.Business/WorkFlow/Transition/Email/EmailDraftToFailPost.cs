
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;

namespace VPC.Framework.Business.WorkFlow.Transition.Email
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._fail,Context = WorkFlowEngine._email,
     Key = "EmailDraftToFailPost", Id = "D5FD11E4-35B3-49AA-BD64-B5692EAA849E",ProcessType=WorkFlowProcessType.PostProcess)]
    public class EmailDraftToFailPost : ITransition
    {
       WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
