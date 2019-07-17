using System;
using System.Collections.Generic;
using VPC.Entities.Common;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowStepInfo
    {
        public Guid WorkFlowStepId { get; set; }
        public Guid WorkFlowId { get; set; }
        public ItemName TransitionType { get; set; }
        public int SequenceNumber { get; set; }     
        public bool IsAssigmentMandatory{get;set;}
        public int? AllotedTime { get; set; }
        public int? CriticalTime { get; set; }
        public List<WorkFlowInnerStepInfo> InnerSteps { get; set; }     
        public WorkFlowPerformanceCheckInfo PerformanceCheck { get; set; }
        public List<WorkFlowRoleInfo> Roles{get;set;}        
    }
}
