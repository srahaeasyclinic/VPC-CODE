using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Product.Entity
{
    [TableProperties("[dbo].[Pricelist]", "[Id]")]  
    [Operation(Operations.Create, Operations.Update, Operations.Delete,Operations.UpdateStatus )]  
    [DisplayName("Pricelist")]
    [PluralName("Pricelists")] 

    public class Pricelist  : PrimaryEntity, IItem<Item>
    {
        [DefaultValue(InfoType.PriceList)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.PriceList);


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
        [BasicColumn]
        public override InternalId InternalId { get; set; }
        

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20021-ST01", "Standard"},           
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Currency]")]
        [NotNull]
        [DisplayName("Currency")]

        public SmallText Currency { get; set; }
        


       
    }
}