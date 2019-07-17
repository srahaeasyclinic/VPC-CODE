using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine.Product;
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
    [TableProperties("[dbo].[Product]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product")]
    [PluralName("Products")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    [SendEmailTask("Send email", TaskType.FrontTask, null)]
    [SendSMSTask("Send sms", TaskType.BackTask, TaskVerb.Post)]
    [MergeTask("Merge", TaskType.BackTask, TaskVerb.Post)]
    [PrintTask("Print", TaskType.FrontTask, null)]

    public class Product : PrimaryEntity, IItem<Item>
    {
        [DefaultValue(InfoType.Product)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Product);

        //
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]

        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }


        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20021-ST01", "Purchased"},
            {"EN20021-ST02", "Manufactured"}
        };

        [AccessibleLayout((int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[AvatarId]")]
        public Image Avatar { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanHaveVariant]")]
        [DefaultValue("0")]
        //[NotNull]
        public BooleanType CanHaveVariant { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanBeSold]")]
        [DefaultValue("0")]
        //[NotNull]
        public BooleanType CanBeSold { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanBePurchased]")]
        [DefaultValue("0")]
        //[NotNull]
        public BooleanType CanBePurchased { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanBeManufactured]")]
        [DefaultValue("0")]
        //[NotNull]
        public BooleanType CanBeManufactured { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ProductCategoryId]")]
        [Tagable]
        //[NotNull]
        //[DynamicPrefix(InfoPrefix.ProductCategory_Product)]
        public PickList<ProductCategory> ProductCategory { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ProductGroupId]")]
        [HierarchyDisplay("ParentsGroup")]
        //[NotNull]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.ProductGroup_Product)]
        public PickList<ProductGroup> ProductGroup { get; set; }   ///need to check.!--.!--.

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ProductTypeId]")]
        [HierarchyDisplay("ParentsGroup")]
        //[NotNull]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.ProductType_Product)]
        public PickList<ProductType> ProductType { get; set; } ///need to check.!--.!--.


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ProcessingTypeId]")]
        //[NotNull]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.ProcessingType_Product)]
        public PickList<ProcessingType> ProcessingType { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        //Lockcode
        [ColumnName("[DrawingRefNo]")]
        [Tagable]
        public XSmallText DrawingRefNo { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Revision]")]
        [Tagable]
        public XSmallText Revision { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SalesPrice]")]
        [Tagable]
        public DecimalType SalesPrice { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Cost]")]
        [Tagable]
        public DecimalType Cost { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[UOM]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.UOM_Product)]
        public PickList<Uom> UOM { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[PurchaseUOM]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.PurchaseUOM_Product)]
        public PickList<Uom> PurchaseUOM { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[SalesUOM]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.SalesUOM_Product)]
        public PickList<Uom> SalesUOM { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[DangerousGoods]")]
        [Tagable]
        public XSmallText DangerousGoods { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[TaxCode]")]
        //[DynamicPrefix(InfoPrefix.TAXCode_Product)]
        public PickList<TaxCodeCategory> TaxCode { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[EANCode]")]
        [Tagable]
        public XSmallText EANCode { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[YieldPercentage]")]
        [Tagable]
        public DecimalType YieldPercentage { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[ManufacturingLeadTime]")]
        [Tagable]
        public DecimalType ManufacturingLeadTime { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CustomerLeadTime]")]
        [Tagable]
        public DecimalType CustomerLeadTime { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[StockGroupCategoryId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.StockGroupCategory_Product)]
        public PickList<StockGroupCategory> StockGroupCategory { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[DefaultLocationId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.DefaultLocation_Product)]
        public Lookup<Location> DefaultLocation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[StopStructureDrilldown]")]
        [Tagable]
        public BooleanType StopStructureDrilldown { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseMinimumStockLevel]")]
        [Tagable]
        public BooleanType UseMinimumStockLevel { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MinimumStockLevel]")]
        [Tagable]
        public DecimalType MinimumStockLevel { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[IncludeInRequirementCalculation]")]
        [Tagable]
        public BooleanType IncludeInRequirementCalculation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ProcurementRuleId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.ProcurementRule_Product)]
        public Lookup<ProcurementRule> ProcurementRule { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MinimumQuantity]")]
        [Tagable]
        public DecimalType MinimumQuantity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MaximumQuantity]")]
        [Tagable]
        public DecimalType MaximumQuantity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MultiplicationFactor]")]
        [Tagable]
        public NumericType MultiplicationFactor { get; set; }

        //[ColumnName("[SerialNoCounterId]")]

        //public Guid SerialNoCounter { get; set; }


        public BooleanType UseInBondSerialNo { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseOutBondSerialNo]")]
        public BooleanType UseOutBondSerialNo { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseInventorySerialNo]")]
        public BooleanType UseInventorySerialNo { get; set; }

        //[ColumnName("[BatchNoCounterId]")]

        //public Guid BatchNoCounterId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseInBondBatchNo]")]
        public BooleanType UseInBondBatchNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseOutBondBatchNo]")]
        public BooleanType UseOutBondBatchNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseInventoryBatchNo]")]
        public BooleanType UseInventoryBatchNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Weight]")]
        [Tagable]
        public DecimalType Weight { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[WeightUOM]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.WeightUOM_Product)]
        public PickList<Uom> WeightUOM { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Volume]")]
        [Tagable]
        public DecimalType Volume { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[VolumeUOM]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.VolumeUOM_Product)]
        public PickList<Uom> VolumeUOM { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[BatchSize]")]
        [Tagable]
        public DecimalType BatchSize { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[LotSize]")]
        [Tagable]
        public DecimalType LotSize { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[ManufacturerCode]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.ManufacturerCode_Product)]
        public Lookup<Manufacturer> ManufacturerCode { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[ManufacturerProductNumber]")]
        [Tagable]
        public XSmallText ManufacturerProductNumber { get; set; }

        //[ColumnName("[DangerousGoodsId]")]
        //public PickList<DangerousGoods> DangerousGoods { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[CountryOfOrigin]")]
        //[DynamicPrefix(InfoPrefix.CountryOfOrigin_Product)]
        public PickList<Country> CountryOfOrigin { get; set; }

        //[ColumnName("[CustomsTariff]")]
        //public PickList<CustomsTariff> CustomsTariff { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseInBoundSerialNo]")]
        //[NotNull]
        [DefaultValue("0")]
        public BooleanType UseInBoundSerialNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseOutBoundSerialNo]")]
        //[NotNull]
        [DefaultValue("0")]
        public BooleanType UseOutBoundSerialNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseInBoundBatchNo]")]
        //[NotNull]
        [DefaultValue("0")]
        public BooleanType UseInBoundBatchNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[UseOutBoundBatchNo]")]
        //[NotNull]
        [DefaultValue("0")]
        public BooleanType UseOutBoundBatchNo { get; set; }

        //public Lookup<LockCode> LockCode{get;set;}

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CurrentWorkFlowStep]")]
        [IsReadonly]
        public PickList<ProductWorkFlow> CurrentWorkFlowStep { get; set; }


    }
}