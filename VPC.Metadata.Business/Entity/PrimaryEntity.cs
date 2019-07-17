using System;
using System.Collections.Generic;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Metadata.Business.Entity
{
    public abstract class PrimaryEntity : EntityBase
    {
        public abstract Name Name { get; set; }

        public abstract Dictionary<string, string> SubTypes { get; }
        public abstract XSmallText SubType { get; set; }
    }
}
