using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class Minute  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Minute);
     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
             var lists = PickListHelper.GetMinute();
            return PickListHelper.GetValues(lists);
        }
    }

}

