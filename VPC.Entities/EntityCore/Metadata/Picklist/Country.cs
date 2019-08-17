using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Entities.EntityCore.Model.Storage;
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
    [DisplayName("Country")]
    [PluralName("Countries")]
    [CascadeDelete]
    [FixedValue]
    [CustomizeValue]
    [SupportWorkflow(false)]

    public class Country : ComplexPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("20002")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        [NotNull]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Country);

        [NonQueryable]
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        [DisplayName("Key")]
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [DefaultOrder]
        [BasicColumn]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        [DisplayName("Text")]
        public SmallText Text { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue()] //TODO::Remove this attribute
        [NonQueryable]
        [InverseProperty("[Id]")]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.UpdatedBy_Country)]
        [DisplayName("Updated by")]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue()] //TODO::Remove this attribute
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        [DisplayName("Updated date")]
        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        //[DynamicPrefix(InfoPrefix.Active_Country)]
        [DisplayName("Active")] 
        public PickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [DisplayName("Is deleted")]
        public BooleanType IsDeletetd { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        [DisplayName("Flagged")]
        public BooleanType Flagged { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        public PickListValueForCountry PickListValueForCountry { get; set; }
    }
}