using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Role
{
    public class RoleInfo
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } 
        public RoleTypeEnum RoleType{get;set;}
        public string RoleTypeName{get;set;}

        public AuditDetail AuditDetail{get;set;}
        public RoleMapperEntityInfo Entity{get;set;}
    }
}
