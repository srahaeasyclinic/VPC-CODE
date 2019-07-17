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
    [TableProperties("[dbo].[ProductVariantRuleAttributeValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product variant rule attribute value")]
    [PluralName("Product variant rule attribute values")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    public class ProductVariantRuleAttributeValue : PrimaryEntity, IItem<Item>
    {
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductVariantRuleAttributeValue);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20040-ST01", "Standard"}
        };

        public override XSmallText SubType { get; set; }
       
        public Guid RuleAttributeId { get; set; }
        [Tagable]
        public Lookup<AttributeValue> AttributeValueCode { get; set; }
    }
}