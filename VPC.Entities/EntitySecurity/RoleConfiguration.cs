using System;
using System.Collections.Generic;

namespace VPC.Entities.EntitySecurity
{
     public abstract class RoleConfiguration
    {
        public virtual Dictionary<Guid, string> ChildEntities()
        {
            return null;
        }

        public virtual Dictionary<Guid, string> ParentEntities()
        {
            return null;
        }

        public virtual List<RoleOperations> Operations()
        {
            return null;
        }

        public virtual string Functions()
        {
            return null;
        }
    }
}
