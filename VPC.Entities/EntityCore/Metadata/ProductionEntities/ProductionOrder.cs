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
using DateTime = System.DateTime;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
{
    [TableProperties("[dbo].[ProductionOrder]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Production order")]
    [PluralName("Production orders")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
   
    public class ProductionOrder : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrder);

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
            {"EN20044-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [Tagable]
        [DisplayName("Product variant")]
        public Lookup<ProductVariant> ProductVariant { get; set; }

        [Tagable]
        [DisplayName("BOM Id")]
        public Lookup<BOM> BOMId { get; set; }

        [Tagable]
        [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }
               
        [DisplayName("Route Id")]       
        public Guid RouteId { get; set; }

        [Tagable]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Tagable]
        [DisplayName("Due date")]
        public DateTime DueDate { get; set; }

        [Tagable]
        [DisplayName("Responsible")]
        public PickList<User> Responsible { get; set; }

        [Tagable]
        [DisplayName("Source")]
        public SmallText Source { get; set; }

        [Tagable]
        [DisplayName("Customer")]
        public Lookup<Customer> Customer { get; set; }

        [Tagable]
        [DisplayName("Project number")]
        public SmallText ProjectNumber { get; set; }
    }
}