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
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities {
    [TableProperties ("[dbo].[T_ProductVersionTest]", "[Id]")]
   // [Operation (Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName ("T_ProductVersionTest")]
    [PluralName ("T_ProductVersionTest")]
  //  [Import (false)]
 //   [Export (false)]
    [SupportWorkflow (true)]
    // [SendEmailTask ("Send email", TaskType.FrontTask, null)]
    // [SendSMSTask ("Send sms", TaskType.BackTask, TaskVerb.Post)]
    // [MergeTask ("Merge", TaskType.BackTask, TaskVerb.Post)]
    // [PrintTask ("Print", TaskType.FrontTask, null)]

    [VersionOf ("ProductTest")]

    public class ProductTestVersion : PrimaryEntity, IItem<Item> {
        [DefaultValue (InfoType.T_ProductVersion)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext (InfoType.T_ProductVersion);

        //
        [NonQueryable]
        [ColumnName ("[TenantId]")]
        [NotNull]
        [DisplayName ("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout ((int) LayoutType.View, (int) LayoutType.List)]
        [BasicColumn]

        [ColumnName ("[Id]")]
        [NotNull]
        [DisplayName ("Internal Id")]
        public override InternalId InternalId { get; set; }

        [DisplayName ("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { 
            {"EN30021-STV01", "Standard"}
        };

        [AccessibleLayout ((int) LayoutType.List)]
        [DisplayName ("Sub type")]
        public override XSmallText SubType { get; set; }


           [ColumnName("[VersionTxt]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Version txt")]
        public SmallText VersionTxt { get; set; }

           [ColumnName("[SellPrice]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Sell price")]
        public NumericType SellPrice { get; set; }

        [ColumnName("[CostPrice]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("Cost price")]
        public NumericType CostPrice { get; set; }

    }
}