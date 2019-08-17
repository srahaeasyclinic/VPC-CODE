using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Rules;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForLanguage]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for language")]
    [PluralName("Pick list value for languages")]
    [CascadeDelete]
    public class PickListValueForLanguage : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }
        public override PicklistContext PicklistContext => new PicklistContext(0);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ForeignKey("[dbo].[PickListValue]", "[Id]")]
        [ColumnName("[PickListValueId]")]
        [NotNull]
        [DisplayName("Picklist value")]
        public InternalId PicklistValueId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[DateFormat]")]
        [NotNull]
        [DisplayName("Date format")]
        public SmallText DateFormat { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[IsoCode]")]
        [FreeTextSearch]
        [DisplayName("ISO code")]
        public SmallText IsoCode { get; set; }
    }
}