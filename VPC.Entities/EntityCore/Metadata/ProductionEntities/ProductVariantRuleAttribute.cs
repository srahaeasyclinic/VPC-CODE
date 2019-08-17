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
    [TableProperties("[dbo].[ProductVariantRuleAttribute]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product variant rule attribute")]
    [PluralName("Product variant rule attributes")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
 
    public class ProductVariantRuleAttribute : PrimaryEntity, IItem<Item>
    {
        [DisplayName ("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVariantRuleAttribute);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName ("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName ("Name")]
        public override Name Name { get; set; }

        [DisplayName ("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20039-ST01", "Standard"}
        };

        [DisplayName ("Sub type")]
        public override XSmallText SubType { get; set; }
       
       [DisplayName ("Product variant rule Id")]
        public Guid ProductVariantRuleId { get; set; }
        
        [Tagable]
        [DisplayName ("Attribute code")]
        public PickList<Attribute> AttributeCode { get; set; }
    }
}