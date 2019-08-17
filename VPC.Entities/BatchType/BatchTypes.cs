using System.ComponentModel;
using System.Data;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public class BatchTypes  : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.BatchType);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(BatchTypeEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum BatchTypeEnum
    {
        [Description("On demand")]
        OnDemand = 1,

        [Description("Indefinite")]
        Indefinite = 2,

        [Description("Scheduled")]
        Scheduled = 3
    }
}
