using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Entities.EntityCore.Metadata.Product.Entity
{
    [TableProperties("[dbo].[Pricelist]", "[Id]")]    
    [DisplayName("Pricelist")]
    [PluralName("Pricelists")] 
    [CascadeDelete]
    public class PriceListExtend : ExtendedEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

       

        [ColumnName("[ProductId]")]
        [DisplayName("Product Id")]
        // [ForeignKey("[dbo].[Product]", "[Id]")]
        public InternalId ProductId { get; set; }




        [ColumnName("[VersionId]")]
        [DisplayName("Product Version Id")]
        // [ForeignKey("[dbo].[ProductVersion]", "[Id]")]
        public InternalId VersionId { get; set; }
        // public Lookup<ProductVersion> VersionId { get; set; }


       
        // [ColumnName("[VariantId]")]
        // [DisplayName("Variant")]
        // [ForeignKey("[dbo].[ProductVariant]", "[Id]")]
        // public PickList<ProductVersion> Variant { get; set; }




        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[SalesPrice]")]
        // [Tagable]
        // [DisplayName("Sales price")]
        // public DecimalType SalesPrice { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CostPrice]")]
        // [Tagable]
        // [DisplayName("Cost price")]
        // public DecimalType CostPrice { get; set; }
      
    }
}