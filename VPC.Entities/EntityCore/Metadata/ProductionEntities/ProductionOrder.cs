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
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrder);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20044-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        [Tagable]
        public Lookup<ProductVariant> ProductVariant { get; set; }
        [Tagable]
        public Lookup<BOM> BOMId { get; set; }
        [Tagable]
        public DecimalType Quantity { get; set; }
               
        public Guid RouteId { get; set; }
        [Tagable]
        public DateTime StartDate { get; set; }
        [Tagable]
        public DateTime DueDate { get; set; }
        [Tagable]
        
        public PickList<User> Responsible { get; set; }
        [Tagable]
        public SmallText Source { get; set; }
        [Tagable]
        public Lookup<Customer> Customer { get; set; }
        [Tagable]
        public SmallText ProjectNumber { get; set; }
    }
}