using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("Menu type")]
    [PluralName("Menu types")]
    [SupportWorkflow(false)]
    [FixedValue]
    [Standard]
    public class MenuType : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.MenuType);

        [AccessibleLayout(1, 3)]
        [NonQueryable]
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(MenuTypeEnum));
            return PickListHelper.GetValues(lists);

        } 
    }

    public enum MenuTypeEnum
    {
        Entity = 1,
        PickList = 2,
        Context = 3,
        WellKnown = 4
    }
}