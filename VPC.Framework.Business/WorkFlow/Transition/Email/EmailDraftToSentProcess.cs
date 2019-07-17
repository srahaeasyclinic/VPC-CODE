
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.Email
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._sent,Context = WorkFlowEngine._email,
     Key = "EmailDraftToSentProcess", Id = "3B3EC719-E917-4398-919F-322E7F2E0D30",ProcessType=WorkFlowProcessType.Process)]
    public class EmailDraftToSentProcess : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
