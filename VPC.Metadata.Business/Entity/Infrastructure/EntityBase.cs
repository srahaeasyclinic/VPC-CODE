using System;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Metadata.Business.Entity.Infrastructure
{
    public abstract class EntityBase
    {      
        public abstract InternalId InternalId { get; set; }
        public abstract EntityContext EntityContext { get; }
    }
}
