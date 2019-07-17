using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VPC.Entities.EntitySecurity
{
    public enum RoleAccessLevel
    {
        [AccessLevelAttribute(AccessLevelGuid.RoleFunction)]
        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [AccessLevelAttribute(AccessLevelGuid.RoleReport)]        
        [Description("No access")]
        NoAccess = 1,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        //[AccessLevelAttrib(AccessLevelGuid.Dashlet)]
        [Description("Me and my team scope")]
        MeAndMyTeamScope = 2,

        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]     
        [Description("Location scope")]
        LocationScope = 3,

        [AccessLevelAttribute(AccessLevelGuid.RoleFunction)]
        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]        
        [Description("Company scope")]
        CompanyScope = 4,

        [AccessLevelAttribute(AccessLevelGuid.RoleFunction)]
        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]   
        [Description("Organization scope")]
        OrganisationScope = 5,

        [AccessLevelAttribute(AccessLevelGuid.RoleReport)]
        [AccessLevelAttribute(AccessLevelGuid.RoleEntity)]
        [Description("Access")]
        Access = 6

    }
}
