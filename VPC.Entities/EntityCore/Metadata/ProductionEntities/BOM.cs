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
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20022-ST01", "Standard"}
        };

         [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

         [DisplayName("Product code")]
        public Lookup<VPC.Entities.EntityCore.Metadata.Product.Entity.Product> ProductCode { get; set; }

         [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

         [DisplayName("UOM")]
        public PickList<Uom> UOM { get; set; }

         [DisplayName("Type")]
        public PickList<BomType> Type { get; set; }

         [DisplayName("Category")]
        public PickList<BomCategory> Category { get; set; }

         [DisplayName("Revision")]
        public XSmallText Revision { get; set; }

         [DisplayName("Is default")]
        public BooleanType IsDefault { get; set; }

         [DisplayName("Merge component")]
        public BooleanType MergeComponent { get; set; }

         [DisplayName("Is imported")]
        public BooleanType IsImported { get; set; }

    }
}