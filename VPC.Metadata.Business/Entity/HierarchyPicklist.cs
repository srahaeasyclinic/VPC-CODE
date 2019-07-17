using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Metadata.Business.Entity
{
    public abstract class HierarchyPicklist<T>: PicklistBase
    {
        public abstract PickList<T> ParentPicklist { get; set; }
        public abstract InternalId ParentId { get; set; }
    }
}
