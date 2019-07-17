
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;


namespace VPC.Framework.Business.WorkFlow.Transition.User
{
    //put Guid from Work flow engine and context for work flow type
    [Transition(From = WorkFlowEngine._registration, To = WorkFlowEngine._sendToDoctor,Context = WorkFlowEngine._user,
     Key = "User_Task_Two_Process_Regis_To_SendToDoct", Id = "B81C4F2F-F004-48C7-B46C-CBA4524D2BCB",ProcessType=WorkFlowProcessType.Process)]
    public class UserTaskTwoProcessRegistrationToDoctorAttending : ITransition
    {
       WorkFlowProcessMessage ITransition.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            return objWorkFlowProcessMessage;
        }
    }
    
}
