using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;


namespace VPC.Entities.EntityCore.Metadata.Product.Entity
{
    [TableProperties("[dbo].[AttributeValue]", "[Id]")]
    [Operation(Operations.Delete)]
    [DisplayName("Attribute value")]
    [PluralName("Attribute values")]
    [CascadeDelete]    
    
    public class AttributeValue : DetailEntity, IItem<Item>
    {    

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("TenantId")]
        public override InternalId InternalId { get; set; }

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.AttributeValue);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20002-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Description]")]
        [Tagable]
        [DisplayName("Description")]
        public XLargeText Description { get; set; }

      

        [ColumnName("[AttributeId]")]
        [DisplayName("Attribute Id")]
        [ForeignKey("[dbo].[Attribute]", "[Id]")]
        public Lookup<Attribute> AttributeId { get; set; }

    }
}