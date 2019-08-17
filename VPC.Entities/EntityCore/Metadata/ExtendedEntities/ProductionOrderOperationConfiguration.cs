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
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.ProductionOrderOperation)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderOperationConfiguration);

        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [DisplayName("Operation Id")]
        public Guid OperationId { get; set; }

        [DisplayName("Cost per hour")]
        public DecimalType CostPerHour { get; set; }

        [DisplayName("Labour cost per hour")]
        public DecimalType LabourCostPerHour { get; set; }

        [DisplayName("Price per hour")]
        public DecimalType PricePerHour { get; set; }

        [DisplayName("Labour price per hour")]
        public DecimalType LabourPricePerHour { get; set; }

        [DisplayName("Auto release mode")]
        public PickList<AutoReleaseMode> AutoReleaseMode { get; set; }

        [DisplayName("Auto release next operation")]
        public BooleanType AutoReleaseNextOperation { get; set; }

        [DisplayName("Is unmanned stop")]
        public BooleanType IsUnmannedStop { get; set; }

        [DisplayName("Slack")]
        public DecimalType Slack { get; set; }

        [DisplayName("Slack UOM")]

        public PickList<Uom> SlackUOM { get; set; }

        [DisplayName("Loss time")]
        public DecimalType LossTime { get; set; }

        [DisplayName("Capital cost")]
        public DecimalType CapitalCost { get; set; }

        [DisplayName("Maintenance cost")]
        public DecimalType MaintenanceCost { get; set; }

        [DisplayName("Various cost")]
        public DecimalType VariousCost { get; set; }

        [DisplayName("Use warehouse for consumables")]
        public BooleanType UseWarehouseForConsumables { get; set; }

        [DisplayName("Consumable warehouse code")]
        public Lookup<Warehouse> ConsumableWarehouseCode { get; set; }

        [DisplayName("Use warehouse for produced")]
        public BooleanType UseWarehouseForProduced { get; set; }

        [DisplayName("Produced warehouse code")]
        public Lookup<Warehouse> ProducedWarehouseCode { get; set; }

        [DisplayName("Time calculation type")]
        public PickList<TimeCalculationType> TimeCalculationType { get; set; }

        [DisplayName("Production count")]
        public NumericType ProductionCount { get; set; }

        [DisplayName("Setup time")]
        public DecimalType SetupTime { get; set; }

        [DisplayName("Production time")]
        public DecimalType ProductionTime { get; set; }

        [DisplayName("Restructuring time")]
        public DecimalType RestructuringTime { get; set; }

        [DisplayName("Programming time")]
        public DecimalType ProgrammingTime { get; set; }

        [DisplayName("Operator share setup")]
        public DecimalType OperatorShareSetup { get; set; }

        [DisplayName("Operator share production")]
        public DecimalType OperatorShareProduction { get; set; }

        [DisplayName("Operator share restructuring")]
        public DecimalType OperatorShareRestructuring { get; set; }

        [DisplayName("Operator share programming")]
        public DecimalType OperatorShareProgramming { get; set; }

        [DisplayName("Use operation offset")]
        public BooleanType UseOperationOffset { get; set; }

        [DisplayName("Offset time")]
        public DecimalType OffsetTime { get; set; }

        [DisplayName("Offset unit")]
        public DecimalType OffsetUnit { get; set; }

        [DisplayName("Register operator time")]
        public BooleanType RegisterOperatorTime { get; set; }

        [DisplayName("Register machine time")]
        public BooleanType RegisterMachineTime { get; set; }

        [DisplayName("Show planned operator time")]
        public BooleanType ShowPlannedOperatorTime { get; set; }

        [DisplayName("Show planned machine time")]
        public BooleanType ShowPlannedMachineTime { get; set; }

        [DisplayName("Register consumed")]
        public BooleanType RegisterConsumed { get; set; }

        [DisplayName("Register produced")]
        public BooleanType RegisterProduced { get; set; }









    }
}
