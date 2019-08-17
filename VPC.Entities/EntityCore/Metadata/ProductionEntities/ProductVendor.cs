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
         [DisplayName ("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVendor);

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
            {"EN20043-ST01", "Standard"}
        };

         [DisplayName ("Sub type")]
        public override XSmallText SubType { get; set; }

        [Tagable]
         [DisplayName ("Workcenter code")]
        public XSmallText WorkcenterCode { get; set; }

        [Tagable]
         [DisplayName ("Vendor code")]
        public XSmallText VendorCode { get; set; }

        [Tagable]
         [DisplayName ("Vendor product number")]
        public XSmallText VendorProductNumber { get; set; }

        [Tagable]
         [DisplayName ("Price")]
        public DecimalType Price { get; set; }

        [Tagable]
         [DisplayName ("Currency")]
        public PickList<Currency> Currency { get; set; }

        [Tagable]
         [DisplayName ("UOM")]
        public PickList<Uom> UOM { get; set; }
        
        [Tagable]
         [DisplayName ("Lead time")]
        public DecimalType LeadTime { get; set; }

        [Tagable]
         [DisplayName ("Obsolete date")]
        public DateTime ObsoleteDate { get; set; }
       
       [DisplayName ("Is default")]
        public BooleanType IsDefault { get; set; }

        // Vendor/Manufacturer
        [Tagable]
         [DisplayName ("Manufacturer no")]
        public MediumText ManufacturerNo { get; set; }

    }
}