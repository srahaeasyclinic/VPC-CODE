using System;
using System.ComponentModel;

namespace VPC.Entities.WorkFlow
{
    [Serializable]
    public enum WorkFlowMessage
    {
        [Description("Insufficient balance")]
        Insufficientbalance = 1,
        
        [Description("No work flow configured for this workflow type !")]
        NoOperationActivity = 2,

        [Description("Invalid Operation")]
        InValidOperation = 3,

        [Description("Action is not valid")]
        InvalidAction = 4,

        [Description("This transition is invalid")]
        InValidTransition = 5,
        [Description("Something went wrong, please try again later!")]
        ApplicationError = 6,
    }
}
