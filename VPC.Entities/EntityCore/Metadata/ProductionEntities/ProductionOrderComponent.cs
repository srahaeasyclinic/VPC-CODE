using System;
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
    [TableProperties("[dbo].[ProductionOrderComponent]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Production order component")]
    [PluralName("Production order components")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
   
    public class ProductionOrderComponent : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderComponent);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20045-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        [Tagable]
        public Lookup<ProductionOrder> ProductionOrderNumber { get; set; }
        [Tagable]
        public PickList<Product> Product { get; set; }
       
        public Guid ParentId { get; set; }
       
        public Guid OperationId { get; set; }
        [Tagable]
        public DecimalType Quantity { get; set; }
        [Tagable]
        public PickList<Uom> UOM { get; set; }
      
        public BooleanType StopStructureDrilldown { get; set; }
        [Tagable]
        public DecimalType Cost { get; set; }
       
        public BooleanType IsSparepart { get; set; }
        [Tagable]
        public XLargeText Comment { get; set; }
        [Tagable]
        public SmallText BOMText { get; set; }
        [Tagable]
        public SmallText BOMText2 { get; set; }
        [Tagable]
        public XSmallText DrawingPosition { get; set; }
        [Tagable]
        public NumericType MaterialFactor { get; set; }
        [Tagable]
        public DecimalType Length { get; set; }
        [Tagable]
        public PickList<Uom> LengthUOM { get; set; }
        [Tagable]
        public DecimalType Width { get; set; }
        [Tagable]
        public PickList<Uom> WidthUOM { get; set; }
        [Tagable]
        public DecimalType Height { get; set; }
        [Tagable]
        public PickList<Uom> HeightUOM { get; set; }
        [Tagable]
        public DecimalType Amount { get; set; }
        
        public BooleanType TrackingRequired { get; set; }
    }
}