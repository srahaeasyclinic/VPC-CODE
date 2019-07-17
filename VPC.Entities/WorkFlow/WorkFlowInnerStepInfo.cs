using System;
using System.Collections.Generic;
using VPC.Entities.Common;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowInnerStepInfo
    {
        public Guid InnerStepId { get; set; }
        public Guid WorkFlowStepId { get; set; }
        public Guid WorkFlowId { get; set; }
        public ItemName TransitionType { get; set; }
        public int SequenceNumber { get; set; }
        public List<WorkFlowProcessInfo> WorkFlowProcess{get;set;}
    }
}
