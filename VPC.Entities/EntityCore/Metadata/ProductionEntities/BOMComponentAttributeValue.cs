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
    [TableProperties("[dbo].[BOMComponentAttributeValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("BOM component attribute value")]
    [PluralName("BOM component attribute values")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
   
   
    public class BOMComponentAttributeValue : PrimaryEntity, IItem<Item>
    {
        [DisplayName("EntityContext")]
        public override EntityContext EntityContext => new EntityContext(InfoType.BOMComponentAttributeValue);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20025-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("BOM component attribute Id")]
        public Guid BOMComponentAttributeId { get; set; }
        //public PickList<AttributeValue> AttributeValueCode { get; set; }
    }
}