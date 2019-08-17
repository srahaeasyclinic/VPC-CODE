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
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderComponent);

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
            {"EN20045-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [Tagable]
        [DisplayName("Production order number")]
        public Lookup<ProductionOrder> ProductionOrderNumber { get; set; }

        [Tagable]
        [DisplayName("Product")]
        public PickList<VPC.Entities.EntityCore.Metadata.Product.Entity.Product> Product { get; set; }
       
       [DisplayName("Parent Id")]
        public Guid ParentId { get; set; }

       [DisplayName("Operation Id")]
        public Guid OperationId { get; set; }

        [Tagable]
        [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

        [Tagable]
        [DisplayName("UOM")]
        public PickList<Uom> UOM { get; set; }
      
      [DisplayName("Stop structure drilldown")]
        public BooleanType StopStructureDrilldown { get; set; }

        [Tagable]
        [DisplayName("Cost")]
        public DecimalType Cost { get; set; }
       
       [DisplayName("Is sparepart")]
        public BooleanType IsSparepart { get; set; }

        [Tagable]
        [DisplayName("Comment")]
        public XLargeText Comment { get; set; }

        [Tagable]
        [DisplayName("BOM text")]
        public SmallText BOMText { get; set; }

        [Tagable]
        [DisplayName("BOM text 2")]
        public SmallText BOMText2 { get; set; }

        [Tagable]
        [DisplayName("Drawing position")]
        public XSmallText DrawingPosition { get; set; }

        [Tagable]
        [DisplayName("Material factor")]
        public NumericType MaterialFactor { get; set; }

        [Tagable]
        [DisplayName("Length")]
        public DecimalType Length { get; set; }

        [Tagable]
        [DisplayName("Length UOM")]
        public PickList<Uom> LengthUOM { get; set; }

        [Tagable]
        [DisplayName("Width")]
        public DecimalType Width { get; set; }

        [Tagable]
        [DisplayName("Width UOM")]
        public PickList<Uom> WidthUOM { get; set; }

        [Tagable]
        [DisplayName("Height")]
        public DecimalType Height { get; set; }

        [Tagable]
        [DisplayName("Height UOM")]
        public PickList<Uom> HeightUOM { get; set; }

        [Tagable]
        [DisplayName("Amount")]
        public DecimalType Amount { get; set; }

        [DisplayName("Tracking required")]
        public BooleanType TrackingRequired { get; set; }
    }
}