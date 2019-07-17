using System.Data;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Metadata.Business.Entity.Infrastructure
{
    public abstract class PicklistBase
    {
        public abstract PicklistContext PicklistContext { get;  }
        public abstract InternalId TenantId { get; set; }
        public abstract InternalId InternalId { get; set; } 
        public abstract Name Name { get; set; }



        public virtual DataTable GetValues()
        {
            throw new System.NotImplementedException();
        }
    }
}