
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.User
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._registration, To = WorkFlowEngine._sendToDoctor,Context = WorkFlowEngine._user,
     Key = "User_One_Post_Regis_To_SendToDoct", Id = "5A8302C8-C137-48B8-A1D6-540AC705630D",ProcessType=WorkFlowProcessType.PostProcess)]
    public class UserOnePostRegistrationToDoctorAttending : ITransition
    {
       WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
