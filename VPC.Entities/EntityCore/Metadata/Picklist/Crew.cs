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
    [DisplayName("Crew")]
    [PluralName("Crews")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    public class Crew : SimplePicklist
    {
        [DefaultValue("10048")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        [NotNull]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Crew);

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        [ColumnName("[Key]")]
        [BasicColumn]
        [FreeTextSearch]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        public SmallText Key { get; set; }

        [ColumnName("[Text]")]
        [BasicColumn]
        [FreeTextSearch]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [InverseProperty("[Id]")]
        [ColumnName("[UpdatedBy]")]
        [NotNull]

        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        public DateTime UpdatedDate { get; set; }

        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        public PickList<Active> Active { get; set; }

        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        public BooleanType IsDeletetd { get; set; }

        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        public BooleanType Flagged { get; set; }
    }
}
