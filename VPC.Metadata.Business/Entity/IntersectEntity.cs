using System;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Metadata.Business.Entity
{
    public abstract class IntersectEntity : EntityBase
    {
        public abstract Name Name { get; set; }
    }
}
