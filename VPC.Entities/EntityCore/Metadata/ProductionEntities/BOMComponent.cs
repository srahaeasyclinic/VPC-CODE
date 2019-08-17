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
    [TableProperties("[dbo].[BOMComponent]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("BOM component")]
    [PluralName("BOM components")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    
    public class BOMComponent : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.BOMComponent);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20023-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("BOM Id")] 
        public Lookup<BOM> BOMId { get; set; }

        [DisplayName("Child BOM Id")]
        public Lookup<BOM> ChildBOMId { get; set; }

        [DisplayName("Position")]
        public NumericType Position { get; set; }

        [DisplayName("Operation no")]
        public XSmallText OperationNo { get; set; }

        [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

        [DisplayName("UOM")]
        public PickList<Uom> UOM { get; set; }

        [DisplayName("Step")]
        public NumericType Step { get; set; }

        [DisplayName("Is fixed step")]
        public BooleanType IsFixedStep { get; set; }

        [DisplayName("Factor")]
        public DecimalType Factor { get; set; }

        [DisplayName("Wastage percentage")]
        public DecimalType WastagePercentage { get; set; }

        [DisplayName("Stop structure drilldown")]
        public BooleanType StopStructureDrilldown { get; set; }

        [DisplayName("Cost")]
        public DecimalType Cost { get; set; }

        [DisplayName("Is sparepart")]
        public BooleanType IsSparepart { get; set; }

        [DisplayName("Comment")]
        public XLargeText Comment { get; set; }

        [DisplayName("BOM text")]
        public SmallText BOMText { get; set; }

        [DisplayName("BOM text 2")]
        public SmallText BOMText2 { get; set; }

        [DisplayName("Drawing position")]
        public XSmallText DrawingPosition { get; set; }

        [DisplayName("Material factor")]
        public NumericType MaterialFactor { get; set; }

        [DisplayName("Length")]
        public DecimalType Length { get; set; }

        [DisplayName("Length UOM")]
        public PickList<Uom> LengthUOM { get; set; }

        [DisplayName("Width")]
        public DecimalType Width { get; set; }

        [DisplayName("Width UOM")]
        public PickList<Uom> WidthUOM { get; set; }

        [DisplayName("Height")]
        public DecimalType Height { get; set; }

        [DisplayName("Height UOM")]
        public PickList<Uom> HeightUOM { get; set; }

        [DisplayName("Amount")]
        public DecimalType Amount { get; set; }

        [DisplayName("Phantom")]
        public BooleanType Phantom { get; set; }
    }
}