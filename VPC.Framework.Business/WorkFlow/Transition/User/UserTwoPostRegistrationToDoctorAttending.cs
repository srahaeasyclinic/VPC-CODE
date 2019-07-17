
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;

namespace VPC.Framework.Business.WorkFlow.Transition.User
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._registration, To = WorkFlowEngine._sendToDoctor,Context = WorkFlowEngine._user,
     Key = "User_Two_Post_Regis_To_SendToDoct", Id = "FA9FA1A8-9740-4291-837F-E804489BE1DA",ProcessType=WorkFlowProcessType.PostProcess)]
    public class UserTwoPostRegistrationToDoctorAttending : ITransition
    {
       WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
