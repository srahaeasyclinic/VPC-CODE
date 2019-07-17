
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;

namespace VPC.Framework.Business.WorkFlow.Transition.User
{
     //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._registration, To = WorkFlowEngine._sendToDoctor,Context = WorkFlowEngine._user,
     Key = "User_Email_Pre_Regis_To_SendToDoct", Id = "A100F27F-7239-4628-887C-B122393C1418",ProcessType=WorkFlowProcessType.PreProcess)]
    public class UserEmailPreRegistrationToDoctorAttending : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
