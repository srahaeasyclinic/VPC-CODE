using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Product.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine.Product;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Entity.Version;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Relations;

namespace VPC.Entities.EntityCore.Metadata.Product.Entity
{
    [TableProperties("[dbo].[ProductVersion]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product version")]
    [PluralName("Product versions")]   
    [SupportWorkflow(true)] 
    [VersionOf ("Product")]  
    public class ProductVersion : PrimaryEntity, IItem<Item>
    {
        [DefaultValue(InfoType.ProductVersion)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVersion);

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
            {"EN20058-ST01", "Standard"},           
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[VersionNo]")]
        [DefaultValue("1")]
        [DisplayName("Version no.")]
        [AutoIncrement(IncrementType.Version)]
        public NumericType VersionNo { get; set; }  
        //please do not change this field name  
        ///:::: infrastructure related work
        // solution :: need to add autoCalculate value based on entityId.


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]  
        [ColumnName("[DrawingRefNo]")]
        [Tagable]
        [DisplayName("Drawing ref no")]
        public XSmallText DrawingRefNo { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[RevisionNo]")]
        [Tagable]
        [DisplayName("Revision no.")]
        public XSmallText RevisionNo { get; set; }



        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[SalesPrice]")]
        // [Tagable]
        // [DisplayName("Sales price")]
        // public DecimalType SalesPrice1 { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [ColumnName("[CostPrice]")]
        // [Tagable]
        // [DisplayName("Cost price")]
        // public DecimalType CostPrice1 { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[EANCode]")]
        [Tagable]
        [DisplayName("EAN code")]
        public XSmallText EANCode { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[DangerousGoodsId]")]
        [Tagable]
        [DisplayName("Dangerous goods")]
        public PickList<ProductDangerousGoodsCode> DangerousGoods { get; set; }





        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CountryId]")]
        [Tagable]
        [DisplayName("Country of origin")]
        public PickList<Country> CountryOfOrigin { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[CustomsTariff]")]     
        [DisplayName("Customs tariff")]
        public PickList<ProductCustomTariff> CustomsTariff { get; set; } 




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SerialNoPurchase]")]
        [Tagable]
        [DisplayName("Purchase")]
        public BooleanType SerialNoPurchase { get; set; }




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SerialNoManufacture]")]
        [Tagable]
        [DisplayName("Manufacture")]
        public BooleanType SerialNoManufacture { get; set; }




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SerialNoSales]")]
        [Tagable]
        [DisplayName("Sale")]
        public BooleanType SerialNoSales { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[BatchNoPurchase]")]
        [Tagable]
        [DisplayName("Purchase")]
        public BooleanType BatchNoPurchase { get; set; }




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[BatchNoManufacture]")]
        [Tagable]
        [DisplayName("Manufacture")]
        public BooleanType BatchNoManufacture { get; set; }




        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[BatchNoSales]")]
        [Tagable]
        [DisplayName("Sale")]
        public BooleanType BatchNoSales { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[HasVariants]")]
        [Tagable]
        [DisplayName("Product has variants")]
        public BooleanType HasVariants { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[StopDrillDown]")]
        [Tagable]
        [DisplayName("Stop structure drilldown")]
        public BooleanType StopDrillDown { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CurrentWorkFlowStep]")]
        [IsReadonly]
        [DisplayName("Current workflow step")]
        public PickList<ProductVersionWorkFlow> CurrentWorkFlowStep { get; set; }


        // [ColumnName("[ProductId]")]
        // [NotNull]
        // [DisplayName("Product Id")]
        // public  InternalId ProductId { get; set; }




        [ColumnName("[ProductId]")]
        [DisplayName("Product Id")]
        [ForeignKey("[dbo].[Product]", "[Id]")]
        public Lookup<Product> ProductId { get; set; }

        

        public PriceListValue PriceListExtend { get; set; }


        public OneToMany<AttributeDetail> Attribute {get;set;}


    }
}