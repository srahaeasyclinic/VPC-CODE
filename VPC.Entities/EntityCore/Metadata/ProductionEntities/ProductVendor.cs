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
    [TableProperties("[dbo].[ProductVendor]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product vendor")]
    [PluralName("Product vendors")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    public class ProductVendor : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVendor);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20043-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        [Tagable]
        public XSmallText WorkcenterCode { get; set; }
        [Tagable]
        public XSmallText VendorCode { get; set; }
        [Tagable]
        public XSmallText VendorProductNumber { get; set; }
        [Tagable]
        public DecimalType Price { get; set; }
        [Tagable]
        public PickList<Currency> Currency { get; set; }
        [Tagable]
        public PickList<Uom> UOM { get; set; }
        [Tagable]
        public DecimalType LeadTime { get; set; }
        [Tagable]
        public DateTime ObsoleteDate { get; set; }
       
      
        public BooleanType IsDefault { get; set; }

        // Vendor/Manufacturer
        [Tagable]
        public MediumText ManufacturerNo { get; set; }

    }
}