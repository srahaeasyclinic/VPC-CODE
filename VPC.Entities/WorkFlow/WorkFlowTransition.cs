using System;
using VPC.Entities.Common;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowTransition
    {
        public Guid TransitionHistoryId { get; set; }
        public Guid RefId { get; set; }
        public string EntityId { get; set; }
        public Guid WorkFlowStepId { get; set; }        
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid AssignedUserId { get; set; }
        public AuditDetail AuditInfo { get; set; }
        public dynamic Data { get; set; }
        //public bool IsActive{get;set;}
        public ItemName TransitionType { get; set; }

       // public Guid CurrentStepId{get;set;}

    }
}
