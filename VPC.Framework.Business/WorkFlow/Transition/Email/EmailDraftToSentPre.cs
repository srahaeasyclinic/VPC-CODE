
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.Email
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._draft, To = WorkFlowEngine._sent,Context = WorkFlowEngine._email,
     Key = "EmailDraftToSentPre", Id = "FDCD64A3-6E53-468E-8FE9-947308E08A3F",ProcessType=WorkFlowProcessType.PreProcess)]
    public class EmailDraftToSentPre : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
