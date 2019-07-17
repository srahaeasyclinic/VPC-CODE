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
    [TableProperties("[dbo].[CollectionTaskDetail]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Collection task detail")]
    [PluralName("Collection task details")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    
    public class CollectionTaskDetail : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.CollectionTaskDetail);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20054-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
        public Guid TaskId { get; set; }
        public XSmallText ProductVariantCode { get; set; }
        public DecimalType PlannedQuantity { get; set; }
        public PickList<ProducedType> ProducedType { get; set; }
        public Lookup<Location> LocationCode { get; set; }

        // Status
        public DecimalType Quantity { get; set; }
        public SmallText BatchNo { get; set; }
        public SmallText SerialNo { get; set; }
        public Guid ParentId { get; set; }
        public NumericType SetNo { get; set; }
    }
}