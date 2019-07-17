using System;

namespace VPC.Entities.WorkFlow
{
  public class WorkFlowPerformanceCheckInfo
    {
        public Guid PerformanceCheckId { get; set; }
         public Guid WorkFlowId { get; set; }
         public Guid WorkFlowStepId { get; set; }
        public Guid TransitionType { get; set; } 
        public int? AllotedTime { get; set; }
        public int? CriticalTime { get; set; }
    }
}
