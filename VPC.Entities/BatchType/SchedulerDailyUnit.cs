using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class SchedulerDailyUnit  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.SchedulerDailyUnit);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(SchedulerDailyUnitType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum SchedulerDailyUnitType
    {
        [Description("Every")]
        DailyEvery = 1,

        [Description("Every weekday")]
        DailyWeekDay = 2,

        // [Description("Day")]
        // MonthlyDay = 3,

        // [Description("The")]
        // MonthlyThe = 4,

        // [Description("On")]
        // YearlyOn = 5,

        // [Description("On the")]
        // YearlyOnThe = 6
    }
}

