using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("Action type")]
    [PluralName("Action types")]
    [FixedValue]
    public class ActionType : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
         [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("10018")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.ActionType);

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(ActionTypeEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum ActionTypeEnum
    {
        EntityListLayout = 1,
        EntityMetaData = 2,
        PicklistMetaData = 3,
        WorkflowDesigner = 4
    }
}