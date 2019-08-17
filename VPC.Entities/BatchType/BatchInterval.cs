using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class BatchInterval  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.BatchInterval);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(BatchIntervalType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum BatchIntervalType
    {
        [Description("Daily")]
        Daily = 1,

        [Description("Weekly")]
        Weekly = 2,

        [Description("Monthly")]
        Monthly = 3,

        [Description("Yearly")]
        Yearly = 4
    }
}

