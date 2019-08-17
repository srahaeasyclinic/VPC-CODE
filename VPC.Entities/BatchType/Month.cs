using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class Month  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Month);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(MonthType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum MonthType
    {
        [Description("January")]
        January = 1,
        [Description("February")]
        February = 2,
        [Description("March")]
        March = 3,
        [Description("April")]
        April = 4,
        [Description("May")]
        May = 5,
        [Description("June")]
        June = 6,
        [Description("July")]
        July = 7,
        [Description("August")]
        August = 8,
        [Description("September")]
        September = 9,
        [Description("October")]
        October = 10,
        [Description("November")]
        November = 11,
        [Description("December")]
        December = 12,
    }
}

