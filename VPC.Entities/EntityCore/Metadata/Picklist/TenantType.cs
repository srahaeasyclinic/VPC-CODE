using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
   
    [DisplayName("Tenant type")]
    [PluralName("Tenant types")]
    [FixedValue]
    public class TenantType : SimplePicklist
    {
        [NonQueryable]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [BasicColumn]
        [NonQueryable]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("10020")]
        [NonQueryable]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.TenantType);

        [NonQueryable]
        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(TenantTypeEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum TenantTypeEnum
    {
        [System.ComponentModel.Description("Production")]
        Production = 1,
        [System.ComponentModel.Description("Test")]
        Test = 2,
        [System.ComponentModel.Description("Demo")]
        Demo = 3,
        [System.ComponentModel.Description("Trial")]
        Trial = 4
    }
}
