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
        public override EntityContext EntityContext => new EntityContext(InfoType.BOMComponent);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20023-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        public Lookup<BOM> BOMId { get; set; }
        public Lookup<BOM> ChildBOMId { get; set; }
        public NumericType Position { get; set; }
        public XSmallText OperationNo { get; set; }
        public DecimalType Quantity { get; set; }
        public PickList<Uom> UOM { get; set; }
        public NumericType Step { get; set; }
        public BooleanType IsFixedStep { get; set; }
        public DecimalType Factor { get; set; }
        public DecimalType WastagePercentage { get; set; }
        public BooleanType StopStructureDrilldown { get; set; }
        public DecimalType Cost { get; set; }
        public BooleanType IsSparepart { get; set; }
        public XLargeText Comment { get; set; }
        public SmallText BOMText { get; set; }
        public SmallText BOMText2 { get; set; }
        public XSmallText DrawingPosition { get; set; }
        public NumericType MaterialFactor { get; set; }
        public DecimalType Length { get; set; }
        public PickList<Uom> LengthUOM { get; set; }
        public DecimalType Width { get; set; }
        public PickList<Uom> WidthUOM { get; set; }
        public DecimalType Height { get; set; }
        public PickList<Uom> HeightUOM { get; set; }
        public DecimalType Amount { get; set; }
        public BooleanType Phantom { get; set; }
    }
}