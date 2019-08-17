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
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.CollectionTaskDetail);

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
            {"EN20054-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("Task Id")]
        public Guid TaskId { get; set; }

        [DisplayName("Product variant code")]
        public XSmallText ProductVariantCode { get; set; }

        [DisplayName("Planned quantity")]
        public DecimalType PlannedQuantity { get; set; }

        [DisplayName("Produced type")]
        public PickList<ProducedType> ProducedType { get; set; }

        [DisplayName("Location code")]
        public Lookup<Location> LocationCode { get; set; }

        // Status
        [DisplayName("Quantity")]
        public DecimalType Quantity { get; set; }

        [DisplayName("Batch no")]
        public SmallText BatchNo { get; set; }

        [DisplayName("Serial no")]
        public SmallText SerialNo { get; set; }

        [DisplayName("Parent Id")]
        public Guid ParentId { get; set; }

        [DisplayName("Set no")]
        public NumericType SetNo { get; set; }
    }
}