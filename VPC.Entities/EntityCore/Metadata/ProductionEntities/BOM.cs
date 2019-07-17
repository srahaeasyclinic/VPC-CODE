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
    [TableProperties("[dbo].[BOM]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Bill of material")]
    [PluralName("Bill of materials")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    
    public class BOM : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.BOM);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20022-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        public Lookup<Product> ProductCode { get; set; }
        public DecimalType Quantity { get; set; }
        public PickList<Uom> UOM { get; set; }
        public PickList<BomType> Type { get; set; }
        public PickList<BomCategory> Category { get; set; }
        public XSmallText Revision { get; set; }
        public BooleanType IsDefault { get; set; }
        public BooleanType MergeComponent { get; set; }
        public BooleanType IsImported { get; set; }

    }
}