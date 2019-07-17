
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.Email
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._sent,Context = WorkFlowEngine._email,
     Key = "EmailDraftToSentPost", Id = "A5644ED8-3BE0-4677-BEC8-D76B3832BD0A",ProcessType=WorkFlowProcessType.PostProcess)]
    public class EmailDraftToSentPost : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
