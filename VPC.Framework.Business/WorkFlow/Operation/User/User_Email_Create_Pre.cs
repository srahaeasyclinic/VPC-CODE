using VPC.Entities.WorkFlow;
using VPC.Entities.WorkFlow.Engine;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Framework.Business.WorkFlow.Operation.User
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create,Context = WorkFlowEngine._user,
     Key = "User_Create_Email_Pre", Id = "FD73F8D5-406D-426D-9BCB-CBFF49E730C9",ProcessType=WorkFlowProcessType.PreProcess)]
    public class User_Email_Create_Pre : IOperation
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
