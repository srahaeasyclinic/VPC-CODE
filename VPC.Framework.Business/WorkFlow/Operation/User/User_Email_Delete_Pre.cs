using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Framework.Business.WorkFlow.Operation.User
{
     //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Delete,Context = WorkFlowEngine._user,
     Key = "User_Email_Delete_Pre", Id = "408821A6-6C53-463F-BAB3-ACC10AF3C1C0",ProcessType=WorkFlowProcessType.PreProcess)]
    public class User_Email_Delete_Pre : IOperation
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
