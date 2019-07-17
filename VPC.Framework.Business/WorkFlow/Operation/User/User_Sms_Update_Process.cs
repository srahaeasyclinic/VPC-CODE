

using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Framework.Business.WorkFlow.Operation.User
{    
    [Operation(OperationName = Operations.Update,Context = WorkFlowEngine._user,
     Key = "User_Sms_Update_Process", Id = "8DCAFC95-AD12-4123-92E6-02F3B0AF62CE",ProcessType=WorkFlowProcessType.Process)]
    public class User_Sms_Update_Process : IOperation
    {
         #region ITransition Members

        // WorkFlowProcessMessage IOperation.PreProcess(dynamic obj)
        // {
        //     var workFlowProcessProperties = (WorkFlowProcessProperties) obj[0];
        //   //  var extraInfo = (ObjectWrapper) obj[1];
        //     var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
        //     objWorkFlowProcessMessage.Success = true;
           
        //     return objWorkFlowProcessMessage;
        // }

        // WorkFlowProcessMessage IOperation.PostProcess(dynamic obj)
        // {
        //     var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};

        //     //throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperation);
        //     return objWorkFlowProcessMessage;
        // }

        // WorkFlowProcessMessage IOperation.OnCompleteProcess(dynamic obj)
        // {
        //     var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};

        //     //throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperation);
        //     return objWorkFlowProcessMessage;
        // }

        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
             var objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};

            //throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperation);
            return objWorkFlowProcessMessage;
        }

        #endregion
    }
    
}
