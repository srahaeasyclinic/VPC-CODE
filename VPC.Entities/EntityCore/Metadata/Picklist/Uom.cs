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
    [DisplayName("UOM")]
    [PluralName("UOM")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    [Standard]
    public class Uom : ComplexPicklist
    {
        [DefaultValue("10017")]
        [ColumnName("[PickListId]")]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Uom);

        
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        [BasicColumn]
        [DisplayName("Key")]
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        [BasicColumn]
        [DisplayName("Text")]
        public MediumText Text { get; set; }

        [DefaultValue()]
        [AccessibleLayout((int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        [DisplayName("Updated by")]
        public Lookup<User> UpdatedBy { get; set; }

        [DefaultValue()]
        [AccessibleLayout((int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        [DisplayName("Updated date")]

        public DateTime UpdatedDate { get; set; }

        [DefaultValue("1")]
        [AccessibleLayout((int)LayoutType.List)]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [DisplayName("Active")]

        public LocalPickList<Active> Active { get; set; }

        [DefaultValue("0")]
        [AccessibleLayout((int)LayoutType.List)]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [DisplayName("Is deleted")]

        public BooleanType IsDeletetd { get; set; }

        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [AccessibleLayout((int)LayoutType.List)]
        [NotNull]
        [DisplayName("Flagged")]

        public BooleanType Flagged { get; set; }
    }
}