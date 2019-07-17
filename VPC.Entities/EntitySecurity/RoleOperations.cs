using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VPC.Entities.EntitySecurity
{
     public enum RoleOperations
    {
        [AccessLevelAttribute(AccessLevelGuid.RoleReport)]
        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Access")]
        Access = 1,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Access sensitive")]
        AccessSensitive = 2,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Access confidential")]
        AccessConfidential = 3,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Create")]
        Create = 4,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Modify")]
        Modify = 5,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Delete")]
        Delete = 6,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Share")]
        Share = 7,

        [AccessLevelAttribute(AccessLevelGuid.RoleFunction)]     
        [Description("Scope")]
        Scope = 8
    }

    
}
