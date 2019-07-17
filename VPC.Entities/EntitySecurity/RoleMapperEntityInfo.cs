using System;
using System.Collections.Generic;

namespace VPC.Entities.EntitySecurity
{
    public class RoleMapperEntityInfo
    {       
        public EntitySecurityInfo Data{get;set;}
        public List<AccessLevel> AccessLevel{get;set;}
        public List<AccessLevel> OperationLevel{get;set;}
        public List<EntitySecurityInfo> Functions{get;set;}

    }

}
