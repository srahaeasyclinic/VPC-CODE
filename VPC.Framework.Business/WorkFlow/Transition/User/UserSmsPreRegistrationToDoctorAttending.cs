
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.User
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._registration, To = WorkFlowEngine._sendToDoctor,Context = WorkFlowEngine._user,
     Key = "User_Sms_Pre_Regis_To_SendToDoct", Id = "38F168EE-A140-489D-9803-39FE9702A89C",ProcessType=WorkFlowProcessType.PreProcess)]
    public class UserSmsPreRegistrationToDoctorAttending : ITransition
    {
        WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
