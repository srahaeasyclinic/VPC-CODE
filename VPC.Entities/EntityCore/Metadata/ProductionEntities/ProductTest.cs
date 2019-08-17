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
using VPC.Metadata.Business.Entity.Version;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Relations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
{
    [TableProperties("[dbo].[T_ProductTest]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.Checkout, Operations.CheckIn, Operations.Cancel)]
    [DisplayName("Product test")]
    [PluralName("Products test")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    [SendEmailTask("Send email", TaskType.FrontTask, null)]
    [SendSMSTask("Send sms", TaskType.BackTask, TaskVerb.Post)]
    [MergeTask("Merge", TaskType.BackTask, TaskVerb.Post)]
    [PrintTask("Print", TaskType.FrontTask, null)]

    [VersionControl("ProductTestVersion")]
    public class ProductTest : PrimaryEntity, IItem<Item>
    {
        [DefaultValue(InfoType.T_Product)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.T_Product);

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

        [ColumnName("[Name]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN30021-ST01", "Standard"}
        };


        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }


        public PickList<ProductWorkFlow> CurrentWorkFlowStep { get; set; }


         [ColumnName("[TestValue1]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Test value 1")]
        public SmallText TestValue1 { get; set; }

        [ColumnName("[ActiveVersion]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Active version")]
        public SmallText ActiveVersion { get; set; }

    }
}