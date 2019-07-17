using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
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
    [TableProperties("[dbo].[ProcurementRule]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Procurement rule")]
    [PluralName("Procurement rules")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    public class ProcurementRule : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.ProcurementRule);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        
        [ColumnName("[TenantId]")]
        [NotNull]
        public  InternalId TenantId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20057-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[WarehouseCode]")]
        [InverseProperty("[Id]")]
        public Lookup<Warehouse> WarehouseCode { get; set; }

        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[LocationCode]")]
        [InverseProperty("[Id]")]
        public Lookup<Location> LocationCode { get; set; }

        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[ProcurementGroup]")]
        [InverseProperty("[Id]")]
        public PickList<ProcurementGroup> ProcurementGroup { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MinimumQuantity]")]
        public NumericType MinimumQuantity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MaximumQuantity]")]
        public NumericType MaximumQuantity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MultiplicationFactor]")]
        public NumericType MultiplicationFactor { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[DaysToPurchase]")]
        public NumericType DaysToPurchase { get; set; }
    }
}