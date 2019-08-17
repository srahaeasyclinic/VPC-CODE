using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using ComponentModel = System.ComponentModel;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    // [TableProperties("[dbo].[PickListValue]", "[Id]")]
    // [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
     [DisplayName("Active")]
     [PluralName("Actives")]
    // //[SupportWorkflow(false)]
     [FixedValue]
     [Standard]
    public class Active : SimplePicklist
    {
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Active);

        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [DisplayName("Name")]
        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(ActiveEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum ActiveEnum
    {
        [ComponentModel.Description("Disable")]
        Disable = 0,

        [ComponentModel.Description("Enable")]
        Enable = 1,

        [ComponentModel.Description("All")]
        All = 2
    }
}