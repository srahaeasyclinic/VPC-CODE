using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowRoleInfo
    {
        public Guid RoleAssignmetId { get; set; }
        public Guid WorkFlowStepId { get; set; }        
        public Guid RoleId { get; set; }
        public string RoleName{get;set;}
        public Guid WorkFlowId { get; set; }
        public int AssignmentOperationType { get; set; }
     
    }
}
