using System;
using System.Collections.Generic;
using VPC.Entities.WorkFlow;

namespace VPC.Entities.EntitySecurity
{ 
    public class EntitySecurityCacheInfo
    {
        public List<EntitySecurityInfo> EntitySecurity{get;set;}
        public List<WorkFlowInfo> WorkFlow{get;set;}
        public List<EntitySecurityInfo> FunctionSecurity{get;set;}
    }

}
