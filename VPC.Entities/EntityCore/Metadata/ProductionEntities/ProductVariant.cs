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
        [DisplayName ("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVariant);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName ("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName ("Name")]
        public override Name Name { get; set; }

        [DisplayName ("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20034-ST01", "Standard"}
        };

        [DisplayName ("Sub type")]
        public override XSmallText SubType { get; set; }

        [Tagable]
        [DisplayName ("Product code")]
        public Lookup<VPC.Entities.EntityCore.Metadata.Product.Entity.Product> ProductCode { get; set; }

        [Tagable]
        [DisplayName ("Sale price")]
        public DecimalType SalePrice { get; set; }

        [Tagable]
        [DisplayName ("Cost")]
        public DecimalType Cost { get; set; }

        [Tagable]
        [DisplayName ("Weight")]
        public DecimalType Weight { get; set; }

        [Tagable]
        [DisplayName ("Weight UOM")]
        public PickList<Uom> WeightUOM { get; set; }

        [Tagable]
        [DisplayName ("Volume")]
        public DecimalType Volume { get; set; }

        [Tagable]
        [DisplayName ("Volume UOM")]
        public PickList<Uom> VolumeUOM { get; set; }

        [DisplayName ("Avatar Id")]
        public Guid AvatarId { get; set; }
    }
}