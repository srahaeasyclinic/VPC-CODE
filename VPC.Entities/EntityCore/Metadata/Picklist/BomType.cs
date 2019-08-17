using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("BOM type")]
    [PluralName("BOM types")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    [Standard]
    public class BomType : SimplePicklist
    {
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("10037")]
        [ColumnName("[PickListId]")]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.BomType);

        [NonQueryable]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        public PickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        public BooleanType IsDeletetd { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        public BooleanType Flagged { get; set; }
    }
}
