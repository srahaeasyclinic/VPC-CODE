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
    [TableProperties("[dbo].[ProductVariant]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product variant")]
    [PluralName("Product variants")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    public class ProductVariant : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVariant);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20034-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        [Tagable]
        public Lookup<Product> ProductCode { get; set; }
        [Tagable]
        public DecimalType SalePrice { get; set; }
        [Tagable]
        public DecimalType Cost { get; set; }
        [Tagable]
        public DecimalType Weight { get; set; }
        [Tagable]
        public PickList<Uom> WeightUOM { get; set; }
        [Tagable]
        public DecimalType Volume { get; set; }
        [Tagable]
        public PickList<Uom> VolumeUOM { get; set; }
        
        public Guid AvatarId { get; set; }
    }
}