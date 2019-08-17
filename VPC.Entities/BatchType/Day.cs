using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class Day  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Day);     
        public override InternalId InternalId { get; set; }
        public override Name Name { get; set; }
        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(DayType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum DayType
    {
        [Description("Monday")]
        Monday = 1,
        [Description("Tuesday")]
        Tuesday = 2,
        [Description("Wednesday")]
        Wednesday = 3,
        [Description("Thrusday")]
        Thrusday = 4,
        [Description("Friday")]
        Friday = 5,
        [Description("Saturday")]
        Saturday = 6,
        [Description("Sunday")]
        Sunday = 7
    }
}

