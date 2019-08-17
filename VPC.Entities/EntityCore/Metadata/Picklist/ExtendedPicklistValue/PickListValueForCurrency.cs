using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForCurrency]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for currencies")]
    [PluralName("Pick list value for currencies")]
    [CascadeDelete]
    public class PickListValueForCurrency : ExtendedPicklist
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
        [ColumnName("[DecimalPrecision]")] //one to one relation
        [NotNull]
        [DisplayName("Decimal precision")]
        public AnnualIncome DecimalPrecision { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[DecimalVisualization]")] //one to one relation
        [NotNull]
        [DisplayName("Decimal visualization")]
        public AnnualIncome DecimalVisualization { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[IsoCode]")] //one to one relation
        [NotNull]
        [FreeTextSearch]
        [DisplayName("ISO code")]
        public SmallText IsoCode { get; set; }

    }
}