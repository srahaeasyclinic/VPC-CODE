using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class SchedulerYearlyUnit  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.SchedulerYearlyUnit);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(SchedulerYearlyUnitType));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum SchedulerYearlyUnitType
    {
        [Description("Specific month of the year")]
        YearlyOn = 1,

        [Description("Inferred month of the year")]
        YearlyOnThe = 2
    }
}

