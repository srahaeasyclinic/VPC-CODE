using System;
using System.ComponentModel;
namespace VPC.Entities.WorkFlow
{
    public enum WorkFlowRoleType
    {
        [Description("ActivatedRoles")]
        ActivatedRoles = 1,

        [Description("AccessedRoles")]
         AccessedRoles = 2,
         
        [Description("AssignedRoles")]
         AssignedRoles = 3

    }

}
