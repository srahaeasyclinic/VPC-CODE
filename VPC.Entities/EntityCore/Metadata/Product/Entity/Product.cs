using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Product.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Entity.Version;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata.Product.Entity
{
    [TableProperties("[dbo].[Product]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete,Operations.UpdateStatus )]
    [DisplayName("Product")]
    [PluralName("Products")]   
    [SupportWorkflow(true)]   
    [ProductVersionCheckoutTask("ProductVersionCheckout", TaskType.BackTask, TaskVerb.Post)]
    [VersionControl("ProductVersion")]
    public class Product : PrimaryEntity, IItem<Item>
    {
        [DefaultValue(InfoType.Product)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Product);

        //
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]

        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20021-ST01", "Standard"},           
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[ProductGroupId]")]      
        [NotNull] 
        [DisplayName("Group")]
        public PickList<ProductGroup> ProductGroup { get; set; } 

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanBeSold]")]
        [DefaultValue("0")]
        [DisplayName("Can be sold")]
        public BooleanType CanBeSold { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CanBePurchased]")]
        [DefaultValue("0")]  
        [DisplayName("Can be purchased")]
        public BooleanType CanBePurchased { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[ProductTypeId]")]    
        [NotNull]    
        [DisplayName("Product type")]
        public PickList<ProductType> ProductType { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[ProductCategoryId]")]
        [Tagable]
        [NotNull]        
        [DisplayName("Product category")]
        public PickList<ProductCategory> ProductCategory { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        // [ColumnName("[ReplacementProductId]")]
        // [DisplayName("Replacement product")]
        // public Lookup<Product> ReplacementProduct { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[LockCodeId]")]
        [Tagable]
        [NotNull]        
        [DisplayName("Lock code")]
        public PickList<ProductLockCode> LockCode { get; set; }

      
        [ColumnName("[ActiveVersionId]")]
        [DisplayName("Active version")]
     
        [ActiveVersion]
        public Lookup<ProductVersion> ActiveVersion { get; set; }


        [ColumnName("[DraftVersionId]")]
        [DisplayName("Draft version")]
        [DraftVersion]
        public Lookup<ProductVersion> DraftVersion { get; set; }


        

        


        







        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[AvatarId]")]
        // [DisplayName("Avatar")]
        // public Image Avatar { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CanHaveVariant]")]
        // [DefaultValue("0")]
        // //[NotNull]
        // [DisplayName("Can have variant")]
        // public BooleanType CanHaveVariant { get; set; }

       

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CanBeManufactured]")]
        // [DefaultValue("0")]
        // //[NotNull]
        // [DisplayName("Can be manufactured")]
        // public BooleanType CanBeManufactured { get; set; }

     

        

        


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[ProcessingTypeId]")]
        // //[NotNull]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.ProcessingType_Product)]
        // [DisplayName("Processing type")]
        // public PickList<ProcessingType> ProcessingType { get; set; }


        


       

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[UOM]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.UOM_Product)]
        // [DisplayName("UOM")]
        // public PickList<Uom> UOM { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[PurchaseUOM]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.PurchaseUOM_Product)]
        // [DisplayName("Purchase UOM")]
        // public PickList<Uom> PurchaseUOM { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[SalesUOM]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.SalesUOM_Product)]
        // [DisplayName("Sales UOM")]
        // public PickList<Uom> SalesUOM { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[DangerousGoods]")]
        // [Tagable]
        // [DisplayName("Dangerous goods")]
        // public XSmallText DangerousGoods { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[TaxCode]")]
        // //[DynamicPrefix(InfoPrefix.TAXCode_Product)]
        // [DisplayName("Tax code")]
        // public PickList<TaxCodeCategory> TaxCode { get; set; }

        

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[YieldPercentage]")]
        // [Tagable]
        // [DisplayName("Yield percentage")]
        // public DecimalType YieldPercentage { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[ManufacturingLeadTime]")]
        // [Tagable]
        // [DisplayName("Manufacturing lead time")]
        // public DecimalType ManufacturingLeadTime { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CustomerLeadTime]")]
        // [Tagable]
        // [DisplayName("Customer lead time")]
        // public DecimalType CustomerLeadTime { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[StockGroupCategoryId]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.StockGroupCategory_Product)]
        // [DisplayName("Stock group category")]
        // public PickList<StockGroupCategory> StockGroupCategory { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[DefaultLocationId]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.DefaultLocation_Product)]
        // [DisplayName("Default location")]
        // public Lookup<Location> DefaultLocation { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[StopStructureDrilldown]")]
        // [Tagable]
        // [DisplayName("Stop structure drill down")]
        // public BooleanType StopStructureDrilldown { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseMinimumStockLevel]")]
        // [Tagable]
        // [DisplayName("Use minimum stock level")]
        // public BooleanType UseMinimumStockLevel { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[MinimumStockLevel]")]
        // [Tagable]
        // [DisplayName("Minimum stock level")]
        // public DecimalType MinimumStockLevel { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[IncludeInRequirementCalculation]")]
        // [Tagable]
        // [DisplayName("Include in-requirement calculation")]
        // public BooleanType IncludeInRequirementCalculation { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[ProcurementRuleId]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.ProcurementRule_Product)]
        // [DisplayName("Procurement rule")]
        // public Lookup<ProcurementRule> ProcurementRule { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[MinimumQuantity]")]
        // [Tagable]
        // [DisplayName("Minimum quantity")]
        // public DecimalType MinimumQuantity { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[MaximumQuantity]")]
        // [Tagable]
        // [DisplayName("Maximum quantity")]
        // public DecimalType MaximumQuantity { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[MultiplicationFactor]")]
        // [Tagable]
        // [DisplayName("Multiplication factor")]
        // public NumericType MultiplicationFactor { get; set; }

        // //[ColumnName("[SerialNoCounterId]")]

        // //public Guid SerialNoCounter { get; set; }

        // [DisplayName("Use in-bond serial no")]
        // public BooleanType UseInBondSerialNo { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseOutBondSerialNo]")]
        // [DisplayName("Use out-bond serial no")]
        // public BooleanType UseOutBondSerialNo { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseInventorySerialNo]")]
        // [DisplayName("Use inventory serial no")]
        // public BooleanType UseInventorySerialNo { get; set; }

        // //[ColumnName("[BatchNoCounterId]")]

        // //public Guid BatchNoCounterId { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseInBondBatchNo]")]
        // [DisplayName("Use in-bond batch no")]
        // public BooleanType UseInBondBatchNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseOutBondBatchNo]")]
        // [DisplayName("Use out-bond batch no")]
        // public BooleanType UseOutBondBatchNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseInventoryBatchNo]")]
        // [DisplayName("Use inventory batch no")]
        // public BooleanType UseInventoryBatchNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[Weight]")]
        // [Tagable]
        // [DisplayName("Weight")]
        // public DecimalType Weight { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[WeightUOM]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.WeightUOM_Product)]
        // [DisplayName("Weight UOM")]
        // public PickList<Uom> WeightUOM { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[Volume]")]
        // [Tagable]
        // [DisplayName("Volume")]
        // public DecimalType Volume { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[VolumeUOM]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.VolumeUOM_Product)]
        // [DisplayName("Volume UOM")]
        // public PickList<Uom> VolumeUOM { get; set; }


        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[BatchSize]")]
        // [Tagable]
        // [DisplayName("Batch size")]
        // public DecimalType BatchSize { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[LotSize]")]
        // [Tagable]
        // [DisplayName("Lot size")]
        // public DecimalType LotSize { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[ManufacturerCode]")]
        // [Tagable]
        // //[DynamicPrefix(InfoPrefix.ManufacturerCode_Product)]
        // [DisplayName("Manufacturer code")]
        // public Lookup<Manufacturer> ManufacturerCode { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[ManufacturerProductNumber]")]
        // [Tagable]
        // [DisplayName("Manufacturer product number")]
        // public XSmallText ManufacturerProductNumber { get; set; }

        // //[ColumnName("[DangerousGoodsId]")]
        // //public PickList<DangerousGoods> DangerousGoods { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")] //situated in currency table...
        // [ColumnName("[CountryOfOrigin]")]
        // //[DynamicPrefix(InfoPrefix.CountryOfOrigin_Product)]
        // [DisplayName("Country of origin")]
        // public PickList<Country> CountryOfOrigin { get; set; }

        // //[ColumnName("[CustomsTariff]")]
        // //public PickList<CustomsTariff> CustomsTariff { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseInBoundSerialNo]")]
        // //[NotNull]
        // [DefaultValue("0")]
        // [DisplayName("Use in-bound serial no")]
        // public BooleanType UseInBoundSerialNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseOutBoundSerialNo]")]
        // //[NotNull]
        // [DefaultValue("0")]
        // [DisplayName("Use out-bound serial no")]
        // public BooleanType UseOutBoundSerialNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseInBoundBatchNo]")]
        // //[NotNull]
        // [DefaultValue("0")]
        // [DisplayName("Use in-bound batch no")]
        // public BooleanType UseInBoundBatchNo { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[UseOutBoundBatchNo]")]
        // //[NotNull]
        // [DefaultValue("0")]
        // [DisplayName("Use out-bound batch no")]
        // public BooleanType UseOutBoundBatchNo { get; set; }

        // //public Lookup<LockCode> LockCode{get;set;}

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CurrentWorkFlowStep]")]
        // [IsReadonly]
        // [DisplayName("Current workflow step")]
        // public PickList<ProductWorkFlow> CurrentWorkFlowStep { get; set; }


    }
}