using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class SchedulerMonthlyUnit  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.SchedulerMonthlyUnit);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(SchedulerMonthlyUnitType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum SchedulerMonthlyUnitType
    {
       

        [Description("Specific day of the month")]
        MonthlyDay = 1,

        [Description("Inferred day of the month")]
        MonthlyThe = 2,

        // [Description("On")]
        // YearlyOn = 5,

        // [Description("On the")]
        // YearlyOnThe = 6
    }
}

