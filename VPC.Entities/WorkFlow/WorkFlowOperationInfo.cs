using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowOperationInfo
    {
        public Guid WorkFlowOperationId { get; set; }
        public WorkFlowOperationType OperationType { get; set; }
        public Guid WorkFlowId { get; set; }      
        public bool IsSync { get; set; }
        public List<WorkFlowProcessInfo> WorkFlowProcess{get;set;}
    }
}
