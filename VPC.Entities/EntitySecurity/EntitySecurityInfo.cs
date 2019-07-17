using System;
using System.Collections.Generic;

namespace VPC.Entities.EntitySecurity
{ 
    public class EntitySecurityInfo
    {
        public Guid EntitySecurityId { get; set; }
        public string EntityId { get; set; } 
        public Guid RoleId{get;set;}
        public int SecurityCode{get;set;}
        public Guid FunctionContext{get;set;}
        public string Name{get;set;}
    }

}
