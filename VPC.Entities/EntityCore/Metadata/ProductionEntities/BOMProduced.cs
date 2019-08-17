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
    [TableProperties("[dbo].[BOMProduced]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("BOM produced")]
    [PluralName("BOM produced")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
  
  
    public class BOMProduced : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.BOMProduced);

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
            {"EN20027-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("BOM Id")]
        public Lookup<BOM> BOMId { get; set; }

        [DisplayName("Product code")]
        public Lookup<VPC.Entities.EntityCore.Metadata.Product.Entity.Product> ProductCode { get; set; }

        [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

        [DisplayName("UOM")]
        public PickList<Uom> UOM { get; set; }

        [DisplayName("Produced type")]
        public PickList<ProducedType> ProducedType { get; set; }

        [DisplayName("Share percentage")]
        public DecimalType SharePercentage { get; set; }

        [DisplayName("Yield percentage")]
        public DecimalType YieldPercentage { get; set; }
    }
}