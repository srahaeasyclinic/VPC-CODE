using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class Week  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Week);     
        public override InternalId InternalId { get; set; }
        public override Name Name { get; set; }
        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(WeekType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum WeekType
    {
        [Description("First")]
        First = 1,
        [Description("Second")]
        Second = 2,
        [Description("Third")]
        Third = 3,
        [Description("Fourth")]
        Fourth = 4
    }
}

