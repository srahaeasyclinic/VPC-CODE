using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
{
    [TableProperties("[dbo].[ProductionOrderProduced]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Production order produced")]
    [PluralName("Production order produced")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
 
    public class ProductionOrderProduced : PrimaryEntity, IItem<Item>
    {
         [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderProduced);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
         [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
         [DisplayName("Name")]
        public override Name Name { get; set; }

         [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20051-ST01", "Standard"}
        };

         [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [Tagable]
         [DisplayName("Production order number")]
        public Lookup<ProductionOrder> ProductionOrderNumber { get; set; }

        [Tagable]
         [DisplayName("Product code")]
        public PickList<VPC.Entities.EntityCore.Metadata.Product.Entity.Product> Productcode	 { get; set; }

        [Tagable]
         [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

        [Tagable]
         [DisplayName("UOM")]
        public PickList<Uom> UOM { get; set; }

        [Tagable]
         [DisplayName("Cost")]
        public DecimalType Cost { get; set; }

        [Tagable]
         [DisplayName("Type")]
        public PickList<ProducedType> Type { get; set; }
    }
}