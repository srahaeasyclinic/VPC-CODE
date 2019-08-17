using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForCountry]", "[Id]")]
    [DisplayName("Pick list value for country")]
    [PluralName("Pick list value for countries")]
    [CascadeDelete]
    public class PickListValueForCountry : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(0);

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
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[Currency]")] //one to one relation
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Currency_Country)]
        [DisplayName("Currency")]
        public PickList<Currency> Currency { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[Language]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Language_Country)]
        [DisplayName("Language")]
        public PickList<Language> Language { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[Timezone]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Timezone_Country)]
        [DisplayName("Timezone")]
        public PickList<Timezone> Timezone { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [FreeTextSearch]
        [ColumnName("[IsoCode]")]
        [DisplayName("ISO code")]
        public SmallText IsoCode { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [FreeTextSearch]
        [ColumnName("[Nationality]")]
        [NotNull]
        [DisplayName("Nationality")]
        public SmallText Nationality { get; set; }

    }
}