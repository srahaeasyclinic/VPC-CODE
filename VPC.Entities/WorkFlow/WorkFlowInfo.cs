using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowInfo
    {
        public Guid WorkFlowId { get; set; }
        public string EntityId { get; set; }        
        public bool Status { get; set; }       
        public string SubTypeCode { get; set; }
        public List<WorkFlowStepInfo> Steps { get; set; }
        public List<WorkFlowOperationInfo> Operations { get; set; }
    }
}
