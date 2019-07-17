using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class StepOperators
    {
        public Guid OperatorType { get; set; }
        public List<Guid> StepActivatedRoles { get; set; }
        public List<Guid> StepAccessedRoles { get; set; }
        public List<Guid> StepAssignmentRoles { get; set; }
        public bool IsAccessRoleMandatory { get; set; }
        public bool IsActivateRoleMandatory { get; set; }
        public bool IsAssignmentMandatory { get; set; }
        public bool IsMaintainQueue { get; set; }
        public Guid? QueueId { get; set; }
        public Guid WorkFlowStepId { get; set; }
        public Guid WorkFlowId { get; set; }
        public Guid StepOperatorId { get; set; }
        public bool IsSync { get; set; }
    }
}