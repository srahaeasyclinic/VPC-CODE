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
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderProduced);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20051-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        [Tagable]
        public Lookup<ProductionOrder> ProductionOrderNumber { get; set; }
        [Tagable]
        public PickList<Product> Productcode	 { get; set; }
        [Tagable]
        public DecimalType Quantity { get; set; }
        [Tagable]
        public PickList<Uom> UOM { get; set; }
        [Tagable]
        public DecimalType Cost { get; set; }
        [Tagable]
        public PickList<ProducedType> Type { get; set; }
    }
}