using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
// using VPC.Entities.EntityCore.Metadata.ProductionEntities;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
{
    [TableProperties("[dbo].[ProductionOrderOperationConfiguration]", "[Id]")]
    [DisplayName("Production order operation configuration")]
    [PluralName("Production order operation configurations")]
    public class ProductionOrderOperationConfiguration : ProductionOrderOperation
    {
        public override Name Name { get; set; }

        [DefaultValue(InfoType.ProductionOrderOperation)]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderOperationConfiguration);

        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        public Guid OperationId { get; set; }
        public DecimalType CostPerHour { get; set; }
        public DecimalType LabourCostPerHour { get; set; }
        public DecimalType PricePerHour { get; set; }
        public DecimalType LabourPricePerHour { get; set; }
        public PickList<AutoReleaseMode> AutoReleaseMode { get; set; }
        public BooleanType AutoReleaseNextOperation { get; set; }
        public BooleanType IsUnmannedStop { get; set; }
        public DecimalType Slack { get; set; }
        public PickList<Uom> SlackUOM { get; set; }
        public DecimalType LossTime { get; set; }
        public DecimalType CapitalCost { get; set; }
        public DecimalType MaintenanceCost { get; set; }
        public DecimalType VariousCost { get; set; }
        public BooleanType UseWarehouseForConsumables { get; set; }
        public Lookup<Warehouse> ConsumableWarehouseCode { get; set; }
        public BooleanType UseWarehouseForProduced { get; set; }
        public Lookup<Warehouse> ProducedWarehouseCode { get; set; }
        public PickList<TimeCalculationType> TimeCalculationType { get; set; }
        public NumericType ProductionCount { get; set; }
        public DecimalType SetupTime { get; set; }
        public DecimalType ProductionTime { get; set; }
        public DecimalType RestructuringTime { get; set; }
        public DecimalType ProgrammingTime { get; set; }
        public DecimalType OperatorShareSetup { get; set; }
        public DecimalType OperatorShareProduction { get; set; }
        public DecimalType OperatorShareRestructuring { get; set; }
        public DecimalType OperatorShareProgramming { get; set; }
        public BooleanType UseOperationOffset { get; set; }
        public DecimalType OffsetTime { get; set; }
        public DecimalType OffsetUnit { get; set; }
        public BooleanType RegisterOperatorTime { get; set; }
        public BooleanType RegisterMachineTime { get; set; }
        public BooleanType ShowPlannedOperatorTime { get; set; }
        public BooleanType ShowPlannedMachineTime { get; set; }
        public BooleanType RegisterConsumed { get; set; }
        public BooleanType RegisterProduced { get; set; }









    }
}
