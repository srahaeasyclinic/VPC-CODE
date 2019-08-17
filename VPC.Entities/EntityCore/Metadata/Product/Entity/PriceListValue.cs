using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Entities.EntityCore.Metadata.Product.Entity {

    [TableProperties("[dbo].[PricelistValue]", "[Id]")]    
    [DisplayName("Pricelist")]
    [PluralName("Pricelists")] 
    public class PriceListValue : ExtendedEntity {
    
        [NonQueryable]
        [ColumnName ("[TenantId]")]
        [NotNull]
        [DisplayName ("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List)]
        [NonQueryable]
        [ColumnName ("[Id]")]
        [NotNull]
        [DisplayName ("Internal Id")]
        public override InternalId InternalId { get; set; }


        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List, (int) LayoutType.Form)]
        [NonQueryable]
        
        [NotNull]
        [DisplayName ("Pricelist Id")]
        [NonQueryable]
        [ColumnName ("[PricelistId]")]
  
        public Lookup<Pricelist> PricelistId { get; set; }


        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List)]
        [NonQueryable]
        [ColumnName ("[ProductId]")]
        [NotNull]
        [DisplayName ("ProductId Id")]
        [ForeignKey("[dbo].[Product]", "[Id]")]
        public PickList<Product> ProductId { get; set; } 
        // public InternalId ProductId { get; set; }


        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List)]
        [NonQueryable]
        [ColumnName ("[VersionId]")]
        [NotNull]
        [DisplayName ("VersionId Id")]
        [ForeignKey("[dbo].[ProductVersion]", "[Id]")]
        public InternalId VersionId { get; set; }
        //public PickList<ProductVersion> VersionId { get; set; } 



        [DefaultValue (InfoType.PriceListValue)]
        [DisplayName ("Entity context")]
        public override EntityContext EntityContext => new EntityContext (InfoType.PriceListValue);

        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List, (int) LayoutType.Form)]
        [ColumnName ("[SalesPrice]")]
        [Tagable]
        [DisplayName ("Sales price")]
        public SmallText SalesPrice1 { get; set; }

        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List, (int) LayoutType.Form)]
        [ColumnName ("[CostPrice]")]
        [Tagable]
        [DisplayName ("Cost price")]
        public SmallText CostPrice1 { get; set; }

    }
}