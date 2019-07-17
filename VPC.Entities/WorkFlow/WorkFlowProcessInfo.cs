using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowProcessInfo
    {
        public Guid WorkFlowProcessId { get; set; }
        public Guid WorkFlowId{get;set;}
        public Guid OperationOrTransactionId { get; set; }
        public int OperationOrTransactionType { get; set; }
        public int ProcessType { get; set; }    
        public List<WorkFlowProcessTaskInfo> WorkFlowProcessTasks{get;set;}
    }
}