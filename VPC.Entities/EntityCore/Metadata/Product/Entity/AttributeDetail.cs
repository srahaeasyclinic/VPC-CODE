
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
    [TableProperties("[dbo].[Attribute]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Attribute")]
    [PluralName("Attributes")] 
   
    public class AttributeDetail : DetailEntity, IItem<Item>
    {

        [DefaultValue(InfoType.Attribute)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Attribute);

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

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20001-ST01", "Standard"},           
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }



        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Description]")]
        [Tagable]
        [DisplayName("Description")]
        public XLargeText Description { get; set; }

      //  public OneToMany<AttributeValue> AttributeValue {get;set;}

        

        // public override EntityContext EntityContext => new EntityContext(InfoType.Attribute);

        // [NonQueryable]
        // [ColumnName("[Id]")]
        // [NotNull]
        // public override InternalId InternalId { get; set; }

        // [NonQueryable]
        // public override Name Name { get; set; }

        // public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        // {
        //     {"EN20001-ST01", "Standard"}
        // };

        // public override XSmallText SubType { get; set; }
    }


}